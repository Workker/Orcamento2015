using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas
{
    public class UsuarioExcel
    {
        public int Linha { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }

    public class Usuarios : ProcessaCarga
    {
        private IList<UsuarioExcel> usuariosExcel { get; set; }
        private Domain.DB.Repositorio.Usuarios repositorioUsuarios;


        public override void Processar(Carga carga, bool salvar)
        {
            try
            {
                this.carga = carga;
                usuariosExcel = new List<UsuarioExcel>();

                LerExcel();

                if (NenhumRegistroEncontrado(carga))
                    return;

                ValidarCarga();

                if (salvar)
                {
                    if (!CargaContemErros())
                        ProcessarUsuarios();

                    if (!CargaContemErros())
                        SalvarAlteracoes(salvar);
                }
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar Estrutura Orçamentária", "Ocorreu um erro ao tentar processar a Estrutura Orçamentária.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ValidarCarga()
        {
            try
            {
                repositorioUsuarios = new DB.Repositorio.Usuarios();

                foreach (var usuarioExcel in usuariosExcel)
                {

                    if (string.IsNullOrEmpty(usuarioExcel.Login))
                        carga.AdicionarDetalhe("Erro ao processar usuários", "Login não informado.", 0, TipoDetalheEnum.erroDeValidacao);

                    if (string.IsNullOrEmpty(usuarioExcel.Nome))
                        carga.AdicionarDetalhe("Erro ao processar usuários", "Nome não informado.", 0, TipoDetalheEnum.erroDeValidacao);

                    if (usuarioExcel.TipoUsuario == null)
                        carga.AdicionarDetalhe("Erro ao processar usuários", "Tipo usuário não informado.", 0, TipoDetalheEnum.erroDeValidacao);


                    var usuario = repositorioUsuarios.ObterAcesso(usuarioExcel.Login);

                    if (usuario != null)
                        carga.AdicionarDetalhe("Erro ao processar usuários", "Usuário: " + usuarioExcel.Login + " já existe.", 0, TipoDetalheEnum.erroDeValidacao);


                    if (string.IsNullOrEmpty(usuarioExcel.Login))
                        carga.AdicionarDetalhe("Erro ao processar usuários", "Login não informado.", 0, TipoDetalheEnum.erroDeValidacao);

                    if (carga.Ok() && usuario != null)
                        usuarioExcel.Usuario = usuario;
                }
            }
            catch (Exception ex)
            {

                carga.AdicionarDetalhe("Erro ao validar usuários", "Ocorreu um erro ao tentar processar os usuários.", 0, TipoDetalheEnum.erroDeValidacao, ex.Message);
            }
        }

        private bool NenhumRegistroEncontrado(Carga carga)
        {
            if (usuariosExcel.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }

            return false;
        }

        private void ProcessarUsuarios()
        {
            try
            {
                TipoUsuarios tipoUsuarios = new TipoUsuarios();

                var adm = tipoUsuarios.Obter<TipoUsuario>((int)TipoUsuarioEnum.Administrador);
                var corp = tipoUsuarios.Obter<TipoUsuario>((int)TipoUsuarioEnum.Corporativo);
                var hosp = tipoUsuarios.Obter<TipoUsuario>((int)TipoUsuarioEnum.Hospital);


                foreach (var usuarioExcel in usuariosExcel)
                {
                    if (usuarioExcel.Usuario == null)
                    {
                        usuarioExcel.Usuario = new Usuario();
                        usuarioExcel.Usuario.Nome = usuarioExcel.Nome;
                        usuarioExcel.Usuario.Login = usuarioExcel.Login;

                        switch (usuarioExcel.TipoUsuario)
                        {
                            case TipoUsuarioEnum.Corporativo:
                                usuarioExcel.Usuario.TipoUsuario = corp;
                                break;
                            case TipoUsuarioEnum.Administrador:
                                usuarioExcel.Usuario.TipoUsuario = adm;
                                this.AtribuirPermissaoParaADM(usuarioExcel.Usuario);
                                break;
                            case TipoUsuarioEnum.Hospital:
                                usuarioExcel.Usuario.TipoUsuario = hosp;
                                break;
                            default:
                                throw new Exception("Tipo usuário não informado.");

                        }

                    }
                }
            }
            catch (Exception ex)
            {

                carga.AdicionarDetalhe("Erro ao processar usuários", "Ocorreu um erro ao tentar processar a carga.", 0, TipoDetalheEnum.erroDeValidacao, ex.Message);
            }
        }

        internal override void SalvarDados()
        {
            var usuarios = usuariosExcel.Select(s => s.Usuario).ToList();

            repositorioUsuarios.SalvarLista(usuarios);

            carga.AdicionarDetalhe("Carga realizada com sucesso.",
                                     "Carga de Estrutura Orçamentária nome: " + carga.NomeArquivo + " .", 0, TipoDetalheEnum.sucesso);
        }

        private void LerExcel()
        {
            try
            {
                processo = new Processo();
                var reader = processo.InicializarCarga(carga);

                if (reader == null)
                    carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
                else
                    LerExcel(reader);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel, ex.Message);
            }
            finally
            {
                processo.FinalizarCarga();
            }
        }

        private void LerExcel(OleDbDataReader reader)
        {
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    if (i > 0)
                    {
                        if (reader[0] == DBNull.Value || string.IsNullOrEmpty(reader[0].ToString()))
                            break;

                        var usuarioExcel = new UsuarioExcel();
                        usuarioExcel.Nome = reader[0].ToString();
                        usuarioExcel.Login = reader[1].ToString();
                        usuarioExcel.TipoUsuario = (TipoUsuarioEnum)int.Parse(reader[2].ToString());

                        usuarioExcel.Linha = i + 1;

                        usuariosExcel.Add(usuarioExcel);
                    }
                }
                catch (Exception ex)
                {
                    carga.AdicionarDetalhe("Erro na linha", "Ocorreu um erro ao tentar ler a linha do excel", i + 1,
                                           TipoDetalheEnum.erroLeituraExcel);
                }
                finally
                {
                    i++;
                }
            }
        }
    }
}
