<%@ Page Title="Or&ccedil;amento 2016 - Cadastro de Tickets de Viagem" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
         CodeBehind="TicketDeViagem.aspx.cs" Inherits="WebSimuladorRH.Tickets" %>

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

                this.value = this.value.replace(",00", "");

                this.value = RemoverTodosOsCaracteresNaoNumericos(this.value); //.replace(/\(|\)+/g, '');

                this.select();
            });

            $('.sonumero').blur(function () {

                
                this.value = "(" + AplicarMascaraDeMilhar(this.value) + ",00)";
            });
        }
//        $(document).ready(function() {
//            sonumero();
//            $(".sonumero").keydown(function (event) {
//                PermitirSomenteADigitacaoDeNumeros(event);
//            });
//        });

//        function sonumero() {
//            $('.sonumero').numeric();

//            $('.sonumero').focus(function () {
//                this.value = this.value.replace(/\(|\)+/g, '');

//                this.select();
//            });

//            $('.sonumero').blur(function() {
//                this.value = "(" + this.value + ")";
//            });
//        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="grid-12-12 workkerForm-no-lbl">
    <h2>Cadastro de Tickets de Viagem</h2>
    <hr/>
</div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptTickets">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                                Cidade
                            </th>
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
                        <td class="coluna150px">
                            <%#Eval("NomeCidade") %>
                        </td>
                        <td>
                            <%#Eval("Descricao") %>
                        </td>
                        <td class="coluna66px">
                            <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                            <asp:TextBox runat="server" MaxLength="6" class="sonumero" ID="txtValor" Text='<%# "(" +Eval("Valor") + ",00)"%>'></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table> </div>
    <div class="grid-2-12 workkerForm-no-lbl">
        <asp:Button ID="Button1" runat="server" Text="Salvar Tickets" OnClick="Unnamed1_Click" />
    </div>
</asp:Content>