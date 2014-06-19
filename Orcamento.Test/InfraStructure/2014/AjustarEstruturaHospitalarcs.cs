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
    [Ignore]
    public class AjustarEstruturaHospitalarcs
    {
        [Test]
        [Ignore]
        public void insetir_setor_Semi()
        {
            ContasHospitalares contas = new ContasHospitalares();

            var leito = new ContaHospital("Capacidade Operacional", TipoValorContaEnum.Quantidade);
            var ocupacao = new ContaHospital("Taxa de Ocupação", TipoValorContaEnum.Porcentagem);
            var capacidadeFisica = new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false);
            leito.MultiPlicaPorMes = true;

            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            var semi = new SetorHospitalar("Semi");
            var subSemiIntensiva = new SubSetorHospital("Uti Semi-Intensiva");

            semi.AdicionarSetor(subSemiIntensiva);
            semi.AdicionarConta(leito);
            semi.AdicionarConta(ocupacao);
            semi.AdicionarConta(capacidadeFisica);

            var setores = new SetoresHospitalares();
            setores.Salvar(semi);

            Insumos insumos = new Insumos();

            foreach (var departamento in todos)
            {
                departamento.AdicionarSetor(semi);
                departamentos.Salvar(departamento);

                var insumo = insumos.ObterInsumo(departamento);
                insumo.CriarCustoUnitarioHospitalarPor(semi);
                insumos.Salvar(insumo);

                foreach (var subsetor in semi.SubSetores)
                {
                    var ticket = new TicketDeProducao(semi, subsetor, departamento);

                    insumos.Salvar(ticket);
                }
            }

        }
        [Test]
        [Ignore]
        public void InserirQuimioTerapiaNaOncologia()
        {

            var contas = new ContasHospitalares();
            var setoresHospitalares = new SetoresHospitalares();
            var setorOncologia = setoresHospitalares.Obter<SetorHospitalar>(8);

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
        [Ignore]
        public void InserirCapacidadeFisica()
        {
            var contas = new ContasHospitalares();
            var setoresHospitalares = new SetoresHospitalares();
            var setorUti = setoresHospitalares.Obter<SetorHospitalar>(3);
            var setorUNI = setoresHospitalares.Obter<SetorHospitalar>(4);
            var setorUNI2 = setoresHospitalares.Obter<SetorHospitalar>(13);
            var setorUNI3 = setoresHospitalares.Obter<SetorHospitalar>(22);
            var setorUNI4 = setoresHospitalares.Obter<SetorHospitalar>(31);
            var setorbercario1 = setoresHospitalares.Obter<SetorHospitalar>(9);
            var setorbercario2 = setoresHospitalares.Obter<SetorHospitalar>(18);
            var setorbercario3 = setoresHospitalares.Obter<SetorHospitalar>(27);
            var setorbercario4 = setoresHospitalares.Obter<SetorHospitalar>(36);

            setorUti.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorUNI.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorUNI2.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorUNI3.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorUNI4.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorbercario1.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorbercario2.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorbercario3.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));
            setorbercario4.AdicionarConta(new ContaHospital("Capacidade Fisica", TipoValorContaEnum.Quantidade, false, false));

            setoresHospitalares.Salvar(setorUti);
            setoresHospitalares.Salvar(setorUNI);
            setoresHospitalares.Salvar(setorUNI2);
            setoresHospitalares.Salvar(setorUNI3);
            setoresHospitalares.Salvar(setorUNI4);
            setoresHospitalares.Salvar(setorbercario1);
            setoresHospitalares.Salvar(setorbercario2);
            setoresHospitalares.Salvar(setorbercario3);
            setoresHospitalares.Salvar(setorbercario4);
        }

        [Test]
        [Ignore]
        public void r_insetir_SubSetoresParaUti()
        {
            var contas = new ContasHospitalares();
            var setoresHospitalares = new SetoresHospitalares();
            var setorUti = setoresHospitalares.Obter<SetorHospitalar>(3);

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
                departamentos.Salvar(departamento);

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
        }

        [Test]
        [Ignore]
        public void inserir_ticket_de_producao_nos_novos_sub_setores()
        {
            var insumos = new Insumos();
            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            foreach (var departamento in todos)
            {
                departamentos.Salvar(departamento);

                var insumo = insumos.ObterInsumo(departamento);
                var setor = departamento.Setores.Single(s => s.NomeSetor == "UTI");
                insumo.CriarCustoUnitarioHospitalarPor(setor);
                insumos.Salvar(insumo);

                foreach (var subsetor in setor.SubSetores.Where
                (
                s => s.NomeSetor == "UTI Neuro-Intensiva" ||
                    s.NomeSetor == "UTI Pós-Operatória" ||
                    s.NomeSetor == "UTI Ventilatória" ||
                    s.NomeSetor == "UTI Hepático"
                )
                )
                {
                    var ticket = new TicketDeProducao(setor, subsetor, departamento);

                    insumos.Salvar(ticket);
                }
            }
        }

        [Test]
        [Ignore]
        public void insetir_setor_Suporte()
        {
            ContasHospitalares contas = new ContasHospitalares();

            var outrasReceitas = new ContaHospital("OutrasReceitas", TipoValorContaEnum.Quantidade);

            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            var suporte = new SetorHospitalar("Maternidade");
            var subSuporte = new SubSetorHospital("Maternidade");

            suporte.AdicionarSetor(subSuporte);
            suporte.AdicionarConta(outrasReceitas);

            var setores = new SetoresHospitalares();
            setores.Salvar(suporte);

            Insumos insumos = new Insumos();

            foreach (var departamento in todos)
            {
                departamento.AdicionarSetor(suporte);
                departamentos.Salvar(departamento);

                var insumo = insumos.ObterInsumo(departamento);
                insumo.CriarCustoUnitarioHospitalarPor(suporte);
                insumos.Salvar(insumo);

                foreach (var subsetor in suporte.SubSetores)
                {
                    var ticket = new TicketDeProducao(suporte, subsetor, departamento);

                    insumos.Salvar(ticket);
                }
            }

        }
    }
}
