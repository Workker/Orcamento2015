using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DTO;
using Orcamento.Domain.Gerenciamento;
using System.Web.Services;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace WebSimuladorRH
{

    public class FatorReceitaDTO
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string SubSetor { get; set; }

        public virtual string Setor { get; set; }

        public virtual IList<IncrementoDaComplexidadeDTO> Incrementos { get; set; }
    }

    public class IncrementoDaComplexidadeDTO
    {
        public virtual int Id
        {
            get;
            set;
        }
        public virtual MesEnum Mes { get; set; }
        public virtual double ReceitaLiquida { get; set; }
    }

    public partial class OrcamentoDeProducao : BasePage, IViewOrcamentoDeProducao
    {
        private Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos;

        #region Implement View

        public OrcamentoDeProducaoController controller
        {
            get
            {
                return
                    (OrcamentoDeProducaoController)
                    (Session["OrcamentoDeProducaoController"] ?? new OrcamentoDeProducaoController(this));
            }

            set { Session["OrcamentoDeProducaoController"] = value; }
        }

        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoProducao"]; }
            set { Session["DepartamentoProducao"] = value; }
        }

        public Orcamento.Domain.Orcamento Orcamento
        {
            get { return (Orcamento.Domain.Orcamento)Session["OrcamentoDeProducao"]; }
            set { Session["OrcamentoDeProducao"] = value; }
        }

        public List<ContaHospitalarDTO> Contas
        {
            get { return (List<ContaHospitalarDTO>)Session["ContasHospitalares"]; }
            set
            {
                Session["ContasHospitalares"] = value;
                PreencherContas();
                PreencherComplexidade();
                PreencherUnitarios();
                PreencherReceitaBruta();
            }
        }

        public void IrParaOrcamentosDeProducao()
        {
            List<Orcamento.Domain.Orcamento> listaOrcamentos =
                new Orcamento.Domain.DB.Repositorio.Orcamentos().TodosOrcamentosHospitalares(Departamento);


            if (listaOrcamentos != null && listaOrcamentos.Any() && !listaOrcamentos.Any(o => o.VersaoFinal))
                Response.Redirect("/OrcamentosDeProducao.aspx?final=0");
            else
                Response.Redirect("/OrcamentosDeProducao.aspx");
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            VerificarSeUsuarioHospitalar();
            var departamentos = new Departamentos();
            Departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);
            ltNomehospital.Text = Departamento.Nome;
            controller.View = this;
            ConfigurarVersaoFinal();

            if (!Page.IsPostBack)
            {
                controller = new OrcamentoDeProducaoController(this);
                controller.PreencherOrcamentos();
                DesabilitarBotoes();
                HabilitarControlesDevisualizacaodaPagina();
            }
        }

        protected void SelecionandoVersoesDeOrcamento(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelecionarVersao")
            {
                int orcamentoId = int.Parse(e.CommandArgument.ToString());
                controller.CarregarOrcamento(orcamentoId);
                ConfigurarVersaoFinal();
                HabilitarBotoes();
                ExibirDivFlutuanteDeCalculo();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarOrcamento(true);
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        protected void btnClonar_Click(object sender, EventArgs e)
        {
            try
            {
                controller.Clonar();
                HabilitarBotoes();
                ExibirDivFlutuanteDeCalculo();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        protected void VersaoFinal_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarOrcamento(false);
                controller.AtribuirVersaoFinal();
                ConfigurarVersaoFinal();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        protected void ApagarVersao_Click(object sender, EventArgs e)
        {

            try
            {
                controller.ApagarVersao();
                DesabilitarBotoes();

                ZerarRepeaters();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();

        }

        //protected void btnCalcularTotalMensal_Click(object sender, EventArgs e)
        //{
        //    List<ContaHospitalarDTO> contas = PreencherServicosHospitalares();
        //    PreencherComplexidadeNoOrcamento();
        //    preencherOrcamento(Orcamento, contas);
        //    var tickets = new TicketsDeProducao();
        //    TicketsDeReceita ticketsDeReceita = new TicketsDeReceita();
        //    var ticketDeReceita = ticketsDeReceita.Obter(this.Departamento, TipoTicketDeReceita.ReajusteDeConvenios);

        //    Orcamento.CalcularReceitaLiquida(tickets.Todos(Orcamento.Setor).ToList(),ticketDeReceita.Parcelas.ToList());
        //    var lista = new List<TotalizadorCalculo>();

        //    for (int i = 1; i < 13; i++)
        //    {
        //        lista.Add(new TotalizadorCalculo { Mes = (MesEnum)i, Valor = 0 });
        //    }

        //    foreach (FatorReceita fator in Orcamento.FatoresReceita)
        //    {
        //        foreach (IncrementoDaComplexidade incremento in fator.Incrementos)
        //        {
        //            lista.Where(l => l.Mes == incremento.Mes).FirstOrDefault().Valor += incremento.ReceitaLiquida;
        //        }
        //    }
        //    for (int i = 1; i < 13; i++)
        //    {
        //        var literalTotalMensal = (Literal)tabelaDeRodapeFixo.FindControl("ltlTotalMes" + i.ToString());

        //        literalTotalMensal.Text = lista.Where(l => l.Mes == (MesEnum)i).FirstOrDefault().Valor.ToString("#,###,###,###0.##");
        //    }

        //    var totalAno = (Literal)tabelaDeRodapeFixo.FindControl("ltlTotalAno");

        //    totalAno.Text = lista.Sum(l => l.Valor).ToString("#,###,###,###0.##");
        //}

        protected void PreencherComplexidade(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var incrementoDaComplexidade = (IncrementoDaComplexidade)e.Item.DataItem;
                var txtComplexidade = (TextBox)e.Item.FindControl("Complexidade");

                if (incrementoDaComplexidade.Negativo)
                    txtComplexidade.Text += "-";

                txtComplexidade.Text += incrementoDaComplexidade.Complexidade.ToString("#,###,###,###0.##");

                txtComplexidade.Text += "%";
            }
        }

        [WebMethod]
        public static void InformarMemoriaDeCalculoComplexidade(string parametro, string texto)
        {
            try
            {
                int idOrcamento = 0;
                if (Int32.TryParse(texto, out idOrcamento))
                {
                    var orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
                    var orcamento = orcamentos.Obter<Orcamento.Domain.Orcamento>(idOrcamento);
                    orcamento.MemoriaDeCalculoComplexidade = parametro;

                    orcamentos.Salvar(orcamento);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [WebMethod]
        public static void InformarMemoriaDeCalculoUnitario(string parametro, string texto)
        {
            try
            {
                int idOrcamento = 0;
                if (Int32.TryParse(texto, out idOrcamento))
                {
                    var orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
                    var orcamento = orcamentos.Obter<Orcamento.Domain.Orcamento>(idOrcamento);
                    orcamento.MemoriaDeCalculoUnitarios = parametro;

                    orcamentos.Salvar(orcamento);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void CarregarTotalizadorComplexidade(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                var txtMemoriaDeCalculo = (TextBox)e.Item.FindControl("txtMemoriaDeCalculo");
                txtMemoriaDeCalculo.Text = Orcamento.MemoriaDeCalculoComplexidade;

                var lknOrcamento = (Button)e.Item.FindControl("lknOrcamento");

                lknOrcamento.Attributes.Add("onclick", "Carregar('" + txtMemoriaDeCalculo.ClientID +
                                                                             "', " + Orcamento.Id + ");");

                if (!string.IsNullOrEmpty(txtMemoriaDeCalculo.Text))
                    ((Image)(e.Item.FindControl("imgInformation"))).Attributes.Add("style", "display: inline;");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtTotal = (TextBox)e.Item.FindControl("txtTotal");
                var fatorReceita = (FatorReceita)e.Item.DataItem;

                double total = 0;
                foreach (var incremento in fatorReceita.Incrementos)
                {
                    if (incremento.Negativo)
                        total -= incremento.Complexidade;
                    else
                        total += incremento.Complexidade;
                }

                if (total != 0)
                    total = total / 12;

                //if (total < 0)
                //    txtTotal.Text = "-";

                txtTotal.Text += total.ToString("#,###,###,###0.##");
                txtTotal.Text += "%";
            }
        }

        protected void TotalizarMedia(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Header)
            {
                var txtMemoriaDeCalculo = (TextBox)e.Item.FindControl("txtMemoriaDeCalculo");
                txtMemoriaDeCalculo.Text = Orcamento.MemoriaDeCalculoUnitarios;

                var lknOrcamento = (Button)e.Item.FindControl("lknOrcamento");

                lknOrcamento.Attributes.Add("onclick", "CarregarUnitario('" + txtMemoriaDeCalculo.ClientID +
                                                                             "', " + Orcamento.Id + ");");

                if (!string.IsNullOrEmpty(txtMemoriaDeCalculo.Text))
                    ((Image)(e.Item.FindControl("imgInformation"))).Attributes.Add("style", "display: inline;");
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var repeaterValores = (Repeater)e.Item.FindControl("rptValoresDosSetores");

                double valorMedia = 0;
                bool porcentagem = false;

                foreach (RepeaterItem item in repeaterValores.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var Hidenporcentagem = (HiddenField)item.FindControl("tipoValor");

                        porcentagem = (Hidenporcentagem.Value == TipoValorContaEnum.Porcentagem.ToString());
                        var txtValor = (TextBox)item.FindControl("txtValor");
                        var valor = txtValor.Text.Replace("%", "").Replace(".", "");

                        valorMedia += double.Parse(valor);

                    }
                }

                var label = (Label)e.Item.FindControl("valorDespesa");
                valorMedia = (valorMedia / 12);

                label.Text = valorMedia.ToString("#,###,###,###0.##");
                if (porcentagem)
                    label.Text += "%";

            }
        }

        protected void CarregandoValoresDosSetores(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((ValorContaDTO)e.Item.DataItem).TipoValor == TipoValorContaEnum.Porcentagem)
                {
                    ((TextBox)e.Item.FindControl("txtValor")).Text += "%";
                    ((TextBox)e.Item.FindControl("txtValor")).Attributes.Add("onblur", "AplicarMascaraDePorcentagemNoValorDoCampo(this);");
                    ((TextBox)e.Item.FindControl("txtValor")).Attributes.Add("onkeydown", "PermitirSomenteADigitacaoDeNumerosComVirgulaESinalNegativo(event);");
                }
                else
                {
                    ((TextBox)e.Item.FindControl("txtValor")).Attributes.Add("onkeyup", "InsereMascaraDeMilhar(this);");
                    ((TextBox)e.Item.FindControl("txtValor")).Attributes.Add("onkeydown", "PermitirSomenteADigitacaoDeNumerosComCopiarEColar(event);");
                }
            }
        }

        protected void CarregarValores(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ((TextBox)e.Item.FindControl("txtValor")).Attributes.Add("onblur",
                                                                          "AplicarMascaraDePorcentagemNoValorDoCampo(this);");
            }
        }

        protected void TotalReceitaBruta(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var id = (HiddenField)e.Item.FindControl("Id");
                var SubSetor = (HiddenField)e.Item.FindControl("SubSetor");

                if (SubSetor.Value == "Centro Cirúrgico")
                    ((Literal)e.Item.FindControl("ltlTotal")).Text =
                    Orcamento.FatoresReceita.Where(f => f.Setor.Id == int.Parse(id.Value) && f.SubSetor.NomeSetor == "Centro Cirúrgico").Sum(i => i.Incrementos.Sum(u => u.ReceitaLiquida)).ToString("#,###,###,###0.##");
                else if (SubSetor.Value == "Centro Obstétrico")
                    ((Literal)e.Item.FindControl("ltlTotal")).Text =
                    Orcamento.FatoresReceita.Where(f => f.Setor.Id == int.Parse(id.Value) && f.SubSetor.NomeSetor == "Centro Obstétrico").Sum(i => i.Incrementos.Sum(u => u.ReceitaLiquida)).ToString("#,###,###,###0.##");
                else
                    ((Literal)e.Item.FindControl("ltlTotal")).Text =
                   Orcamento.FatoresReceita.Where(f => f.Setor.Id == int.Parse(id.Value)).Sum(i => i.Incrementos.Sum(u => u.ReceitaLiquida)).ToString("#,###,###,###0.##");

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var lista = new List<TotalizadorCalculo>();

                for (int i = 1; i < 13; i++)
                {
                    lista.Add(new TotalizadorCalculo { Mes = (MesEnum)i, Valor = 0 });
                }

                foreach (FatorReceita fator in Orcamento.FatoresReceita)
                {
                    foreach (IncrementoDaComplexidade incremento in fator.Incrementos)
                    {
                        lista.Where(l => l.Mes == incremento.Mes).FirstOrDefault().Valor += incremento.ReceitaLiquida;
                    }
                }
                for (int i = 1; i < 13; i++)
                {
                    var literalTotalMensal = (Literal)e.Item.FindControl("ltlTotalMes" + i.ToString());

                    literalTotalMensal.Text = lista.Where(l => l.Mes == (MesEnum)i).FirstOrDefault().Valor.ToString("#,###,###,###0.##");
                }

                var totalAno = (Literal)e.Item.FindControl("ltlTotalAno");

                totalAno.Text = lista.Sum(l => l.Valor).ToString("#,###,###,###0.##");
            }
        }

        protected void IncluindoNovaVersao(object sender, EventArgs e)
        {
            controller.CriarNovoOrcamentoOperacional(Departamento);
            HabilitarBotoes();
            ExibirDivFlutuanteDeCalculo();
        }

        #endregion

        #region Métodos

        public List<Orcamento.Domain.Orcamento> Orcamentos
        {
            get { return (List<Orcamento.Domain.Orcamento>)Session["OrcamentosDeProducao"]; }
            set { Session["OrcamentosDeProducao"] = value; }
        }

        public void CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao()
        {
            btnIncluirNovaVersao.Attributes.Add("onclick",
                                                "alert('Não é possível criar mais de 10 versões'); return false; ");
        }

        public void RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao()
        {
            btnIncluirNovaVersao.Attributes.Remove("onclick");
        }

        public void PreencherOrcamentos()
        {
            rptOrcamentosDeProducao.DataSource = ObterOrcamentos();
            rptOrcamentosDeProducao.DataBind();

            rptOrcamentosDeProducao.Visible = (Orcamentos != null && Orcamentos.Count > 0);
        }

        public void PreencherReceitaBruta()
        {
            var tickets = new TicketsDeProducao();

            TicketsDeReceita ticketsDeReceita = new TicketsDeReceita();
            var ticketDeReceita = ticketsDeReceita.Obter(this.Departamento, TipoTicketDeReceita.ReajusteDeConvenios);

            Orcamento.CalcularReceitaLiquida(tickets.Todos(Orcamento.Setor).ToList(), ticketDeReceita.Parcelas.ToList());

            List<FatorReceitaDTO> fatoresDeReceita = new List<FatorReceitaDTO>();

            foreach (var fatorReceita in Orcamento.FatoresReceita.Where(g => g.Setor.NomeSetor != "Centro Cirúrgico").GroupBy(f => f.Setor))
            {

                var fator = new FatorReceitaDTO { Setor = fatorReceita.Key.NomeSetor, SubSetor = fatorReceita.Key.NomeSetor, Id = fatorReceita.Key.Id };



                fator.Incrementos = new List<IncrementoDaComplexidadeDTO>();

                foreach (var item in fatorReceita.Select(f => f.Incrementos).GroupBy(i => i.GroupBy(b => b.Mes)))
                {
                    foreach (var item1 in item)
                    {
                        foreach (var item2 in item1)
                        {
                            if (fator.Incrementos.Any(i => i.Mes == item2.Mes))
                                fator.Incrementos.Where(i => i.Mes == item2.Mes).FirstOrDefault().ReceitaLiquida += item2.ReceitaLiquida;
                            else
                                fator.Incrementos.Add(new IncrementoDaComplexidadeDTO() { Mes = item2.Mes, ReceitaLiquida = item2.ReceitaLiquida });
                        }
                    }
                }

                fatoresDeReceita.Add(fator);
            }

            foreach (var fatorReceita in Orcamento.FatoresReceita.Where(g => g.Setor.NomeSetor == "Centro Cirúrgico"))
            {
                var fator = new FatorReceitaDTO { Setor = fatorReceita.Setor.NomeSetor, SubSetor = fatorReceita.SubSetor.NomeSetor, Id = fatorReceita.Setor.Id };

                fator.Incrementos = new List<IncrementoDaComplexidadeDTO>();
                foreach (var item in fatorReceita.Incrementos)
                {
                    fator.Incrementos.Add(new IncrementoDaComplexidadeDTO() { Mes = item.Mes, ReceitaLiquida = item.ReceitaLiquida });

                }

                fatoresDeReceita.Add(fator);
            }

            rptReceitaBruta.DataSource = fatoresDeReceita.OrderBy(x => x.Setor).ThenBy(y => y.SubSetor);
            rptReceitaBruta.DataBind();
        }

        public void InformarNaoExisteVersaoFinal()
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Key",
                                                        "alert('Existe versão gravada porém nenhuma apontada como versão final.');",
                                                        true);
        }

        private List<object> ObterOrcamentos()
        {
            var objetos = new List<object>();

            orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
            Orcamentos = orcamentos.TodosOrcamentosHospitalares(Departamento);

            if (Orcamentos != null && Orcamentos.Count() > 0 && !Orcamentos.Any(o => o.VersaoFinal))
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "Key",
                                                            "alert('Existe versão gravada porém nenhuma apontada como versão final.');",
                                                            true);


            foreach (Orcamento.Domain.Orcamento orcamento in Orcamentos)
            {
                orcamento.CalcularTotalDRE();

                var objeto =
                    new
                        {
                            orcamento.Id,
                            Versao = orcamento.VersaoFinal ? "Versão Final" : orcamento.NomeOrcamento,
                            Total = orcamento.ValorTotalDRE
                        };
                objetos.Add(objeto);
            }

            return objetos;
        }

        private void HabilitarBotoes()
        {
            divBotaoSalvar.Visible = true;
            divBotaoClonar.Visible = true;
            divBotaoApagandoVersao.Visible = true;
            divBotaoVersaoFinal.Visible = true;
            ltlVersao.Visible = true;
            versaoDoOrcamento.Visible = true;
            ConfigurarVersaoFinal();
        }

        private void DesabilitarBotoes()
        {
            divBotaoSalvar.Visible = false;
            divBotaoClonar.Visible = false;
            divBotaoApagandoVersao.Visible = false;
            divBotaoVersaoFinal.Visible = false;
            ltlVersao.Visible = false;
            versaoDoOrcamento.Visible = false;
        }

        private void ConfigurarVersaoFinal()
        {
            if (Orcamento != null)
                versaoDoOrcamento.Text = Orcamento.VersaoFinal ? "Versão Final" : Orcamento.NomeOrcamento;
        }

        private void HabilitarControlesDevisualizacaodaPagina()
        {
            divBotaoIncluirNovaVersao.Visible = true;
        }

        private void PreencherContas()
        {
            rptUtiUnidades.DataSource = Contas.OrderBy(x => x.Setor).ThenByDescending(c => c.Subsetor);
            rptUtiUnidades.DataBind();
        }

        private void PreencherComplexidade()
        {
            Repeater1.DataSource = Orcamento.FatoresReceita.OrderBy(x => x.Setor.NomeSetor).ThenBy(y => y.SubSetor.NomeSetor);
            Repeater1.DataBind();
        }

        private void PreencherUnitarios()
        {
            var tickets = new TicketsDeProducao();
            List<Orcamento.Domain.TicketDeProducao> ticketsDeHospital = tickets.Todos(Orcamento.Setor).ToList();
            RptUnitarios.DataSource = ticketsDeHospital.OrderBy(x => x.Setor.NomeSetor).ThenBy(y => y.SubSetor.NomeSetor);
            RptUnitarios.DataBind();
        }

        private void SalvarOrcamento(bool carregaOrcamentos)
        {
            List<ContaHospitalarDTO> contas = PreencherServicosHospitalares();
            PreencherComplexidadeNoOrcamento();
            controller.SalvarOrcamento(contas, carregaOrcamentos);
        }

        private void PreencherComplexidadeNoOrcamento()
        {
            foreach (RepeaterItem fatorReceita in Repeater1.Items)
            {
                var rptIncrementos = (Repeater)fatorReceita.FindControl("rptIncrementos");
                var idFator = (HiddenField)fatorReceita.FindControl("Id");


                foreach (RepeaterItem repeaterComplexidade in rptIncrementos.Items)
                {
                    var incrementoId = (HiddenField)repeaterComplexidade.FindControl("Id");
                    var txtcomplexidade = (TextBox)repeaterComplexidade.FindControl("Complexidade");
                    var mes = (HiddenField)repeaterComplexidade.FindControl("Mes");

                    IncrementoDaComplexidade incremento =
                        Orcamento.FatoresReceita.Where(f => f.Id == int.Parse(idFator.Value)).FirstOrDefault().
                            Incrementos.Where(i => i.Id == int.Parse(incrementoId.Value)).FirstOrDefault();
                    float complexidade = 0;

                    incremento.Negativo = txtcomplexidade.Text.Substring(0, 1) == "-";
                    if (float.TryParse(txtcomplexidade.Text.Replace("%", "").Replace(".", "").Replace("-", ""), out complexidade))
                        incremento.Complexidade = complexidade;
                }
            }
        }

        private List<ContaHospitalarDTO> PreencherServicosHospitalares()
        {
            var contas = new List<ContaHospitalarDTO>();

            foreach (RepeaterItem despesaOperacional in rptUtiUnidades.Items)
            {
                var rptValoresDosSetores = (Repeater)despesaOperacional.FindControl("rptValoresDosSetores");
                var IdServico = (HiddenField)despesaOperacional.FindControl("IdServico");

                var conta = new ContaHospitalarDTO();
                conta.IdServico = Convert.ToInt32(IdServico.Value);
                conta.Valores = new List<ValorContaDTO>();

                foreach (RepeaterItem repeaterDespesa in rptValoresDosSetores.Items)
                {
                    var valorDTO = new ValorContaDTO();
                    var valorID = (HiddenField)repeaterDespesa.FindControl("ValorID");
                    var calculado = (HiddenField)repeaterDespesa.FindControl("Calculado");
                    var textValor = (TextBox)repeaterDespesa.FindControl("txtValor");
                    var mes = (HiddenField)repeaterDespesa.FindControl("Mes");
                    var contaId = (HiddenField)repeaterDespesa.FindControl("contaId");

                    valorDTO.ValorID = Convert.ToInt32(valorID.Value);
                    valorDTO.Mes = (MesEnum)Enum.Parse(typeof(MesEnum), mes.Value);
                    conta.ContaID = int.Parse(contaId.Value);

                    if (!Convert.ToBoolean(calculado.Value))
                    {
                        double valor = 0;
                        if (double.TryParse(textValor.Text.Replace("%", "").Replace(".", ""), out valor))
                            valorDTO.Valor = valor;
                    }
                    else
                    {
                        var repositorio = new Contas();
                        var contaObtida = repositorio.Obter<ContaHospital>(conta.ContaID);
                        List<int> anexos = contaObtida.ContasAnexadas.Select(c => c.Id).ToList();
                        IEnumerable<IEnumerable<ValorContaDTO>> valoresCalculados =
                            (contas.Where(cs => anexos.Any(a => a == cs.ContaID)).Select(
                                c => c.Valores.Where(v => v.Mes == (MesEnum)Enum.Parse(typeof(MesEnum), mes.Value))).
                                Select(s => s.Select(sv => sv)));

                        if (valoresCalculados.ToList().Count > 2)
                        {
                            if (valoresCalculados.ToList()[2].FirstOrDefault().Valor != 0 &&
      valoresCalculados.ToList()[3].FirstOrDefault().Valor != 0)
                                valorDTO.Valor = valoresCalculados.ToList()[2].FirstOrDefault().Valor /
                                                 valoresCalculados.ToList()[3].FirstOrDefault().Valor;
                            else
                                valorDTO.Valor = 0;
                        }
                        else
                        {
                            if (valoresCalculados.FirstOrDefault().FirstOrDefault().Valor != 0 &&
                                valoresCalculados.ToList()[1].FirstOrDefault().Valor != 0)
                                valorDTO.Valor = valoresCalculados.FirstOrDefault().FirstOrDefault().Valor /
                                                 valoresCalculados.ToList()[1].FirstOrDefault().Valor;
                            else
                                valorDTO.Valor = 0;
                        }
                    }
                    conta.Valores.Add(valorDTO);
                }
                contas.Add(conta);
            }
            return contas;
        }

        private void ZerarRepeaters()
        {
            rptUtiUnidades.DataSource = null;
            rptUtiUnidades.DataBind();

            Repeater1.DataSource = null;
            Repeater1.DataBind();

            RptUnitarios.DataSource = null;
            RptUnitarios.DataBind();

            rptReceitaBruta.DataSource = null;
            rptReceitaBruta.DataBind();

            OcultarDivFlutuanteDeCalculo();
        }

        public void preencherOrcamento(Orcamento.Domain.Orcamento orcamento, List<ContaHospitalarDTO> contas)
        {
            foreach (ServicoHospitalar servico in orcamento.Servicos)
            {
                foreach (ProducaoHospitalar valor in servico.Valores)
                {
                    valor.Valor =
                        contas.Where(c => c.IdServico == servico.Id).FirstOrDefault().Valores.Where(
                            t => t.Mes == valor.Mes).FirstOrDefault().Valor;
                }
            }
        }

        private void OcultarDivFlutuanteDeCalculo()
        {
            espacamento.Visible = false;
           // footer.Visible = false;
        }

        private void ExibirDivFlutuanteDeCalculo()
        {
            espacamento.Visible = true;
          //  footer.Visible = true;
        }

        #endregion
    }

    public class TotalizadorCalculo
    {
        public double Valor { get; set; }
        public MesEnum Mes { get; set; }
    }
}