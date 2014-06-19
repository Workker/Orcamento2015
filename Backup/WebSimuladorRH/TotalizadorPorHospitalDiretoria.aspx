<%@ Page Title="Or&ccedil;amento 2013 - Total por Hospital / Diretoria" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="TotalizadorPorHospitalDiretoria.aspx.cs" Inherits="WebSimuladorRH.TotalizadorPorHospitalDiretoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>Total por Hospital / Diretoria</h2>
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptCentroDeCusto">
                <HeaderTemplate>
                    <thead>
                        <tr><th style="background-color: #000; text-align: center;">Total de Centro de Custo por Hospital / Diretoria</th><th style="background-color: #000; width: 0%;"></th></tr>
                        <tr>
                            <th>
                            </th>
                            <asp:Repeater runat="server" ID="rptHospitalDiretoria">
                                <HeaderTemplate></HeaderTemplate>
                                <ItemTemplate>
                                    <th class="coluna100px">
                                        <%#Eval("NomeHospitalDiretoria") %>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex%2 == 0 ? "even" : "odd" %>">
                        <td>
                            <%#Eval("NomeCentroDeCusto") %>
                        </td>
                        <td class="coluna100px">
                            <%#"(" + Eval("ValorTotalCentroDeCusto") + ")" %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-12-12"></div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="Repeater1">
                <HeaderTemplate>
                    <thead>
                        <tr><th style="background-color: #000; text-align: center;">Total de Conta por Hospital / Diretoria</th><th style="background-color: #000; width: 0%;"></th></tr>
                        <tr>
                            <th>
                            </th>
                            <asp:Repeater runat="server" ID="rptHospitalDiretoria">
                                <HeaderTemplate></HeaderTemplate>
                                <ItemTemplate>
                                    <th class="coluna100px">
                                        <%#Eval("NomeHospitalDiretoria") %>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex%2 == 0 ? "even" : "odd" %>">
                        <td>
                            <%#Eval("NomeDaConta") %>
                        </td>
                        <td class="coluna100px">
                            <%#"(" + Eval("ValorTotalDaConta") + ")" %>
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