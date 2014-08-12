using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace Orcamento.WS
{
    [ServiceContract]
    public interface IServicoProcessaCargaDeOrcamento
    {
        [OperationContract(IsOneWay = true)]
        void ProcessarCargaDeOrcamento();
    }
}
