using System;
namespace Orcamento.Domain.DB.Repositorio
{
    public interface IHospitais
    {
        void Adicionar(Hospital hospital);
        Hospital ObterPor(string nome);
        Hospital ObterPor(int id);
        System.Collections.Generic.IList<Hospital> Todos();
    }
}
