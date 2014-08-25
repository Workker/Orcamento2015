using System;

namespace Orcamento.Domain.Gerenciamento
{
    public class FabricaDeDepartamento
    {
        public static Departamento Construir(TipoDepartamento tipoDepartamento, string nome)
        {
            switch (tipoDepartamento)
            {
                case TipoDepartamento.hospital:
                    return new Hospital(nome);
                case TipoDepartamento.setor:
                    return new Setor(nome);
            }

            throw new Exception("Erro ao criar um novo departamento.");
        }
    }
}