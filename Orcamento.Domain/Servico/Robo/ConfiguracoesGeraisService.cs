using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DB;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;

namespace Orcamento.Domain.Servico.Robo
{
    public class ConfiguracoesGeraisService
    {
        public List<Processo> processos;
        public Processos repositorioProcessos = new Processos();

        public void DeletarEstruturaOrcamentaria(TipoProcessoEnum processo)
        {
            switch (processo)
            {
                case TipoProcessoEnum.DeletarEstruturaCompleta:
                    DeletarEstruturaOrcamentaria();
                    break;
                case TipoProcessoEnum.DeletarAcordosdeconvencao:
                    DeletarAcordosDeConvencao();
                    break;
                case TipoProcessoEnum.DeletarCentrosDeCusto:
                    DeletarCentrosDeCusto();
                    break;
                case TipoProcessoEnum.DeletarConta:
                    DeletarContas();
                    break;
                case TipoProcessoEnum.DeletarDepartamentos:
                    DeletarDepartamentos();
                    break;
                case TipoProcessoEnum.DeletarFuncionarios:
                    DeletarFuncionarios();
                    break;
                case TipoProcessoEnum.DeletarGruposDeContas:
                    DeletarGrupoDeContas();
                    break;
                case TipoProcessoEnum.DeletarOrcamentosPessoais:
                    DeletarOrcamentoDePessoal();
                    break;
                case TipoProcessoEnum.DeletarOrcammentosDeProdução:
                    DeletarOrcamentosDeProducao();
                    break;
                case TipoProcessoEnum.DeletarOrcammentosDeViagem:
                    DeletarOrcamentosDeViagem();
                    break;
                case TipoProcessoEnum.DeletarOrcammentosOperacionais:
                    DeletarOrcamentosDeDespesasOperacionais();
                    break;
                case TipoProcessoEnum.DeletarTicketsDeInsumos:
                    DeletarInsumos();
                    break;
                case TipoProcessoEnum.DeletarTicketsDeReceita:
                    DeletarTicketsDeReceita();
                    break;
                case TipoProcessoEnum.DeletarTicketsDeUnitários:
                    DeletarTicketsDeUnitarios();
                    break;
                case TipoProcessoEnum.DeletarTotaisDaDRE:
                    DeletarDRETotal();
                    break;
                case TipoProcessoEnum.DeletarUsuarios:
                    DeletarUsuarios();
                    break;
                default:
                    DeletarEstruturaOrcamentaria();
                    break;
            }
        }

        public void DeletarEstruturaOrcamentaria()
        {
            processos = new List<Processo>();
            repositorioProcessos = new Processos();
            try
            {
                ReiniciarProcessos();
                AdicionarProcesso(TipoProcessoEnum.DeletarEstruturaCompleta);


                IniciarProcessos();

                FinalizarProcesso(TipoProcessoEnum.DeletarEstruturaCompleta);
            }
            catch (Exception)
            {

                ReportarErro(TipoProcessoEnum.DeletarEstruturaCompleta, "Erro não previsto ao deletar estrutura completa, contacte o suporte da ferramenta.");
            }
        }

        private void IniciarProcessos()
        {
            try
            {
                DeletarDRETotal();

                DeletarFuncionarios();

                DeletarOrcamentoDePessoal();

                DeletarTicketsDeReceita();

                DeletarTicketsDeUnitarios();

                DeletarInsumos();

                DeletarOrcamentosDeProducao();

                DeletarOrcamentosDeViagem();

                DeletarOrcamentosDeDespesasOperacionais();

                DeletarAcordosDeConvencao();

                DeletarUsuarios();

                DeletarDepartamentos();

                DeletarCentrosDeCusto();

                DeletarGrupoDeContas();

                DeletarContas();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void ReiniciarProcessos()
        {
            var processos = repositorioProcessos.Todos<Processo>();
            foreach (var processo in processos)
            {
                processo.Status = "Não Iniciado.";
                processo.Iniciado = null;
                processo.Finalizado = null;
                repositorioProcessos.Salvar(processo);
            }


        }

        public void AdicionarProcesso(TipoProcessoEnum tipo)
        {
            var processo = repositorioProcessos.ObterProcesso(tipo);
            processo.Status = "Iniciado.";
            processo.Iniciado = DateTime.Now;
            repositorioProcessos.Salvar(processo);

        }

        public void FinalizarProcesso(TipoProcessoEnum tipo)
        {
            var processo = repositorioProcessos.ObterProcesso(tipo);
            processo.Status = "Finalizado.";
            processo.Finalizado = DateTime.Now;
            repositorioProcessos.Salvar(processo);
        }

        public void ReportarErro(TipoProcessoEnum tipo, string msg)
        {
            var processo = repositorioProcessos.ObterProcesso(tipo);
            processo.Status = "Erro.";
            processo.MsgDeErro = msg;
            processo.Finalizado = DateTime.Now;
            repositorioProcessos.Salvar(processo);
        }


        public void DeletarDRETotal()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTotaisDaDRE);
                DRES dres = new DRES();
                var todos = dres.Todos();

                dres.Deletar(todos);
                FinalizarProcesso(TipoProcessoEnum.DeletarTotaisDaDRE);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTotaisDaDRE, ex.Message);
            }
        }

        public void DeletarFuncionarios()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarFuncionarios);
                Funcionarios funcionarios = new Funcionarios();
                var todos = funcionarios.Todos<Funcionario>();

                funcionarios.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarFuncionarios);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarFuncionarios, ex.Message);
            }


        }

        public void DeletarOrcamentoDePessoal()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcamentosPessoais);
                NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();
                var todos = orcamentos.Todos();

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcamentosPessoais);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcamentosPessoais, ex.Message);
            }


        }


        public void DeletarTicketsDeReceita()
        {

            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDeReceita);
                var receitas = new TicketsDeReceita();
                var todos = receitas.Todos<TicketDeReceita>();
                receitas.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDeReceita);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDeReceita, ex.Message);
            }

        }


        public void DeletarAcordosDeConvencao()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarAcordosdeconvencao);
                AcordosDeConvencao acordos = new AcordosDeConvencao();
                var todos = acordos.Todos<AcordoDeConvencao>();

                acordos.Deletar(todos);
                FinalizarProcesso(TipoProcessoEnum.DeletarAcordosdeconvencao);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarAcordosdeconvencao, ex.Message);
            }


        }

        public void DeletarTicketsDeUnitarios()
        {

            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDeUnitários);
                TicketsDeProducao tickets = new TicketsDeProducao();
                var todos = tickets.Todos();

                tickets.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDeUnitários);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDeUnitários, ex.Message);
            }


        }

        public void DeletarInsumos()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDeInsumos);
                Insumos insumos = new Insumos();
                var todos = insumos.Todos<Insumo>();

                insumos.Deletar(todos);

                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDeInsumos);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDeInsumos, ex.Message);
            }


        }

        public void DeletarOrcamentosDeViagem()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcammentosDeViagem);
                Orcamentos orcamentos = new Orcamentos();
                var todos = orcamentos.Todos<OrcamentoDeViagem>();

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcammentosDeViagem);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcammentosDeViagem, ex.Message);
            }


        }

        public void DeletarOrcamentosDeProducao()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcammentosDeProdução);
                Orcamentos orcamentos = new Orcamentos();
                var todos = orcamentos.Todos<OrcamentoHospitalar>();

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcammentosDeProdução);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcammentosDeProdução, ex.Message);
            }


        }
        public void DeletarOrcamentosDeDespesasOperacionais()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcammentosOperacionais);
                Orcamentos orcamentos = new Orcamentos();
                var todos = orcamentos.Todos<OrcamentoOperacionalVersao>();

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcammentosOperacionais);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcammentosOperacionais, ex.Message);
            }



        }


        public void DeletarUsuarios()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarUsuarios);
                Usuarios usuarios = new Usuarios();
                TipoUsuarios tipos = new TipoUsuarios();

                var hospitalares = usuarios.TodosPor(tipos.Obter<TipoUsuario>((int)TipoUsuarioEnum.Hospital));
                var corporativos = usuarios.TodosPor(tipos.Obter<TipoUsuario>((int)TipoUsuarioEnum.Corporativo));
                var adms = usuarios.TodosPor(tipos.Obter<TipoUsuario>((int)TipoUsuarioEnum.Administrador));

                foreach (var usuario in adms)
                {
                    usuario.Departamentos = null;
                }
                usuarios.Salvar(adms);
                usuarios.Deletar(hospitalares);
                usuarios.Deletar(corporativos);

                FinalizarProcesso(TipoProcessoEnum.DeletarUsuarios);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarUsuarios, ex.Message);
            }


        }

        public void DeletarGrupoDeContas()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarGruposDeContas);
                GruposDeConta gruposDeConta = new GruposDeConta();
                var todos = gruposDeConta.Todos<GrupoDeConta>();

                gruposDeConta.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarGruposDeContas);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarGruposDeContas, ex.Message);
            }


        }

        public void DeletarContas()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarConta);
                Contas contas = new Contas();
                var todas = contas.Todos();

                contas.Deletar(todas);

                FinalizarProcesso(TipoProcessoEnum.DeletarConta);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarConta, ex.Message);
            }


        }

        public void DeletarCentrosDeCusto()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarCentrosDeCusto);
                CentrosDeCusto centros = new CentrosDeCusto();
                var todos = centros.Todos();

                centros.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarCentrosDeCusto);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarCentrosDeCusto, ex.Message);
            }



        }

        public void DeletarDepartamentos()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarDepartamentos);
                Departamentos departamentos = new Departamentos();
                var todos = departamentos.Todos();

                departamentos.Deletar(todos);

                FinalizarProcesso(TipoProcessoEnum.DeletarDepartamentos);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarDepartamentos, ex.Message);
            }


        }
    }
}
