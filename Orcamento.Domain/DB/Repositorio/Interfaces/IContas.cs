using System;
using Orcamento.Domain.Gerenciamento;
using System.Collections.Generic;
namespace Orcamento.Domain.DB.Repositorio
{
    public interface IContas
    {
        Conta ObterContaPor(string codigo);
        Conta ObterContaPor(int id);
        void Salvar(Conta conta);
        void Alterar(Conta conta);
        IList<Conta> Todos();
    }
}
