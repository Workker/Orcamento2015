<%@ Page Title="Or&ccedil;amento 2014 - Totalizador de Despesas" Language="C#" MasterPageFile="Principal.Master" AutoEventWireup="true" CodeBehind="TotalizadorDeDespesas.aspx.cs" Inherits="WebSimuladorRH.TotalizadorDeDespesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div></div>
    <div class="grid-12-12">
        <h2>Totalizador de Despesas</h2>
        <hr />
    </div>
    <%--<div class="grid-1-12  bold" style="border: solid 2px #808080; padding: 0px;color: #c00032;">
        <label class="workkerForm-lbl-value" style="color: #c00032;">Despesa Total Or&ccedil;ada:</label><asp:Literal runat="server" ID="ltlDespesaTutalOrcada"></asp:Literal>
    </div>--%>
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater runat="server" ID="rptOrcamentos" >
                <HeaderTemplate>
                    <thead>
                        <tr><th style="background-color: #000; text-align: center;">Totalizador de Despesas</th><th style="background-color: #000; width: 0%;"></th></tr>
                        <tr>
                            <th>
                                Conta
                            </th>
                            <th class="coluna100px">
                                Valor Total
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd"%>">
                        <td>
                            <%#Eval("NomeDespesa") %>
                        </td>
                        <td class="coluna100px">
                            <%# "(" + Eval("ValorTotalDespesa") + ")"%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
        </div> 
</asp:Content>