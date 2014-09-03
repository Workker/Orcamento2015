<%@ Page Title="Or&ccedil;amento 2015 - Resultado Or&ccedil;ado" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
         CodeBehind="ResultadoOrcado.aspx.cs" Inherits="WebSimuladorRH.ResultadoOrcado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <thead>
            <tr>
                <th>
                    Orçamento
                </th>
                <th>
                    Valor Total
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    Outras despesas
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOutrasDespesas"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Pessoal
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPessoal"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trReceita">
                <td>
                    Receita
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtReceita"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trViagens">
                <td>
                    Viagem/Hospedagem
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtViagens"></asp:TextBox>
                </td>
            </tr>

                  <tr runat="server" id="trInsumos">
                <td>
                    Insumos
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtInsumos"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>