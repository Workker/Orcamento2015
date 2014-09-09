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
using Workker.Framework.Domain;


namespace Orcamento.Domain.Servico.Robo
{
    public class ConfiguracoesGeraisDoDepartamentoService
    {

        public List<Processo> processos;
        public Processos repositorioProcessos = new Processos();
        private Departamento Departamento { get; set; }

        public void DeletarEstruturaOrcamentaria(TipoProcessoEnum processo, Departamento departamento)
        {
            Assertion.NotNull(departamento, "Departamento nulo.").Validate();

            this.Departamento = departamento;

            switch (processo)
            {
                case TipoProcessoEnum.DeletarEstruturaCompletaPorDepartamento:
                    DeletarEstruturaOrcamentaria();
                    break;
                case TipoProcessoEnum.DeletarAcordosdeconvencaoporDepartamento:
                    DeletarAcordosDeConvencao();
                    break;
                case TipoProcessoEnum.DeletarDepartamentosporDepartamento:
                    DeletarDepartamentos();
                    break;
                case TipoProcessoEnum.DeletarFuncionariosporDepartamento:
                    DeletarFuncionarios();
                    break;
                case TipoProcessoEnum.DeletarOrcamentosPessoaisporDepartamento:
                    DeletarOrcamentoDePessoal();
                    break;
                case TipoProcessoEnum.DeletarOrcammentosDeProduçãoporDepartamento:
                    DeletarOrcamentosDeProducao();
                    break;
                case TipoProcessoEnum.DeletarOrcammentosDeViagemporDepartamento:
                    DeletarOrcamentosDeViagem();
                    break;
                case TipoProcessoEnum.DeletarOrcammentosOperacionaisporDepartamento:
                    DeletarOrcamentosDeDespesasOperacionais();
                    break;
                case TipoProcessoEnum.DeletarTicketsDeInsumosporDepartamento:
                    DeletarInsumos();
                    break;
                case TipoProcessoEnum.DeletarTicketsDeReceitaporDepartamento:
                    DeletarTicketsDeReceita();
                    break;
                case TipoProcessoEnum.DeletarTicketsDeUnitáriosporDepartamento:
                    DeletarTicketsDeUnitarios();
                    break;
                case TipoProcessoEnum.DeletarTotaisdaDREporDepartamento:
                    DeletarDRETotal();
                    break;
                case TipoProcessoEnum.DeletarUsuariosporDepartamento:
                    DeletarUsuarios();
                    break;
                case TipoProcessoEnum.DeletarTicketsdePessoalporDepartamento:
                    DeletarTicketDePessoal();
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
                Iniciar();

                ReiniciarProcessos();

                AdicionarProcesso(TipoProcessoEnum.DeletarEstruturaCompletaPorDepartamento);

                IniciarProcessos();

                FinalizarProcesso(TipoProcessoEnum.DeletarEstruturaCompletaPorDepartamento);
            }
            catch (Exception)
            {

                ReportarErro(TipoProcessoEnum.DeletarEstruturaCompletaPorDepartamento, "Erro não previsto ao deletar estrutura completa, contacte o suporte da ferramenta.");
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

                DeletarTicketDePessoal();

                DeletarInsumos();

                DeletarOrcamentosDeProducao();

                DeletarOrcamentosDeViagem();

                DeletarOrcamentosDeDespesasOperacionais();

                DeletarAcordosDeConvencao();

                DeletarUsuarios();

                DeletarDepartamentos();

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Iniciar()
        {
            processos = CriarProcessos();
        }

        public void ReiniciarProcessos()
        {

            foreach (var processo in processos)
            {
                processo.Status = "Não Iniciado.";
                processo.Iniciado = null;
                processo.Finalizado = null;
                repositorioProcessos.Salvar(processo);
            }
        }

        private List<Processo> CriarProcessos()
        {
            GerenciarProcessosPorDepartamentoService gerenciador = new GerenciarProcessosPorDepartamentoService();
            return gerenciador.ObterProcessos(Departamento.Id);
        }

        public void AdicionarProcesso(TipoProcessoEnum tipo)
        {
            var processo = repositorioProcessos.ObterProcesso(tipo, Departamento);
            processo.Status = "Iniciado.";
            processo.Iniciado = DateTime.Now;
            repositorioProcessos.Salvar(processo);
        }

        public void FinalizarProcesso(TipoProcessoEnum tipo)
        {
            var processo = repositorioProcessos.ObterProcesso(tipo, Departamento);
            processo.Status = "Finalizado.";
            processo.Finalizado = DateTime.Now;
            repositorioProcessos.Salvar(processo);
        }

        public void ReportarErro(TipoProcessoEnum tipo, string msg)
        {
            var processo = repositorioProcessos.ObterProcesso(tipo, Departamento);
            processo.Status = "Erro.";
            processo.MsgDeErro = msg;
            processo.Finalizado = DateTime.Now;
            repositorioProcessos.Salvar(processo);
        }


        public void DeletarDRETotal()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTotaisdaDREporDepartamento);
                DRES dres = new DRES();
                var dre = dres.Obter(Departamento);

                dres.Deletar(dre);
                FinalizarProcesso(TipoProcessoEnum.DeletarTotaisdaDREporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTotaisdaDREporDepartamento, ex.Message);
            }
        }

        public void DeletarTicketDePessoal()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsdePessoalporDepartamento);

                TicketsDeOrcamentoPessoal tickets = new TicketsDeOrcamentoPessoal();
                var todos = tickets.Todos(Departamento);

                tickets.Deletar(todos);
                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsdePessoalporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsdePessoalporDepartamento, ex.Message);
            }
        }

        public void DeletarFuncionarios()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarFuncionariosporDepartamento);
                Funcionarios funcionarios = new Funcionarios();
                var todos = funcionarios.ObterPor(Departamento);

                funcionarios.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarFuncionariosporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarFuncionariosporDepartamento, ex.Message);
            }


        }

        public void DeletarOrcamentoDePessoal()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcamentosPessoaisporDepartamento);
                NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();
                var todos = orcamentos.Todos(Departamento.Id);

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcamentosPessoaisporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcamentosPessoaisporDepartamento, ex.Message);
            }


        }


        public void DeletarTicketsDeReceita()
        {

            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDeReceitaporDepartamento);
                var receitas = new TicketsDeReceita();
                var todos = receitas.Todos(Departamento);
                receitas.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDeReceitaporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDeReceitaporDepartamento, ex.Message);
            }

        }


        public void DeletarAcordosDeConvencao()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarAcordosdeconvencaoporDepartamento);
                AcordosDeConvencao acordos = new AcordosDeConvencao();
                var todos = acordos.ObterAcordoDeConvencao(Departamento);

                acordos.Deletar(todos);
                FinalizarProcesso(TipoProcessoEnum.DeletarAcordosdeconvencaoporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarAcordosdeconvencaoporDepartamento, ex.Message);
            }


        }

        public void DeletarTicketsDeUnitarios()
        {

            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDeUnitáriosporDepartamento);
                TicketsDeProducao tickets = new TicketsDeProducao();
                var todos = tickets.Todos(Departamento);

                tickets.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDeUnitáriosporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDeUnitáriosporDepartamento, ex.Message);
            }


        }

        public void DeletarInsumos()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDeInsumosporDepartamento);
                Insumos insumos = new Insumos();
                var todos = insumos.ObterInsumo(Departamento);

                insumos.Deletar(todos);

                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDeInsumosporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDeInsumosporDepartamento, ex.Message);
            }


        }

        public void DeletarOrcamentosDeViagem()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcammentosDeViagemporDepartamento);
                OrcamentosDeViagens orcamentos = new OrcamentosDeViagens();
                var todos = orcamentos.TodosPor(Departamento);

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcammentosDeViagemporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcammentosDeViagemporDepartamento, ex.Message);
            }
        }

        public void DeletarOrcamentosDeProducao()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcammentosDeProduçãoporDepartamento);
                Orcamentos orcamentos = new Orcamentos();
                var todos = orcamentos.TodosOrcamentosHospitalares(Departamento);

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcammentosDeProduçãoporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcammentosDeProduçãoporDepartamento, ex.Message);
            }


        }
        public void DeletarOrcamentosDeDespesasOperacionais()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarOrcammentosOperacionaisporDepartamento);
                Orcamentos orcamentos = new Orcamentos();
                var todos = orcamentos.TodosOrcamentosOperacionaisPor(Departamento);

                orcamentos.Deletar(todos.ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarOrcammentosOperacionaisporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarOrcammentosOperacionaisporDepartamento, ex.Message);
            }



        }


        public void DeletarUsuarios()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarUsuariosporDepartamento);
                Usuarios usuarios = new Usuarios();
                TipoUsuarios tipos = new TipoUsuarios();

                var usuariosobj = usuarios.TodosPor(Departamento);

                foreach (var usuario in usuariosobj)
                {
                    if(usuario.Departamentos != null && usuario.Departamentos.Count > 0 && usuario.Departamentos.Any((d=> d.Id == Departamento.Id)))
                    {
                        usuario.Departamentos.Remove(Departamento);
                    }
                }

                usuarios.Salvar(usuariosobj.Where(u => (u.TipoUsuario.Id == (int)TipoUsuarioEnum.Administrador)).ToList());
                usuarios.Deletar(usuariosobj.Where(u => !(u.TipoUsuario.Id == (int)TipoUsuarioEnum.Administrador)).ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarUsuariosporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarUsuariosporDepartamento, ex.Message);
            }
        }



        public void DeletarDepartamentos()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarDepartamentosporDepartamento);
                Departamentos departamentos = new Departamentos();

                Departamento.Setores = null;
                Departamento.CentrosDeCusto = null;

                departamentos.Deletar(Departamento);

            //    FinalizarProcesso(TipoProcessoEnum.DeletarDepartamentosporDepartamento);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarDepartamentosporDepartamento, ex.Message);
            }


        }
    }
}
