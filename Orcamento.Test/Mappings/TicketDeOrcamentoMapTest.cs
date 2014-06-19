using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Testing;
using NUnit.Framework;
using Orcamento.Domain;

namespace Orcamento.Test.Mappings
{
    [TestFixture]
    public class TicketDeOrcamentoMapTest : PersistenceTestBase
    {
        [Test]
        public void testar_mapeamentos_do_ticke_de_orcamento()
        {
            new PersistenceSpecification<TicketDeOrcamentoPessoal>(session: Session)
                .CheckProperty(x => x.Id, 1)
                .CheckProperty(x => x.Descricao, "Ticket 1")
                .VerifyTheMappings();
        }
    }
}
