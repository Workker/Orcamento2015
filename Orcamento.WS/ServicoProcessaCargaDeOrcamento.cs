using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace Orcamento.WS
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ServicoProcessaCargaDeOrcamento : IServicoProcessaCargaDeOrcamento
    {
        public void ProcessarCargaDeOrcamento(Guid id)
        {
            Thread.Sleep(5000);
        }
    }
}
