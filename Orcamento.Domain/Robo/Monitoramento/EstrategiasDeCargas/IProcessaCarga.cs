using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Entities.Monitoramento
{
    public interface IProcessaCarga
    {
        void Processar(Carga carga);
     
    }
}
