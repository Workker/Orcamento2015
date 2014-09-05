using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria;
using Orcamento.Domain.Servico.Hospitalar;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas
{
    public class EstruturaOrcamentaria : ProcessaCarga
    {
        private MotorDeValidacaoDeEstruturaOrcamentaria motor;
        private List<EstruturaOrcamentariaExcel> estruturaOrcamentariaExcel;

        private Contas contasRepositorio;
        public virtual Contas ContasRepositorio
        {
            get
            {
                return contasRepositorio ?? (contasRepositorio = new Contas());
            }
        }

        private GruposDeConta gruposDeContaRepositorio;
        public GruposDeConta GruposDeContaRepositorio { get { return gruposDeContaRepositorio ?? (gruposDeContaRepositorio = new GruposDeConta()); } }

        private List<GrupoDeConta> gruposDeConta;
        public virtual List<GrupoDeConta> GruposDeConta
        {
            get
            {
                return gruposDeConta ?? (gruposDeConta = new List<GrupoDeConta>());
            }
        }

        private IList<Conta> contas;
        public virtual IList<Conta> Contas
        {
            get
            {
                return contas ?? (contas = new List<Conta>());
            }
        }

        private IList<Departamento> departamentos;
        public virtual IList<Departamento> Departamentos
        {
            get { return departamentos; }
            set { departamentos = value; }
        }

        private IList<CentroDeCusto> centrosDeCustos;
        public virtual IList<CentroDeCusto> CentrosDeCustos { get { return centrosDeCustos ?? (centrosDeCustos = new List<CentroDeCusto>()); } }
        private CentrosDeCusto centrosDeCustoRepositorio;
        public virtual CentrosDeCusto CentrosDeCustoRepositorio { get { return centrosDeCustoRepositorio ?? (centrosDeCustoRepositorio = new CentrosDeCusto()); } }

        private TiposConta tiposContaRepositorio;
        public virtual TiposConta TiposContaRepositorio
        {
            get
            {
                return tiposContaRepositorio ?? (tiposContaRepositorio = new TiposConta());
            }
        }

        private Departamentos departamentosRepositorio;
        public virtual Departamentos DepartamentosRepositorio
        {
            get
            {
                return departamentosRepositorio ?? (departamentosRepositorio = new Departamentos());
            }
        }

        public override void Processar(Carga carga, bool salvar = false)
        {
            try
            {
                this.carga = carga;
                estruturaOrcamentariaExcel = new List<EstruturaOrcamentariaExcel>();

                LerExcel();

                if (NenhumRegistroEncontrado(carga))
                    return;

                ValidarCarga();

                if (!CargaContemErros())
                    SalvarAlteracoes(salvar);

            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar Estrutura Orçamentária", "Ocorreu um erro ao tentar processar a Estrutura Orçamentária.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ValidarCarga()
        {
            motor = new MotorDeValidacaoDeEstruturaOrcamentaria(carga, estruturaOrcamentariaExcel);
            motor.Validar();
        }

        private bool NenhumRegistroEncontrado(Carga carga)
        {
            if (estruturaOrcamentariaExcel.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }

            return false;
        }

        private void ProcessarEstruturaOrcamentaria()
        {
            Departamentos = DepartamentosRepositorio.Todos();

            Contract.Requires(Departamentos != null, "Departamentos não encontrados");

            ProcessarContas();
            ProcessarGruposDeConta();
            ProcessarCentrosDeCusto();
            ProcessarDepartamentos();
        }

        internal override void SalvarDados()
        {
            ProcessarEstruturaOrcamentaria();

            ContasRepositorio.SalvarLista(Contas);
            GruposDeContaRepositorio.SalvarLista(GruposDeConta);
            CentrosDeCustoRepositorio.SalvarLista(CentrosDeCustos);
            DepartamentosRepositorio.SalvarLista(Departamentos);

            carga.AdicionarDetalhe("Carga realizada com sucesso.",
                                     "Carga de Estrutura Orçamentária nome: " + carga.NomeArquivo + " .", 0, TipoDetalheEnum.sucesso);
        }

        private void ProcessarContas()
        {
            foreach (var estruturaOrcamentaria in estruturaOrcamentariaExcel)
            {
                TipoConta tipoContaOutras = TiposContaRepositorio.ObterPor((int)TipoContaEnum.Outros);
                Conta conta = null;

                if (estruturaOrcamentaria.TipoAlteracaoConta == TipoAlteracao.Inclusao)
                {
                    conta = new Conta(estruturaOrcamentaria.NomeDaConta, tipoContaOutras,
                                          estruturaOrcamentaria.CodigoDaConta);
                    Contas.Add(conta);

                    ContasRepositorio.Salvar(conta);
                }
                if (estruturaOrcamentaria.TipoAlteracaoConta == TipoAlteracao.Alteracao)
                {
                    conta = ContasRepositorio.ObterContaPor(estruturaOrcamentaria.CodigoDaConta);

                    if (conta == null)
                        conta = Contas.FirstOrDefault(p => p.CodigoDaConta == estruturaOrcamentaria.CodigoDaConta);

                    conta.Nome = estruturaOrcamentaria.NomeDaConta;
                }

                if (!Contas.Any(p => p.CodigoDaConta == estruturaOrcamentaria.CodigoDaConta))
                    Contas.Add(conta);
            }
        }

        private void ProcessarGruposDeConta()
        {
            foreach (var estruturaOrcamentaria in estruturaOrcamentariaExcel)
            {
                GrupoDeConta grupoDeConta = null;

                if (estruturaOrcamentaria.TipoAlteracaoGrupoDeConta == TipoAlteracao.Inclusao)
                {
                    grupoDeConta = new GrupoDeConta(estruturaOrcamentaria.NomeDoGrupoDeConta);
                    grupoDeConta.Contas = new List<Conta>();
                    GruposDeConta.Add(grupoDeConta);
                }
                if (estruturaOrcamentaria.TipoAlteracaoGrupoDeConta == TipoAlteracao.Alteracao)
                {
                    grupoDeConta = GruposDeContaRepositorio.ObterPor(estruturaOrcamentaria.NomeDoGrupoDeConta);

                    if (grupoDeConta == null)
                        grupoDeConta = GruposDeConta.FirstOrDefault(p => p.Nome == estruturaOrcamentaria.NomeDoGrupoDeConta);

                    grupoDeConta.Nome = estruturaOrcamentaria.NomeCentroDeCusto;
                }

                var contas = estruturaOrcamentariaExcel.Where(p => p.NomeDoGrupoDeConta == grupoDeConta.Nome).Select(p => p.NomeDaConta);

                foreach (var conta in contas)
                {
                    var contaRecuperada = Contas.FirstOrDefault(p => p.Nome == conta);

                    Contract.Requires(conta != null, "Conta não existe no processamento.");

                    if (!grupoDeConta.Contas.Any(p => p.Nome == contaRecuperada.Nome))
                        grupoDeConta.Adicionar(contaRecuperada);
                }

                if (!GruposDeConta.Any(g => g.Nome == grupoDeConta.Nome))
                    gruposDeConta.Add(grupoDeConta);
            }

        }

        private void ProcessarCentrosDeCusto()
        {
            foreach (var estruturaOrcamentaria in estruturaOrcamentariaExcel)
            {
                if (estruturaOrcamentaria.TipoAlteracaoCentroDeCusto == TipoAlteracao.Inclusao)
                {
                    var centroDeCusto = new CentroDeCusto(estruturaOrcamentaria.NomeCentroDeCusto, estruturaOrcamentaria.CodigoCentroDeCusto);
                    CentrosDeCustos.Add(centroDeCusto);

                    AdicionarContas(centroDeCusto);

                    CentrosDeCustoRepositorio.Salvar(centroDeCusto);
                }
                if (estruturaOrcamentaria.TipoAlteracaoCentroDeCusto == TipoAlteracao.Alteracao)
                {
                    CentroDeCusto centroDeCusto = CentrosDeCustoRepositorio.ObterPor(estruturaOrcamentaria.CodigoCentroDeCusto);

                    if (centroDeCusto == null)
                        centroDeCusto = CentrosDeCustos.FirstOrDefault(p => p.CodigoDoCentroDeCusto == estruturaOrcamentaria.CodigoCentroDeCusto);

                    centroDeCusto.AlterarNome(estruturaOrcamentaria.NomeCentroDeCusto);

                    if (!CentrosDeCustos.Any(p => p.CodigoDoCentroDeCusto == estruturaOrcamentaria.CodigoCentroDeCusto))
                        CentrosDeCustos.Add(centroDeCusto);

                    AdicionarContas(centroDeCusto);
                }


            }
        }

        private void ProcessarDepartamentos()
        {
            ServicoSalvarDepartamento servico = new ServicoSalvarDepartamento();

            foreach (var estruturaOrcamentaria in estruturaOrcamentariaExcel)
            {
                if (estruturaOrcamentaria.TipoAlteracaoDepartamento == TipoAlteracao.Inclusao)
                {
                    var departamento = FabricaDeDepartamento.Construir(estruturaOrcamentaria.TipoDepartamento,
                                                                       estruturaOrcamentaria.Departamento);

                    AdicionarCentrosDeCusto(departamento);

                    servico.Salvar(departamento);
                    Departamentos.Add(departamento);
                }
                if (estruturaOrcamentaria.TipoAlteracaoDepartamento == TipoAlteracao.Alteracao)
                {
                    Departamento departamento = null;

                    if (Departamentos.Any(p => p.Nome == estruturaOrcamentaria.Departamento))
                        departamento = Departamentos.FirstOrDefault(p => p.Nome == estruturaOrcamentaria.Departamento);
                    else
                    {
                        departamento = DepartamentosRepositorio.ObterPor(estruturaOrcamentaria.Departamento);
                        Departamentos.Add(departamento);
                    }

                    departamento.Nome = estruturaOrcamentaria.Departamento;
                    AdicionarCentrosDeCusto(departamento);
                }


            }


        }

        private void AdicionarContas(CentroDeCusto centro)
        {
            var registros = estruturaOrcamentariaExcel.Where(d => d.CodigoCentroDeCusto == centro.CodigoDoCentroDeCusto);

            if (registros == null || registros.Count() == 0)
                return;

            foreach (var registro in registros)
            {
                var conta = Contas.FirstOrDefault(c => c.CodigoDaConta == registro.CodigoDaConta);
                if (conta == null)
                {
                    carga.AdicionarDetalhe("Conta inexistente", "Não foi possivel adicionar a conta ao centro de custo, conta:" + registro.CodigoDaConta, 0, TipoDetalheEnum.erroDeProcesso);
                    continue;
                }

                if (centro.Contas != null && centro.Contas.Count > 0 && centro.Contas.Any(c => c.CodigoDaConta == registro.CodigoDaConta))
                    continue;

                centro.AdicionarConta(conta);
            }
        }

        private void AdicionarCentrosDeCusto(Departamento departamento)
        {
            var registros = estruturaOrcamentariaExcel.Where(d => d.Departamento == departamento.Nome);

            if (registros == null || registros.Count() == 0)
                return;

            foreach (var registro in registros)
            {
                var centroDeCusto = CentrosDeCustos.FirstOrDefault(c => c.CodigoDoCentroDeCusto == registro.CodigoCentroDeCusto);
                if (centroDeCusto == null)
                {
                    carga.AdicionarDetalhe("Centro de custo inexistente", "Não foi possivel adicionar o departamento no centro de custo informado:" + registro.CodigoCentroDeCusto, 0, TipoDetalheEnum.erroDeProcesso);
                    continue;
                }

                if (departamento.CentrosDeCusto != null && departamento.CentrosDeCusto.Count > 0 && departamento.CentrosDeCusto.Any(c => c.CodigoDoCentroDeCusto == registro.CodigoCentroDeCusto))
                    continue;

                departamento.AdicionarCentroDeCusto(centroDeCusto);
            }
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
                    LerExcel(estruturaOrcamentariaExcel, carga, reader);
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

        private void LerExcel(List<EstruturaOrcamentariaExcel> estruturaOrcamentariaExcel, Carga carga,
                                     OleDbDataReader reader)
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

                        var itemEstruturaOrcamentariaExcel = new EstruturaOrcamentariaExcel();
                        itemEstruturaOrcamentariaExcel.Departamento = reader[0].ToString();
                        itemEstruturaOrcamentariaExcel.TipoDepartamento = (TipoDepartamento)int.Parse(reader[1].ToString());
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoDepartamento = (TipoAlteracao)int.Parse(reader[2].ToString());
                        itemEstruturaOrcamentariaExcel.NomeDaConta = reader[3].ToString();
                        itemEstruturaOrcamentariaExcel.CodigoDaConta = reader[4].ToString();
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoConta = (TipoAlteracao)int.Parse(reader[5].ToString());
                        itemEstruturaOrcamentariaExcel.NomeCentroDeCusto = reader[6].ToString();
                        itemEstruturaOrcamentariaExcel.CodigoCentroDeCusto = reader[7].ToString();
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoCentroDeCusto = (TipoAlteracao)int.Parse(reader[8].ToString());
                        itemEstruturaOrcamentariaExcel.NomeDoGrupoDeConta = reader[9].ToString();
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoGrupoDeConta = (TipoAlteracao)int.Parse(reader[10].ToString());
                        itemEstruturaOrcamentariaExcel.Linha = i + 1;

                        estruturaOrcamentariaExcel.Add(itemEstruturaOrcamentariaExcel);
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

    public class EstruturaOrcamentariaExcel
    {
        public string Departamento { get; set; }
        public TipoDepartamento TipoDepartamento { get; set; }
        public TipoAlteracao TipoAlteracaoDepartamento { get; set; }
        public Departamento DepartamentoEntidade { get; set; }
        public string NomeDaConta { get; set; }
        public string CodigoDaConta { get; set; }
        public Conta Conta { get; set; }
        public TipoAlteracao TipoAlteracaoConta { get; set; }
        public string NomeCentroDeCusto { get; set; }
        public string CodigoCentroDeCusto { get; set; }
        public CentroDeCusto CentroDeCusto { get; set; }
        public TipoAlteracao TipoAlteracaoCentroDeCusto { get; set; }
        public GrupoDeConta GrupoDeConta { get; set; }
        public string NomeDoGrupoDeConta { get; set; }
        public TipoAlteracao TipoAlteracaoGrupoDeConta { get; set; }
        public int Linha { get; set; }
    }

    public enum TipoAlteracao
    {
        Inclusao = 1,
        Alteracao = 2
    }
}
