<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="CadastroDeConta.aspx.cs" Inherits="WebSimuladorRH.CadastroDeConta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("[id*=btnSalvar]").click(function () {

                return validarCampos();
            });
        });

        function validarCampos() {

            if ($("[id*=txtDescricao]").val() == "") {
                alert("O campo descrição deve ser preenchido.");
                return false;

            } else {

                if ($("[id*=txtCodigo]").val() == "") {
                    alert("O campo código deve ser preenchido.");
                    return false;

                } else {

                    return true;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>
            Conta</h2>
        <hr />
    </div>

    <div id="divAlteracaoDeContas" class="grid-3-12" runat="server" visible="false">
        <label class="workkerForm-lbl-value bold">Conta:</label>
        <br />
        <asp:DropDownList ID="ddlContas" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlContas_SelectedIndexChanged">
        </asp:DropDownList>
    </div>

    <div class="grid-12-12">    
    </div>

    <div class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            Descri&ccedil;&atilde;o:</label>
        <br />
        <asp:TextBox ID="txtDescricao" runat="server" MaxLength="20"></asp:TextBox>
    </div>
    <div class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            C&oacute;digo:</label>
        <br />
        <asp:TextBox ID="txtCodigoConta" runat="server" MaxLength="20"></asp:TextBox>
    </div>
    <div class="grid-3-12">
        <asp:HiddenField ID="hdnIdConta" runat="server" />
    </div>

    <div class="grid-12-12 workkerForm-no-lbl">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" 
            onclick="btnSalvar_Click" />
    </div>


</asp:Content>
