using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Util;
using Orcamento.Domain.ComponentesDeOrcamento;

namespace Orcamento.Domain.Servico.Hospitalar
{
    public class ServicoSalvarDepartamento : IServicoSalvarDepartamento
    {
        public void Salvar(Departamento departamento)
        {

            Departamentos departamentos = new Departamentos();

            if (departamento.Id == 0)
            {
                VerificarTipo.Verifique(departamento,
                    VerificarTipo.Caso<Hospital>(c => InserirInformacoesHospitalares(c)),
                    VerificarTipo.Caso<Setor>(s => departamentos.Salvar(departamento)));


                departamentos.Salvar(departamento);

                InserirAcordoConvencao(departamento);
                InserirTicketsDePessoal(departamento);
               // AtribuirPermissaoParaADM(departamento);
            }
            departamentos.Salvar(departamento);
        }

        private void AtribuirPermissaoParaADM(Departamento departamento)
        {
            TipoUsuarios tipoUsuarios = new TipoUsuarios();


            Departamentos repositorioDepartamentos = new Departamentos();

            Usuarios usuarios = new Usuarios();
            var todos = usuarios.TodosPor(tipoUsuarios.Obter<TipoUsuario>(1));

            var departamentos = repositorioDepartamentos.Todos<Setor>();

            foreach (var usuario in todos)
            {
                usuario.ParticiparDe(departamento);

                usuarios.Salvar(usuario);
            }
        }

        private void InserirInformacoesHospitalares(Departamento departamento)
        {
            //InserirSetoresESubSetores(departamento);
            InserirSetores(departamento);
            InserirInsumo(departamento);
            InserirTicketDeProducao(departamento);
            InsetirTicketDeReceita(departamento);
            InserirDRE2012(departamento);
        }

        private void InserirSetores(Departamento departamento)
        {
            Departamentos repositorio = new Departamentos();
            var barraDor = repositorio.Obter(1);
            foreach (var setor in barraDor.Setores)
            {
                departamento.AdicionarSetor(setor);
            }

            repositorio.Salvar(departamento);
        }

        private void InserirDRE2012(Departamento departamento)
        {
            DRETotal dre = new DRETotal(departamento);
            DRES dres = new DRES();
            dres.Salvar(dre);
        }

        private void InsetirTicketDeReceita(Departamento departamento)
        {
            Departamentos departamentos = new Departamentos();
            List<TicketDeReceita> ticketsDeReceita = new List<TicketDeReceita>();
            ticketsDeReceita.Add(new TicketDeReceita(departamento, "Glosa Interna", TipoTicketDeReceita.GlosaInterna));
            ticketsDeReceita.Add(new TicketDeReceita(departamento, "Glosa Externa", TipoTicketDeReceita.GlosaExterna));
            ticketsDeReceita.Add(new TicketDeReceita(departamento, "Reajuste de Convênios", TipoTicketDeReceita.ReajusteDeConvenios));
            ticketsDeReceita.Add(new TicketDeReceita(departamento, "Impostos", TipoTicketDeReceita.Impostos));
            ticketsDeReceita.Add(new TicketDeReceita(departamento, "Reajustes de Insumos", TipoTicketDeReceita.ReajusteDeInsumos));
            ticketsDeReceita.Add(new TicketDeReceita(departamento, "Descontos Obtidos", TipoTicketDeReceita.Descontos));

            departamentos.SalvarLista(ticketsDeReceita);
        }

        public void InserirTicketsDePessoal(Departamento departamento)
        {
            TicketsDeProducao tickets = new TicketsDeProducao();
            NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();
            GruposDeConta grupos = new GruposDeConta();

            var encargosSociais = grupos.ObterPor("Encargos Sociais");
            var remuneracao = grupos.ObterPor("Remuneração");
            var beneficios = grupos.ObterPor("Benefícios");



            var ticketDeAlimentacao = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.Alimentação, Descricao = "Alimentação", Valor = 300 };
            var ticketDeAssistenciaMedica = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AssistenciaMedica, Descricao = "Assistência Médica", Valor = 300 };
            var ticketAssistencia = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AssistenciaOdontologica, Descricao = "Assistência Odontológica", Valor = 50 };
            var ticketDeBeneficios = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.OutrosBeneficios, Descricao = "Outros Benefícios", Valor = 50 };
            var ticketTreinamentoPessoal = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.TreinamentoPessoal, Descricao = "Treinamento Pessoal", Valor = 50 };
            var ticketValeTransporte = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.ValeDeTransporte, Descricao = "Vale de Transporte", Valor = 150 };
            var ticketDeOutrasDespesas = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.OutrasDespesas, Descricao = "Outras Despesas", Valor = 50 };

            var adicionalNoturno = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionalNoturno, Descricao = "Adicional Noturno", Valor = 4 };
            var insalubridade = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionalDeInsalubridade, Descricao = "Adicional de Insalubridade", Valor = 10 };
            var periculosidade = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionaDePericulosidade, Descricao = "Adicional de Periculosidade", Valor = 1 };
            var gratificacoes = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.Gratificacoes, Descricao = "Gratificações", Valor = 1 };
            var horasExtras = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.HorasExtras, Descricao = "Horas Extras", Valor = 2 };
            var sobreaviso = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionalDeSobreaviso, Descricao = "Adicional de Sobreaviso", Valor = 0 };
            var indenizacao = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.Indenizacao, Descricao = "Indenização", Valor = 235 };
            var bolsaDeEstagio = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.BolsaDeEstagio, Descricao = "Bolsa de Estágio", Valor = 0 };
            var fgts = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "FGTS", Valor = 8 };
            var inss = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.INSS, Descricao = "INSS", Valor = 28 };

            foreach (var centroDeCusto in departamento.CentrosDeCusto)
            {
                if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == beneficios.Id))
                    centroDeCusto.Adicionar(beneficios);

                if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == remuneracao.Id))
                    centroDeCusto.Adicionar(remuneracao);

                if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == encargosSociais.Id))
                    centroDeCusto.Adicionar(encargosSociais);
            }

            tickets.Salvar(ticketDeAlimentacao);
            tickets.Salvar(ticketDeAssistenciaMedica);
            tickets.Salvar(ticketAssistencia);
            tickets.Salvar(ticketDeBeneficios);
            tickets.Salvar(ticketTreinamentoPessoal);
            tickets.Salvar(ticketValeTransporte);
            tickets.Salvar(ticketDeOutrasDespesas);

            tickets.Salvar(adicionalNoturno);
            tickets.Salvar(insalubridade);
            tickets.Salvar(periculosidade);
            tickets.Salvar(gratificacoes);
            tickets.Salvar(horasExtras);
            tickets.Salvar(sobreaviso);
            tickets.Salvar(indenizacao);
            tickets.Salvar(bolsaDeEstagio);
            tickets.Salvar(fgts);
            tickets.Salvar(inss);
        }

        private void InserirAcordoConvencao(Departamento departamento)
        {
            AcordosDeConvencao acordos = new AcordosDeConvencao();
            AcordoDeConvencao acordo = new AcordoDeConvencao(departamento, 0, 0);

            acordos.Salvar(acordo);
        }

        public void InserirSetoresESubSetores(Departamento departamento)
        {
            Orcamentos orcamentos = new Orcamentos();

            var setorHemodinamica = new SetorHospitalar("Hemodinâmica");
            var subSetorHemodinamica = new SubSetorHospital("Hemodinâmica");
            setorHemodinamica.AdicionarSetor(subSetorHemodinamica);

            var setorOncologia = new SetorHospitalar("Oncologia");
            var subSetorOncologia = new SubSetorHospital("Oncologia");
            setorOncologia.AdicionarSetor(subSetorOncologia);

            var procedimento = new ContaHospital("Procedimendo", TipoValorContaEnum.Quantidade);

            setorHemodinamica.AdicionarConta(procedimento);
            setorOncologia.AdicionarConta(procedimento);

            var centroCirurgico = new SetorHospitalar("Centro Cirúrgico");
            var centroCirurgicoSubSetor = new SubSetorHospital("Centro Cirúrgico");
            var centroCirurgicoObstetrico = new SubSetorHospital("Centro Obstétrico");
            centroCirurgico.AdicionarSetor(centroCirurgicoSubSetor);
            centroCirurgico.AdicionarSetor(centroCirurgicoObstetrico);

            //Contas ta cirurgia
            var cirurgia = new ContaHospital("Cirurgias", TipoValorContaEnum.Quantidade);
            var salas = new ContaHospital("Salas", TipoValorContaEnum.Quantidade, false, true);
            var cirurgiaPorSala = new ContaHospital("Cirurgias por Sala", TipoValorContaEnum.Quantidade, true, false);

            centroCirurgico.AdicionarConta(cirurgia);
            centroCirurgico.AdicionarConta(salas);
            centroCirurgico.AdicionarConta(cirurgiaPorSala);

            cirurgiaPorSala.AnexarConta(cirurgia);
            cirurgiaPorSala.AnexarConta(salas);

            //UTI
            var uti = new SetorHospitalar("UTI");
            var utiSemiMaternidade = new SubSetorHospital("UTI Semi Maternidade");
            var utiAdulto = new SubSetorHospital("UTI Adulto");
            var utiPediatrica = new SubSetorHospital("Uti Pediátrica");
            var utiNeoNatal = new SubSetorHospital("Uti Neo-Natal");
            var utiCoronariana = new SubSetorHospital("Uti Coronariana");
            var semiIntensiva = new SubSetorHospital("Uti Semi-Intensiva");

            //Contas da UTI
            var leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            var ocupacao = new ContaHospital("Taxa de Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;
            uti.AdicionarConta(leito);
            uti.AdicionarConta(ocupacao);

            //SubSetores da UTI
            uti.AdicionarSetor(utiSemiMaternidade);
            uti.AdicionarSetor(utiAdulto);
            uti.AdicionarSetor(utiPediatrica);
            uti.AdicionarSetor(utiNeoNatal);
            uti.AdicionarSetor(utiCoronariana);
            uti.AdicionarSetor(semiIntensiva);

            //UNI
            var uni = new SetorHospitalar("UNI");
            var uniAdulto = new SubSetorHospital("Uni Adulto");
            var uniPediatrica = new SubSetorHospital("Uni Pediátrica");
            var uniMaternidade = new SubSetorHospital("Maternidade");

            uni.AdicionarSetor(uniAdulto);
            uni.AdicionarSetor(uniPediatrica);
            uni.AdicionarSetor(uniMaternidade);

            //Contas UNI
            uni.AdicionarConta(leito);
            uni.AdicionarConta(ocupacao);

            //Conta Atendimento
            var atendimento = new ContaHospital("Atendimento", TipoValorContaEnum.Quantidade);

            //Emergencia
            var emergencia = new SetorHospitalar("Emergência");
            var subEmergenciaMaternidade = new SubSetorHospital("Emergência Maternidade");
            var subEmergenciaAdulto = new SubSetorHospital("Emergência Adulto");
            var subEmergenciaPediatrica = new SubSetorHospital("Emergência Pediátrica");
            emergencia.AdicionarSetor(subEmergenciaMaternidade);
            emergencia.AdicionarSetor(subEmergenciaAdulto);
            emergencia.AdicionarSetor(subEmergenciaPediatrica);
            emergencia.AdicionarConta(atendimento);

            //Ambulatorio
            var ambulatorio = new SetorHospitalar("Ambulatório");
            var subAmbulatorio = new SubSetorHospital("Ambulatório");
            ambulatorio.AdicionarSetor(subAmbulatorio);
            ambulatorio.AdicionarConta(atendimento);

            //SADT
            var sadt = new SetorHospitalar("SADT");
            var cardiologico = new SubSetorHospital("Cardiológico");
            var resonanciaMagnetica = new SubSetorHospital("Resonância Mag");
            var ultrassonografica = new SubSetorHospital("Ultrassonografia");
            var tomografiaCompleta = new SubSetorHospital("Tomografia Comp");
            var radiologia = new SubSetorHospital("Radiologia");
            var patologiaClinica = new SubSetorHospital("Patologia Clínica");
            var outros = new SubSetorHospital("Outros");
            var exames = new ContaHospital("Exames", TipoValorContaEnum.Quantidade);

            sadt.AdicionarSetor(cardiologico);
            sadt.AdicionarSetor(resonanciaMagnetica);
            sadt.AdicionarSetor(ultrassonografica);
            sadt.AdicionarSetor(tomografiaCompleta);
            sadt.AdicionarSetor(radiologia);
            sadt.AdicionarSetor(patologiaClinica);
            sadt.AdicionarSetor(outros);
            sadt.AdicionarConta(exames);

            var bercario = new SetorHospitalar("Berçário");
            var bercarioAltoRisco = new SubSetorHospital("Berçário Alto Risco");
            var bercarioSemiIntensiva = new SubSetorHospital("Berçário Semi-intensiva");

            bercario.AdicionarSetor(bercarioAltoRisco);
            bercario.AdicionarSetor(bercarioSemiIntensiva);

            bercario.AdicionarConta(ocupacao);
            bercario.AdicionarConta(leito);

            departamento.AdicionarSetor(sadt);
            departamento.AdicionarSetor(centroCirurgico);
            departamento.AdicionarSetor(uti);
            departamento.AdicionarSetor(uni);
            departamento.AdicionarSetor(emergencia);
            departamento.AdicionarSetor(ambulatorio);
            departamento.AdicionarSetor(setorHemodinamica);
            departamento.AdicionarSetor(setorOncologia);
            departamento.AdicionarSetor(bercario);



            Departamentos repositorio = new Departamentos();

            repositorio.Salvar(sadt);
            repositorio.Salvar(centroCirurgico);
            repositorio.Salvar(uti);
            repositorio.Salvar(uni);
            repositorio.Salvar(emergencia);
            repositorio.Salvar(ambulatorio);
            repositorio.Salvar(setorHemodinamica);
            repositorio.Salvar(setorOncologia);
            repositorio.Salvar(bercario);
            repositorio.Salvar(departamento);
        }

        public void InserirInsumo(Departamento departamento)
        {
            Insumos insumos = new Insumos();
            var insumo = new Insumo(departamento);
            insumos.Salvar(insumo);

        }

        public void InserirTicketDeProducao(Departamento departamento)
        {
            TicketsDeProducao tickets = new TicketsDeProducao();
            foreach (var setor in departamento.Setores)
            {
                foreach (var subSetor in setor.SubSetores)
                {
                    var ticket = new TicketDeProducao(setor, subSetor, (Hospital)departamento);
                    tickets.Salvar(ticket);
                }
            }
        }
    }
}
