using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.DTO.OrcamentoPessoal;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;

namespace Orcamento.Domain.Servico.Pessoal
{
    public class ServicoObterTotalizadorDePessoal
    {
        public Departamento Departamento { get; set; }
        public NovoOrcamentoPessoal Orcamento { get; set; }
        private NovoOrcamentoPessoal novoOrcamento;
        public NovosOrcamentosPessoais NovosOrcamentosPessoais
        {
            get { return new NovosOrcamentosPessoais(); }
        }

        public IList<OrcamentoPessoalDTO> Obter(NovoOrcamentoPessoal orcamentoPessoal)
        {
            this.Departamento = orcamentoPessoal.Departamento;
            Orcamento = orcamentoPessoal;

            var orcamentosPessoaisDTO = new List<OrcamentoPessoalDTO>();

            var grupoDeContasDTO = new List<GrupoDeContaDTO>();

            foreach (var grupoDeConta in orcamentoPessoal.CentroDeCusto.GrupoDeContas)
            {
                var grupoDeContaDTO = ObterGrupoDeContaDTOInicializado(grupoDeConta);

                foreach (var conta in grupoDeConta.Contas)
                    AdicionarDespesas(grupoDeContaDTO, conta, orcamentoPessoal.CentroDeCusto);

                CalcularTotalizadorGrupoDeConta(grupoDeContaDTO);

                grupoDeContasDTO.Add(grupoDeContaDTO);
            }

            var orcamentoPessoalDTO = new OrcamentoPessoalDTO();

            CalcularTotalizadorDoOrcamento(orcamentoPessoalDTO, grupoDeContasDTO);

            orcamentosPessoaisDTO.Add(orcamentoPessoalDTO);

            return orcamentosPessoaisDTO;
        }

        private void CalcularTotalizadorDoOrcamento(OrcamentoPessoalDTO orcamentoPessoalDTO, List<GrupoDeContaDTO> grupoDeContasDTO)
        {
            orcamentoPessoalDTO.GruposDeConta = new List<GrupoDeContaDTO>();
            orcamentoPessoalDTO.GruposDeConta.AddRange(grupoDeContasDTO);
            orcamentoPessoalDTO.TotaisOrcamentoMensal = new List<TotalOrcamentoMensalDTO>();

            for (int i = 1; i < 13; i++)
            {
                int i1 = i;
                var despesas = grupoDeContasDTO.SelectMany(x => x.DespesasGrupoDeConta.Where(y => y.Mes == i));

                if (despesas.Any())
                {
                    //double valor = 0;

                    //foreach (var despesa in despesas)
                    //{
                    //    valor += despesa.Valor;
                    //}


                    var valor = despesas.Sum(a => a.Valor);

                    orcamentoPessoalDTO.TotaisOrcamentoMensal.Add(new TotalOrcamentoMensalDTO { Mes = i, Valor = valor });
                }
            }

            AdicionarZeroOsTotaisDeOrcamentoComGruposDeContasComContasSemDespesas(orcamentoPessoalDTO);


            //double valortotal = 0;
            //foreach (var totalmensal in orcamentoPessoalDTO.TotaisOrcamentoMensal)
            //{
            //    valortotal += totalmensal.Valor;
            //}

            var valortotal = orcamentoPessoalDTO.TotaisOrcamentoMensal.Sum(a => a.Valor);

            orcamentoPessoalDTO.TotalOrcamento = valortotal;
        }

        private void AdicionarDespesas(GrupoDeContaDTO grupoDeContaDTO, Conta conta, CentroDeCusto centroDeCusto)
        {
            var despesaConta = new ContaDespesaPessoalDTO { Conta = conta.Nome, Despesas = new List<DespesaPessoalDTO>() };

            AdicionarDespesas(grupoDeContaDTO, conta, centroDeCusto, despesaConta);

            AdicionarZeroAsContasSemDespesa(despesaConta);
            despesaConta.TotalConta = ObterOSomatorioTotalDasParcelasDeDespesasNaoRalacionadasAoFuncionario(conta, centroDeCusto);
            //despesaConta.TotalConta = grupoDeContaDTO.GrupoConta == "Benefícios" ? ObterOSomatorioTotalDasParcelasDeDespesasNaoRalacionadasAoFuncionario(conta, centroDeCusto) : ObterOSomatorioTotalDasParcelasDeDespesasRelacionadasAoFuncionario(conta, centroDeCusto);

            grupoDeContaDTO.Contas.Add(despesaConta);
        }

        private void AdicionarDespesas(GrupoDeContaDTO grupoDeContaDTO, Conta conta, CentroDeCusto centroDeCusto, ContaDespesaPessoalDTO despesaConta)
        {
            for (var i = 1; i < 13; i++)
            {
                var mes = i;

                IEnumerable<Parcela> parcelas = null;

                parcelas = grupoDeContaDTO.GrupoConta == "Benefícios"
                               ? ObterParcelasDeDespesasNaoRelacionadasAoFuncionarioAoCentroDeCusto(conta, centroDeCusto, mes)
                               : ObterParcelasDeDespesasRelacionadasAoFuncionario(conta, centroDeCusto, mes);

                if (parcelas.Any())
                {
                    double valor = 0;
                    foreach (var parcela in parcelas.Where(p=> p != null && p.Valor != null))
                    {
                        valor += parcela.Valor;
                    }

                    //var valor = parcelas.AsParallel().Sum(a => a.Valor);

                    despesaConta.Despesas.Add(new DespesaPessoalDTO { Mes = i, Valor = valor });
                }
            }
        }

        //private double ObterOSomatorioTotalDasParcelasDeDespesasRelacionadasAoFuncionario(Conta conta, CentroDeCusto centroDeCusto)
        //{
        //    return (from despesasPessoais in Orcamento.Despesas.Where(d => d.Conta == conta)
        //            join df in centroDeCusto.Funcionarios.Where(f => f.Departamento == Departamento).SelectMany(x => x.Despesas)
        //                on despesasPessoais.Id equals df.Id
        //            from p in despesasPessoais.Parcelas
        //            select p.Valor).Sum();
        //}

        private double ObterOSomatorioTotalDasParcelasDeDespesasNaoRalacionadasAoFuncionario(Conta conta, CentroDeCusto centroDeCusto)
        {
            return (from despesasPessoais in Orcamento.Despesas.Where(d => d.Conta.Id == conta.Id && d.Parcelas != null)
                    from p in despesasPessoais.Parcelas
                    //                    where despesasPessoais.CentroDeCusto.Id == centroDeCusto.Id
                    select p.Valor).Sum();
        }

        private IEnumerable<Parcela> ObterParcelasDeDespesasRelacionadasAoFuncionario(Conta conta, CentroDeCusto centroDeCusto, int i1)
        {
            return from despesasPessoais in Orcamento.Despesas.Where(d => d.Conta.Id == conta.Id && d.Parcelas != null)
                   join df in centroDeCusto.Funcionarios.Where(f => f.Departamento.Id == Departamento.Id).SelectMany(x => x.Despesas)
                       on despesasPessoais.Guid equals df.Guid
                   from p in despesasPessoais.Parcelas
                   where p.Mes == i1
                   select p;
        }

        private IEnumerable<Parcela> ObterParcelasDeDespesasNaoRelacionadasAoFuncionarioAoCentroDeCusto(Conta conta, CentroDeCusto centroDeCusto, int mes)
        {
            return from despesasPessoais in Orcamento.Despesas.Where(d => d.Conta.Id == conta.Id && d.Parcelas != null)
                   from p in despesasPessoais.Parcelas
                   where p.Mes == mes
                   select p;
            //         where p.Mes == mes && despesasPessoais.CentroDeCusto.Id == centroDeCusto.Id

        }

        private void AdicionarZeroAsContasSemDespesa(ContaDespesaPessoalDTO despesaConta)
        {
            var quantidadeDeMesesComDespesa = despesaConta.Despesas.Count;

            if (quantidadeDeMesesComDespesa < 12)
            {
                quantidadeDeMesesComDespesa++;

                for (var i = quantidadeDeMesesComDespesa; i < 13; i++)
                    despesaConta.Despesas.Add(new DespesaPessoalDTO() { Mes = quantidadeDeMesesComDespesa, Valor = 0 });
            }
        }

        private void AdicionarZeroOsTotaisDeOrcamentoComGruposDeContasComContasSemDespesas(OrcamentoPessoalDTO orcamentoPessoalDTO)
        {
            var quantidadeDeMesesComDespesa = orcamentoPessoalDTO.TotaisOrcamentoMensal.Count;

            if (quantidadeDeMesesComDespesa < 12)
            {
                quantidadeDeMesesComDespesa++;

                for (var i = quantidadeDeMesesComDespesa; i < 13; i++)
                    orcamentoPessoalDTO.TotaisOrcamentoMensal.Add(new TotalOrcamentoMensalDTO { Mes = quantidadeDeMesesComDespesa, Valor = 0 });
            }
        }

        private void CalcularTotalizadorGrupoDeConta(GrupoDeContaDTO grupoDeContaDTO)
        {
            for (int i = 1; i < 13; i++)
            {
                int i1 = i;
                var despesas = grupoDeContaDTO.Contas.SelectMany(x => x.Despesas.Where(y => y.Mes == i1));

                var despesaPessoalDtos = despesas as DespesaPessoalDTO[] ?? despesas.ToArray();
                if (despesaPessoalDtos.Any())
                {
                    //double valor = 0;
                    //foreach (var parcela in despesaPessoalDtos)
                    //{
                    //    valor += parcela.Valor;
                    //}
                    var valor = despesaPessoalDtos.AsParallel().Sum(a => a.Valor);

                    grupoDeContaDTO.DespesasGrupoDeConta.Add(new DespesasGrupoDeContaDTO { Mes = i, Valor = valor });
                }
            }

            AdicionarZeroAosGruposComContasSemDespesaNenhuma(grupoDeContaDTO);

            //double valortotal = 0;
            //foreach (var totalmensal in grupoDeContaDTO.DespesasGrupoDeConta)
            //{
            //    valortotal += totalmensal.Valor;
            //}

            var valortotal = grupoDeContaDTO.DespesasGrupoDeConta.AsParallel().Sum(a => a.Valor);

            grupoDeContaDTO.TotalGrupoConta = valortotal;
        }

        private void AdicionarZeroAosGruposComContasSemDespesaNenhuma(GrupoDeContaDTO grupoDeContaDTO)
        {
            var quantidadeDeMesesComDespesa = grupoDeContaDTO.DespesasGrupoDeConta.Count;

            if (quantidadeDeMesesComDespesa < 12)
            {
                quantidadeDeMesesComDespesa++;

                for (var i = quantidadeDeMesesComDespesa; i < 13; i++)
                    grupoDeContaDTO.DespesasGrupoDeConta.Add(new DespesasGrupoDeContaDTO { Mes = i, Valor = 0 });
            }
        }

        private GrupoDeContaDTO ObterGrupoDeContaDTOInicializado(GrupoDeConta grupoDeConta)
        {
            var grupoDeContaDTO = new GrupoDeContaDTO
                                      {
                                          GrupoConta = grupoDeConta.Nome,
                                          Contas = new List<ContaDespesaPessoalDTO>(),
                                          DespesasGrupoDeConta = new List<DespesasGrupoDeContaDTO>()
                                      };

            return grupoDeContaDTO;
        }
    }
}
