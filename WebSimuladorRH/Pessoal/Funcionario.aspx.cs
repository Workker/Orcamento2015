using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using System.Linq;
using System.Linq.Expressions;
using Orcamento.Domain.DB.Repositorio;
using System.Collections.Generic;
using Orcamento.Domain.DB.Mappers;
using System.Web.Services;
using System.Web;

namespace WebSimuladorRH.Pessoal
{
    public partial class Funcionario : BasePage
    {
        #region Atributos

        private FuncionarioController _controller;

        public List<FuncionarioDTO> Funcionarios
        {
            get { return (List<FuncionarioDTO>)Session["Funcionarios"]; }
            set { Session["Funcionarios"] = value; }
        }

        public int Aumento
        {
            get { return (int)Session["Aumento"]; }
            set { Session["Aumento"] = value; }
        }

        public string Justificativa
        {
            get { return (string)Session["Justificativa"]; }
            set { Session["Justificativa"] = value; }
        }

        #endregion

        #region  Propriedades

        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoFuncionario"]; }
            set { Session["DepartamentoFuncionario"] = value; }
        }

        public Orcamento.Domain.Gerenciamento.CentroDeCusto centroDeCusto
        {
            get { return (Orcamento.Domain.Gerenciamento.CentroDeCusto)Session["CentroCustoFuncionario"]; }
            set { Session["CentroCustoFuncionario"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new FuncionarioController();
            //DivVaga.Visible = txtNome.SelectedItem.Value == "Reposição de Vaga";
            if (IsPostBack) return;

            var departamentoId = int.Parse(Session["DepartamentoLogadoId"].ToString());

            Departamento = _controller.ObterDepartamentoPor(departamentoId);

            CarregarComboDeCentrosDeCusto(departamentoId);
        }

        private void CarregarComboDeCentrosDeCusto(int departamentoId)
        {
            ddlCentrosDeCusto.DataSource = _controller.ObterCentrosDeCustoPor(departamentoId).OrderBy(x => x.Nome);
            ddlCentrosDeCusto.DataTextField = "Nome";
            ddlCentrosDeCusto.DataValueField = "CodigoDoCentroDeCusto";
            ddlCentrosDeCusto.DataBind();

            ddlCentrosDeCusto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void Dll_Motivo(object sender, EventArgs e)
        {
            DivVaga.Visible = txtNome.SelectedItem.Value == "Reposição de Vaga";
            if (txtNome.SelectedItem.Value == "Reposição de Vaga")
            {
                if (centroDeCusto.Funcionarios == null ||
                    centroDeCusto.Funcionarios.Where(d => d.Departamento.Id == Departamento.Id).Count() == 0 ||
                    centroDeCusto.Funcionarios.Where(f => f.Departamento.Id == Departamento.Id && f.Demitido).Count() == 0 ||
                    centroDeCusto.Funcionarios.Where(f => f.Departamento.Id == Departamento.Id && f.Demitido && (f.FuncionarioReposicao == null || f.FuncionarioReposicao == 0)).Count() == 0)
                {
                    Notificacao = "Não existe vaga disponível para reposição neste centro de custo";
                    EnviarMensagemScriptManager();
                    return;
                }

            }
        }

        public void EnviarMensagemScriptManager()
        {
            if (!string.IsNullOrEmpty(Notificacao))
            {
                Page P = HttpContext.Current.Handler as Page;
                ScriptManager.RegisterClientScriptBlock(P, P.GetType(), Guid.NewGuid().ToString(),
                                                      string.Format(
                    //  "<script type=\"text/javascript\">alert('{0}');</script>",
                                                         "alert('{0}');",
                                                          Notificacao), true);
            }
        }

        protected void Dll_DataAdmissaoChange(object sender, EventArgs e)
        {
            if (DivVaga.Visible == true)
            {
                //if (((DropDownList)sender).SelectedValue == string.Empty)
                //{
                //    ddlVaga.DataSource = null;
                //    ddlVaga.DataBind();
                //    return;
                //}

                if (ddlCentrosDeCusto.SelectedValue == "0")
                {
                    Notificacao = "É obrigatório selecionar um centro de custo.";
                    EnviarMensagem();
                    return;
                }

                centroDeCusto =
                      _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);


                var funcionariosReposicao = centroDeCusto
                    .Funcionarios
                    .Where(f => f.Departamento.Id == Departamento.Id && f.Demitido &&
                (f.FuncionarioReposicao == null
                 || f.FuncionarioReposicao == 0)
                && ((DropDownList)sender).SelectedValue != string.Empty
                && int.Parse(((DropDownList)sender).SelectedValue) >= f.MesDeDemissao) ??
                new List<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario>();


                ddlVaga.DataSource = funcionariosReposicao;
                ddlVaga.DataValueField = "NumeroDeVaga";
                ddlVaga.DataTextField = "NumeroDeVaga";
                ddlVaga.DataBind();
                ddlVaga.Items.Insert(0, new ListItem("Selecione", "0"));

                if (funcionariosReposicao == null || funcionariosReposicao.Count() == 0)
                {
                    if (ddlDataDeAdmissao.SelectedItem.Value != string.Empty)
                        Notificacao = "NNão existe vaga disponível para reposição neste mês.";
                    else if (centroDeCusto.Funcionarios.Where(f => f.Departamento.Id == Departamento.Id && f.Demitido && (f.FuncionarioReposicao == null || f.FuncionarioReposicao == 0)).Count() == 0)
                        Notificacao = "Não existe vaga disponível para reposição neste mês.";
                    else
                        Notificacao = "Selecione o mes da reposição.";

                    EnviarMensagemScriptManager();
                }

            }
        }

        protected void IncluindoNovoFuncionario(object sender, EventArgs e)
        {
            try
            {
                if (!NHibernateSessionPerRequest.GetCurrentSession().Contains(Departamento))
                    NHibernateSessionPerRequest.GetCurrentSession().Refresh(Departamento);

                if (ddlCentrosDeCusto.SelectedValue == "0")
                {
                    Notificacao = "É obrigatório selecionar um centro de custo para incluir um novo funcionário";
                    EnviarMensagem();
                    return;
                }
                //if (this.Departamento.CentrosDeCusto.Where(c => c.CodigoDoCentroDeCusto == ddlCentrosDeCusto.SelectedValue).FirstOrDefault().Funcionarios != null &&
                //    this.Departamento.CentrosDeCusto.Where(c => c.CodigoDoCentroDeCusto == ddlCentrosDeCusto.SelectedValue).FirstOrDefault().Funcionarios.Any(f => f.Matricula == txtMatricula.Text))
                //{
                //    Notificacao = "Não é possivel adicionar um funcionário com o mesmo numero de matricula.";
                //    EnviarMensagem();
                //    return;
                //}
                if (string.IsNullOrEmpty(txCargo.Text))
                {
                    Notificacao = "Não é possivel adicionar um funcionário sem cargo.";
                    EnviarMensagem();
                    return;
                }

                double salario = 0;
                if (!string.IsNullOrEmpty(txtSalario.Text))
                    salario = double.Parse(txtSalario.Text.Replace(".", ""));
                else
                {
                    Notificacao = "Salário é obrigatório";
                    EnviarMensagem();
                    return;
                }

                int dataAdmissao = 0;
                if (!string.IsNullOrEmpty(ddlDataDeAdmissao.SelectedItem.Value))
                    dataAdmissao = int.Parse(ddlDataDeAdmissao.SelectedItem.Value);
                else
                {
                    Notificacao = "Data de admissão é obrigatória";
                    EnviarMensagem();
                    return;
                }

                if (txtNome.SelectedValue == "0")
                {
                    Notificacao = "Selecione motivo da contratacão";
                    EnviarMensagem();
                    return;
                }
                if (DivVaga.Visible == true && ddlVaga.SelectedItem.Value == "0")
                {

                    Notificacao = "Não é possivel repor um funcionario sem informar a vaga.";
                    EnviarMensagem();
                    return;
                }

                if (DivVaga.Visible == true && Funcionarios.Where(f => f.NumeroDeVaga == int.Parse(ddlVaga.SelectedItem.Value)).Count() > 1)
                {
                    Notificacao = "Esta vaga não esta disponivel.";
                    EnviarMensagem();
                    return;
                }
                if (DivVaga.Visible == true && Funcionarios.FirstOrDefault(f => f.NumeroDeVaga == int.Parse(ddlVaga.SelectedItem.Value) && f.Demitido) == null)
                {
                    Notificacao = "Não existe vaga disponível para reposição neste centro de custo";
                    EnviarMensagem();
                    return;
                }

                var funcionarioProximaMatricula =
                    Funcionarios.Where(f => !string.IsNullOrEmpty(f.InicialNumeroMatricula))
                        .OrderByDescending(f => int.Parse(f.Matricula.Substring(1))).FirstOrDefault();
                int countFuncionarios = Funcionarios.Where(f => !string.IsNullOrEmpty(f.InicialNumeroMatricula) && f.InicialNumeroMatricula == "N").Count();
                var funcionario = new FuncionarioDTO()
                {
                    Hospital = Departamento.Nome,
                    Nome = txtNome.SelectedValue.ToUpper(),
                    Matricula = txtNome.SelectedValue == "Aumento de Quadro" ? funcionarioProximaMatricula != null ? "N" + (Convert.ToInt32(funcionarioProximaMatricula.Matricula.Substring(1)) + 1) : "N1" :
                    funcionarioProximaMatricula != null ? "R" + (Convert.ToInt32(funcionarioProximaMatricula.Matricula.Substring(1)) + 1) : "R1",
                    InicialNumeroMatricula = txtNome.SelectedValue == "Aumento de Quadro" ? "N" : "R",
                    DataDeAdmissao = dataAdmissao,
                    SalarioBase = salario,
                    Cargo = txCargo.Text.ToUpper(),
                    AnoAdmissao = 2016,
                    NumeroDeVaga = DivVaga.Visible == false ?
                    countFuncionarios > 0 ?
                    Funcionarios.Where(f => !string.IsNullOrEmpty(f.InicialNumeroMatricula) && f.InicialNumeroMatricula == "N").OrderByDescending(f => f.NumeroDeVaga).FirstOrDefault().NumeroDeVaga + 1 :
                    60000
                    : int.Parse(ddlVaga.SelectedItem.Value)
                };

                IncluirFuncionario(funcionario);


            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }
            //if (string.IsNullOrEmpty(Notificacao))
            //    SalvarOrcamento();

            EnviarMensagem();
        }

        private void IncluirFuncionario(FuncionarioDTO funcionario)
        {
            Funcionarios.Add(funcionario);
            rptFuncionarios.DataSource = Funcionarios.OrderByDescending(f => f.ObterNumeroSequencial);
            rptFuncionarios.DataBind();
        }

        protected void SelecionandoCentroDeCusto(object sender, EventArgs e)
        {
            SelecionandoCentroDeCusto();
        }

        private void SelecionandoCentroDeCusto()
        {
            if (ddlCentrosDeCusto.SelectedValue == "0")
            {
                rptFuncionarios.DataSource = null;
                rptFuncionarios.DataBind();
                btnSalvar.Visible = false;
                return;
            }

            btnSalvar.Visible = true;
            divHeadCount.Visible = true;

            var departamentoId = int.Parse(Session["DepartamentoLogadoId"].ToString());

            centroDeCusto =
                 _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);

            HeadCount.Text = centroDeCusto.Funcionarios.Where(d => d.Departamento.Id == Departamento.Id).Count().ToString();

            PreencherFuncionarios(departamentoId);

            var orcamentoPessoal = _controller.ObterOrcamento(centroDeCusto.Id, departamentoId);
            if (orcamentoPessoal != null)
            {
                txtJustificativa.Text = orcamentoPessoal.Justificativa;
                Justificativa = orcamentoPessoal.Justificativa;
            }
        }

        private void PreencherFuncionarios(int departamentoId)
        {
            Funcionarios = _controller.ObterTodosFuncionariosPor(departamentoId,
                                                                 codigoDeCentroDeCusto:
                                                                     ddlCentrosDeCusto.SelectedValue);
            if (Funcionarios == null)
                Funcionarios = new List<FuncionarioDTO>();

            Aumento = Convert.ToInt32(Funcionarios.Sum(f => f.PercentualDeAumento));
            rptFuncionarios.DataSource = Funcionarios.OrderByDescending(f => f.ObterNumeroSequencial);
            rptFuncionarios.DataBind();
        }

        protected void Salvando(object sender, EventArgs e)
        {

            if (!ValidarOrcamento())
                return;

            if (Funcionarios != null && Funcionarios.Count(f => f.Id == 0) > 0 && txtJustificativa.Text == Justificativa)
            {
                Notificacao = "Por favor, Justifique inclusão de novo funcionario.";
                EnviarMensagem();
                return;
            }

            try
            {
                SalvarOrcamento();
                PreencherFuncionarios(Departamento.Id);
                Aumento = Convert.ToInt32(Funcionarios.Sum(f => f.PercentualDeAumento));
                Justificativa = txtJustificativa.Text;
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            if (string.IsNullOrEmpty(Notificacao))
                Notificacao = "Funcionários atualizados com sucesso!";

            EnviarMensagem();
        }

        private bool ExisteReposicao(int numeroVaga)
        {
            foreach (RepeaterItem item in rptFuncionarios.Items)
            {
                if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem) continue;

                var funcionarioId = int.Parse(((HiddenField)item.FindControl("hdnId")).Value);

                var sequencial = int.Parse(((HiddenField)item.FindControl("Sequencial")).Value);
                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == sequencial);
                var numeroVagaNovo = int.Parse(((HiddenField)item.FindControl("HiddenNumeroVaga")).Value);
                var chkDemitido = (CheckBox)item.FindControl("chkDemitido");
                var ddlMesDeDemissao = (DropDownList)item.FindControl("ddlMesDeDemissao");
                var chkAumentado = (CheckBox)item.FindControl("chkAumentado");
                var txtPercentualDeAumento = (TextBox)item.FindControl("txtPercentualDeAumento");
                var ddlMesesDeAumento = (DropDownList)item.FindControl("ddlMesesDeAumento");

                if (numeroVagaNovo == numeroVaga && funcionarioId == 0)
                    return true;
            }
            return false;
        }

        private bool ValidarOrcamento()
        {
            int aumento = 0;
            List<FuncionarioDTO> funcionariosDtos = new List<FuncionarioDTO>();
            foreach (RepeaterItem item in rptFuncionarios.Items)
            {
                if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem) continue;

                var funcionarioId = int.Parse(((HiddenField)item.FindControl("hdnId")).Value);

                var sequencial = int.Parse(((HiddenField)item.FindControl("Sequencial")).Value);
                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == sequencial);

                var chkDemitido = (CheckBox)item.FindControl("chkDemitido");
                var ddlMesDeDemissao = (DropDownList)item.FindControl("ddlMesDeDemissao");
                var chkAumentado = (CheckBox)item.FindControl("chkAumentado");
                var txtPercentualDeAumento = (TextBox)item.FindControl("txtPercentualDeAumento");
                var ddlMesesDeAumento = (DropDownList)item.FindControl("ddlMesesDeAumento");
                if (txtPercentualDeAumento.Text != string.Empty)
                    aumento += int.Parse(txtPercentualDeAumento.Text);

                if (funcionarioId > 0 && Funcionarios != null)
                {
                    var funcionario = Funcionarios.FirstOrDefault(f => f.Id == funcionarioId);

                    if (funcionario != null && funcionario.Demitido &&
                        (!chkDemitido.Checked && funcionarioDTO.FuncionarioReposicao > 0) ||
                         (!chkDemitido.Checked && ExisteReposicao(funcionario.NumeroDeVaga)))
                    {
                        Notificacao = "Não é possivel readimitir um funcionario que tem reposicao.";
                        EnviarMensagem();
                        return false;
                    }

                    if (funcionario != null && funcionario.Demitido &&
                        (int.Parse(ddlMesDeDemissao.SelectedItem.Value) != funcionario.MesDeDemissao &&
                        funcionarioDTO.FuncionarioReposicao > 0) ||
                        (int.Parse(ddlMesDeDemissao.SelectedItem.Value) != funcionario.MesDeDemissao &&
                        ExisteReposicao(funcionario.NumeroDeVaga)))
                    {
                        Notificacao = "Não é possivel trocar mês de demissão quando já existe reposição de vaga.";
                        EnviarMensagem();
                        return false;
                    }
                }

                if (chkDemitido.Checked && chkAumentado.Checked)
                {
                    Notificacao = "Não é possivel demitir e aumentar um funcionario.";
                    EnviarMensagem();
                    return false;
                }
                if (chkDemitido.Checked && ddlMesDeDemissao.SelectedItem.Value == "0")
                {
                    Notificacao = "Não é possivel demitir sem informar o mês de demissão.";
                    EnviarMensagem();
                    return false;
                }
                if (chkAumentado.Checked && ddlMesesDeAumento.SelectedItem.Value == "0")
                {
                    Notificacao = "Não é possivel aumentar sem informar o mês de aumento.";
                    EnviarMensagem();
                    return false;
                }

                if (chkAumentado.Checked && (txtPercentualDeAumento.Text == "0" || txtPercentualDeAumento.Text == string.Empty))
                {
                    Notificacao = "Não é possivel aumentar sem informar o mês de aumento.";
                    EnviarMensagem();
                    return false;
                }
            }
            if (Aumento < aumento && txtJustificativa.Text == Justificativa)
            {
                Notificacao = "Por favor, Justifique aumento de salário.";
                EnviarMensagem();
                return false;
            }

            return true;
        }

        private void SalvarOrcamento()
        {
            foreach (RepeaterItem item in rptFuncionarios.Items)
            {
                if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem) continue;

                var funcionarioId = int.Parse(((HiddenField)item.FindControl("hdnId")).Value);

                var sequencial = int.Parse(((HiddenField)item.FindControl("Sequencial")).Value);

                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == sequencial && f.Id == funcionarioId );

                var chkDemitido = (CheckBox)item.FindControl("chkDemitido");
                var ddlMesDeDemissao = (DropDownList)item.FindControl("ddlMesDeDemissao");
                var chkAumentado = (CheckBox)item.FindControl("chkAumentado");
                var txtPercentualDeAumento = (TextBox)item.FindControl("txtPercentualDeAumento");
                var ddlMesesDeAumento = (DropDownList)item.FindControl("ddlMesesDeAumento");

                Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario funcionario = funcionarioId > 0 ? _controller.ObterFuncionarioPor(funcionarioId) :
                    new Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario(this.Departamento)
                        {
                            AnoAdmissao = 2016,
                            Aumentado = funcionarioDTO.Aumentado,
                            Cargo = funcionarioDTO.Cargo,
                            DataAdmissao = funcionarioDTO.DataDeAdmissao,
                            Demitido = funcionarioDTO.Demitido,
                            InicialNumeroMatricula = funcionarioDTO.InicialNumeroMatricula,
                            Matricula = funcionarioDTO.Matricula,
                            MesDeAumento = funcionarioDTO.MesDeAumento,
                            MesDeDemissao = funcionarioDTO.MesDeDemissao,
                            Nome = funcionarioDTO.Nome,
                            NumeroDeVaga = funcionarioDTO.NumeroDeVaga,
                            Salario = funcionarioDTO.SalarioBase,
                        };


                if (funcionarioDTO.Excluido)
                {
                    DeletarUsuario(funcionario.Id, funcionario.ObterNumeroSequencial);
                }
                else
                {
                    funcionario.Demitido = chkDemitido.Checked;
                    funcionario.Aumentado = chkAumentado.Checked;

                    if (funcionario.Demitido)
                    {
                        if (ddlMesDeDemissao.SelectedValue != "0")
                            funcionario.MesDeDemissao = short.Parse(ddlMesDeDemissao.SelectedValue);
                    }
                    else
                        funcionario.MesDeDemissao = 0;

                    if (funcionario.Aumentado)
                    {
                        if (!string.IsNullOrEmpty(txtPercentualDeAumento.Text))
                            funcionario.Aumento = Convert.ToDouble(txtPercentualDeAumento.Text);

                        if (ddlMesesDeAumento.SelectedValue != "0")
                            funcionario.MesDeAumento = int.Parse(ddlMesesDeAumento.SelectedValue);
                    }
                    else
                    {
                        funcionario.MesDeAumento = 0;
                        funcionario.Aumento = 0;
                    }

                    _controller.Departamento = Departamento;
                    _controller.CodigoDoCentroDeCusto = ddlCentrosDeCusto.SelectedValue;

                    if (funcionario.Id > 0)
                        _controller.Salvar(funcionario);
                    else
                    {
                        if (!NHibernateSessionPerRequest.GetCurrentSession().Contains(centroDeCusto))
                            NHibernateSessionPerRequest.GetCurrentSession().Refresh(centroDeCusto);

                        _controller.SalvarFuncionario(funcionario);
                        var funcionarioReposicao = centroDeCusto.Funcionarios.FirstOrDefault(f =>f.Departamento.Id == Departamento.Id && f.Demitido &&
                   (f.FuncionarioReposicao == null || f.FuncionarioReposicao == 0) && int.Parse(((DropDownList)ddlVaga).SelectedValue) == f.NumeroDeVaga);
                        if (funcionarioReposicao != null)
                        {
                            funcionarioReposicao.FuncionarioReposicao = funcionario.Id;
                            _controller.Salvar(funcionarioReposicao);
                        }
                    }

                }
            }

            _controller.Departamento = Departamento;
            _controller.CodigoDoCentroDeCusto = ddlCentrosDeCusto.SelectedValue;
            _controller.SalvarOrcamento(justificativa: txtJustificativa.Text);
        }

        protected void DeletarFuncionario(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeletarUsuario")
            {
                int usuarioID = int.Parse(e.CommandArgument.ToString());
                DeletarUsuarioGrid(usuarioID);
            }
        }

        private void DeletarUsuarioGrid(int usuarioID)
        {
            var funcionarios = new Orcamento.Domain.DB.Repositorio.Funcionarios();


            if (usuarioID > 60000)
            {
                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == usuarioID);
                if (funcionarioDTO.Id > 0)
                {
                    var funcionario =
                        funcionarios.Obter<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario>(
                            funcionarioDTO.Id);
                    centroDeCusto =
                        _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);


                }
                if (funcionarioDTO.Excluido)
                    funcionarioDTO.Excluido = false;
                else
                    funcionarioDTO.Excluido = true;


                rptFuncionarios.DataSource = Funcionarios.OrderByDescending(d => d.ObterNumeroSequencial);
                rptFuncionarios.DataBind();
            }
            else
            {
                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == usuarioID);
                var funcionario =
                    funcionarios.Obter<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario>(funcionarioDTO.Id);

                if (funcionarioDTO.Excluido)
                    funcionarioDTO.Excluido = false;
                else
                    funcionarioDTO.Excluido = true;

                rptFuncionarios.DataSource = Funcionarios.OrderByDescending(d => d.ObterNumeroSequencial);
                rptFuncionarios.DataBind();
            }
        }

        private void DeletarUsuario(int usuarioID,int sequencial)
        {
            var funcionarios = new Orcamento.Domain.DB.Repositorio.Funcionarios();


            if (sequencial > 60000)
            {
                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == sequencial && f.Id == usuarioID);
                if (funcionarioDTO.Id > 0)
                {
                    var funcionario =
                        funcionarios.Obter<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario>(
                            funcionarioDTO.Id);
                    centroDeCusto =
                        _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);
                    centroDeCusto.Funcionarios.Remove(funcionario);
                    funcionarios.Salvar(centroDeCusto);
                    funcionarios.Deletar(funcionario);
                }
                Funcionarios.Remove(funcionarioDTO);


                //  rptFuncionarios.DataSource = Funcionarios.OrderByDescending(d => d.ObterNumeroSequencial);
                // rptFuncionarios.DataBind();
            }
            else
            {
                var funcionarioDTO = Funcionarios.FirstOrDefault(f => f.ObterNumeroSequencial == sequencial && f.Id == usuarioID);
                var funcionario =
                    funcionarios.Obter<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario>(funcionarioDTO.Id);

                centroDeCusto =
                    _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);
                centroDeCusto.Funcionarios.Remove(funcionario);
                funcionarios.Salvar(centroDeCusto);
                Funcionarios.Remove(funcionarioDTO);

                // rptFuncionarios.DataSource = Funcionarios.OrderByDescending(d => d.ObterNumeroSequencial);
                //  rptFuncionarios.DataBind();
            }
        }

        #endregion
    }
}
