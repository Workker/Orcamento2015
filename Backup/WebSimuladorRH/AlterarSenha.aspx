<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AlterarSenha.aspx.cs" Inherits="WebSimuladorRH.AlterarSenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ValidarCamposDaPagina() {

            var campoDeLogin = document.getElementById("txtNovaSenha1");

            if (!VerificarSeOCampoEstaVazio(campoDeLogin)) {
                alert("O campo de senha deve ser preenchido.");
                return false;
            }

            var campoDeSenha = document.getElementById("txtNovaSenha2");

            if (!VerificarSeOCampoEstaVazio(campoDeSenha)) {
                alert("Por favor preencha os dois campos de senha.");
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="center" align="center">
        <div runat="server" id="divLogin" class="override-AlterarSenha">
            <div class="override-grid-12-12">
                <h3>
                    <asp:Literal runat="server" ID="ltlLoginFormTitulo" Text="Alterar Senha"></asp:Literal></h3>
                <hr class="ruleLogin" />
            </div>
            <div class="override-grid-12-12" runat="server" id="divTextFieldUsuario">
                <label class="workkerForm-lbl-value">
                    Nova Senha
                </label>
            </div>
            <div class="override-grid-12-12" runat="server" id="div1">
                <asp:TextBox ID="txtNovaSenha1" runat="server" Width="150px" TextMode="Password"
                    ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="override-grid-12-12" runat="server" id="divTextFieldSenha">
                <label class="workkerForm-lbl-value">
                    Repita Nova Senha
                </label>
            </div>
            <div class="override-grid-12-12" runat="server" id="div2">
                <asp:TextBox ID="txtNovaSenha2" runat="server" MaxLength="50" Width="150px" TextMode="Password"
                    ClientIDMode="Static"></asp:TextBox>
            </div>
        <div class="override-grid-12-12" style="padding-left: 0%;" runat="server" id="divBotaoLogar">
            <asp:Button ID="btnAlterarSenha" runat="server" Text="Alterar Senha" CssClass="override-AlterarSenhaSubmit"
                OnClientClick=" return ValidarCamposDaPagina(); " OnClick="IrParaLogin" /></div>
    </div>
    </div>
</asp:Content>
