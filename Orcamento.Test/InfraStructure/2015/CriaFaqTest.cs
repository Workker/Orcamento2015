using NUnit.Framework;
using Orcamento.Domain.DB.Repositorio.Robo;
using Orcamento.Domain.Robo.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Test.InfraStructure._2015
{
    [TestFixture]
    public class CriaFaqTest 
    {
        Faqs faqs = new Faqs();


        [Test]
        public  void InicializarFaq()
        {


            //TipoFaq funcionario = new TipoFaq((int)TipoFaqEnum.CargadeFuncionarios, "Carga de funcionários");
            //TipoFaq geral = new TipoFaq((int)TipoFaqEnum.Geral, "Faq Geral");

            //TipoFaq cargaUsuario = new TipoFaq();
            //cargaUsuario.Id = (int) TipoFaqEnum.CargaDeUsuarios;

            //TipoFaq ticketDeProducao = new TipoFaq();
            //ticketDeProducao.Id = (int) TipoFaqEnum.CargaDeTicketDeProducao;

            //TipoFaq cargaEstruturaOrcamentaria = new TipoFaq();
            //cargaEstruturaOrcamentaria.Id = (int) TipoFaqEnum.CargaDeEstruturaOrcamentaria;



            //faqs.Salvar(cargaEstruturaOrcamentaria);
            //faqs.Salvar(ticketDeProducao);
            //faqs.Salvar(cargaUsuario);
           var funcionario = faqs.Obter<TipoFaq>((int)TipoFaqEnum.CargadeFuncionarios);
           var geral = faqs.Obter<TipoFaq>((int)TipoFaqEnum.Geral);

            criarFaqDeFuncionarios(funcionario);
            criarFaqGeral(geral);

            

        }


        private  void criarFaqDeFuncionarios(TipoFaq tipo)
        {
            Faq faq = new Faq();
            faq.Nome = "Carga de Funcionários";
            faq.Perguntas = new List<Pergunta>();
            faq.Perguntas.Add(new Pergunta(){Nome = "Campos obritatótios.",Resposta = "Nome, Cargo, Salário, Ano, Mês de admissão."});
            faq.TipoFaq = tipo;

            faqs.Salvar(faq);
        }

        private  void criarFaqGeral(TipoFaq tipo)
        {
            Faq faq = new Faq();
            faq.Nome = "Cargas";
            faq.Perguntas = new List<Pergunta>();
            faq.Perguntas.Add(new Pergunta(){Nome = "Erros de validação",Resposta = "Erro de validação acontece quando alguma regra da carga foi quebrada. Pode ser um campo vazio, mal formatado, uma amarração incorreta, N motivos estão associados aos erros de validação. Verifique sempre as regras de cada carga."});
            faq.Perguntas.Add(new Pergunta(){Nome = "Erros de Processp",Resposta = "Erros de processo podem ser erros ligados a alterações recentes no banco de dados, entre o momento da validação da carga e a execução. Algumas vezes pode acontecer por erros de codificação, se o erro persistir contacte o suporte."});
            faq.TipoFaq = tipo;

            faqs.Salvar(faq);
        }
    }
}
