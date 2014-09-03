<%@ Page Title="Or&ccedil;amento 2015 - Cadastro de Tickets de Pessoal" Language="C#"
    MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadastroDeTicketsPessoal.aspx.cs"
    Inherits="WebSimuladorRH.Pessoal.CadastroDeTicketsPessoal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            sonumero();
            $(".sonumero").keydown(function (event) {
                PermitirSomenteADigitacaoDeNumeros(event);
            });
        });

        function sonumero() {

            $(".sonumero").keyup(function (event) {
                InsereMascaraDeMilhar(this);

                if (event.keyCode == 9) {
                    this.select();
                }
            });

            $('.sonumero').focus(function () {
                this.value = RemoverTodosOsCaracteresNaoNumericos(this.value);

                this.select();
            });

            $('.sonumero').blur(function () {
                this.value = "(" + AplicarMascaraDeMilhar(this.value) + ")";
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>
            Cadastro de Tickets de Pessoal</h2>
        <hr />
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptTickets">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th>
                                Descri&ccedil;&atilde;o
                            </th>
                            <th class="coluna66px">
                                Valor
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td>
                            <asp:Literal runat="server" ID="ltlDescricao" Text='<%#Eval("Descricao") %>'></asp:Literal>
                        </td>
                        <td class="coluna66px">
                            <asp:HiddenField runat="server" ID="hdnIdTicket" Value='<%#Eval("Id") %>' />
                            <asp:TextBox runat="server" MaxLength="8" class="sonumero" ID="txtValor" Text='<%# "(" +Eval("Valor") + ")"%>'></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-12-12">
        <table>
            <thead>
                <tr>
                    <th>
                        Descri&ccedil;&atilde;o
                    </th>
                    <th class="coluna66px">
                        Mês
                    </th>
                    <th class="coluna66px">
                       Valor
                    </th>
                </tr>
            </thead>
            <tbody class="gridBorder">
                <tr class="odd">
                    <td>
                        <asp:Literal runat="server" ID="ltlDescricao" Text="Acordo de Convenção"></asp:Literal>
                    </td>
                    <td class="coluna66px">
                        <asp:TextBox runat="server" MaxLength="8" class="sonumero" ID="txtAcordoConvencaoMes" Text=""></asp:TextBox>
                    </td>
                    <td class="coluna66px">
                        <asp:TextBox runat="server" MaxLength="8" class="sonumero" ID="txtAcordoConvencaoValor" Text=""></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="grid-2-12 workkerForm-no-lbl">
        <asp:Button ID="btnSalvarTickets" runat="server" Text="Salvar Tickets" OnClick="Salvando" />
    </div>
    <div class="grid-10-12 workkerForm-no-lbl">
    </div>
</asp:Content>
