using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem
{
    public class OrcamentoDeViagem : Orcamento
    {
        public override IList<DespesaDeViagem> Despesas { get; set; }

        public override TipoOrcamentoEnum Tipo
        {
            get { return TipoOrcamentoEnum.Viagem; }
            set { base.Tipo = value; }
        }

        public override long ValorTotal
        {
            get { return Despesas.Sum(d => d.ValorTotal); }
            set { base.ValorTotal = value; }
        }

        public OrcamentoDeViagem(Departamento setor, CentroDeCusto centroDeCusto, int ano)
        {
            Contract.Requires(setor != null, "Setor não informado.");
            Contract.Requires(centroDeCusto != null, "Centro de custo não informado.");

            Contract.Requires(ano > 0, "ano não informado");

            this.Ano = ano;
            this.Setor = setor;
            this.CentroDeCusto = centroDeCusto;
            this.Despesas = new List<DespesaDeViagem>();
        }

        public OrcamentoDeViagem()
        {
        }

        public virtual void CriarDespesas(List<Viagem> viagens, List<Diaria> diarias)
        {
            Contract.Requires(viagens != null, "Não foram informados viagens.");
            Contract.Requires(diarias != null, "Não foram informadas diarias.");

            CriarViagens(viagens);
            CriarDiarias(diarias);
        }

        private void CriarDiarias(List<Diaria> diarias)
        {
            foreach (var diaria in diarias)
            {
                for (short mes = 1; mes < 13; mes++)
                {
                    DiariaViagem diariaViagem = new DiariaViagem(diaria, (MesEnum) mes);
                    this.Despesas.Add(diariaViagem);
                }
            }
        }

        private void CriarViagens(List<Viagem> viagens)
        {
            foreach (var viagem in viagens)
            {
                for (short mes = 1; mes < 13; mes++)
                {
                    ViagemCidade viagemCidade = new ViagemCidade(viagem, (MesEnum) mes);
                    this.Despesas.Add(viagemCidade);
                }
            }
        }

        public override void CalcularTotalDRE()
        {
            this.Legenda = "Orçamento de Viagem";

            this.ValorTotalDRE = Despesas.Sum(x => x.ValorTotal);
        }
    }
}
