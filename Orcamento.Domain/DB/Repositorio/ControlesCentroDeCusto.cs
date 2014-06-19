using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public class ControlesCentroDeCusto : BaseRepository
    {
        public virtual void SalvarLista(List<ControleDeCentroDeCusto> roots)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public List<ControleDeCentroDeCusto> ObterASalvarPor(Departamento departamento)
        {
            return Session.QueryOver<ControleDeCentroDeCusto>().Where(h => h.Departamento == departamento && !h.Salvo).List<ControleDeCentroDeCusto>().ToList();
        }

        public ControleDeCentroDeCusto ObterPor(Departamento departamento,CentroDeCusto centro)
        {
            return Session.QueryOver<ControleDeCentroDeCusto>().Where(h => h.Departamento == departamento && h.CentroDeCusto == centro).SingleOrDefault();
        }

        public List<ControleDeCentroDeCusto> ObterTodos(Departamento departamento)
        {
            return Session.QueryOver<ControleDeCentroDeCusto>().Where(h => h.Departamento == departamento).List<ControleDeCentroDeCusto>().ToList();
        }
    }
}
