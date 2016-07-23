
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.DB.Repositorio.Robo;
using Orcamento.Domain.Robo.Fabricas;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using FakeItEasy;

namespace Orcamento.TestMethod.Robo.EstrategiaDeCargas
{
    [TestClass]
    public class carga_ticket_de_producao_cenario__inserir_dois_sub_setores_em_um_setor_com_um_hospital
    {
        public Carga Carga { get; set; }

       // [SetUp]
        public void iniciar()
        {
           // Carga = A.Fake<Carga>();

            Carga = new Carga(FabricaDeImportacao.Criar(TipoEstrategiaDeCargaEnum.TicketsDeProducao), TipoEstrategiaDeCargaEnum.TicketsDeProducao, "inserir_dois_sub_setores_em_um_setor_com_um_hospital", "d://casosdeTestMethodes//inserir_dois_sub_setores_em_um_setor_com_um_hospital.xls", false);
            Carga.Processa();
        }

        [TestMethod]
        public  void a_validar_valores()
        {
            Assert.IsNotNull(Carga);
        }

        //[TearDown]
        public void finalizar()
        {
            //
        }
    }
}
