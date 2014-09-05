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
                InicializarModuloDePessoal();
                InserirTicketsDePessoal(departamento);
                AtribuirPermissaoParaADM(departamento);
            }
            departamentos.Salvar(departamento);
        }

        private void AtribuirPermissaoParaADM(Departamento departamento)
        {
            TipoUsuarios tipoUsuarios = new TipoUsuarios();


            Departamentos repositorioDepartamentos = new Departamentos();

            Usuarios usuarios = new Usuarios();
            var todos = usuarios.TodosPor(tipoUsuarios.Obter<TipoUsuario>((int)TipoUsuarioEnum.Administrador));

            var departamentos = repositorioDepartamentos.Todos<Departamento>();

            foreach (var usuario in todos)
            {
                usuario.ParticiparDe(departamento);

                usuarios.Salvar(usuario);
            }
        }

        private void InserirInformacoesHospitalares(Departamento departamento)
        {
            InserirSetoresESubSetores(departamento);
            InserirSetores(departamento);
            InserirInsumo(departamento);
            InserirTicketDeProducao(departamento);
            InsetirTicketDeReceita(departamento);
            InserirDRE2012(departamento);
        }

        private void InserirSetores(Departamento departamento)
        {
            Departamentos repositorio = new Departamentos();

            foreach (var setor in departamento.Setores)
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

        private TipoConta ObterTipoConta(string nome)
        {
            TiposConta tipos = new TiposConta();
            TipoConta tipoConta = tipos.ObterPor(nome);

            if (tipoConta == null)
            {
                tipoConta = new TipoConta();
            }

            tipos.Salvar(tipoConta);
            return tipoConta;
        }

        private Conta ObterConta(string nome, TipoConta tipoConta)
        {
            Contas contas = new Contas();
            Conta conta = contas.ObterContaPor(nome, tipoConta);

            if (conta == null)
            {
                conta = new Conta(nome, tipoConta);
            }
            else
            {
                return conta;
            }

            contas.Salvar(conta);
            return conta;
        }

        private GrupoDeConta ObterGrupoConta(string nome)
        {
            GruposDeConta gruposDeConta = new GruposDeConta();
            var grupoDeConta = gruposDeConta.ObterPor(nome);

            if (grupoDeConta == null)
            {
                grupoDeConta = new GrupoDeConta(nome);
            }
            else
            {
                return grupoDeConta;
            }

            gruposDeConta.Salvar(grupoDeConta);
            return grupoDeConta;
        }


        public void InicializarModuloDePessoal()
        {
            var tiposConta = new TiposConta();
            var tipoContaBeneficios = ObterTipoConta("Beneficios");
            tiposConta.Adicionar(tipoContaBeneficios);

            Departamentos departamentos = new Departamentos();
            var listaDepartamentos = departamentos.Todos();

            var tipoContaFGTS = ObterTipoConta("FGTS");
            var tipoContaINSS = ObterTipoConta("INSS");
            var tipoContaFerias = ObterTipoConta("Férias");
            var tipoContaIndenizacao = ObterTipoConta( "Indenização");
            var tipoContaDecimoTerceiro = ObterTipoConta("Décimo Terceiro");
            var tipoContaSalario = ObterTipoConta("Salário");
            var tipoContaBolsasDeEstagio = ObterTipoConta("Bolsas de Estágio");
            var tipoContaExtras = ObterTipoConta("Extras");

            tiposConta.Adicionar(tipoContaFGTS);
            tiposConta.Adicionar(tipoContaINSS);
            tiposConta.Adicionar(tipoContaFerias);
            tiposConta.Adicionar(tipoContaIndenizacao);
            tiposConta.Adicionar(tipoContaDecimoTerceiro);
            tiposConta.Adicionar(tipoContaSalario);
            tiposConta.Adicionar(tipoContaBolsasDeEstagio);
            tiposConta.Adicionar(tipoContaExtras);

            var gruposDeConta = new GruposDeConta();
            var encargosSociais = ObterGrupoConta("Encargos Sociais");
            var remuneracao = ObterGrupoConta( "Remuneração");
            var beneficios = ObterGrupoConta("Benefícios");

            gruposDeConta.Salvar(beneficios);
            gruposDeConta.Salvar(remuneracao);
            gruposDeConta.Salvar(encargosSociais);

            var contaAlimentacao = ObterConta("Alimentação", tipoContaBeneficios);
            contaAlimentacao.Adicionar(TipoTicketDePessoal.Alimentação);

            var contaAssistenciaMedica = ObterConta("Assistência Médica", tipoContaBeneficios);
            contaAssistenciaMedica.Adicionar(TipoTicketDePessoal.AssistenciaMedica);

            var contaOutrosBeneficios = ObterConta("Outros Benefícios", tipoContaBeneficios);
            contaOutrosBeneficios.Adicionar(TipoTicketDePessoal.OutrosBeneficios);

            var contaTreinamentoPessoal = ObterConta("Treinamento Pessoal", tipoContaBeneficios);
            contaTreinamentoPessoal.Adicionar(TipoTicketDePessoal.TreinamentoPessoal);

            var contaValeDeTransporte = ObterConta("Vale de Transporte", tipoContaBeneficios);
            contaValeDeTransporte.Adicionar(TipoTicketDePessoal.ValeDeTransporte);

            var contaOutrasDespesas = ObterConta("Outras Despesas", tipoContaBeneficios);
            contaOutrasDespesas.Adicionar(TipoTicketDePessoal.OutrasDespesas);

            var contaAssistenciaOdontologica = ObterConta("Assistência Odontológica", tipoContaBeneficios);
            contaAssistenciaOdontologica.Adicionar(TipoTicketDePessoal.AssistenciaOdontologica);

            beneficios.Adicionar(contaAlimentacao);
            beneficios.Adicionar(contaAssistenciaMedica);
            beneficios.Adicionar(contaAssistenciaOdontologica);
            beneficios.Adicionar(contaOutrosBeneficios);
            beneficios.Adicionar(contaTreinamentoPessoal);
            beneficios.Adicionar(contaValeDeTransporte);
            beneficios.Adicionar(contaOutrasDespesas);

            var contaFGTS = ObterConta("FGTS", tipoContaFGTS);
            contaFGTS.Adicionar(TipoTicketDePessoal.FGTS);
            encargosSociais.Adicionar(contaFGTS);

            var contaINSS = ObterConta("INSS", tipoContaINSS);
            contaINSS.Adicionar(TipoTicketDePessoal.INSS);
            encargosSociais.Adicionar(contaINSS);



            var contaFerias = ObterConta("Férias", tipoContaFerias);
            encargosSociais.Adicionar(contaFerias);

            var contaIndenizacao = ObterConta("Indenização", tipoContaIndenizacao);
            encargosSociais.Adicionar(contaIndenizacao);

            var contaDecimoTerceiro = ObterConta("Décimo Terceiro", tipoContaDecimoTerceiro);
            encargosSociais.Adicionar(contaDecimoTerceiro);

            foreach (var conta in encargosSociais.Contas)
            {
                if (conta.Nome == "Indenização")
                {
                    conta.Adicionar(TipoTicketDePessoal.Indenizacao);
                }
                else
                {
                    if (conta.Nome == "INSS" || conta.Nome == "FGTS")
                        conta.Adicionar(TipoTicketDePessoal.AdicionalDeSobreaviso);

                    conta.Adicionar(TipoTicketDePessoal.AdicionalNoturno);
                    conta.Adicionar(TipoTicketDePessoal.AdicionalDeInsalubridade);
                    conta.Adicionar(TipoTicketDePessoal.AdicionaDePericulosidade);
                    conta.Adicionar(TipoTicketDePessoal.Gratificacoes);
                    conta.Adicionar(TipoTicketDePessoal.HorasExtras);
                }
            }

            remuneracao.Adicionar(new Conta("Salário", tipoContaSalario));

            var contaBolsaDeEstagio = ObterConta("Bolsas Estágio", tipoContaBolsasDeEstagio);
            contaBolsaDeEstagio.Adicionar(TipoTicketDePessoal.BolsaDeEstagio);
            remuneracao.Adicionar(contaBolsaDeEstagio);


            var contaAdicionalNoturno = ObterConta("Adicional Noturno", tipoContaExtras);
            contaAdicionalNoturno.Adicionar(TipoTicketDePessoal.AdicionalNoturno);
            remuneracao.Adicionar(contaAdicionalNoturno);

            var contaPericulosidade = ObterConta("Periculosidade", tipoContaExtras);
            contaPericulosidade.Adicionar(TipoTicketDePessoal.AdicionaDePericulosidade);
            remuneracao.Adicionar(contaPericulosidade);

            var contaInsalubridade = ObterConta("Insalubridade", tipoContaExtras);
            contaInsalubridade.Adicionar(TipoTicketDePessoal.AdicionalDeInsalubridade);
            remuneracao.Adicionar(contaInsalubridade);

            var contaHorasExtras = ObterConta("Horas Extras", tipoContaExtras);
            contaHorasExtras.Adicionar(TipoTicketDePessoal.HorasExtras);
            remuneracao.Adicionar(contaHorasExtras);

            var contaGratificacoes = ObterConta("Gratificações", tipoContaExtras);
            contaGratificacoes.Adicionar(TipoTicketDePessoal.Gratificacoes);
            remuneracao.Adicionar(contaGratificacoes);

            Contas contas = new Contas();

            contas.Salvar(contaGratificacoes);
            contas.Salvar(contaHorasExtras);
            contas.Salvar(contaInsalubridade);
            contas.Salvar(contaPericulosidade);
            contas.Salvar(contaAdicionalNoturno);
            contas.Salvar(contaBolsaDeEstagio);
            contas.Salvar(contaDecimoTerceiro);
            contas.Salvar(contaIndenizacao);
            contas.Salvar(contaFerias);
            contas.Salvar(contaINSS);
            contas.Salvar(contaFGTS);
            contas.Salvar(contaAssistenciaOdontologica);
            contas.Salvar(contaOutrasDespesas);
            contas.Salvar(contaValeDeTransporte);
            contas.Salvar(contaTreinamentoPessoal);
            contas.Salvar(contaOutrosBeneficios);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAlimentacao);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAssistenciaMedica);


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

        private SetorHospitalar ObterSetor(string nome)
        {
            SetoresHospitalares setores = new SetoresHospitalares();
            SetorHospitalar setor = setores.Obter(nome);

            if (setor == null)
            {
                setor = new SetorHospitalar(nome);
            }

            setores.Salvar(setor);
            return setor;
        }

        private SubSetorHospital ObterSubSetor(string nome)
        {
            SetoresHospitalares setores = new SetoresHospitalares();
            SubSetorHospital setor = setores.ObterSub(nome);

            if (setor == null)
            {
                setor = new SubSetorHospital(nome);
            }

            setores.Salvar(setor);
            return setor;
        }

        private ContaHospital ObterConta(string nome, TipoValorContaEnum tipo, bool calculado, bool contabilizaProducao)
        {
            ContasHospitalares contas = new ContasHospitalares();
            ContaHospital conta = contas.ObterContaPor(nome);

            if (conta == null)
            {
                conta = new ContaHospital(nome, tipo, calculado, contabilizaProducao);
            }

            contas.Salvar(conta);
            return conta;
        }

        private ContaHospital ObterConta(string nome, TipoValorContaEnum tipo)
        {
            ContasHospitalares contas = new ContasHospitalares();
            ContaHospital conta = contas.ObterContaPor(nome);

            if (conta == null)
            {
                conta = new ContaHospital(nome, tipo);
            }

            contas.Salvar(conta);
            return conta;
        }

        private void InserirSubSetorNoSetor(SetorHospitalar setor, SubSetorHospital subSetor)
        {
            setor.AdicionarSetor(subSetor);
        }

        public void InserirSetoresESubSetores(Departamento departamento)
        {
            Orcamentos orcamentos = new Orcamentos();

            var setorHemodinamica = ObterSetor("Hemodinâmica");
            var subSetorHemodinamica = ObterSubSetor("Hemodinâmica");
            InserirSubSetorNoSetor(setorHemodinamica, subSetorHemodinamica);

            var setorOncologia = ObterSetor("Oncologia");
            var subSetorOncologia = ObterSubSetor("Oncologia");
            InserirSubSetorNoSetor(setorOncologia, subSetorOncologia);

            var procedimento = ObterConta("Procedimendo", TipoValorContaEnum.Quantidade);

            setorHemodinamica.AdicionarConta(procedimento);
            setorOncologia.AdicionarConta(procedimento);

            var centroCirurgico = ObterSetor("Centro Cirúrgico");
            var centroCirurgicoSubSetor = ObterSubSetor("Centro Cirúrgico");
            var centroCirurgicoObstetrico = ObterSubSetor("Centro Obstétrico");
            centroCirurgico.AdicionarSetor(centroCirurgicoSubSetor);
            centroCirurgico.AdicionarSetor(centroCirurgicoObstetrico);

            //Contas ta cirurgia
            var cirurgia = ObterConta("Cirurgias", TipoValorContaEnum.Quantidade);
            var salas = ObterConta("Salas", TipoValorContaEnum.Quantidade, false, true);
            var cirurgiaPorSala = ObterConta("Cirurgias por Sala", TipoValorContaEnum.Quantidade, true, false);

            centroCirurgico.AdicionarConta(cirurgia);
            centroCirurgico.AdicionarConta(salas);
            centroCirurgico.AdicionarConta(cirurgiaPorSala);

            cirurgiaPorSala.AnexarConta(cirurgia);
            cirurgiaPorSala.AnexarConta(salas);

            //UTI
            var uti = ObterSetor("UTI");
            var utiSemiMaternidade = ObterSubSetor("UTI Semi Maternidade");
            var utiAdulto = ObterSubSetor("UTI Adulto");
            var utiPediatrica = ObterSubSetor("Uti Pediátrica");
            var utiNeoNatal = ObterSubSetor("Uti Neo-Natal");
            var utiCoronariana = ObterSubSetor("Uti Coronariana");
            var semiIntensiva = ObterSubSetor("Uti Semi-Intensiva");

            //Contas da UTI
            var leito = ObterConta("Leito", TipoValorContaEnum.Quantidade);
            var ocupacao = ObterConta("Taxa de Ocupação", TipoValorContaEnum.Porcentagem);
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
            var uni = ObterSetor("UNI");
            var uniAdulto = ObterSubSetor("Uni Adulto");
            var uniPediatrica = ObterSubSetor("Uni Pediátrica");
            var uniMaternidade = ObterSubSetor("Maternidade");

            uni.AdicionarSetor(uniAdulto);
            uni.AdicionarSetor(uniPediatrica);
            uni.AdicionarSetor(uniMaternidade);

            //Contas UNI
            uni.AdicionarConta(leito);
            uni.AdicionarConta(ocupacao);

            //Conta Atendimento
            var atendimento = ObterConta("Atendimento", TipoValorContaEnum.Quantidade);

            //Emergencia
            var emergencia = ObterSetor("Emergência");
            var subEmergenciaMaternidade = ObterSubSetor("Emergência Maternidade");
            var subEmergenciaAdulto = ObterSubSetor("Emergência Adulto");
            var subEmergenciaPediatrica = ObterSubSetor("Emergência Pediátrica");
            emergencia.AdicionarSetor(subEmergenciaMaternidade);
            emergencia.AdicionarSetor(subEmergenciaAdulto);
            emergencia.AdicionarSetor(subEmergenciaPediatrica);
            emergencia.AdicionarConta(atendimento);

            //Ambulatorio
            var ambulatorio = ObterSetor("Ambulatório");
            var subAmbulatorio = ObterSubSetor("Ambulatório");
            ambulatorio.AdicionarSetor(subAmbulatorio);
            ambulatorio.AdicionarConta(atendimento);

            //SADT
            var sadt = ObterSetor("SADT");
            var cardiologico = ObterSubSetor("Cardiológico");
            var resonanciaMagnetica = ObterSubSetor("Resonância Mag");
            var ultrassonografica = ObterSubSetor("Ultrassonografia");
            var tomografiaCompleta = ObterSubSetor("Tomografia Comp");
            var radiologia = ObterSubSetor("Radiologia");
            var patologiaClinica = ObterSubSetor("Patologia Clínica");
            var outros = ObterSubSetor("Outros");
            var exames = ObterConta("Exames", TipoValorContaEnum.Quantidade);

            sadt.AdicionarSetor(cardiologico);
            sadt.AdicionarSetor(resonanciaMagnetica);
            sadt.AdicionarSetor(ultrassonografica);
            sadt.AdicionarSetor(tomografiaCompleta);
            sadt.AdicionarSetor(radiologia);
            sadt.AdicionarSetor(patologiaClinica);
            sadt.AdicionarSetor(outros);
            sadt.AdicionarConta(exames);

            var bercario = ObterSetor("Berçário");
            var bercarioAltoRisco = ObterSubSetor("Berçário Alto Risco");
            var bercarioSemiIntensiva = ObterSubSetor("Berçário Semi-intensiva");

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
