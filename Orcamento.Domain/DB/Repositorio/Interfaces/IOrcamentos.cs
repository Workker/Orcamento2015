using System.Collections.Generic;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface IOrcamentos
    {
        void AtualizarDespesasOperacionais(List<Despesa> despesas);
        List<Orcamento> TodosOrcamentosDeViagemPor(CentroDeCusto centroDeCusto, Departamento setor, int id);
        List<Orcamento> TodosOrcamentosOperacionaisPor(CentroDeCusto centroDeCusto, Departamento setor, int id);
        List<DRE> ObterDRE(Departamento departamento);
        List<Orcamento> TodosPor(Departamento setor);
        Orcamento ObterOrcamentoFinalOrcamentoDeViagem(CentroDeCusto centroDeCusto);
        Orcamento ObterOrcamentoFinalOrcamentoOperacional(CentroDeCusto centroDeCusto, Departamento setor);
        List<Orcamento> TodosOrcamentosHospitalares(Departamento departamento);
        Orcamento ObterOrcamentoHospitalarFinal(Departamento hospital);
        List<Orcamento> TodosOrcamentosDeViagemPor(CentroDeCusto centroDeCusto, Departamento setor);
        List<Orcamento> TodosOrcamentosOperacionaisPor(CentroDeCusto centroDeCusto, Departamento setor);
        List<Orcamento> TodosOrcamentosDeViagem(Departamento departamento);
        void Salvar(IAggregateRoot<int> root);
        void Refresh(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }
}