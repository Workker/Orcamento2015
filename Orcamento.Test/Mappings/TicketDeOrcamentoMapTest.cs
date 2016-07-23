using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibernate.Testing;

namespace Orcamento.TestMethod.Mappings
{
    [TestClass]
    public class TicketDeOrcamentoMapTestMethod : PersistenceTestMethodBase
    {
        [TestMethod]
        public void TestMethodar_mapeamentos_do_ticke_de_orcamento()
        {
            new PersistenceSpecification<TicketDeOrcamentoPessoal>(session: Session)
                .CheckProperty(x => x.Id, 1)
                .CheckProperty(x => x.Descricao, "Ticket 1")
                .VerifyTheMappings();
        }
    }
}
