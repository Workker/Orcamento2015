using Orcamento.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.SelfHostingServices
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the base address for the service.
            Uri baseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/Orcamento.WS/ServicoProcessaCargaDeOrcamento/");

            // Create a ServiceHost for the CalculatorService type and provide the base address.
            using (ServiceHost serviceHost = new ServiceHost(typeof(ServicoProcessaCargaDeOrcamento), baseAddress))
            {
                // Add an endpoint using the IOneWayCalculator contract and the WSHttpBinding
                serviceHost.AddServiceEndpoint(typeof(IServicoProcessaCargaDeOrcamento), new WSHttpBinding(), "");

                // Turn on the metadata behavior, this allows svcutil to get metadata for the service.
                ServiceMetadataBehavior smb = (ServiceMetadataBehavior)serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (smb == null)
                {
                    smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    serviceHost.Description.Behaviors.Add(smb);
                }

                // Open the ServiceHostBase to create listeners and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
