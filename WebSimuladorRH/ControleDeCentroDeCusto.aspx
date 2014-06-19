<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ControleDeCentroDeCusto.aspx.cs" Inherits="WebSimuladorRH.ControleDeCentroDeCusto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-8-12">
        <asp:Literal runat="server" ID="Literal1" Text="Centros De Custo a salvar"></asp:Literal>
    </div>
    <div class="grid-8-12">
        <table>
            <asp:Repeater runat="server" ID="rptControleDeCentroDeCustos">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna100px">
                                Centro De Custo
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td class="coluna100px">
                            <asp:Literal runat="server" ID="Literal1" Text='<%#Eval("CentroDeCusto.Nome") %>'></asp:Literal>
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
