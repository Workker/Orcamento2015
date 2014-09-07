using System;
using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.DB.Mappers;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using NHibernate.Criterion;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface IFuncionarios
    {
        List<FuncionarioDTO> ObterTodos(int departamentoId, string codigoCentroDeCusto);
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }

    public class Funcionarios : BaseRepository, IFuncionarios
    {
        public virtual void Deletar(List<Funcionario> roots)
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


        public List<FuncionarioDTO> ObterTodos(int departamentoId, string codigoCentroDeCusto)
        {
            var departamento = Obter<Departamento>(departamentoId);

            var centroDeCusto = departamento.ObterCentroDeCustoPor(codigoCentroDeCusto);

            var funcionarios = from f in centroDeCusto.Funcionarios
                               where f.Departamento.Id == departamentoId
                               select new FuncionarioDTO
                                          {
                                              Id = f.Id,
                                              Aumentado = f.Aumentado,
                                              CentroDeCusto = centroDeCusto.Nome,
                                              DataDeAdmissao = f.DataAdmissao,
                                              MesDeDemissao = f.MesDeDemissao,
                                              DataDeSaidaDeFerias = f.DataSaidaFerias.HasValue ? f.DataSaidaFerias.Value : DateTime.MinValue,
                                              MesDeAumento = f.MesDeAumento,
                                              Demitido = f.Demitido,
                                              Nome = f.Nome,
                                              Hospital = departamento.Nome,
                                              Matricula = f.Matricula,
                                              SalarioBase = f.Salario,
                                              PercentualDeAumento = f.Aumento,
                                              Cargo = f.Cargo,
                                              AnoAdmissao = f.AnoAdmissao,
                                              NumeroDeVaga =  f.NumeroDeVaga,
                                              InicialNumeroMatricula = f.InicialNumeroMatricula,
                                              FuncionarioReposicao = f.FuncionarioReposicao
                                          };

            return funcionarios.ToList();
        }

        public List<Funcionario> ObterPor(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<Funcionario>();
            criterio.Add(Expression.Eq("Departamento", departamento));
            return criterio.List<Funcionario>().ToList();
        }

        public virtual void SalvarLista(List<Funcionario> roots)
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
    }
}
