
using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.DTO.Viagem;

namespace Orcamento.Domain.Servico
{
    public class ServicoDesnormalizarTotaisEmDespesasPorMeses
    {
        #region Atributos

        private readonly Orcamento orcamento;
        private readonly IList<Cidade> cidades;
        private readonly List<DespesaDeViagemDTO> despesasDeViagemDTO;

        private DespesaDeViagemDTO despesaDeTaxiDTO;
        private DespesaDeViagemDTO despesaDeRefeicaoDTO;
        private DespesaDeViagemDTO despesaDeDiariaDTO;
        private DespesaDeViagemDTO despesaDePassagemDTO;

        #endregion

        #region Construtores

        public ServicoDesnormalizarTotaisEmDespesasPorMeses(Orcamento orcamento, IList<Cidade> cidades)
        {
            this.orcamento = orcamento;
            this.cidades = cidades;
            despesasDeViagemDTO = new List<DespesaDeViagemDTO>();
        }

        #endregion

        #region Métodos

        public virtual IOrderedEnumerable<DespesaDeViagemDTO> ObterDespesasDeViagemDesnormalizadas()
        {
            foreach (var cidade in cidades)
            {
                InformarDespesasASeremAdicionadasMesAMesParaCidadeCorrente(cidade);

                AdicionarDespesasMesAMes(cidade);

                AdicionarDespesaAoResultadoFinal();
            }

            return ObterDespesasDeViagemDesnormalizadasOrdenadasPeloNomeDeCidade();
        }

        private IOrderedEnumerable<DespesaDeViagemDTO> ObterDespesasDeViagemDesnormalizadasOrdenadasPeloNomeDeCidade()
        {
            return despesasDeViagemDTO.OrderBy(x=> x.NomeCidade);
        }

        private void AdicionarDespesaAoResultadoFinal()
        {
            despesasDeViagemDTO.Add(despesaDeTaxiDTO);
            despesasDeViagemDTO.Add(despesaDeRefeicaoDTO);
            despesasDeViagemDTO.Add(despesaDeDiariaDTO);
            despesasDeViagemDTO.Add(despesaDePassagemDTO);
        }

        private void AdicionarDespesasMesAMes(Cidade cidade)
        {
            for (int mes = 1; mes < 13; mes++)
            {
                AdicionarDespesasDoMesCorrente(cidade, mes);
            }
        }

        private void AdicionarDespesasDoMesCorrente(Cidade cidade, int mes)
        {
            Cidade obtida = cidade;
            foreach (DespesaDeViagem item in this.orcamento.Despesas.Where(d => d.NomeCidade == obtida.Descricao && d.Mes == (MesEnum) mes))
            {
                despesaDeTaxiDTO.AdicionarItem((MesEnum) mes, item.ValorTotalTaxi, despesaDeTaxiDTO.Despesa);
                despesaDeRefeicaoDTO.AdicionarItem((MesEnum) mes, item.ValorTotalRefeicao, despesaDeRefeicaoDTO.Despesa);
                despesaDeDiariaDTO.AdicionarItem((MesEnum) mes, item.ValorTotalDiaria, despesaDeDiariaDTO.Despesa);
                despesaDePassagemDTO.AdicionarItem((MesEnum) mes, item.ValorTotalPassagem, despesaDePassagemDTO.Despesa);
            }
        }

        private void InformarDespesasASeremAdicionadasMesAMesParaCidadeCorrente(Cidade cidade)
        {
            despesaDeTaxiDTO = new DespesaDeViagemDTO() {NomeCidade = cidade.Descricao, Despesa = "Taxi"};
            despesaDeRefeicaoDTO = new DespesaDeViagemDTO() {NomeCidade = cidade.Descricao, Despesa = "Refeição"};
            despesaDeDiariaDTO = new DespesaDeViagemDTO() {NomeCidade = cidade.Descricao, Despesa = "Diaria"};
            despesaDePassagemDTO = new DespesaDeViagemDTO() {NomeCidade = cidade.Descricao, Despesa = "Passagem"};
        }

        #endregion
    }
}
