<%@ Page Title="Or&ccedil;amento 2014 - Login" Language="C#" MasterPageFile="Principal.Master" AutoEventWireup="true"
         CodeBehind="Login.aspx.cs" Inherits="WebSimuladorRH.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ValidarCamposDaPagina() {

            var campoDeLogin = document.getElementById("txtlogin");

            if (!VerificarSeOCampoEstaVazio(campoDeLogin)) {
                alert("O campo login deve ser preenchido.");
                return false;
            }

            var campoDeSenha = document.getElementById("txtSenha");

            if(!VerificarSeOCampoEstaVazio(campoDeSenha)) {
                alert("O campo senha deve ser preenchido.");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="center" align="center">
        <div runat="server" ID="divLogin" class="override-Login">
            <div class="override-grid-12-12">
                <h3><asp:Literal runat="server" ID="ltlLoginFormTitulo" Text="Login"></asp:Literal></h3>
                <hr class="ruleLogin"/>
            </div>
            <div class="override-grid-12-12" runat="server" ID="divTextFieldUsuario">
                <label class="workkerForm-lbl-value">
                    Usu&aacute;rio:&nbsp;
                </label>
                <asp:TextBox ID="txtlogin" runat="server" Width="150px" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="override-grid-12-12" runat="server" ID="divTextFieldSenha">
                <label class="workkerForm-lbl-value">
                    Senha:&nbsp;&nbsp;&nbsp;&nbsp;
                </label>
                <asp:TextBox ID="txtSenha" runat="server" MaxLength="50" Width="150px" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="override-grid-12-12" style="padding-left: 22%;" runat="server" ID="divBotaoLogar">
                <asp:Button ID="btnLogar" runat="server" Text="Logar" CssClass="override-LoginSubmit"
                            OnClientClick=" return ValidarCamposDaPagina(); " OnClick="Logando" /></div>
            <div class="override-grid-12-12" align="center" runat="server" Visible="False" ID="divComboBoxDeSelecaoDeUnidade">
                <asp:DropDownList ID="ddlDepartamentos" runat="server" AutoPostBack="True"
                                  OnSelectedIndexChanged="ddlDepartamentos_SelectedIndexChanged" Width="90%">
                </asp:DropDownList>
                
            </div>
        </div>
    </div>
</asp:Content>