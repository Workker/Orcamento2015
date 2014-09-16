using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orcamento.API;
using Orcamento.API.Controllers;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.API.Controller;

namespace Orcamento.API.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            
            CargasController controller = new CargasController();


            IEnumerable<Carga> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            CargasController controller = new CargasController();

            // Act
            Carga result = controller.Get(new Guid("67f2bead-fb87-48b3-b3e8-bd1610e15a5a"));

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            CargasController controller = new CargasController();

            // Act
            controller.Post(new Carga());

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            CargasController controller = new CargasController();

            // Act
            controller.Put(5, new Carga());

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            CargasController controller = new CargasController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
