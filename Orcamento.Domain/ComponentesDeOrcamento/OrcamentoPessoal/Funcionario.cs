using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.Gerenciamento;
using System.Linq;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal
{
    public class Funcionario : IAggregateRoot<int>
    {
        public virtual DateTime? DataSaidaFerias { get; set; }
        public virtual int DataAdmissao { get; set; }
        public virtual Double Salario { get; set; }
        public virtual string Matricula { get; set; }
        public virtual int CargaHoraria { get; set; }
        public virtual bool Demitido { get; set; }
        public virtual int MesDeDemissao { get; set; }
        public virtual double Aumento { get; set; }
        public virtual bool Aumentado { get; set; }
        public virtual int MesDeAumento { get; set; }
        public virtual string Nome { get; set; }
        public virtual IList<DespesaPessoal> Despesas { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual string Cargo { get; set; }
        public virtual int AnoAdmissao { get; set; }
        public virtual string InicialNumeroMatricula { get; set; }
        public virtual int NumeroDeVaga { get; set; }

        public virtual int ObterNumeroSequencial
        {
            get
            {
                return string.IsNullOrEmpty(InicialNumeroMatricula) ? 0 : InicialNumeroMatricula == "N" ? 999999 + NumeroDeVaga : 555555 + NumeroDeVaga;
            }
        }

        public virtual int FuncionarioReposicao { get; set; }

        #region IAggregateRoot<int> Members

        public virtual int Id { get; set; }

        #endregion

        public Funcionario(Departamento departamento)
        {
            this.Departamento = departamento;
        }

        protected Funcionario() { }

        public virtual void Adicionar(DespesaPessoal despesa)
        {
            Contract.Requires(despesa != null, "A despesa não foi informada");
            Contract.Requires(despesa.Parcelas != null, "A despesa não possui parcelas");

            if (Despesas == null)
            {
                Despesas = new List<DespesaPessoal>();
            }

            Despesas.Add(despesa);
        }

        public virtual IList<DespesaPessoal> ObterDespesas()
        {
            return Despesas;
        }

        public virtual void DeletarTodasAsDespesas()
        {
            if (Despesas != null)
                while (Despesas.Count != 0)
                {
                    Despesas.Remove(Despesas[0]);
                }
        }

        public virtual void CalcularDespesa(CentroDeCusto centroDeCusto, Conta conta, NovoOrcamentoPessoal orcamento, int mesAdmissao, double AumentoConvencao, int mesAumentoConvencao)
        {
            var despesa = LegislacaoBrasileira.ObterDespesasParaO(this, conta, orcamento);

            if (despesa == null) return;

            despesa.Funcionario = this;

            if (despesa.GetType() == typeof(Extras) && this.Aumentado)
            {
                var salarioAumentado = Salario + (Salario * (Aumento / 100));
                despesa.Calcular(Salario, mesAdmissao, Aumentado, salarioAumentado, MesDeAumento, AumentoConvencao, mesAumentoConvencao);
            }
            else
                despesa.Calcular(Salario, mesAdmissao,AumentoConvencao,mesAumentoConvencao);

            Adicionar(despesa);
        }

        public virtual bool Excluido { get; set; }
    }
}