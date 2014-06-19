using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain
{
    [Serializable]
    public class GerenciadorDeOrcamentos
    {
        public virtual bool PodeCriarOrcamento(List<Orcamento> Orcamentos, Departamento setor, CentroDeCusto centroDeCusto, TipoOrcamentoEnum tipo)
        {
            if (Orcamentos == null || Orcamentos.Count == 0)
                return true;

            var orcamentos = Orcamentos.Where(o => o.Tipo == tipo && o.Setor.Id == setor.Id && o.CentroDeCusto.Id == centroDeCusto.Id).ToList();

            return orcamentos == null || orcamentos.Count < 10;
        }

        public virtual bool PodeCriarOrcamento(List<Orcamento> Orcamentos, Departamento setor, TipoOrcamentoEnum tipo)
        {
            if (Orcamentos == null || Orcamentos.Count == 0)
                return true;

            var orcamentos = Orcamentos.Where(o => o.Tipo == tipo && o.Setor.Id == setor.Id).ToList();

            return orcamentos == null || orcamentos.Count < 10;
        }

        public virtual void InformarNomeOrcamento(List<Orcamento> Orcamentos, Orcamento orcamento, Departamento setor,TipoOrcamentoEnum tipo)
        {
            var orcamentos = Orcamentos.Where(o => o.Tipo == tipo && o.Setor.Id == setor.Id).ToList();

            int nomeid = 1;
            foreach (var orcamentoGerencial in orcamentos)
            {
                orcamentoGerencial.NomeOrcamento = "Versão" + (nomeid).ToString();
                nomeid++;
            }
        }

        public virtual void InformarNomeOrcamento(List<Orcamento> Orcamentos, Orcamento orcamento, Departamento setor, CentroDeCusto centroDeCusto, TipoOrcamentoEnum tipo)
        {
            var orcamentos = Orcamentos.Where(o => o.Tipo == tipo && o.Setor.Id == setor.Id && o.CentroDeCusto.Id == centroDeCusto.Id).ToList();

            int nomeid = 1;
            foreach (var orcamentoGerencial in orcamentos)
            {
                orcamentoGerencial.NomeOrcamento ="Versão" + (nomeid).ToString();
                nomeid++;
            }
        }
    }
}
