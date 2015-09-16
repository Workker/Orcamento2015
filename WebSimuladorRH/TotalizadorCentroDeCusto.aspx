<%@ Page Title="Or&ccedil;amento 2016 - Totalizador dos Centros de Custo" Language="C#" MasterPageFile="Principal.Master" AutoEventWireup="true"
         CodeBehind="TotalizadorCentroDeCusto.aspx.cs" Inherits="WebSimuladorRH.TotalizadorCentroDeCusto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>Totalizador dos Centros de Custo</h2>
        <hr />
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptOrcamentos" 
               >
                <HeaderTemplate>
                    <thead>
                        <tr><th style="background-color: #000; text-align: center;">Totalizador por centro de Custo</th><th style="background-color: #000; width: 0%;"></th></tr>
                        <tr>
                            <th>
                                Centro de Custo
                            </th>
                            <th class="coluna100px">
                                Valor Total
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                        <tr class="<%#Container.ItemIndex % 2 == 0? "even" : "odd"%>">
                            <td>
                                <%#Eval("NomeCentroDeCusto") %>
                            </td>
                            <td class="coluna100px">
                                <%# "(" + Eval("ValorTotalCentroDeCusto") + ")"%>
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