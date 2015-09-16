<%@ Page Title="Or&ccedil;amento 2016 - Total Or&ccedil;ado" Language="C#" MasterPageFile="~/Principal.Master"
    AutoEventWireup="true" CodeBehind="Totalizador.aspx.cs" Inherits="WebSimuladorRH.Totalizador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    </div>
    <div class="grid-12-12">
        <h2>
            Totalizador de Despesas</h2>
        <hr />
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater runat="server" ID="rptOrcamentos" OnItemDataBound="rptOrcamentos_ItemDataBound">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th style="background-color: #000; text-align: center;">
                                Totalizador de Despesas
                            </th>
                            <th style="background-color: #000; width: 0%;">
                            </th>
                        </tr>
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
                            <%# "(" +string.Format("{0:#,###;#,###;0}", Eval("ValorTotalDespesa"))  + ")"%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>
                                Total
                            </th>
                            <td>
                                <asp:Literal runat="server" ID="ltlTotal" Text="0"> </asp:Literal>
                            </td>
                        </tr>
                    </tfoot>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-12-12">
    </div>
    <div class="grid-12-12">
        <h2>
            Totalizador dos Centros de Custo</h2>
        <hr />
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptCentroDecusto" OnItemDataBound="rptCentroDecusto_ItemDataBound">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th style="background-color: #000; text-align: center;">
                                Totalizador por centro de Custo
                            </th>
                            <th style="background-color: #000; width: 0%;">
                            </th>
                        </tr>
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
                            <%# "(" + string.Format("{0:#,###;#,###;0}", Eval("ValorTotalCentroDeCusto")) + ")"%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>
                                Total
                            </th>
                            <td>
                                <asp:Literal runat="server" ID="ltlTotal" Text="0"> </asp:Literal>
                            </td>
                        </tr>
                    </tfoot>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
