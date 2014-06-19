using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Servico.Hospitalar;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Controller
{
    public class ContaController
    {
        private IContas contas;

        public IContas Contas
        {
            private get { return contas ?? (contas = new Contas()); }
            set { contas = value; }
        }

        public void Salvar(Conta conta)
        {
            Contas.Salvar(conta);
        }

        public IList<Conta> BuscarTodasAsContas()
        {
            return Contas.Todos();
        }

        public Conta BuscarContaPor(int id)
        {
            return Contas.ObterContaPor(id);
        }

        public Conta BuscarContaPor(string codigo)
        {
            return Contas.ObterContaPor(codigo);
        }

        public void Alterar(Conta conta)
        {
            Contas.Alterar(conta);
        }
    }
}
