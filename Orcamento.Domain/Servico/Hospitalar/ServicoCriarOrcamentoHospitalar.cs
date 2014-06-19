using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.InfraStructure;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain.Servico
{
    public class ServicoCriarOrcamentoHospitalar
    {
        private IOrcamentos orcamentos;

        public IOrcamentos Orcamentos
        {
            get { return this.orcamentos ?? (this.orcamentos = new Orcamentos()); }
            set { this.orcamentos = value; }
        }

        private ITicketsDeProducao tickets;

        public ITicketsDeProducao Tickets
        {
            get { return this.tickets ?? (this.tickets = new TicketsDeProducao()); }
            set { this.tickets = value; }
        }

        //TODO: colocar a regra de negocio no Negocio
        public void AtribuirVersaoFinal(Orcamento orcamento)
        {
            Contract.Requires(orcamento != null, "Orçamento não informado.");
            Contract.Requires(orcamento.Servicos != null, "Produção não informada.");
            Contract.Requires(!orcamento.Servicos.All(d => d.Valores.All(v => v.Valor == 0)), "Não é possivel atribuir versão final para orçamento de produção, quando existir todos os valores zerados");

            var listaTickets =  Tickets.Todos(orcamento.Setor);
            Contract.Requires(listaTickets.Any(t => t.Parcelas.Sum(p=> p.Valor) > 0),"Todos os tickets de produção estão iguais a zero, por favor insira tickets antes de atribuir versão final.");

            var orcamentoFinal = Orcamentos.ObterOrcamentoHospitalarFinal(orcamento.Setor);

            if (orcamentoFinal != null)
            {
                orcamentoFinal.VersaoFinal = false;
                Orcamentos.Salvar(orcamentoFinal);
            }

            orcamento.AtribuirVersaoFinal();
            Orcamentos.Salvar(orcamento);
        }

        public Orcamento CriarOrcamentoHospitalar(List<Orcamento> orcamentosGerenciamento, Departamento setor, int ano)
        {
            Contract.Requires(setor != null, "Departamento não informado");

            Orcamento orcamento = new OrcamentoHospitalar(setor, ano);

            if (orcamentosGerenciamento == null)
                orcamentosGerenciamento = new List<Orcamento>();

            GerenciadorDeOrcamentos gerenciador = new GerenciadorDeOrcamentos();

            if (!gerenciador.PodeCriarOrcamento(orcamentosGerenciamento, setor, TipoOrcamentoEnum.Hospitalar))
                throw new Exception("Orçamento já tem dez versões");

            orcamentosGerenciamento.Add(orcamento);

            gerenciador.InformarNomeOrcamento(orcamentosGerenciamento, orcamento, setor, TipoOrcamentoEnum.Hospitalar);

            foreach (var orcamentoGerenciamento in orcamentosGerenciamento)
            {
                Orcamentos.Salvar(orcamentoGerenciamento);
            }

            return orcamento;
        }

        public Orcamento CriarOrcamentoHospitalar(List<Orcamento> orcamentosGerenciamento, Departamento setor, int ano,Orcamento novoOrcamento)
        {
            Contract.Requires(setor != null, "Departamento não informado");

            if (orcamentosGerenciamento == null)
                orcamentosGerenciamento = new List<Orcamento>();

            GerenciadorDeOrcamentos gerenciador = new GerenciadorDeOrcamentos();

            if (!gerenciador.PodeCriarOrcamento(orcamentosGerenciamento, setor, TipoOrcamentoEnum.Hospitalar))
                throw new Exception("Orçamento já tem dez versões");

            orcamentosGerenciamento.Add(novoOrcamento);

            gerenciador.InformarNomeOrcamento(orcamentosGerenciamento, novoOrcamento, setor, TipoOrcamentoEnum.Hospitalar);

            foreach (var orcamentoGerenciamento in orcamentosGerenciamento)
            {
                Orcamentos.Salvar(orcamentoGerenciamento);
            }

            return novoOrcamento;
        }

        public void DeletarOrcamento(Orcamento orcamento, List<Orcamento> orcamentos, Departamento departamento)
        {
            Contract.Requires(orcamento != null, "Orçamento não informado");
            Contract.Requires(departamento != null, "Departamento não informado");

            Orcamentos.Deletar(orcamento);

            if (orcamentos.Exists(c => c == orcamento))
                orcamentos.Remove(orcamento);

            GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();
            gerenciamento.InformarNomeOrcamento(orcamentos, orcamento, departamento, TipoOrcamentoEnum.Hospitalar);

            foreach (var item in orcamentos)
            {
                Orcamentos.Salvar(item);
            }
        }
    }
}
