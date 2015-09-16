<%@ Page Title="Or&ccedil;amento 2016 - Cadastro de Tickets de Produ&ccedil;&atilde;o"
    Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="TicketDeProducao.aspx.cs"
    Inherits="WebSimuladorRH.TicketDeProducao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function PermitirSomenteADigitacaoDeNumerosComVirgulaESinalNegativo(event) {

            // Allow: backspace, delete, tab, escape, and enter                                                                  // Allow: Ctrl+A                                   // Allow: home, end, left, right
            if (event.keyCode == 189 || event.keyCode == 110 || event.keyCode == 194 || (event.keyCode == 67 && event.ctrlKey === true) || (event.keyCode == 86 && event.ctrlKey === true) || event.keyCode == 109 || event.keyCode == 188 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {

                if (event.srcElement.parentNode.parentNode.parentNode.parentNode.id != "complexidade" && (event.keyCode == 109 || event.ketCode == 189)) {
                    event.preventDefault ? event.preventDefault() : event.returnValue = false;
                }
                else {
                    return;
                }
            } else {
                // Ensure that it is a number and stop the keypress
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault ? event.preventDefault() : event.returnValue = false;
                }
            }
        }


        $(document).ready(function () {

            $("input[id*=Valor]").focus(function () {

                this.select();

            });


            $("#unitarios input[id*=Valor]").keydown(function (event) {

                PermitirSomenteADigitacaoDeNumeros(event);

            });


            $("#unitarios input[id*=Valor]").blur(function () {

                if (this.value == "") {

                    this.value = "0";
                }
                else {

                    this.value = AplicarMascaraDeMilhar(this.value);
                }
            });


            $("#ticketsReceita input[id*=Valor]").blur(function () {

                if (this.value == "") {

                    this.value = "0%";
                }
                else {

                    if (this.value.indexOf("%") < 0) {

                        this.value += "%";
                    }
                }
            });

            $("#ticketsReceita span").each(function () {


                if (this.innerText == "Reajustes de Insumos" || this.innerText == "Reajuste de Convênios") {

                    $(this).parents("tr").find("input[id*=Valor]").keydown(function () {

                        PermitirSomenteADigitacaoDeNumerosComVirgulaESinalNegativo(event);

                    });


        



                    $(this).parents("tr").find("input[id*=Valor]").focus(function () {

                        this.value = this.value.replace("%", "");
                        this.select();

                    });

                    $(this).parents("tr").find("input[id*=Valor]").blur(function () {

                        if (this.value.replace("%", "") > 100) {

                            this.value = "0,00%";

                        } else {

                            if (/^-?[0-9]{0,2}(\,[0-9]{1,2})?$|^-?(100)(\,[0]{1,2})?$/.test(this.value.replace("%", "")) == false) {

                                this.value = "0,00%"; 
                            }
                        }
                    });

                    $(this).parents("tr").find("input[id*=Valor]").attr("maxlength", "6");
                }
                else {

                    $(this).parents("tr").find("input[id*=Valor]").priceFormat({

                        prefix: '',
                        suffix: '%',
                        centsSeparator: ',',
                        thousandsSeparator: '.',
                        limit: false,
                        centsLimit: 2,
                        clearPrefix: true,
                        clearSufix: true,
                        allowNegative: false

                    });

                    $(this).parents("tr").find("input[id*=Valor]").attr("maxlength", "6");
                }
            });
        });



</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>
            Cadastro de Tickets de Produ&ccedil;&atilde;o</h2>
        <hr />
    </div>
    <div class="grid-12-12">
        <table id="unitarios">
            <asp:Repeater ID="RptUnitarios" runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                                Setor
                            </th>
                            <th class="coluna150px">
                                Sub setor
                            </th>
                            <th class="coluna66px">
                                Jan
                            </th>
                            <th class="coluna66px">
                                Fev
                            </th>
                            <th class="coluna66px">
                                Mar
                            </th>
                            <th class="coluna66px">
                                Abr
                            </th>
                            <th class="coluna66px">
                                Mai
                            </th>
                            <th class="coluna66px">
                                Jun
                            </th>
                            <th class="coluna66px">
                                Jul
                            </th>
                            <th class="coluna66px">
                                Ago
                            </th>
                            <th class="coluna66px">
                                Set
                            </th>
                            <th class="coluna66px">
                                Out
                            </th>
                            <th class="coluna66px">
                                Nov
                            </th>
                            <th class="coluna66px">
                                Dez
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td class="coluna150px">
                            <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                            <%#Eval("Setor.NomeSetor") %>
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Subsetor.NomeSetor") %>
                        </td>
                        <asp:Repeater ID="rptIncrementos" runat="server" DataSource='<%#Eval("Parcelas") %>'>
                            <ItemTemplate>
                                <td class="coluna66px">
                                    <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                                    <asp:TextBox ID="Valor" CssClass="sonumero" runat="server" Text='<%#String.Format("{0:#.###;#,###;0}", Eval("Valor")) %>' MaxLength="7" />
                                    <asp:HiddenField runat="server" ID="Mes" Value='<%#Eval("Mes") %>' />
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-12-12"></div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>
            Cadastro de Premissas</h2>
        <hr />
    </div>
    <div class="grid-12-12">
        <table id="ticketsReceita">
            <asp:Repeater ID="rptTipoDeTickets" runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                                Tipo
                            </th>
                            <th class="coluna66px">
                                Jan
                            </th>
                            <th class="coluna66px">
                                Fev
                            </th>
                            <th class="coluna66px">
                                Mar
                            </th>
                            <th class="coluna66px">
                                Abr
                            </th>
                            <th class="coluna66px">
                                Mai
                            </th>
                            <th class="coluna66px">
                                Jun
                            </th>
                            <th class="coluna66px">
                                Jul
                            </th>
                            <th class="coluna66px">
                                Ago
                            </th>
                            <th class="coluna66px">
                                Set
                            </th>
                            <th class="coluna66px">
                                Out
                            </th>
                            <th class="coluna66px">
                                Nov
                            </th>
                            <th class="coluna66px">
                                Dez
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td class="coluna150px">
                            <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                            <asp:Label runat="server" ID="nomeTicket" Text='<%#Eval("Nome") %>'></asp:Label>                           
                        </td>
                        <asp:Repeater ID="rptIncrementos" runat="server" OnItemDataBound="PreencherTicketDeReceita" DataSource='<%#Eval("Parcelas") %>'>
                            <ItemTemplate>
                                <td class="coluna66px">
                                    <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                                    <asp:TextBox ID="Valor" CssClass="sonumero" runat="server" Text="" />
                                    <asp:HiddenField runat="server" ID="Mes" Value='<%#Eval("Mes") %>' />
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <%--        
    X	% de Reajuste de convênios
    X	% de Glosa interna
    X	% de Impostos
    X	% de Serviços Médicos
    X	% de Descontos Obtidos
    --%>
    <div class="grid-1-12" id="divBotaoSalvar" runat="server">
        <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" Text="Salvar"/>
    </div>
</asp:Content>
