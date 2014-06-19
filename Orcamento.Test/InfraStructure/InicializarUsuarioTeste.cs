using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;

namespace Orcamento.Test.InfraStructure
{
    [TestFixture]
    [Ignore]
    public class InicializarUsuarioTeste
    {
        [Test]
        public void inicializar_usuario_de_teste()
        {
            Usuario usuario = new Usuario() { Nome = "Isaac" };

            Departamentos repositorioDepartamentos = new Departamentos();

            var departamentos = repositorioDepartamentos .Todos<Departamento>();

            foreach (var departamento in departamentos)
            {
                usuario.ParticiparDe(departamento);
            }

            Usuarios usuarios = new Usuarios();

            usuarios.Salvar(usuario);
        }
    }
}
