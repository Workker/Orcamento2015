<%@ Page Title="Or&ccedil;amento 2015 - Pessoal" Language="C#" MasterPageFile="../Principal.Master"
    AutoEventWireup="true" CodeBehind="OrcamentoPessoal.aspx.cs" Inherits="WebSimuladorRH.Pessoal.OrcamentoPessoal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
    <h2>Or&ccedil;amento Pessoal</h2>
    <hr />
</div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Centro de Custo:</label>
        <asp:DropDownList runat="server" OnSelectedIndexChanged="SelecionandoOCentroDeCusto"
            ID="dropCentroDeCusto" AutoPostBack="true">
        </asp:DropDownList>
    </div>
    <div class="grid-12-12" runat="server" id="divHeadCount" Visible="False">
        <label class="workkerForm-lbl-value bold" >Head Count:</label>
        <asp:Literal runat="server" ID="HeadCount">
        </asp:Literal>
    </div>
    <div class="grid-12-12">
        <asp:Repeater ID="rptDespesasPessoais" runat="server">
            <ItemTemplate>
                <table>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                               &nbsp;
                            </th>
                            <th class="coluna88px">
                                Jan
                            </th>
                            <th class="coluna88px">
                                Fev
                            </th>
                            <th class="coluna88px">
                                Mar
                            </th>
                            <th class="coluna88px">
                                Abr
                            </th>
                            <th class="coluna88px">
                                Mai
                            </th>
                            <th class="coluna88px">
                                Jun
                            </th>
                            <th class="coluna88px">
                                Jul
                            </th>
                            <th class="coluna88px">
                                Ago
                            </th>
                            <th class="coluna88px">
                                Set
                            </th>
                            <th class="coluna88px">
                                Out
                            </th>
                            <th class="coluna88px">
                                Nov
                            </th>
                            <th class="coluna88px">
                                Dez
                            </th>
                            <th class="coluna88px">
                                Total
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                        <tr>
                            <td class="coluna150px bold">
                               Pessoal
                            </td>
                            <asp:Repeater ID="rptTotalOrcamento" runat="server" DataSource='<%#Eval("TotaisOrcamentoMensal") %>'>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <td class="coluna88px">
                                        (<%#string.Format("{0:#,###;#,###;0}", Eval("Valor"))%>)
                                    </td>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            <td class="coluna88px">
                                (<asp:Literal ID="Literal1" runat="server" Text='<%#string.Format("{0:#,###;#,###;0}", Eval("TotalOrcamento")) %>' />)
                            </td>
                        </tr>
                        <tr>
                            <asp:Repeater ID="rptGrupoConta" runat="server" DataSource='<%# Eval("GruposDeConta") %>'>
                                <ItemTemplate>
                                    <table>
                                        
                                        <tbody class="gridBorder">
                                            <tr class="header bold">
                                                <td class="coluna150px">
                                                    <p style="margin-left: 10px"><%#Eval("GrupoConta")%></p>
                                                </td>
                                                <asp:Repeater ID="rptDespesasDosGruposDecontas" runat="server" DataSource='<%#Eval("DespesasGrupoDeConta") %>'>
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <td class="coluna88px">
                                                            (<%#string.Format("{0:#,###;#,###;0}", Eval("Valor"))%>)
                                                        </td>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <td class="coluna88px">
                                                    (<asp:Literal ID="Literal1" runat="server" Text='<%#string.Format("{0:#,###;#,###;0}", Eval("TotalGrupoConta")) %>' />)
                                                </td>
                                            </tr>
                                            <tr>
                                                <asp:Repeater ID="rptContas" runat="server" DataSource='<%# Eval("Contas") %>'>
                                                    <HeaderTemplate>
                                                        <table>
                                                            <tbody class="gridBorder">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                                            <td class="coluna150px">
                                                                <p style="margin-left: 20px;"><%#Eval("Conta") %></p>
                                                            </td>
                                                            <asp:Repeater ID="rptDespesasDeContas" runat="server" DataSource='<%#Eval("Despesas") %>'>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <td class="coluna88px">
                                                                         
                                                                        (<%#string.Format("{0:#,###;#,###;0}", Eval("Valor"))%>)
                                                                    </td>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                            <td class="coluna88px">
                                                                (<asp:Literal ID="Literal1" runat="server" Text='<%#string.Format("{0:#,###;#,###;0}", Eval("TotalConta")) %>' />)
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody> </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody> </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tr>
                    </tbody>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
