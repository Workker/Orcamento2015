using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.Gerenciamento;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace Orcamento.Domain.DB.Repositorio
{

    public class Orcamentos : BaseRepository, IOrcamentos
    {
        public virtual void Deletar(List<Orcamento> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                foreach (var root in roots)
                {
                    Session.Delete(root);
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }

        public virtual void Deletar(List<OrcamentoDeViagem> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                foreach (var root in roots)
                {
                    Session.Delete(root);
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }

        public virtual void Deletar(List<OrcamentoHospitalar> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                foreach (var root in roots)
                {
                    Session.Delete(root);
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }

        public virtual void Deletar(List<OrcamentoOperacionalVersao> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                foreach (var root in roots)
                {
                    Session.Delete(root);
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }

        public virtual void Salvar(List<Orcamento> roots)
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

        public virtual List<Orcamento> TodosPor(Departamento setor)
        {
            var criterio = Session.CreateCriteria<Orcamento>();
            criterio.Add(Expression.Eq("Setor", setor));

            return criterio.List<Orcamento>().ToList();
        }



        public virtual Orcamento ObterOrcamentoFinalOrcamentoDeViagem(CentroDeCusto centroDeCusto)
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.UniqueResult<Orcamento>();
        }

        public virtual Orcamento ObterOrcamentoFinalOrcamentoOperacional(CentroDeCusto centroDeCusto, Departamento setor)
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>("o");
            criterio.CreateCriteria("o.DespesasOperacionais", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("o.VersaoFinal", true));
            criterio.Add(Expression.Eq("o.CentroDeCusto", centroDeCusto));
            criterio.Add(Expression.Eq("o.Setor", setor));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());

            return criterio.UniqueResult<Orcamento>();
        }

        public virtual List<Orcamento> ObterOrcamentosFinaisOperacionaisHospitalares()
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>("o");
            criterio.CreateCriteria("o.Setor", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());
            var orcamentos = criterio.List<Orcamento>().ToList();
            //.CreateCriteria("n.Departamento", "d", JoinType.InnerJoin)
            //  .Add(Restrictions.Eq("d.Id", departamentoId))
            //  .SetResultTransformer(new DistinctRootEntityResultTransformer())

            //var orcamentos = Session.QueryOver<OrcamentoOperacionalVersao>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            return orcamentos.Where(o => o.Setor.Tipo == TipoDepartamento.hospital).ToList();
            //var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
            //criterio.Add(Expression.Eq("VersaoFinal", true));
            //var orcamentos = criterio.List<Orcamento>().ToList();

            ////var orcamentos = Session.QueryOver<OrcamentoOperacionalVersao>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            //return orcamentos.Where(o => o.Setor.GetType() == typeof(Hospital)).ToList();
        }

        public virtual List<Orcamento> ObterOrcamentosFinaisOperacionaisCoorporativo()
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>("o");
            criterio.CreateCriteria("o.Setor", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());
            var orcamentos = criterio.List<Orcamento>().ToList();
            //.CreateCriteria("n.Departamento", "d", JoinType.InnerJoin)
            //  .Add(Restrictions.Eq("d.Id", departamentoId))
            //  .SetResultTransformer(new DistinctRootEntityResultTransformer())

            //var orcamentos = Session.QueryOver<OrcamentoOperacionalVersao>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            return orcamentos.Where(o => o.Setor.Tipo == TipoDepartamento.setor).ToList();
        }

        public virtual List<Orcamento> ObterOrcamentosFinaisViagensHospitalares()
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>("o");
            criterio.CreateCriteria("o.Setor", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());
            var orcamentos = criterio.List<Orcamento>().ToList();
            //.CreateCriteria("n.Departamento", "d", JoinType.InnerJoin)
            //  .Add(Restrictions.Eq("d.Id", departamentoId))
            //  .SetResultTransformer(new DistinctRootEntityResultTransformer())

            //var orcamentos = Session.QueryOver<OrcamentoOperacionalVersao>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            return orcamentos.Where(o => o.Setor.Tipo == TipoDepartamento.hospital).ToList();

            //var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            //criterio.Add(Expression.Eq("VersaoFinal", true));
            //var orcamentos = criterio.List<Orcamento>().ToList();

            //// var orcamentos = Session.QueryOver<OrcamentoDeViagem>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            //return orcamentos.Where(o => o.Setor.GetType() == typeof(Hospital)).ToList();
        }
        public virtual List<Orcamento> ObterOrcamentosFinaisViagensCoorporativo()
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>("o");
            criterio.CreateCriteria("o.Setor", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());
            var orcamentos = criterio.List<Orcamento>().ToList();
            //.CreateCriteria("n.Departamento", "d", JoinType.InnerJoin)
            //  .Add(Restrictions.Eq("d.Id", departamentoId))
            //  .SetResultTransformer(new DistinctRootEntityResultTransformer())

            //var orcamentos = Session.QueryOver<OrcamentoOperacionalVersao>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            return orcamentos.Where(o => o.Setor.Tipo == TipoDepartamento.setor).ToList();
            //var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            //criterio.Add(Expression.Eq("VersaoFinal", true));
            //var orcamentos = criterio.List<Orcamento>().ToList();

            //var orcamentos = Session.QueryOver<OrcamentoDeViagem>().Where(o => o.VersaoFinal).List<Orcamento>().ToList();
            //return orcamentos.Where(o => o.Setor.GetType() == typeof(Setor)).ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosHospitalares(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<OrcamentoHospitalar>();
            criterio.Add(Expression.Eq("Setor", departamento));

            return criterio.List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosHospitalaresMenos(Departamento departamento, int id)
        {
            return Session.QueryOver<OrcamentoHospitalar>().Where(o => o.Id != id && o.Setor.Id == departamento.Id).List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosDeViagem(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            criterio.Add(Expression.Eq("Setor", departamento));

            return criterio.List<Orcamento>().ToList();
        }

        public virtual Orcamento ObterOrcamentoHospitalarFinal(Departamento hospital)
        {
            var criterio = Session.CreateCriteria<OrcamentoHospitalar>();
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.Add(Expression.Eq("Setor", hospital));

            return criterio.UniqueResult<Orcamento>();
        }

        public virtual List<Orcamento> ObterOrcamentosHospitalaresFinais()
        {
            var criterio = Session.CreateCriteria<OrcamentoHospitalar>();
            criterio.Add(Expression.Eq("VersaoFinal", true));

            return criterio.List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosDeViagemPor(CentroDeCusto centroDeCusto, Departamento setor)
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            criterio.Add(Expression.Eq("Setor", setor));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosDeViagemPor(CentroDeCusto centroDeCusto)
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.List<Orcamento>().ToList();
        }


        public virtual List<Orcamento> TodosOrcamentosDeViagemPor(CentroDeCusto centroDeCusto, Departamento setor, int id)
        {
            return Session.QueryOver<OrcamentoHospitalar>().Where(o => o.CentroDeCusto.Id == centroDeCusto.Id &&
                  o.Setor.Id == setor.Id && o.Id != id).List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosOperacionaisPor(CentroDeCusto centroDeCusto, Departamento setor)
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
            criterio.Add(Expression.Eq("Setor", setor));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosOperacionaisPor(Departamento setor)
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
            criterio.Add(Expression.Eq("Setor", setor));
            return criterio.List<Orcamento>().ToList();
        }
        public virtual List<Orcamento> TodosOrcamentosOperacionaisPor(CentroDeCusto centroDeCusto)
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.List<Orcamento>().ToList();
        }

        public virtual List<Orcamento> TodosOrcamentosOperacionaisPor(CentroDeCusto centroDeCusto, Departamento setor, int id)
        {
            return Session.QueryOver<OrcamentoOperacionalVersao>().Where(o => o.CentroDeCusto.Id == centroDeCusto.Id
                   && o.Setor.Id == setor.Id && o.Id != id).List<Orcamento>().ToList();
        }

        public List<DRE> ObterDRE(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<Orcamento>();
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.Add(Expression.Eq("Setor", departamento));

            List<Orcamento> orcamentos = criterio.List<Orcamento>().ToList();

            DRE dre = new DRE();

            List<DRE> dres = new List<DRE>();

            foreach (var orcamento in orcamentos)
            {
                orcamento.CalcularTotalDRE();

                DRE totalDeViagens = new DRE();

                totalDeViagens.ValorTotal = Math.Round(orcamento.ValorTotalDRE, 2);
                totalDeViagens.Nome = orcamento.Legenda;

                dres.Add(totalDeViagens);
            }

            return dres;
        }



        public void AtualizarDespesasOperacionais(List<Despesa> despesas)
        {
            throw new NotImplementedException();
        }

        public void Refresh(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                Session.Refresh(root);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }
    }
}