<%@ Page Title="Or&ccedil;amento 2013 - Insumos" Language="C#" MasterPageFile="~/Principal.Master"
         AutoEventWireup="true" CodeBehind="Insumos.aspx.cs" Inherits="WebSimuladorRH.Insumo.Insumos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/OrcamentoDeProducao.js"> </script>
    <script type="text/javascript">
        $(document).ready(function() {

            IniciarModal(document);

            sonumero();

            $("[id*=Complexidade]").keydown(function(event) {

                PermitirSomenteADigitacaoDeNumerosComVirgulaESinalNegativo(event);
            });

            $("#custoUnitario input[id*=txtValor]").blur(function() {

                var nosTotalizadores = $(this).parent().parent().children(":last").find("span").get(0);

                if (this.value == "") {

                    this.value = "(0)";
                } else {

                    if (this.value.indexOf("(") < 0) {

                        this.value = "(" + this.value + ")";
                    }
                }

                //nosTotalizadores.innerText = "(" + roundNumber((ObterASomaDeTodosOsCamposDeUmaLinhaDeUmaTabela(this) / 12), 2).toString().replace(".", ",") + ")";

                var media = roundNumber((ObterASomaDeTodosOsCamposDeUmaLinhaDeUmaTabela(this) / 12), 2);

                if (media.toString().indexOf(".") > 0) {

                    media = media.toString().split(".");

                    if (media[0].indexOf("-") >= 0) {

                        media = "-" + AplicarMascaraDeMilhar(media[0]) + "," + media[1];
                    } else {

                        media = AplicarMascaraDeMilhar(media[0]) + "," + media[1];
                    }
                } else {
                    media = AplicarMascaraDeMilhar(media);
                }

                nosTotalizadores.innerText = media;
            });

            $("#custoUnitario input[id*=txtValor]").focus(function() {

                this.select();
            });

        });

        function ObterASomaDeTodosOsCamposDeUmaLinhaDeUmaTabela(campo) {

            var total = 0;

            $(campo).closest('tr').find("input[id*=txtValor]").each(function() {

                if ($(this).val().indexOf("-") >= 0) {

                    total += parseInt("-" + RemoverTodosOsCaracteresNaoNumericos($(this).val()));
                } else {

                    total += parseInt(RemoverTodosOsCaracteresNaoNumericos($(this).val()));
                }
            });

            return total.toString();
        }


        function MostrarIconeDeMemoriaDeCalculo(object) {

            var idImagemMemoriaDeCalculo = $("#idImagemMemoriaDeCalculo").val();

            var valorDaMemoriaDeCalculo = $("#" + object.id).parent().find("textarea").val();

            if (valorDaMemoriaDeCalculo === "") {
                $("#" + idImagemMemoriaDeCalculo).hide();
            } else {
                $("#" + idImagemMemoriaDeCalculo).show();
            }
        }

        function Carregar(caixaDeTexto, conta) {

            var memoriaDeCalculo = document.getElementById(caixaDeTexto).value;

            $.ajax({
                type: "POST",
                url: "/OrcamentoDeProducao.aspx/InformarMemoriaDeCalculoComplexidade",
                data: "{'parametro':'" + memoriaDeCalculo + "','texto':'" + conta + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {

                }
            });
        }

        function CarregarUnitario(caixaDeTexto, conta) {

            var memoriaDeCalculo = document.getElementById(caixaDeTexto).value;

            $.ajax({
                type: "POST",
                url: "/OrcamentoDeProducao.aspx/InformarMemoriaDeCalculoUnitario",
                data: "{'parametro':'" + memoriaDeCalculo + "','texto':'" + conta + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {

                }
            });
        }

        $(window).on('beforeunload', function() {

            $("[id*=btnApagarVersao]").attr('disabled', 'disabled');
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList runat="server" Visible="False" ID="ddlSetor">
    </asp:DropDownList>
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>
            Insumos</h2>
        <hr/>
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater ID="rptUtiUnidades" runat="server" OnItemDataBound="TotalizarMediaUnitaria">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th style="background-color: #000;" class="coluna150px">
                                Produção
                            </th>
                            <th style="background-color: #000;" class="coluna150px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                        </tr>
                        <tr>
                            <th class="coluna150px">
                                Setor
                            </th>
                            <th class="coluna150px">
                                Sub Setor
                            </th>
                            <th class="coluna100px">
                                Conta
                            </th>
                            <th class="coluna100px">
                                Jan
                            </th>
                            <th class="coluna100px">
                                Fev
                            </th>
                            <th class="coluna100px">
                                Mar
                            </th>
                            <th class="coluna100px">
                                Abr
                            </th>
                            <th class="coluna100px">
                                Mai
                            </th>
                            <th class="coluna100px">
                                Jun
                            </th>
                            <th class="coluna100px">
                                Jul
                            </th>
                            <th class="coluna100px">
                                Ago
                            </th>
                            <th class="coluna100px">
                                Set
                            </th>
                            <th class="coluna100px">
                                Out
                            </th>
                            <th class="coluna100px">
                                Nov
                            </th>
                            <th class="coluna100px">
                                Dez
                            </th>
                            <th class="coluna100px">
                                M&eacute;dia
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td class="coluna150px">
                            <%#Eval("Setor") %>
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Subsetor") %>
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Conta") %>
                        </td>
                        <asp:Repeater ID="rptValoresDosSetores" runat="server" DataSource='<%#Eval("Valores") %>'>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td class="coluna100px">
                                    <asp:HiddenField runat="server" ID="tipoValor" Value='<%#Eval("TipoValor") %>' />
                                    <asp:Literal runat="server" ID="ltlValorMes" Text='<%# Eval("Valor") %>'></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td class="coluna100px">
                            <asp:Label ID="ltlValorMedia" runat="server" Text="0"></asp:Label>
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
        <table id="custoUnitario">
            <asp:Repeater ID="rptCustoUnitario" runat="server" OnItemDataBound="TotalizarMedia">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th style="background-color: #000;" class="coluna150px">
                                Custo Unit&aacute;rio
                            </th>
                         
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                        </tr>
                        <tr>
                            <th class="coluna150px">
                                Setor
                            </th>
                            <th class="coluna150px">
                                Sub Setor
                            </th>
                         
                            <th class="coluna100px">
                                Jan
                            </th>
                            <th class="coluna100px">
                                Fev
                            </th>
                            <th class="coluna100px">
                                Mar
                            </th>
                            <th class="coluna100px">
                                Abr
                            </th>
                            <th class="coluna100px">
                                Mai
                            </th>
                            <th class="coluna100px">
                                Jun
                            </th>
                            <th class="coluna100px">
                                Jul
                            </th>
                            <th class="coluna100px">
                                Ago
                            </th>
                            <th class="coluna100px">
                                Set
                            </th>
                            <th class="coluna100px">
                                Out
                            </th>
                            <th class="coluna100px">
                                Nov
                            </th>
                            <th class="coluna100px">
                                Dez
                            </th>
                            <th class="coluna100px">
                                Média
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex%2 == 0 ? "even" : "odd" %>">
                        <td class="coluna150px">
                            <asp:HiddenField runat="server" ID="IdServico" Value='<%#Eval("IdServico") %>' />
                            <%#Eval("Setor") %>
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Subsetor") %>
                        </td>
                        
                        <asp:Repeater ID="rptValoresDosSetores" runat="server" DataSource='<%#Eval("Valores") %>'
                                      OnItemDataBound="CarregandoValoresDosSetores">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td class="coluna100px">
                                    <asp:HiddenField runat="server" ID="tipoValor" Value='<%#Eval("TipoValor") %>' />
                                    <asp:HiddenField runat="server" ID="ValorID" Value='<%#Eval("ValorID") %>' />
                                    <asp:HiddenField runat="server" ID="Mes" Value='<%#Eval("Mes") %>' />
                                    <asp:HiddenField runat="server" ID="Calculado" Value='<%#Eval("Calculado") %>' />
                                    <asp:HiddenField runat="server" ID="contaId" Value='<%#Eval("ContaId") %>' />
                                    <asp:TextBox ID="txtValor" Visible='<%#!Convert.ToBoolean(Eval("Calculado")) %>'
                                                 runat="server" Text='<%#string.Format("{0:#,###;#,###;0}", "(" + Eval("Valor") + ")") %>'
                                                 MaxLength="10" />
                                    <asp:Literal runat="server" Visible='<%#Convert.ToBoolean(Eval("Calculado")) %>'
                                                 ID="ltlValor" Text='<%#Eval("Valor") %>'></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td class="coluna100px">
                            <asp:Label ID="valorDespesa" runat="server" class="sonumero" Text="0"></asp:Label>
                            <input type="text" style="display: none" />
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
            <asp:Repeater ID="rptCustoUnitarioTotal" OnItemDataBound="TotalMesCustoUnitario"
                          runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th style="background-color: #000;" class="coluna150px">
                                Despesa Insumos
                            </th>
                           
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                            <th style="background-color: #000;" class="coluna100px">
                            </th>
                        </tr>
                        <tr>
                            <th class="coluna150px">
                                Setor
                            </th>
                            <th class="coluna150px">
                                Sub Setor
                            </th>
                           
                            <th class="coluna100px">
                                Jan
                            </th>
                            <th class="coluna100px">
                                Fev
                            </th>
                            <th class="coluna100px">
                                Mar
                            </th>
                            <th class="coluna100px">
                                Abr
                            </th>
                            <th class="coluna100px">
                                Mai
                            </th>
                            <th class="coluna100px">
                                Jun
                            </th>
                            <th class="coluna100px">
                                Jul
                            </th>
                            <th class="coluna100px">
                                Ago
                            </th>
                            <th class="coluna100px">
                                Set
                            </th>
                            <th class="coluna100px">
                                Out
                            </th>
                            <th class="coluna100px">
                                Nov
                            </th>
                            <th class="coluna100px">
                                Dez
                            </th>
                            <th class="coluna100px">
                                Total
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#                Container.ItemIndex%2 == 0 ? "even" : "odd" %>">
                        <td class="coluna150px">
                            <%#Eval("Setor") %>
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Subsetor") %>
                        </td>
                      
                        <asp:Repeater ID="rptValoresDosSetores" OnItemDataBound="TotalCustoUnitario" runat="server"
                                      DataSource='<%#Eval("Valores") %>'>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td class="coluna100px">
                                    <asp:HiddenField runat="server" ID="tipoValor" Value='<%#Eval("TipoValor") %>' />
                                    <asp:Literal runat="server" ID="ltlValorMes" Text='<%#                string.Format("{0:#,###;#,###;0}", "(" + Eval("Valor") + ")") %> '></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td class="coluna100px">
                            <asp:Literal ID="ltlTotal" runat="server" Text="0"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </tbody>
                    <tfoot>
                        <tr>
                            <td class="coluna150px bold">
                                Total
                            </td>
                            <td class="coluna150px bold">
                            </td>
                           
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes1" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes2" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes3" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes4" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes5" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes6" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes7" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes8" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes9" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes10" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes11" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalMes12" runat="server" Text="0"></asp:Literal>
                            </td>
                            <td class="coluna100px">
                                <asp:Literal ID="ltlTotalAno" runat="server" Text="0"></asp:Literal>
                            </td>
                        </tr>
                    </tfoot>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-1-12" id="divBotaoSalvar" runat="server">
        <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" Text="Salvar Insumos"
                    OnClientClick=" return confirm('Deseja Insumos?'); " />
    </div>
</asp:Content>