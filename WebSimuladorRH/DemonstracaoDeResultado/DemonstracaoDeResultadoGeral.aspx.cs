using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;
using Orcamento.Domain.DTO;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Servico;
using Orcamento.Domain.ComponentesDeOrcamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Servico.Pessoal;

namespace WebSimuladorRH.DemonstracaoDeResultado
{
    public class CoorporativoDTO
    {
        public string Setor { get; set; }
        public List<ValorMensal> Valores { get; set; }
        public double ValorTotal { get { return Valores.Sum(v => v.Valor); } }
    }

    public class ValorMensal
    {
        public double Valor { get; set; }
        public MesEnum Mes { get; set; }
    }

    public partial class DemonstracaoDeResultadoGeral : System.Web.UI.Page
    {
        private Double receitaBrutaTotal;
        private Double glosaInternaTotal;
        private Double glosaExternaTotal;
        private Double impostosTotal;
        private Double receitaLiquidaTotal;
        private Double insumosTotal;
        private Double margemBrutaTotal;
        private Double despessaOperacionalTotal;

        public ServicoMapperOrcamentoView ServicoMapperOrcamentoView { get { return new ServicoMapperOrcamentoView(); } }
        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoProducao"]; }
            set { Session["DepartamentoProducao"] = value; }
        }

        public List<Orcamento.Domain.Orcamento> OrcamentosHospitalares
        {
            get { return (List<Orcamento.Domain.Orcamento>)Session["OrcamentoHospitalarVersaoFinal"]; }
            set { Session["OrcamentoHospitalarVersaoFinal"] = value; }
        }

        public List<NovoOrcamentoPessoal> OrcamentosPessoal
        {
            get { return (List<NovoOrcamentoPessoal>)Session["OrcamentosPessoal"]; }
            set { Session["OrcamentosPessoal"] = value; }
        }

        public List<Orcamento.Domain.Orcamento> OrcamentosDeDespesasOperacionais
        {
            get { return (List<Orcamento.Domain.Orcamento>)Session["OrcamentosDeDespesasOperacionais"]; }
            set { Session["OrcamentosDeDespesasOperacionais"] = value; }
        }

        public List<FatorReceitaDTO> FatoresDeReceitaBruta
        {
            get { return (List<FatorReceitaDTO>)Session["FatoresDeReceitaBruta"]; }
            set { Session["FatoresDeReceitaBruta"] = value; }
        }

        public List<CoorporativoDTO> Coorporativos
        {
            get { return (List<CoorporativoDTO>)Session["CoorporativosDTO"]; }
            set { Session["CoorporativosDTO"] = value; }
        }

        public List<ReceitaDTO> receitasNoAno
        {
            get { return (List<ReceitaDTO>)Session["receitasNoAno"]; }
            set { Session["receitasNoAno"] = value; }
        }

        public List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita> TicketsGlosaInterna
        {
            get { return (List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita>)Session["TicketsGlosaInterna"]; }
            set { Session["TicketsGlosaInterna"] = value; }
        }

        public List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita> TicketsGlosaExterna
        {
            get { return (List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita>)Session["TicketsGlosaExterna"]; }
            set { Session["TicketsGlosaExterna"] = value; }
        }

        public List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita> TicketsDescontos
        {
            get { return (List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita>)Session["TicketsDescontos"]; }
            set { Session["TicketsDescontos"] = value; }
        }

        public List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita> TicketsDeImpostos
        {
            get { return (List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TicketDeReceita>)Session["TicketsDeImpostos"]; }
            set { Session["TicketsDeImpostos"] = value; }
        }

        public List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.Insumo> Insumos
        {
            get { return (List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.Insumo>)Session["Insumo"]; }
            set { Session["Insumo"] = value; }
        }

        private List<Orcamento.Domain.Orcamento> PreencherResultadoOrçadoViagens(List<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            if (listaOrcamentos == null || listaOrcamentos != null && listaOrcamentos.Count == 0 || listaOrcamentos != null && listaOrcamentos.Count > 0 && listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Count() == 0)
                return new List<Orcamento.Domain.Orcamento>();

            return listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).ToList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var departamentos = new Departamentos();
            Departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);

            if (!Page.IsPostBack)
            {
                PreencherReceitasVazias();
                PreencherReceitaBruta();
                PreencherGlosaInterna();
                PreencherTicketGlosaInterna();
                PreencherGlosaExterna();
                PreencherTitcketGlosaExterna();
                PreecherImpostos();
                PreecherTicketsImpostos();
                PreencherDeducoes();
                PreencherReceitaLiquida();
                PreencherPessoal();
                PreencherInsumos();
                PrenencherRLInsumos();
                PreencherServicosContratados();
                PreencherOcupacao();
                PreencherUtilidadeEServico();
                PreencherDespesasGeraisTotal();
                PreencherMarketing();
                PreencherDescontosObtidos();
                PreencherServicosMedicos();
                PreencherDespesasOperacionais();
                PreencherMargemBruta();
                PreencherPercentualMargemBruta();

                PreencherTotal2012();

                PreencherCoorporativo();
            }
        }

        private void PreencherCoorporativo()
        {
            var orcamentos = new Orcamentos();
            var orcamentosPessoais = new NovosOrcamentosPessoais();
            Setores departamentos = new Setores();
            var orcamentosOperacionais = orcamentos.ObterOrcamentosFinaisOperacionaisCoorporativo();
            var orcamentosDeViagem = orcamentos.ObterOrcamentosFinaisViagensCoorporativo();
            var orcamentosDePessoal = orcamentosPessoais.TodosCoorporativo();
            var setores = departamentos.Todos();
            CriarCoorporativos(setores, orcamentosOperacionais, orcamentosDeViagem, orcamentosDePessoal);
        }

        private void CriarCoorporativos(List<Setor> setores, List<Orcamento.Domain.Orcamento> orcamentosOperacional,
        List<Orcamento.Domain.Orcamento> orcamentosDeViagem
            , List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.NovoOrcamentoPessoal> orcamentoPessoal)
        {
            Coorporativos = new List<CoorporativoDTO>();

            foreach (var setor in setores)
            {
                CoorporativoDTO coorporativo = new CoorporativoDTO() { Setor = setor.Nome };
                coorporativo.Valores = new List<ValorMensal>();
                for (int i = 1; i < 13; i++)
                {
                    ValorMensal valor = new ValorMensal() { Mes = (MesEnum)i, Valor = 0 };

                    if (orcamentoPessoal != null && orcamentoPessoal.Count > 0 && orcamentoPessoal.Where(o => o.Departamento.Id == setor.Id).Count() > 0)
                        valor.Valor += orcamentoPessoal.Where(o => o.Departamento.Id == setor.Id && o.Despesas != null).Sum(o => o.Despesas.Where(de => de.Parcelas != null).Sum(d => d.Parcelas.Where(p => p.Mes == i).Sum(p => p.Valor)));

                    if (orcamentosOperacional != null && orcamentosOperacional.Count > 0 && orcamentosOperacional.Where(o => o.Setor.Id == setor.Id).Count() > 0)
                        valor.Valor += orcamentosOperacional.Where(o => o.Setor.Id == setor.Id).Sum(o => o.DespesasOperacionais.Where(d => d.Mes == (MesEnum)i).Sum(f => f.Valor));
                    if (orcamentosDeViagem != null && orcamentosDeViagem.Count > 0 && orcamentosDeViagem.Where(d => d.Setor.Id == setor.Id).Count() > 0)
                        valor.Valor += orcamentosDeViagem.Where(d => d.Setor.Id == setor.Id).Sum(d => d.Despesas.Where(v => v.Mes == (MesEnum)i).Sum(va => va.ValorTotal));

                    if (valor.Valor > 0)
                        valor.Valor = valor.Valor / 1000;

                    coorporativo.Valores.Add(valor);
                }
                Coorporativos.Add(coorporativo);

            }

            rptCoorporativo.DataSource = Coorporativos;
            rptCoorporativo.DataBind();
        }

        protected void TotalCoorporativo(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 1; i < 13; i++)
                {
                    var literalTotalMensal = (Literal)e.Item.FindControl("ltlCoorporativoMes" + i.ToString());

                    if (ValorCoorporativoMes(i) > 0)
                        literalTotalMensal.Text = (ValorCoorporativoMes(i)).ToString("#,###,###,###0.##");
                    else
                        literalTotalMensal.Text = "0";
                }

                var totalAno = (Literal)e.Item.FindControl("ltlCoorporativoMesTotal");

                if (ValorCoorporativoTotal() > 0)
                    totalAno.Text = (ValorCoorporativoTotal()).ToString("#,###,###,###0.##");
                else
                    totalAno.Text = "0";
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                for (int i = 1; i < 13; i++)
                {
                    var literalTotalMensal = (Literal)e.Item.FindControl("ltlEBITDAMes" + i.ToString());

                    if ((ValorMargemBruta(i) - ValorCoorporativoMes(i)) > 0)
                        literalTotalMensal.Text = (ValorMargemBruta(i) - ValorCoorporativoMes(i)).ToString("#,###,###,###0.##");
                    else
                        literalTotalMensal.Text = "0";
                }

                var totalAno = (Literal)e.Item.FindControl("ltlEBITDAAno");
                if ((ValorMargemBrutaTotal() - ValorCoorporativoTotal() > 0))
                    totalAno.Text = (ValorMargemBrutaTotal() - ValorCoorporativoTotal()).ToString("#,###,###,###0.##");
                else
                    totalAno.Text = "0";

                for (int i = 1; i < 13; i++)
                {
                    var literalTotalMensalPorcentagem = (Literal)e.Item.FindControl("ltEBITDAPorcentagemMes" + i.ToString());
                    if ((ValorMargemBruta(i) - ValorCoorporativoMes(i)) - ValorLiquidoMensal(i) > 0)
                        literalTotalMensalPorcentagem.Text = String.Format("{0:n2}", ((ValorMargemBruta(i) - ValorCoorporativoMes(i)) - ValorLiquidoMensal(i)).ToString()) + "%";
                    else
                        literalTotalMensalPorcentagem.Text = "0,00%";
                }

                var ltEBITDAPorcentagemAno = (Literal)e.Item.FindControl("ltEBITDAPorcentagemAno");

                if (((ValorMargemBrutaTotal() - ValorCoorporativoTotal()) - receitasNoAno.Sum(r => r.ValorReceitaLiquida)) > 0)
                    ltEBITDAPorcentagemAno.Text = string.Format("{0:n2}", ((ValorMargemBrutaTotal() - ValorCoorporativoTotal()) - receitasNoAno.Sum(r => r.ValorReceitaLiquida)).ToString()) + "%";
                else
                    ltEBITDAPorcentagemAno.Text = "0,00%";
            }
        }

        private double ValorLiquidoMensal(int i)
        {
            if (receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(r => r.ValorReceitaLiquida) > 0)
                return receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(r => r.ValorReceitaLiquida) / 1000;
            else
                return 0;
        }

        private double ValorCoorporativoTotal()
        {
            if (Coorporativos != null && Coorporativos.Count > 0 && (Coorporativos.Sum(c => c.Valores.Sum(d => d.Valor)) > 0))
                return (Coorporativos.Sum(c => c.Valores.Sum(d => d.Valor)) / 1000);
            else
                return 0;
        }

        private double ValorMargemBrutaTotal()
        {
            if (receitasNoAno != null && receitasNoAno.Count > 0 && (((receitasNoAno.Sum(receita => receita.ValorReceitaLiquida) - receitasNoAno.Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos)) / 1000)) > 0)
                return (((receitasNoAno.Sum(receita => receita.ValorReceitaLiquida) - receitasNoAno.Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos)) / 1000));
            else
                return 0;
        }

        private double ValorCoorporativoMes(int i)
        {
            if (Coorporativos != null && Coorporativos.Count > 0 && (Coorporativos.Sum(c => c.Valores.Where(v => v.Mes == (MesEnum)i).Sum(d => d.Valor)) / 1000) > 0)
                return (Coorporativos.Sum(c => c.Valores.Where(v => v.Mes == (MesEnum)i).Sum(d => d.Valor)) / 1000);
            else
                return 0;
        }

        private double ValorMargemBruta(int i)
        {
            if (receitasNoAno != null && receitasNoAno.Count > 0 && (((receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => receita.ValorReceitaLiquida) - receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos)) / 1000)) > 0)
                return (((receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => receita.ValorReceitaLiquida) - receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos)) / 1000));
            else
                return 0;
        }

        private void PreencherTotal2012()
        {
            DRES dres = new DRES();
            var total = dres.Todos();

            PreencherLiteral(false, false, ltlReceitaBrutaTotal2012, total.Sum(t => t.TotalReceitaBruta));
            PreencherLiteral(true, false, ltlDeducoesTotal2012, total.Sum(t => t.TotalDeducoes));
            PreencherLiteral(true, false, ltlGlosaInternaTotal2012, total.Sum(t => t.TotalGlosaInterna));
            PreencherLiteral(false, true, ltlGlosaInternaRBTotal2012, total.Sum(t => t.TotalPercentualGlosaInterna));
            PreencherLiteral(true, false, ltlGlosaExternaTotal2012, total.Sum(t => t.TotalGlosaExterna));
            PreencherLiteral(false, true, ltlGlosaExternaRBTotal2012, total.Sum(t => t.TotalPercentualGlosaExterna));
            PreencherLiteral(true, false, ltlImpostosTotal2012, total.Sum(t => t.TotalImpostos));
            PreencherLiteral(false, true, ltlImpostosRBTotal2012, total.Sum(t => t.TotalPercentualImpostos));
            PreencherLiteral(false, false, ltlReceitaLiquidaTotal2012, total.Sum(t => t.TotalReceitaLiquida));
            PreencherLiteral(true, false, ltlDespesasOperacionaisTotal2012, total.Sum(t => t.TotalDespesasOperacionais));
            PreencherLiteral(true, false, ltlPessoalTotal2012, total.Sum(t => t.TotalPessoal));
            PreencherLiteral(true, false, ltlServicosMedicosTotal2012, total.Sum(t => t.TotalServicosMedicos));
            PreencherLiteral(true, false, ltlInsumosTotal2012, total.Sum(t => t.TotalInsumos));
            PreencherLiteral(false, true, ltlInsumosRLTotal2012, total.Sum(t => t.TotalPercentualInsumos));
            PreencherLiteral(true, false, ltlServicosContratadosTotal2012, total.Sum(t => t.TotalServicosContratados));
            PreencherLiteral(true, false, ltlOcupacaoTotal2012, total.Sum(t => t.TotalOcupacao));
            PreencherLiteral(true, false, ltlUtilidadesEServicosTotal2012, total.Sum(t => t.TotalUtilidadeServico));
            PreencherLiteral(true, false, ltlDespesasGeraisTotal2012, total.Sum(t => t.TotalDespesasGerais));
            PreencherLiteral(true, false, ltlMarketingTotal2012, total.Sum(t => t.TotalMarketing));
            PreencherLiteral(false, false, ltlDescontosObtidosTotal2012, total.Sum(t => t.TotalDescontosObtidos));
            PreencherLiteral(false, false, ltlMargemBrutaTotal2012, total.Sum(t => t.TotalMargemBruta));
            PreencherLiteral(false, true, ltlMargemBrutaPorcentagemTotal2012, total.Sum(t => t.TotalPercentualMargemBruta));
        }

        private void PreencherLiteral(bool mascaraNegativa, bool mascaraPorcentagem, Literal literal, double texto)
        {
            if (mascaraNegativa)
            {
                literal.Text += "(";
                literal.Text += texto.ToString("#,###,###,###0");
                literal.Text += ")";
            }

            else if (mascaraPorcentagem)
            {
                literal.Text = String.Format("{0:n2}", texto) + "%";
            }

            else
                literal.Text = texto.ToString("#,###,###,###0");

        }

        private void PreencherReceitasVazias()
        {
            receitasNoAno = new List<ReceitaDTO>();

            for (int i = 1; i < 13; i++)
            {
                receitasNoAno.Add(new ReceitaDTO() { Mes = (MesEnum)i });
            }
        }

        private void PreencherPercentualMargemBruta()
        {
            for (int i = 1; i < 13; i++)
            {
                var ltlMargemBrutaPorcentagem = (Literal)divDre.FindControl("ltlMargemBrutaPorcentagem" + i.ToString());
                double valorReceita = receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => receita.ValorReceitaLiquida);
                double margemBruta = receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais +
                    receita.ValorMarketing) - receita.DescontosObtidos);

                double valorMargemBruta = valorReceita - margemBruta;

                double valorReceitaLiquida = (receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => receita.ValorReceitaLiquida));

                if (valorReceitaLiquida == 0)
                    ltlMargemBrutaPorcentagem.Text = "0%";
                else
                    ltlMargemBrutaPorcentagem.Text = String.Format("{0:n2}", (valorMargemBruta / valorReceitaLiquida) * 100) + "%";

            }
            if (receitaLiquidaTotal == 0)
                ltlMargemBrutaPorcentagemTotal.Text = "0%";
            else
                ltlMargemBrutaPorcentagemTotal.Text = String.Format("{0:n2}", (margemBrutaTotal / receitaLiquidaTotal) * 100) + "%";
        }

        private void PreencherDespesasOperacionais()
        {
            for (int i = 1; i < 13; i++)
            {
                var ltlDespesasOperacionais = (Literal)divDre.FindControl("ltlDespesasOperacionais" + i.ToString());

                ltlDespesasOperacionais.Text = "(";
                ltlDespesasOperacionais.Text += (receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos) / 1000).ToString("#,###,###,###0");
                ltlDespesasOperacionais.Text += ")";

            }

            despessaOperacionalTotal =
                (receitasNoAno.Sum(
                    receita =>
                        (
                    receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos +
                    receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais +
                    receita.ValorMarketing) - receita.DescontosObtidos) / 1000);

            ltlDespesasOperacionaisTotal.Text = "(";
            ltlDespesasOperacionaisTotal.Text += despessaOperacionalTotal.ToString("#,###,###,###0");
            ltlDespesasOperacionaisTotal.Text += ")";
        }

        private void PreencherMargemBruta()
        {

            for (int i = 1; i < 13; i++)
            {
                var ltlMargemBruta = (Literal)divDre.FindControl("ltlMargemBruta" + i.ToString());

                ltlMargemBruta.Text = ((receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => receita.ValorReceitaLiquida) - receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(receita => (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos + receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico + receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos)) / 1000).ToString("#,###,###,###0");

            }
            margemBrutaTotal = (receitaLiquidaTotal - despessaOperacionalTotal);
            //margemBrutaTotal = ((receitasNoAno.Sum(receita => receita.ValorReceitaLiquida) -
            //                     receitasNoAno.Sum(
            //                         receita =>
            //                         (receita.ValorDespesaPessoal + receita.ServicosMedicos + receita.ValorInsumos +
            //                         receita.ServicosContratados + receita.Ocupacao + receita.UtilidadeEServico +
            //                         receita.DespesasGerais + receita.ValorMarketing) - receita.DescontosObtidos))/1000);


            ltlMargemBrutaTotal.Text = margemBrutaTotal.ToString("#,###,###,###0");
        }

        private void PreencherServicosMedicos()
        {
            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Serviços Médicos");

            var DespesasAgrupadas = OrcamentosDeDespesasOperacionais.Where(o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                .Select(c => c.DespesasOperacionais.Where(d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

            for (int i = 1; i < 13; i++)
            {
                var ltlServicosMedicos = (Literal)divDre.FindControl("ltlServicosMedicos" + i.ToString());

                ltlServicosMedicos.Text = "(";
                ltlServicosMedicos.Text += (DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
                ltlServicosMedicos.Text += ")";

                receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().ServicosMedicos += DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));
            }

            ltlServicosMedicosTotal.Text = "(";
            ltlServicosMedicosTotal.Text += (DespesasAgrupadas.Sum(d1 => d1.Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
            ltlServicosMedicosTotal.Text += ")";
        }

        private void PreencherDescontosObtidos()
        {
            TicketsDeReceita tickets = new TicketsDeReceita();
            TicketsDescontos = tickets.Obter(Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.Descontos);

            foreach (var ticket in TicketsDescontos)
            {
                ticket.CalcularGlosa(receitasNoAno);
            }

            for (int i = 1; i < 13; i++)
            {
                var ltlDescontosObtidos = (Literal)divDre.FindControl("ltlDescontosObtidos" + i.ToString());

                ltlDescontosObtidos.Text = (receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.DescontosObtidos) / 1000).ToString("#,###,###,###0");
            }

            ltlDescontosObtidosTotal.Text = (receitasNoAno.Sum(f => f.DescontosObtidos) / 1000).ToString("#,###,###,###0");
        }

        private void PreencherMarketing()
        {
            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Mkt - Propaganda e Publicidade");

            var DespesasAgrupadas = OrcamentosDeDespesasOperacionais.Where(o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                .Select(c => c.DespesasOperacionais.Where(d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

            for (int i = 1; i < 13; i++)
            {
                var ltlUtilidade = (Literal)divDre.FindControl("ltlMarketing" + i.ToString());
                ltlUtilidade.Text = "(";
                ltlUtilidade.Text += (DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
                ltlUtilidade.Text += ")";

                receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().ValorMarketing += DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));
            }

            ltlMarketingTotal.Text = "(";
            ltlMarketingTotal.Text += (DespesasAgrupadas.Sum(d1 => d1.Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
            ltlMarketingTotal.Text += ")";
        }

        private void PreencherDespesasGeraisTotal()
        {
            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Despesas Gerais");
            Orcamentos orcamentos = new Orcamentos();

            var listaOrcamentos = orcamentos.ObterOrcamentosFinaisViagensHospitalares();

            if (listaOrcamentos == null)
                listaOrcamentos = new List<Orcamento.Domain.Orcamento>();

            listaOrcamentos = listaOrcamentos.Where(l => l.Tipo == TipoOrcamentoEnum.Viagem).ToList();

            var DespesasAgrupadas = OrcamentosDeDespesasOperacionais.Where(o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                .Select(c => c.DespesasOperacionais.Where(d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

            for (int i = 1; i < 13; i++)
            {

                var ltlDespesasGerais = (Literal)divDre.FindControl("ltlDespesasGerais" + i.ToString());

                ltlDespesasGerais.Text = "(";
                ltlDespesasGerais.Text += ((DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor)) + listaOrcamentos.Sum(o => o.Despesas.Where(d => d.Mes == (MesEnum)i).Sum(despesa => despesa.ValorTotal))) / 1000).ToString("#,###,###,###0");
                ltlDespesasGerais.Text += ")";
                receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().DespesasGerais += (DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor))
                    + listaOrcamentos.Sum(o => o.Despesas.Where(d => d.Mes == (MesEnum)i).Sum(despesa => despesa.ValorTotal)));
            }

            ltlDespesasGeraisTotal.Text = "(";
            ltlDespesasGeraisTotal.Text += (DespesasAgrupadas.Sum(d1 => d1.Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
            ltlDespesasGeraisTotal.Text += ")";
        }

        private void PreencherUtilidadeEServico()
        {
            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Utilidades e Servicos");

            var DespesasAgrupadas = OrcamentosDeDespesasOperacionais.Where(o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                .Select(c => c.DespesasOperacionais.Where(d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

            for (int i = 1; i < 13; i++)
            {
                var ltlUtilidade = (Literal)divDre.FindControl("ltlUtilidadesEServicos" + i.ToString());
                ltlUtilidade.Text = "(";
                ltlUtilidade.Text += (DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
                ltlUtilidade.Text += ")";
                receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().UtilidadeEServico += DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));
            }

            ltlUtilidadesEServicosTotal.Text = "(";
            ltlUtilidadesEServicosTotal.Text += (DespesasAgrupadas.Sum(d1 => d1.Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
            ltlUtilidadesEServicosTotal.Text += ")";
        }

        private void PreencherOcupacao()
        {
            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Ocupacao");

            var DespesasAgrupadas = OrcamentosDeDespesasOperacionais.Where(o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                .Select(c => c.DespesasOperacionais.Where(d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

            for (int i = 1; i < 13; i++)
            {
                var ltlocupacao = (Literal)divDre.FindControl("ltlOcupacao" + i.ToString());
                ltlocupacao.Text = "(";
                ltlocupacao.Text += (DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
                ltlocupacao.Text += ")";

                receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().Ocupacao += DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));
            }
            ltlOcupacaoTotal.Text = "(";
            ltlOcupacaoTotal.Text += (DespesasAgrupadas.Sum(d1 => d1.Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
            ltlOcupacaoTotal.Text += ")";
        }

        private void PreencherServicosContratados()
        {

            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Servicos Contratados");

            var DespesasAgrupadas = OrcamentosDeDespesasOperacionais.Where(o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                .Select(c => c.DespesasOperacionais.Where(d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

            for (int i = 1; i < 13; i++)
            {

                var ltlRlinsumos = (Literal)divDre.FindControl("ltlServicosContratados" + i.ToString());

                ltlRlinsumos.Text = "(";
                ltlRlinsumos.Text += (DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
                ltlRlinsumos.Text += ")";

                receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().ServicosContratados += DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));
            }
            ltlServicosContratadosTotal.Text = "(";
            ltlServicosContratadosTotal.Text += (DespesasAgrupadas.Sum(d1 => d1.Sum(despesa => despesa.Valor)) / 1000).ToString("#,###,###,###0");
            ltlServicosContratadosTotal.Text += ")";
        }

        private void PrenencherRLInsumos()
        {

            for (int i = 1; i < 13; i++)
            {

                var ltlRlinsumos = (Literal)divDre.FindControl("ltlInsumosRL" + i.ToString());

                if (receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().ValorInsumos > 0)
                {
                    ltlRlinsumos.Text = String.Format("{0:n2}", (receitasNoAno.Where(r => r.Mes == (MesEnum)i).Sum(r => r.ValorInsumos / r.ValorReceitaLiquida)) * 100) + "%";
                }
                else
                {
                    ltlRlinsumos.Text = "0,00%";
                }
            }

            if (receitasNoAno.Sum(r => r.ValorInsumos) > 0)
            {
                ltlInsumosRLTotal.Text = String.Format("{0:n2}", ((insumosTotal / receitaLiquidaTotal) * 100)) + "%";
            }
            else
            {
                ltlInsumosRLTotal.Text = "0,00%";

            }


        }

        private void PreencherInsumos()
        {
            Orcamento.Domain.DB.Repositorio.Insumos insumos = new global::Orcamento.Domain.DB.Repositorio.Insumos();
            Insumos = insumos.ObterInsumos();

            TicketsDeReceita tickets = new TicketsDeReceita();
           

            if (OrcamentosHospitalares != null && OrcamentosHospitalares.Count > 0)
            {
                foreach (var orcamento in OrcamentosHospitalares)
                {
                    var ticketsDeReceita = tickets.Todos(orcamento.Setor);
                    var contasUnitarias = ServicoMapperOrcamentoView.TransformarProducaoDeInsumos(
                         orcamento.Servicos.Where(s => s.Conta.Nome != "Salas" && s.Conta.TipoValorContaEnum == TipoValorContaEnum.Quantidade && s.Conta.Calculado == false).ToList(),
                         orcamento.Servicos.Where(s => s.Conta.TipoValorContaEnum == TipoValorContaEnum.Porcentagem).ToList());

                    orcamento.CalcularCustoHospitalar(
                   ticketsDeReceita.Where(
                       t =>
                       t.TipoTicket ==
                       global::Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.
                           ReajusteDeInsumos).FirstOrDefault(),
                   Insumos.Where(insumo => insumo.Departamento.Id == orcamento.Setor.Id).FirstOrDefault().CustosUnitarios.ToList(), contasUnitarias);

                }

                GruposDeConta grupos = new GruposDeConta();
                var grupoDeContaPessoal = grupos.ObterPor("Insumos");

                var DespesasAgrupadas =
                    OrcamentosDeDespesasOperacionais.Where(
                        o => o.DespesasOperacionais.Where(ope => ope.Valor > 0).Count() > 0)
                        .Select(
                            c =>
                            c.DespesasOperacionais.Where(
                                d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).
                        ToList();

                for (int i = 1; i < 13; i++)
                {

                    var ltlinsumos = (Literal)divDre.FindControl("ltlInsumos" + i.ToString());

                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorInsumos =
                        OrcamentosHospitalares.Sum(o => o.CustosUnitariosTotal.Sum(
                            c => c.Valores.Where(v => v.Mes == (MesEnum)i).Sum(insumo => insumo.Valor)));
                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorInsumos +=
                        DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));

                    ltlinsumos.Text = "(";
                    ltlinsumos.Text += (
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorInsumos / 1000).ToString("#,###,###,###0");
                    ltlinsumos.Text += ")";
                }

                insumosTotal = (receitasNoAno.Sum(r => r.ValorInsumos) / 1000);

                ltlInsumosTotal.Text = "(";
                ltlInsumosTotal.Text += insumosTotal.ToString("#,###,###,###0");
                ltlInsumosTotal.Text += ")";
            }
            else
            {
                for (int i = 1; i < 13; i++)
                {

                    var ltlinsumos = (Literal)divDre.FindControl("ltlInsumos" + i.ToString());
                    ltlinsumos.Text += "(0)";
                }
                ltlInsumosTotal.Text += "(0)";

            }
        }

        private void PreencherPessoal()
        {
            NovosOrcamentosPessoais orcamentosNovos = new NovosOrcamentosPessoais();
            Orcamentos orcamentos = new Orcamentos();

            OrcamentosPessoal = new List<NovoOrcamentoPessoal>();
            OrcamentosDeDespesasOperacionais = new List<Orcamento.Domain.Orcamento>();
            var orcamentosOperacionais = orcamentos.ObterOrcamentosFinaisOperacionaisHospitalares();

            OrcamentosDeDespesasOperacionais = orcamentosOperacionais;

            // var orcamentosPessoal = orcamentosNovos.TodosHospitalares();
            if (OrcamentosPessoal == null)
                OrcamentosPessoal = new List<NovoOrcamentoPessoal>();

            //var departamentos = new Departamentos();
            //var hospitais = departamentos.Todos<Hospital>();

            //foreach (var hospital in hospitais)
            //{
            //    foreach (var centro in hospital.CentrosDeCusto)
            //    {

            //        var servicoCalculaPessoal = new ServicoGerarOrcamentoPessoalPorCentroDeCusto();
            //        servicoCalculaPessoal.CentroDeCusto = centro;
            //        servicoCalculaPessoal.Departamento = Departamento;

            //        servicoCalculaPessoal.Gerar("");
            //        OrcamentosPessoal.Add(servicoCalculaPessoal.Orcamento);
            //    }
            //}

            GruposDeConta grupos = new GruposDeConta();
            var grupoDeContaPessoal = grupos.ObterPor("Pessoal");

            if (receitasNoAno != null)
            {
                var DespesasAgrupadas =
                    OrcamentosDeDespesasOperacionais.Select(
                        c =>
                        c.DespesasOperacionais.Where(
                            d => grupoDeContaPessoal.Contas.Any(g => g.CodigoDaConta == d.Conta.CodigoDaConta))).ToList();

                for (int i = 1; i < 13; i++)
                {

                    var ltlPessoal = (Literal)divDre.FindControl("ltlPessoal" + i.ToString());

                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDespesaPessoal =
                        DespesasAgrupadas.Sum(d1 => d1.Where(d2 => d2.Mes == (MesEnum)i).Sum(despesa => despesa.Valor));
                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDespesaPessoal +=
                        OrcamentosPessoal.Where(o => o.Despesas != null && o.Despesas.Count > 0).Sum(
                            o => o.Despesas.Sum(d => d.Parcelas.Where(p => p.Mes == i).Sum(p1 => p1.Valor)));

                    ltlPessoal.Text = "(";
                    ltlPessoal.Text += (
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDespesaPessoal / 1000).ToString("#,###,###,###0");
                    ltlPessoal.Text += ")";
                }

                ltlPessoalTotal.Text = "(";
                ltlPessoalTotal.Text += (receitasNoAno.Sum(r => r.ValorDespesaPessoal) / 1000).ToString("#,###,###,###0");
                ltlPessoalTotal.Text += ")";
            }
        }

        private void PreencherReceitaLiquida()
        {
            if (receitasNoAno != null)
            {
                for (int i = 1; i < 13; i++)
                {
                    var ltlReceitaLiquida = (Literal)divDre.FindControl("ltlReceitaLiquida" + i.ToString());

                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorReceitaLiquida =
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.Valor) -
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorDeducao);

                    ltlReceitaLiquida.Text =
                        (receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorReceitaLiquida / 1000).ToString("#,###,###,###0");
                }
                receitaLiquidaTotal = (receitasNoAno.Sum(R => R.ValorReceitaLiquida) / 1000);
                ltlReceitaLiquidaTotal.Text = receitaLiquidaTotal.ToString("#,###,###,###0");
            }
        }

        private void PreencherDeducoes()
        {
            if (receitasNoAno != null)
            {
                for (int i = 1; i < 13; i++)
                {
                    var ltlDeducoes = (Literal)divDre.FindControl("ltlDeducoes" + i.ToString());

                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDeducao =
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorImpostos);
                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDeducao +=
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorGlosaInterna);
                    receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDeducao +=
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorGlosaExterna);
                    ltlDeducoes.Text = "(";
                    ltlDeducoes.Text += (
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).FirstOrDefault().ValorDeducao / 1000).ToString("#,###,###,###0");
                    ltlDeducoes.Text += ")";
                }
                //var valorDeducaoTotal = receitasNoAno.Sum(f => f.ValorImpostos);
                //valorDeducaoTotal += receitasNoAno.Sum(f => f.ValorGlosaExterna);
                //valorDeducaoTotal += receitasNoAno.Sum(f => f.ValorGlosaInterna);
                ltlDeducoesTotal.Text = "(";
                ltlDeducoesTotal.Text += (receitasNoAno.Sum(R => R.ValorDeducao) / 1000).ToString("#,###,###,###0");
                ltlDeducoesTotal.Text += ")";
            }
        }

        private void PreecherTicketsImpostos()
        {
            for (int i = 1; i < 13; i++)
            {
                var ltlImpostosRB = (Literal)divDre.FindControl("ltlImpostosRB" + i.ToString());

                ltlImpostosRB.Text = String.Format("{0:n2}", TicketsDeImpostos.Sum(t => t.Parcelas.Where(p => p.Mes == (MesEnum)i).Sum(f => f.Valor))) + "%";
            }
            if (receitaBrutaTotal == 0)
                ltlImpostosRBTotal.Text = "0,00%";
            else
                ltlImpostosRBTotal.Text = String.Format("{0:n2}", ((impostosTotal / receitaBrutaTotal)) * 100) + "%";
        }

        private void PreecherImpostos()
        {
            TicketsDeReceita tickets = new TicketsDeReceita();
            TicketsDeImpostos = tickets.Obter(Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.Impostos);
            if (receitasNoAno != null)
            {
                foreach (var ticket in TicketsDeImpostos)
                {
                    ticket.CalcularGlosa(receitasNoAno);
                }
                for (int i = 1; i < 13; i++)
                {
                    var ltlImpostos = (Literal)divDre.FindControl("ltlImpostos" + i.ToString());

                    ltlImpostos.Text = "(";
                    ltlImpostos.Text += (receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorImpostos) / 1000).ToString("#,###,###,###0");
                    ltlImpostos.Text += ")";
                }
                impostosTotal = (receitasNoAno.Sum(f => f.ValorImpostos) / 1000);
                ltlImpostosTotal.Text = "(";
                ltlImpostosTotal.Text += impostosTotal.ToString("#,###,###,###0");
                ltlImpostosTotal.Text += ")";
            }
        }

        private void PreencherTitcketGlosaExterna()
        {
            for (int i = 1; i < 13; i++)
            {
                var tlGlosaExterna = (Literal)divDre.FindControl("ltlGlosaExternaRB" + i.ToString());

                tlGlosaExterna.Text = String.Format("{0:n2}", TicketsGlosaExterna.Sum(t => t.Parcelas.Where(p => p.Mes == (MesEnum)i).Sum(f => f.Valor))) + "%";
            }
            if (receitaBrutaTotal == 0)
                ltlGlosaExternaRBTotal.Text = "0,00%";
            else
                ltlGlosaExternaRBTotal.Text = String.Format("{0:n2}", ((glosaExternaTotal / receitaBrutaTotal) * 100)) + "%";
        }

        private void PreencherGlosaExterna()
        {
            TicketsDeReceita tickets = new TicketsDeReceita();
            TicketsGlosaExterna = tickets.Obter(Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.GlosaExterna);

            if (receitasNoAno != null)
            {
                foreach (var ticket in TicketsGlosaExterna)
                {
                    ticket.CalcularGlosa(receitasNoAno);
                }

                for (int i = 1; i < 13; i++)
                {
                    var ltlGlosaExterna = (Literal)divDre.FindControl("ltlGlosaExterna" + i.ToString());

                    ltlGlosaExterna.Text = "(";
                    ltlGlosaExterna.Text += (
                        receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorGlosaExterna) / 1000).ToString("#,###,###,###0");
                    ltlGlosaExterna.Text += ")";
                }

                glosaExternaTotal = (receitasNoAno.Sum(f => f.ValorGlosaExterna) / 1000);

                ltlGlosaExternaTotal.Text = "(";
                ltlGlosaExternaTotal.Text += glosaExternaTotal.ToString("#,###,###,###0");
                ltlGlosaExternaTotal.Text += ")";

            }
        }

        private void PreencherTicketGlosaInterna()
        {
            for (int i = 1; i < 13; i++)
            {
                var ltlGlosaInterna = (Literal)divDre.FindControl("ltlGlosaInternaRB" + i.ToString());

                ltlGlosaInterna.Text = String.Format("{0:n2}", TicketsGlosaInterna.Sum(t => t.Parcelas.Where(p => p.Mes == (MesEnum)i).Sum(f => f.Valor))) + "%";
            }

            if (receitaBrutaTotal == 0)
                ltlGlosaInternaRBTotal.Text = "0,00%";
            else
                ltlGlosaInternaRBTotal.Text = String.Format("{0:n2}", ((glosaInternaTotal / receitaBrutaTotal * 100))) + "%";
        }

        private void PreencherGlosaInterna()
        {
            TicketsDeReceita tickets = new TicketsDeReceita();
            TicketsGlosaInterna = tickets.Obter(Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.GlosaInterna);

            if (receitasNoAno != null)
            {
                foreach (var ticket in TicketsGlosaInterna)
                {
                    ticket.CalcularGlosa(receitasNoAno);
                }

                for (int i = 1; i < 13; i++)
                {
                    var ltlGlosaInterna = (Literal)divDre.FindControl("ltlGlosaInterna" + i.ToString());

                    ltlGlosaInterna.Text = "(";
                    ltlGlosaInterna.Text += (receitasNoAno.Where(p => p.Mes == (MesEnum)i).Sum(f => f.ValorGlosaInterna) / 1000).ToString("#,###,###,###0");
                    ltlGlosaInterna.Text += ")";
                }

                glosaInternaTotal = (receitasNoAno.Sum(f => f.ValorGlosaInterna) / 1000);
                ltlGlosaInternaTotal.Text = "(";
                ltlGlosaInternaTotal.Text += glosaInternaTotal.ToString("#,###,###,###0");
                ltlGlosaInternaTotal.Text += ")";
            }
        }

        private void PreencherReceitaBruta()
        {
            Orcamentos orcamentos = new Orcamentos();
            OrcamentosHospitalares = orcamentos.ObterOrcamentosHospitalaresFinais();

            if (OrcamentosHospitalares != null && OrcamentosHospitalares.Count > 0)
            {
                var tickets = new TicketsDeProducao();
                TicketsDeReceita ticketsDeReceita = new TicketsDeReceita();

                foreach (var oracamento in OrcamentosHospitalares)
                {
                    var ticketDeReceita = ticketsDeReceita.Obter(oracamento.Setor, TipoTicketDeReceita.ReajusteDeConvenios);
                    oracamento.CalcularReceitaLiquida(tickets.Todos(oracamento.Setor).ToList(), ticketDeReceita.Parcelas.ToList());
                }
                FatoresDeReceitaBruta = new List<FatorReceitaDTO>();

                foreach (var orcamento in OrcamentosHospitalares)
                {
                    foreach (var fatorReceita in orcamento.FatoresReceita)
                    {
                        FatorReceitaDTO fator = new FatorReceitaDTO();
                        fator.Incrementos = new List<IncrementoDaComplexidadeDTO>();

                        foreach (var item in fatorReceita.Incrementos)
                        {
                            if (fator.Incrementos.Any(i => i.Mes == item.Mes))
                                fator.Incrementos.Where(i => i.Mes == item.Mes).FirstOrDefault().ReceitaLiquida += item.ReceitaLiquida;
                            else
                                fator.Incrementos.Add(new IncrementoDaComplexidadeDTO() { Mes = item.Mes, ReceitaLiquida = item.ReceitaLiquida });
                        }

                        FatoresDeReceitaBruta.Add(fator);
                    }
                }

                for (int i = 1; i < 13; i++)
                {
                    var ltlReceitaBruta = (Literal)divDre.FindControl("ltlReceitaBruta" + i.ToString());

                    ltlReceitaBruta.Text = (FatoresDeReceitaBruta.Sum(f => f.Incrementos.Where(p => p.Mes == (MesEnum)i).Sum(incremento => incremento.ReceitaLiquida)) / 1000).ToString("#,###,###,###0");

                    receitasNoAno.Where(r => r.Mes == (MesEnum)i).FirstOrDefault().Valor =
                        FatoresDeReceitaBruta.Sum(
                            f =>
                            f.Incrementos.Where(p => p.Mes == (MesEnum)i).Sum(incremento => incremento.ReceitaLiquida));
                }

                receitaBrutaTotal = (FatoresDeReceitaBruta.Sum(f => f.Incrementos.Sum(incremento => incremento.ReceitaLiquida)) / 1000);

                ltlReceitaBrutaTotal.Text = receitaBrutaTotal.ToString("#,###,###,###0");
            }
            else
            {
                for (int i = 1; i < 13; i++)
                {
                    var ltlReceitaBruta = (Literal)divDre.FindControl("ltlReceitaBruta" + i.ToString());

                    ltlReceitaBruta.Text = "0";
                }

                ltlReceitaBrutaTotal.Text = "0";
            }

        }
    }
}