using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DB;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.Servico.Robo
{
    public class ConfiguracoesGeraisService
    {
        public void DeletarEstruturaOrcamentaria()
        {
            DeletarDRETotal();
            DeletarFuncionarios();
            DeletarOrcamentoDePessoal();
            DeletarTicketsDeReceita();
            DeletarTicketsDeUnitarios();
            DeletarInsumos();
            DeletarOrcamentos();
            DeletarAcordosDeConvencao();
            DeletarUsuarios();
            DeletarDepartamentos();
            DeletarCentrosDeCusto();
            DeletarGrupoDeContas();
            DeletarContas();
        }

        private void DeletarDRETotal()
        {
            DRES dres = new DRES();
            var todos = dres.Todos();

            dres.Deletar(todos);
        }

        private void DeletarFuncionarios()
        {
            Funcionarios funcionarios = new Funcionarios();
            var todos = funcionarios.Todos<Funcionario>();

            funcionarios.Deletar(todos.ToList());
        }

        private void DeletarOrcamentoDePessoal()
        {
            NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();
            var todos = orcamentos.Todos();

            orcamentos.Deletar(todos.ToList());
        }


        private void DeletarTicketsDeReceita()
        {
            var receitas = new TicketsDeReceita();
            var todos = receitas.Todos<TicketDeReceita>();
            receitas.Deletar(todos.ToList());
        }


        private void DeletarAcordosDeConvencao()
        {
            AcordosDeConvencao acordos = new AcordosDeConvencao();
            var todos = acordos.Todos<AcordoDeConvencao>();

            acordos.Deletar(todos);
        }

        private void DeletarTicketsDeUnitarios()
        {
            TicketsDeProducao tickets = new TicketsDeProducao();
            var todos = tickets.Todos();

            tickets.Deletar(todos.ToList());
        }

        private  void DeletarInsumos()
        {
            Insumos insumos = new Insumos();
            var todos = insumos.Todos<Insumo>();

            insumos.Deletar(todos);
        }

        private void DeletarOrcamentos()
        {
            Orcamentos orcamentos = new Orcamentos();
            var todos = orcamentos.Todos<Orcamento>();

            orcamentos.Deletar(todos.ToList());
        }

        private void DeletarUsuarios()
        {
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
        }

        private void DeletarGrupoDeContas()
        {
            GruposDeConta gruposDeConta = new GruposDeConta();
            var todos = gruposDeConta.Todos<GrupoDeConta>();

            gruposDeConta.Deletar(todos.ToList());
        }

        private void DeletarContas()
        {
            Contas contas = new Contas();
            var todas = contas.Todos();

            contas.Deletar(todas);
        }        

        private void DeletarCentrosDeCusto()
        {
            CentrosDeCusto centros = new CentrosDeCusto();
            var todos = centros.Todos();

            centros.Deletar(todos.ToList());
        }

        private void DeletarDepartamentos()
        {
            Departamentos departamentos = new Departamentos();
            var todos = departamentos.Todos();

            departamentos.Deletar(todos);
        }
    }
}
