using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Test.InfraStructure._2014
{
    [TestFixture]
   // [Ignore]
    public class AjustarEstruturaHospitalarcs
    {
    
        [Test]
        // [Ignore]
        public void a_InserirQuimioTerapiaNaOncologia()
        {

            var contas = new ContasHospitalares();
            var setoresHospitalares = new SetoresHospitalares();
            var setorOncologia = setoresHospitalares.Obter("Oncologia");

            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            var quimioTerapia = new SubSetorHospital("Quimioterapia");

            setorOncologia.AdicionarSetor(quimioTerapia);

            setoresHospitalares.Salvar(setorOncologia);

            var insumos = new Insumos();

            foreach (var departamento in todos)
            {
                departamentos.Salvar(departamento);

                var insumo = insumos.ObterInsumo(departamento);
                insumo.CriarCustoUnitarioOncologiaQuimioTerapiaHospitalarPor(setorOncologia);
                insumos.Salvar(insumo);

                foreach (var subsetor in setorOncologia.SubSetores.Where
                (

                s =>
                    s.NomeSetor == "Quimioterapia"
                )
                )
                {
                    var ticket = new TicketDeProducao(setorOncologia, subsetor, departamento);

                    insumos.Salvar(ticket);
                }
            }
        }

        [Test]
        //  [Ignore]
        public void b_InserirQuimioTerapiaParaOsAntigos()
        {
            var setores = new SetoresHospitalares();

            SetorHospitalar oncologia = setores.Obter("Oncologia");

            SubSetorHospital quimioterapia = setores.ObterSub("Quimioterapia");

            var orcamentos = new Orcamentos();
            var departamentosRepositorio = new Departamentos();

            var orcamentosHospitalares = orcamentos.TodosOrcamentosHospitalares();

            foreach (var orcamentoHospitalar in orcamentosHospitalares)
            {
                foreach (var conta in oncologia.Contas)
                {
                    orcamentoHospitalar.CriarServico(conta, quimioterapia, oncologia);
                    orcamentoHospitalar.CriarFatorReceita(quimioterapia, oncologia);

                }
                orcamentos.Salvar(orcamentoHospitalar);
            }
        }

        [Test]
        // [Ignore]
        public void c_InserirCapacidadeFisica()
        {
            //TODO: ao executar este teste retirar o cascade all de SetorHospitalar para conta,
            //Caso contrário, duplica a conta.

            var contas = new ContasHospitalares();
            var setoresHospitalares = new SetoresHospitalares();
            var setorUti = setoresHospitalares.Obter("UTI");
            var setorUNI = setoresHospitalares.Obter("UNI");
            var setorbercario = setoresHospitalares.Obter("Berçário");

            var conta = new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false);
            new ContasHospitalares().Salvar(conta);

            var setores = new List<SetorHospitalar>();
            setorUti.AdicionarConta(conta);
            setorUNI.AdicionarConta(conta);
            setorbercario.AdicionarConta(conta);

            setores.Add(setorUti);
            setores.Add(setorUNI);
            setores.Add(setorbercario);

            setoresHospitalares.Salvar(setorUti);
            setoresHospitalares.Salvar(setorUNI);
            setoresHospitalares.Salvar(setorbercario);


            var orcamentos = new Orcamentos();
            var departamentosRepositorio = new Departamentos();

            var orcamentosHospitalares = orcamentos.TodosOrcamentosHospitalares();
            foreach (var orcamentoHospitalar in orcamentosHospitalares)
            {
                foreach (var setorHospitalar in setores)
                {
                    foreach (var subSetor in setorHospitalar.SubSetores)
                    {
                        orcamentoHospitalar.CriarServico(conta, subSetor, setorHospitalar);
                    }
                }

                orcamentos.Salvar(orcamentoHospitalar);
            }


        }

        [Test]
        // [Ignore]
        public void d_insetir_SubSetoresParaUti()
        {
            var contas = new ContasHospitalares();
            var setoresHospitalares = new SetoresHospitalares();
            var setorUti = setoresHospitalares.Obter("UTI");

            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();


            var neuroIntensiva = new SubSetorHospital("UTI Neuro-Intensiva");
            var utiPosOperatoria = new SubSetorHospital("UTI Pós-Operatória");
            var utiVentilatoria = new SubSetorHospital("UTI Ventilatória");
            var utiHepatico = new SubSetorHospital("UTI Hepático");

            setorUti.AdicionarSetor(neuroIntensiva);
            setorUti.AdicionarSetor(utiPosOperatoria);
            setorUti.AdicionarSetor(utiVentilatoria);
            setorUti.AdicionarSetor(utiHepatico);

            setoresHospitalares.Salvar(setorUti);


            var insumos = new Insumos();

            foreach (var departamento in todos)
            {
                var insumo = insumos.ObterInsumo(departamento);
                insumo.CriarCustoUnitarioUTIHospitalarPor(setorUti);
                insumos.Salvar(insumo);

                foreach (var subsetor in setorUti.SubSetores.Where
                (
                s => s.NomeSetor == "UTI Neuro-Intensiva" ||
                    s.NomeSetor == "UTI Pós-Operatória" ||
                    s.NomeSetor == "UTI Ventilatória" ||
                    s.NomeSetor == "UTI Hepático"
                )
                )
                {
                    var ticket = new TicketDeProducao(setorUti, subsetor, departamento);

                    insumos.Salvar(ticket);
                }
            }


            var orcamentos = new Orcamentos();
            var departamentosRepositorio = new Departamentos();

            var orcamentosHospitalares = orcamentos.TodosOrcamentosHospitalares();
            foreach (var orcamentoHospitalar in orcamentosHospitalares)
            {

                foreach (var subSetor in setorUti.SubSetores.Where
                (
                s => s.NomeSetor == "UTI Neuro-Intensiva" ||
                    s.NomeSetor == "UTI Pós-Operatória" ||
                    s.NomeSetor == "UTI Ventilatória" ||
                    s.NomeSetor == "UTI Hepático"
                ))
                {
                    foreach (var contaHospitalar in setorUti.Contas)
                    {
                        orcamentoHospitalar.CriarServico(contaHospitalar, subSetor, setorUti);
                    }

                    orcamentoHospitalar.CriarFatorReceita(subSetor, setorUti);
                }


                orcamentos.Salvar(orcamentoHospitalar);
            }

        }

        [Test]
        //   [Ignore]
        public void f_insetir_setor_Suporte()
        {
            ContasHospitalares contas = new ContasHospitalares();

            var outrasReceitas = new ContaHospital("OutrasReceitas", TipoValorContaEnum.Quantidade);
            new Contas().Salvar(outrasReceitas);

            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            var suporte = new SetorHospitalar("Suporte");
            var subSuporte = new SubSetorHospital("Suporte");

            suporte.AdicionarSetor(subSuporte);
            suporte.AdicionarConta(outrasReceitas);

            var setores = new SetoresHospitalares();
            setores.Salvar(suporte);

            Insumos insumos = new Insumos();

            foreach (var departamento in todos)
            {
                //TODO: colocar cascade all para setores hositalares.
                departamento.AdicionarSetor(suporte);
                departamentos.Salvar(departamento);

                var insumo = insumos.ObterInsumo(departamento);
                insumo.CriarCustoUnitarioHospitalarPor(suporte);
                insumos.Salvar(insumo);

                var ticket = new TicketDeProducao(suporte, subSuporte, departamento);
                insumos.Salvar(ticket);

            }

            var orcamentos = new Orcamentos();

            var orcamentosHospitalares = orcamentos.TodosOrcamentosHospitalares();
            foreach (var orcamentoHospitalar in orcamentosHospitalares)
            {
                orcamentoHospitalar.CriarServico(outrasReceitas, subSuporte, suporte);
                orcamentoHospitalar.CriarFatorReceita(subSuporte, suporte);
                orcamentos.Salvar(orcamentoHospitalar);
            }

        }
    }
}
