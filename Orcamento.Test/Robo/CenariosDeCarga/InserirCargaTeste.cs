using NUnit.Framework;
using Orcamento.Controller.Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Robo
{
    [TestFixture]
    public class InserirCargaTeste
    {
        readonly CargaController _controller = new CargaController();

        [Test]
        public void Insert()
        {
            var detalhes = new List<Detalhe>();

            var detalhe1 = new Detalhe
                {
                    Id = Guid.NewGuid(),
                    Nome = "XXXX",
                    Descricao = "Null",
                    Linha = 1500
                };

            var detalhe2 = new Detalhe
            {
                Id = Guid.NewGuid(),
                Nome = "BBBB",
                Descricao = "Null",
                Linha = 1300
            };


            var detalhe3 = new Detalhe
            {
                Id = Guid.NewGuid(),
                Nome = "AAAA",
                Descricao = "Null",
                Linha = 1254
            };

            detalhes.Add(detalhe1);
            detalhes.Add(detalhe2);
            detalhes.Add(detalhe3);

            var usuario = new Usuario() { Nome = "Gerson", Id = 24, Login = "gerson" };


            var carga = new Carga
                            {
                                Id = Guid.NewGuid(),
                                NomeArquivo = "FuncionarioCorporativo",
                                DataCriacao = DateTime.Now,
                                Status = "Erro no Processamento",
                                Diretorio = @"D:\ProjectsWorkker\Orcamento2015\Orcamento.Robo.Web",
                                Usuario = usuario,
                                Detalhes = detalhes
                            };

            _controller.Salvar(carga);

        }
    }
}
