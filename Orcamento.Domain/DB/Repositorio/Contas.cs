using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.InfraStructure;

namespace Orcamento.Domain
{
    public class Contas : BaseRepository, IContas
    {
        public void Salvar(Conta conta) 
        {
            Contract.Requires(conta != null, "Conta não informada.");
            base.Salvar(conta);
        }

        public void Alterar(Conta conta)
        {
            
        }

        public Conta ObterContaPor(string codigo)
        {
            return Session.QueryOver<Conta>().Where(c => c.CodigoDaConta == codigo).SingleOrDefault();
        }

        public Conta ObterContaPor(int id)
        {
            return Session.QueryOver<Conta>().Where(c => c.Id == id).SingleOrDefault();
        }

        public virtual IList<Conta> Todos()
        {
            return base.Todos<Conta>();
        }
    }
}
