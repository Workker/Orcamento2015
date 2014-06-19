using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.Gerenciamento;
using Orcamento.InfraStructure;
using System.Diagnostics.Contracts;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.DTO;
using System.Xml.Serialization;

namespace Orcamento.Domain
{
    [Serializable]
    public enum TipoOrcamentoEnum : short
    {
        Viagem = 1,
        DespesaOperacional = 2,
        Hospitalar = 3,
        Pessoal
    }

    [Serializable]
    public abstract class Orcamento : IAggregateRoot<int>
    {
        private IList<ServicoHospitalar> servicos;
        //   private IList<CustoUnitario> custos;
        private IList<CustoUnitarioTotal> custosTotal;

        [NonSerialized()]
        private int id;


        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

      

        public virtual string Legenda { get; set; }
        public virtual double ValorTotalDRE { get; set; }
        public virtual bool VersaoFinal { get; set; }
        public virtual Departamento Setor { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual int Ano { get; set; }
        public virtual TipoOrcamentoEnum Tipo { get; set; }
        public virtual string NomeOrcamento { get; set; }
        public virtual long ValorTotal { get; set; }
        public virtual string MemoriaDeCalculoComplexidade { get; set; }
        public virtual string MemoriaDeCalculoUnitarios { get; set; }

        public virtual IList<DespesaDeViagem> Despesas { get; set; }

        public virtual IList<Despesa> DespesasOperacionais { get; set; }

        public virtual IList<ServicoHospitalar> Servicos
        {
            get { return servicos ?? (servicos = new List<ServicoHospitalar>()); }
            set { servicos = value; }
        }

        //public virtual IList<CustoUnitario> CustosUnitarios
        //{
        //    get { return custos ?? (custos = new List<CustoUnitario>()); }
        //    set { custos = value; }
        //}

        public virtual void AtualizarDespesas() { }
        public virtual IList<CustoUnitarioTotal> CustosUnitariosTotal
        {
            get { return custosTotal ?? (custosTotal = new List<CustoUnitarioTotal>()); }
            set { custosTotal = value; }
        }

        public virtual IList<FatorReceita> FatoresReceita { get; set; }

        public virtual IList<CentroDeCusto> CentrosDeCusto { get; set; }

        public virtual IList<DespesaPessoal> DespesasPessoais { get; set; }

        public virtual void CalcularReceitaBruta(List<TicketDeProducao> tickets) { }

        public virtual void CalcularReceitaLiquida(List<TicketDeProducao> tickets, List<TicketParcela> parcelas) { }

        public virtual void CalcularCustoHospitalar(TicketDeReceita tickets, List<CustoUnitario> custosUnitarios, List<ContaHospitalarDTO> contasUnitarias) { }

        public abstract void CalcularTotalDRE();

        public virtual IEnumerable<long> ObterDespesaFiltradaPor(DespesaDeViagem despesaDeViagem)
        {
            return this.Despesas.Where(
                x => x.NomeCidade == despesaDeViagem.NomeCidade && x.Despesa == despesaDeViagem.Despesa).Select(
                    y => y.Quantidade);
        }

        public virtual long ObterTotalFiltradoPor(DespesaDeViagem despesaDeViagem)
        {
            return ObterDespesaFiltradaPor(despesaDeViagem).Sum();
        }

        public virtual void AtribuirVersaoFinal()
        {
            this.VersaoFinal = true;
        }

        public virtual void InformarDepartamento(Departamento departarmento)
        {
            Contract.Requires(departarmento != null, "Departamento não informado.");

            this.Setor = departarmento;
        }

    }
}
