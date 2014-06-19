<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AlterarLocacaoFuncionario.aspx.cs" Inherits="WebSimuladorRH.AlterarLocacaoFuncionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>
            Alterar Alocacao do Funcion&aacute;rio</h2>
        <hr />
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Departamento atual:
        </label>
        <asp:DropDownList runat="server" ID="DdlDepartamento" AutoPostBack="True" OnSelectedIndexChanged="SelecionandoDepartamento" />
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Centro de Custo atual:
        </label>
        <asp:DropDownList runat="server" ID="ddlCentrosDeCusto" />
    </div>
    <div class="grid-12-12">
    </div>
    <div class="grid-2-12">
        <label class="workkerForm-lbl-value">
            Matr&iacute;cula:&nbsp;</label>
        <asp:TextBox runat="server" ID="txtMatricula"></asp:TextBox>
    </div>
    <div class="grid-2-12">
        <label>
            <br />
        </label>
        <asp:Button runat="server" ID="btnProcurar" Text="Procurar" OnClick="ProcurarFuncionario" />
    </div>
    <div class="grid-4-12">
        <label>
            <br />
        </label>
        <asp:Label Visible="false" runat="server" Text="Nome:" ID="lblNome"></asp:Label>
        <label>
            <br />
        </label>
        <asp:Label Visible="false" runat="server" ID="labelNomeFuncionario"></asp:Label>
    </div>
    <div class="grid-12-12">
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Departamento destino:
        </label>
        <asp:DropDownList runat="server" ID="ddlDepartamentoDestino" AutoPostBack="True"
            OnSelectedIndexChanged="SelecionandoDepartamentoDestino" />
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Centro de Custo destino:
        </label>
        <asp:DropDownList runat="server" ID="ddlCentroDeCustoDestino" />
    </div>
    <div class="grid-2-12">
        <label>
            <br />
        </label>
        <asp:Button runat="server" ID="btnAlterarFuncionario" Text="Alterar Funcion&aacute;rio"
            OnClick="AlterarFuncionario" />
    </div>
</asp:Content>
