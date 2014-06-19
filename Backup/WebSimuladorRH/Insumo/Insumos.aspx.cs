using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain;
using Orcamento.Domain.DTO;
using Orcamento.Domain.Servico;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH.Insumo
{
    public partial class Insumos : BasePage
    {

        public List<ValorContaDTO> valores;
        public List<ContaHospitalarDTO> ContasUnitarias
        {
            get { return (List<ContaHospitalarDTO>)Session["ContasHospitalaresInsumos"]; }
            set
            {
                Session["ContasHospitalaresInsumos"] = value;
            }
        }

        public List<ContaHospitalarDTO> ContasCustoUnitarios
        {
            get { return (List<ContaHospitalarDTO>)Session["ContasCustoHospitalaresInsumos"]; }
            set
            {
                Session["ContasCustoHospitalaresInsumos"] = value;
            }
        }

        public Orcamento.Domain.Orcamento Orcamento
        {
            get { return (Orcamento.Domain.Orcamento)Session["OrcamentoVersaoFinal"]; }
            set { Session["OrcamentoVersaoFinal"] = value; }
        }

        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoProducao"]; }
            set { Session["DepartamentoProducao"] = value; }
        }

        public Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.Insumo Insumo
        {
            get { return (Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.Insumo)Session["Insumo"]; }
            set { Session["Insumo"] = value; }
        }

        public ServicoMapperOrcamentoView ServicoMapperOrcamentoView { get { return new ServicoMapperOrcamentoView(); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            VerificarSeUsuarioHospitalar();
            if (!Page.IsPostBack)
            {
                var departamentos = new Departamentos();
                Departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);

                Orcamento.Domain.DB.Repositorio.Insumos insumos = new global::Orcamento.Domain.DB.Repositorio.Insumos();
                Insumo = insumos.ObterInsumo(Departamento);

                PreencherCustoUnitario();

                Orcamentos orcamentos = new Orcamentos();
                Orcamento = orcamentos.ObterOrcamentoHospitalarFinal(Departamento);

                if (Orcamento != null)
                {
                    this.ContasUnitarias = ServicoMapperOrcamentoView.TransformarProducaoDeInsumos(
                        Orcamento.Servicos.Where(s => s.Conta.Nome != "Salas" && s.Conta.TipoValorContaEnum == TipoValorContaEnum.Quantidade && s.Conta.Calculado == false).ToList(),
                        Orcamento.Servicos.Where(s => s.Conta.TipoValorContaEnum ==  TipoValorContaEnum.Porcentagem).ToList());

                    PreecherUnitarios();
                    PreencherCustoHospitalar();
                }
            }

        }

        private void PreencherCustoHospitalar()
        {
            TicketsDeReceita tickets = new TicketsDeReceita();
            var ticketsDeReceita = tickets.Todos(Departamento);

            this.Orcamento.CalcularCustoHospitalar(ticketsDeReceita.Where(t => t.TipoTicket == global::Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.ReajusteDeInsumos).FirstOrDefault(),
               Insumo.CustosUnitarios.ToList(), this.ContasUnitarias);

            rptCustoUnitarioTotal.DataSource = ServicoMapperOrcamentoView.TransformarProducao(Orcamento.CustosUnitariosTotal.ToList()).OrderBy(c => c.Setor).ThenBy(s => s.Subsetor); ;
            rptCustoUnitarioTotal.DataBind();
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

        private void PreencherCustoUnitario()
        {
            this.ContasCustoUnitarios = ServicoMapperOrcamentoView.TransformarProducao(Insumo.CustosUnitarios.ToList());

            rptCustoUnitario.DataSource = ContasCustoUnitarios.OrderBy(c => c.Setor).ThenBy(s => s.Subsetor);
            rptCustoUnitario.DataBind();
        }

        private void PreecherUnitarios()
        {
            rptUtiUnidades.DataSource = ContasUnitarias.OrderBy(c => c.Setor).ThenBy(s => s.Subsetor);
            rptUtiUnidades.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarOrcamento(true);
                PreencherCustoUnitario();
                if (Orcamento != null)
                {
                    PreecherUnitarios();
                    PreencherCustoHospitalar();
                }

            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        private List<ContaHospitalarDTO> PreencherCustoUnitarioDTO()
        {
            var contas = new List<ContaHospitalarDTO>();

            foreach (RepeaterItem despesaOperacional in rptCustoUnitario.Items)
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
                        if (double.TryParse(textValor.Text.Replace("%", "").Replace(".", "").Replace("(", "").Replace(")", ""), out valor))
                            valorDTO.Valor = valor;
                    }
                    else
                    {
                        var repositorio = new Contas();
                        var contaObtida = repositorio.Obter<ContaHospital>(conta.ContaID);
                        List<int> anexos = contaObtida.ContasAnexadas.Select(c => c.Id).ToList();
                        IEnumerable<IEnumerable<ValorContaDTO>> valoresCalculados =
                            (contas.Where(cs => anexos.Exists(a => a == cs.ContaID)).Select(
                                c => c.Valores.Where(v => v.Mes == (MesEnum)Enum.Parse(typeof(MesEnum), mes.Value))).
                                Select(s => s.Select(sv => sv)));

                        if (valoresCalculados.FirstOrDefault().FirstOrDefault().Valor != 0 &&
                            valoresCalculados.ToList()[1].FirstOrDefault().Valor != 0)
                            valorDTO.Valor = valoresCalculados.FirstOrDefault().FirstOrDefault().Valor /
                                             valoresCalculados.ToList()[1].FirstOrDefault().Valor;
                        else
                            valorDTO.Valor = 0;
                    }
                    conta.Valores.Add(valorDTO);
                }
                contas.Add(conta);
            }
            return contas;
        }

        private void SalvarOrcamento(bool carregaOrcamentos)
        {
            var contas = PreencherCustoUnitarioDTO();
            ServicoMapperOrcamentoView.Salvar(Insumo, contas, true);
        }

        protected void TotalizarMediaUnitaria(object sender, RepeaterItemEventArgs e)
        {

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
                        var txtValor = (Literal)item.FindControl("ltlValorMes");
                        var valor = txtValor.Text.Replace("%", "").Replace(".", "").Replace("(", "").Replace(")", "");

                        valorMedia += double.Parse(valor);

                    }
                }

                var label = (Label)e.Item.FindControl("ltlValorMedia");
                valorMedia = (valorMedia / 12);

                label.Text = valorMedia.ToString("#,###,###,###0.##");
                if (porcentagem)
                    label.Text += "%";

            }
        }

        protected void TotalizarMedia(object sender, RepeaterItemEventArgs e)
        {

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
                        var valor = txtValor.Text.Replace("%", "").Replace(".", "").Replace("(", "").Replace(")", "");

                        valorMedia += double.Parse(valor);

                    }
                }

                var label = (Label)e.Item.FindControl("valorDespesa");
                valorMedia = (valorMedia / 12);

                label.Text = "(";
                label.Text += valorMedia.ToString("#,###,###,###0.##");
                label.Text += ")";

            }
        }


        protected void TotalCustoUnitario(object sender, RepeaterItemEventArgs e)
        {


        }

        protected void TotalMesCustoUnitario(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var contaHospitalar = (ContaHospitalarDTO)e.Item.DataItem;

                ((Literal)e.Item.FindControl("ltlTotal")).Text = "(";
                ((Literal)e.Item.FindControl("ltlTotal")).Text += contaHospitalar.Valores.Sum(v => v.Valor).ToString("#,###,###,###0.##");
                ((Literal)e.Item.FindControl("ltlTotal")).Text += ")";
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                var lista = new List<TotalizadorCalculo>();

                for (int i = 1; i < 13; i++)
                {
                    lista.Add(new TotalizadorCalculo { Mes = (MesEnum)i, Valor = 0 });
                }

                foreach (var custo in Orcamento.CustosUnitariosTotal)
                {
                    foreach (var valor in custo.Valores)
                    {
                        lista.Where(l => l.Mes == valor.Mes).FirstOrDefault().Valor += valor.Valor;
                    }
                }
                for (int i = 1; i < 13; i++)
                {
                    var literalTotalMensal = (Literal)e.Item.FindControl("ltlTotalMes" + i.ToString());

                    literalTotalMensal.Text = "(";
                    literalTotalMensal.Text += lista.Where(l => l.Mes == (MesEnum)i).FirstOrDefault().Valor.ToString("#,###,###,###0.##");
                    literalTotalMensal.Text += ")";
                }

                var totalAno = (Literal)e.Item.FindControl("ltlTotalAno");

                totalAno.Text = "(";
                totalAno.Text += lista.Sum(l => l.Valor).ToString("#,###,###,###0.##");
                totalAno.Text += ")";
            }
        }
    }
}