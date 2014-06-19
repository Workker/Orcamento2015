<%@ Page Title="Or&ccedil;amento 2014 - Totalizador Pessoal por Grupo de Conta" Language="C#" AutoEventWireup="true" MasterPageFile="Principal.Master" CodeBehind="TotalizadorPessoalPorGrupoDeConta.aspx.cs" Inherits="WebSimuladorRH.TotalizadorPessoalPorGrupoDeConta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>Totalizador Pessoal por Grupo de Conta</h2>
    </div>
     <div class="grid-6-12">
         <label class="workkerForm-lbl-value">Centro de Custo:</label>
        <asp:DropDownList runat="server" ID="dropCentroCusto" AutoPostBack="True" 
        onselectedindexchanged="dropCentroCusto_SelectedIndexChanged"/>
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater ID="rptContas" runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                                Grupo de Conta
                            </th>
                            <th class="coluna66px">
                                Total
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                        <tr class="<%#Container.ItemIndex % 2 == 0? "even" : "odd"%>">
                            <td class="coluna150px">
                                <%#Eval("Nome") %>
                            </td>
                            <td class="coluna66px">
                                <%# "(" + Eval("Valor") + ")" %>
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