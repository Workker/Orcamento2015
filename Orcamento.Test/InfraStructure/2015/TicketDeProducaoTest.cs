
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.TestMethod.InfraStructure._2015
{
    [TestClass]
    public class TicketDeProducaoTestMethod
    {

        [TestMethod]
        public void InserirTicketDeProducao()
        {
            var novo = new Departamentos().ObterPor("COPA");

            TicketsDeProducao tickets = new TicketsDeProducao();
            foreach (var setor in novo.Setores)
            {
                foreach (var subSetor in setor.SubSetores)
                {
                    var ticket = new TicketDeProducao(setor, subSetor, (Hospital)novo);
                    tickets.Salvar(ticket);
                }
            }
        }
    }
}
