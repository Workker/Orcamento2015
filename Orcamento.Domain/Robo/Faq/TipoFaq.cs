using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Faq
{
    public enum TipoFaqEnum : short
    {
        Geral = 1,
        CargadeFuncionarios = 2
        
        //CargaDeTicketDeProducao = 2,
        //CargaDeEstruturaOrcamentaria = 3,
        //CargaDeUsuarios = 4,
        
    }

    public class TipoFaq : ValueObject
    {
        public TipoFaq()
        {
        }

        public TipoFaq(int id, string descricao):base(id,descricao)
       {
           
       }
    }
}
