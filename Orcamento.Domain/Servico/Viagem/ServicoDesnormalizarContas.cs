using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.DTO;

namespace Orcamento.Domain.Servico
{
    public class ServicoDesnormalizarContas
    {
        #region Atributos

        private int orcamentoDeViagemId;
        private IList<DespesaDeViagem> despesaDeViagens;
        private Orcamento orcamentoDeViagem;

        #endregion

        #region Construtores

        public ServicoDesnormalizarContas(Orcamento orcamentoDeViagem)
        {
            this.despesaDeViagens = orcamentoDeViagem.Despesas;
            this.orcamentoDeViagemId = orcamentoDeViagem.Id;
            this.orcamentoDeViagem = orcamentoDeViagem;
        }

        #endregion

        #region Métodos

        public virtual List<ContaDTO> ObterContasDesnormalizadas()
        {
            var despesasFiltradas = ObterDespesasQueTenhamTodasAsContasDeViagemNecessariasParaADesnormalizacao(this.despesaDeViagens);

            List<ContaDTO> contasDTO = new List<ContaDTO>();

            foreach (var despesaFiltrada in despesasFiltradas)
                DesnormalizarContas(contasDTO, despesaFiltrada);

            return contasDTO;
        }

        private void DesnormalizarContas(List<ContaDTO> contasDTO, DespesaDeViagem despesaFiltrada)
        {
            var contaDTO = ObterContaPreenchidaPor(despesaFiltrada);

            var despesasOrdenadasPorMes = ObterDespesasOrdenadasPorMes(despesaFiltrada, despesaDeViagens);

            AdicionarTodasAsDespesasOrdenadasPorMesEmConta(contaDTO, despesasOrdenadasPorMes);

            contasDTO.Add(contaDTO);
        }

        private IOrderedEnumerable<DespesaDeViagem> ObterDespesasOrdenadasPorMes(DespesaDeViagem despesaFiltrada, IList<DespesaDeViagem> despesas)
        {
            return despesas.Where(
                x => x.Despesa == despesaFiltrada.Despesa && x.NomeCidade == despesaFiltrada.NomeCidade).OrderBy
                (x => (short)x.Mes);
        }

        private List<DespesaDeViagem> ObterDespesasQueTenhamTodasAsContasDeViagemNecessariasParaADesnormalizacao(IList<DespesaDeViagem> despesas)
        {
            return despesas.Where(d => d.Mes == MesEnum.Janeiro).ToList();
        }

        private ContaDTO ObterContaPreenchidaPor(DespesaDeViagem despesa)
        {
            ContaDTO contaDTO = new ContaDTO()
            {
                Despesas = new List<DespesaDTO>(),
                Despesa = despesa.Despesa,
                Conta = despesa.NomeCidade,
                ContaId = despesa.Id,
                DespesaOperacionalId = this.orcamentoDeViagemId,
                ValorTotal = this.orcamentoDeViagem.ObterTotalFiltradoPor(despesa)
            };

            return contaDTO;
        }

        private void AdicionarTodasAsDespesasOrdenadasPorMesEmConta(ContaDTO contaDTO, IOrderedEnumerable<DespesaDeViagem> despesasOrdenadasPorMes)
        {
            foreach (var despesa in despesasOrdenadasPorMes)
            {
                var despesaDTO = TransformarDespesaEmDespesaDTO(despesa);

                contaDTO.Despesas.Add(despesaDTO);
            }
        }

        private DespesaDTO TransformarDespesaEmDespesaDTO(DespesaDeViagem despesa)
        {
            return new DespesaDTO
                       {
                           Mes = (short)despesa.Mes,
                           Valor = despesa.Quantidade,
                           DespesaId = despesa.Id
                       };
        }

        #endregion
    }
}
