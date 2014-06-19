using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    public class Cidade : ValueObject
    {
        public Cidade( string descricao)
        {
            base.Descricao = descricao;
        }

        public Cidade() { }

        public override bool Equals(object obj)
        {
            Cidade outraCidade = (Cidade) obj;

            return outraCidade.Descricao == this.Descricao;
        }


    }
}
