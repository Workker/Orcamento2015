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
                case TipoProcessoEnum.DeletarEstruturaCompleta:
                    DeletarEstruturaOrcamentaria();
                    break;
                case TipoProcessoEnum.DeletarAcordosdeconvencao:
                    DeletarAcordosDeConvencao();
                    break;
                case TipoProcessoEnum.DeletarDepartamentos:
                    DeletarDepartamentos();
                    break;
                case TipoProcessoEnum.DeletarFuncionarios:
                    DeletarFuncionarios();
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
                case TipoProcessoEnum.DeletarTicketsDePessoal:
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
                AdicionarProcesso(TipoProcessoEnum.DeletarTotaisDaDRE);
                DRES dres = new DRES();
                var dre = dres.Obter(Departamento);

                dres.Deletar(dre);
                FinalizarProcesso(TipoProcessoEnum.DeletarTotaisDaDRE);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTotaisDaDRE, ex.Message);
            }
        }

        public void DeletarTicketDePessoal()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarTicketsDePessoal);

                TicketsDeOrcamentoPessoal tickets = new TicketsDeOrcamentoPessoal();
                var todos = tickets.Todos(Departamento);

                tickets.Deletar(todos);
                FinalizarProcesso(TipoProcessoEnum.DeletarTicketsDePessoal);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarTicketsDePessoal, ex.Message);
            }
        }

        public void DeletarFuncionarios()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarFuncionarios);
                Funcionarios funcionarios = new Funcionarios();
                var todos = funcionarios.ObterPor(Departamento);

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
                var todos = orcamentos.Todos(Departamento.Id);

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
                var todos = receitas.Todos(Departamento);
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
                var todos = acordos.ObterAcordoDeConvencao(Departamento);

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
                var todos = tickets.Todos(Departamento);

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
                var todos = insumos.ObterInsumo(Departamento);

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
                OrcamentosDeViagens orcamentos = new OrcamentosDeViagens();
                var todos = orcamentos.TodosPor(Departamento);

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
                var todos = orcamentos.TodosOrcamentosHospitalares(Departamento);

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
                var todos = orcamentos.TodosOrcamentosOperacionaisPor(Departamento);

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

                var usuariosobj = usuarios.TodosPor(Departamento);

                foreach (var usuario in usuariosobj.Where(u => u.TipoUsuario.Id == (int)TipoUsuarioEnum.Administrador))
                {
                    usuario.Departamentos = null;
                }

                usuarios.Salvar(usuariosobj.Where(u => (u.TipoUsuario.Id == (int)TipoUsuarioEnum.Administrador)).ToList());
                usuarios.Deletar(usuariosobj.Where(u => !(u.TipoUsuario.Id == (int)TipoUsuarioEnum.Administrador)).ToList());

                FinalizarProcesso(TipoProcessoEnum.DeletarUsuarios);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarUsuarios, ex.Message);
            }
        }



        public void DeletarDepartamentos()
        {
            try
            {
                AdicionarProcesso(TipoProcessoEnum.DeletarDepartamentos);
                Departamentos departamentos = new Departamentos();

                Departamento.Setores = null;
                Departamento.CentrosDeCusto = null;

                departamentos.Deletar(Departamento);

                FinalizarProcesso(TipoProcessoEnum.DeletarDepartamentos);
            }
            catch (Exception ex)
            {
                ReportarErro(TipoProcessoEnum.DeletarDepartamentos, ex.Message);
            }


        }
    }
}
