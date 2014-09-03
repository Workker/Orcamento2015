<%@ Page Title="Or&ccedil;amento 2015 - Or&ccedil;amento de Viagem" Language="C#" MasterPageFile="Principal.Master"
         AutoEventWireup="true" CodeBehind="OrcamentoDeViagem.aspx.cs" Inherits="WebSimuladorRH.OrcamentoDeViagem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {
            sonumero();

            $(".sonumero").keydown(function (event) {

                PermitirSomenteADigitacaoDeNumeros(event);
            });
        });

        function InsereMascaraDeMilhar(object) {

            var x = 0;
            var num = object.value.replace(".", "");

            if (num < 0) {
                num = Math.abs(num);
                x = 1;
            }

            if (isNaN(num)) num = "";

            num = Math.floor((num * 100 + 0.5) / 100).toString();

            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)

                num = num.substring(0, num.length - (4 * i + 3)) + '.'
                    + num.substring(num.length - (4 * i + 3));
            var ret = num;

            if (x == 1) ret = ' - ' + ret;

            object.value = ret;
        }


        function sonumero() {

            $('.sonumero').focus(function () {

                //this.value = ""; this.value.replace(/\,00|\(|\)+/g, '');
                this.select();
                //this.value = "";

                SubtrairDoTotalDaConta(this);
            });

            $('.sonumero').blur(function () {

                if (this.value == "") {

                    this.value = 0;
                }

                CalcularTotalDaContaAnual(this);

            });
        }

        function CalcularTotalDaContaAnual(object) {

            var numeroAtual = object.value.replace(".", "");
            var numeroAnterior = object.parentNode.parentNode.lastChild.lastChild.previousSibling.value;

            var numeroComMascaraDeMilhar = "";

            if (numeroAtual != numeroAnterior) {

                var total = (object.parentNode.parentNode.lastChild.firstChild.firstChild.data.replace(/\.|\,00|\(|\)+/g, '') * 1);
                var contador = 0;
                total += ((numeroAtual * 1) - numeroAnterior);

                //insere máscara milhar no total
                for (var i = total.toString().length; i >= 1; i--) {
                    
                    
                    if((contador != 0) && (contador%3===0)) {
                        numeroComMascaraDeMilhar = total.toString().charAt(i - 1) + "." + numeroComMascaraDeMilhar;
                    } else {
                        numeroComMascaraDeMilhar = total.toString().charAt(i - 1) + numeroComMascaraDeMilhar;
                    }

                    contador++;
                }

                object.parentNode.parentNode.lastChild.firstChild.firstChild.data = numeroComMascaraDeMilhar;
            }
        }

        function SubtrairDoTotalDaConta(object) {

            object.parentNode.parentNode.lastChild.firstChild.nextSibling.nextSibling.value = object.value.replace(".", "");
        }

        function CalcularTotalMensal(object) {

            var numeroAtual = object.value;
            var index = $(object.parentNode).index();

            index = index - 1;

            var numeroAnterior = document.getElementById("somaAnteriorMes" + index).value;

            if (numeroAtual != numeroAnterior) {

                var total = (document.getElementById("totalmes" + index).value * 1);

                total += ((object.value * 1) - numeroAnterior);
                document.getElementById("totalmes" + index).value = total;
            }
        }

        function CalcularTotalAnual(object) {

            var numeroAtual = object.value;
            var numeroAnterior = document.getElementById("somaAnteriorAnualGeral").value;

            if (numeroAtual != numeroAnterior) {

                var total = (document.getElementById("totalAnualGeral").value * 1);
                total += ((object.value * 1) - numeroAnterior);

                document.getElementById("totalAnualGeral").value = total;
            }
        }

        function DesabilitaBotaoDeExcluir(botao) {

            if (confirm('Deseja apagar esta versão do orçamento?')) {

                botao.disabled = "true";
                
                return true;
            }
            else {

                return false;
            }
        }

        $(window).on('beforeunload', function () {

            $("[id*=btnApagarVersao]").attr('disabled', 'disabled');
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>
            Or&ccedil;amento de Viagem</h2>
        <hr/>
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Centro de Custo:</label>
        <asp:DropDownList  runat="server" ID="dropCentrosDeCusto" AutoPostBack="True" 
                           onselectedindexchanged="SelecionandoCentroDeCusto">
        </asp:DropDownList>
    </div>
    <div class="grid-12-12">
        <label class="workkerForm-lbl-value bold">
            <asp:Literal Visible="False" runat="server" ID="ltlVersao" Text="Vers&atilde;o"></asp:Literal></label>
            <asp:Literal runat="server" ID="versaoDoOrcamento" Visible="False"></asp:Literal>
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptQuantitativosDeViagem" OnItemDataBound="CarregandoQuantitativosDeViagem">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                                Cidades
                            </th>
                            <th class="coluna150px">
                                Quantitativo
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
                            <th class="coluna66px">
                                2015
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd"%>">
                        <td class="coluna150px">
                            <%#Eval("Conta") %>
                            <asp:HiddenField runat="server" ID="idConta" Value='<%#Eval("ContaID") %>' />
                            <asp:HiddenField runat="server" ID="idDespesaOperacional" Value='<%#Eval("DespesaOperacionalID") %>' />
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Despesa") %>
                        </td>
                        <asp:Repeater runat="server" ID="rptContas" DataSource='<%#Eval("Despesas") %>' OnItemDataBound="CarregandoContas">
                            <ItemTemplate>
                                <td class="coluna66px">
                                    <asp:HiddenField runat="server" ID="idDespesa" Value='<%#Eval("DespesaId") %>' />
                                    <asp:TextBox MaxLength="3" runat="server" class="sonumero" ID="valorDespesa" Text='<%#Eval("Valor") %>'></asp:TextBox>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td class="coluna66px">
                            <asp:Label runat="server" class="sonumero" ID="valorDespesa" Text='<%#Eval("ValorTotal") %>'></asp:Label>
                            <input type="text" id="hdSomaVerdadeira" style="display: none;" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-1-12" ID="divBotaoSalvar" runat="server" Visible="False">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar Or&ccedil;amento" OnClick="Salvando"
                    OnClientClick=" return confirm('Deseja salvar esse orçamento?'); " />
    </div>
    <div class="grid-1-12" ID="divBotaoVersaoFinal"  runat="server" Visible="False">
        <asp:Button ID="btnVersaoFInal" runat="server" Text="Atribuir Vers&atilde;o Final" OnClick="AtribuindoVersaoFinal"
                    OnClientClick=" return confirm('Deseja atribuir este orçamento como versão final?'); " />
    </div>
    <div class="grid-1-12" ID="divBotaoApagandoVersao" runat="server" Visible="False">
        <asp:Button ID="btnApagarVersao" runat="server" Text="Apagar Vers&atilde;o" OnClick="ApagandoVersao"
                    OnClientClick="return confirm('Deseja apagar esta versão do orçamento?');" />
    </div>
    <div class="grid-1-12" ID="divBotaoIncluirNovaVersao" runat="server" Visible="False">
        <asp:Button ID="btnIncluirNovaVersao" runat="server" Text="Incluir Nova Vers&atilde;o" 
                    OnClick="IncluindoNovaVersao" />
    </div>    
    <div class="grid-1-12">
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptValoresTotais" OnItemDataBound="CarregandoTotais">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna150px">
                                Cidade
                            </th>
                            <th class="coluna150px">
                                Despesa
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
                            <th class="coluna66px">
                                2015
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex%2 == 0 ? "even" : "odd" %>">
                        <td class="coluna150px">
                            <%#Eval("NomeCidade") %>
                        </td>
                        <td class="coluna150px">
                            <%#Eval("Despesa") %>
                        </td>
                        <asp:Repeater runat="server" ID="rptContas" DataSource='<%#Eval("Itens") %>'>
                            <ItemTemplate>
                                <td class="coluna66px">
                                    (<asp:Literal runat="server" ID="ltlValor" Text=' <%#string.Format("{0:#,###;#,###;0}", Eval("Valor"))%>'></asp:Literal>)
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td class="coluna66px">
                            <%#string.Format("({0:#,###;#,###;0})", Eval("ValorTotal"))%>
                            <input type="text" id="hdSomaVerdadeira" style="display: none;" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </tbody>
                    <tfoot>
                        <tr>
                            <th class="coluna150px bold">
                                Total
                            </th>
                            <td class="coluna150px">
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_1">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_2">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_3">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_4">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_5">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_6">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_7">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_8">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_9">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_10">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_11">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal_12">  </asp:Literal>
                            </td>
                            <td class="coluna66px">
                                <asp:Literal runat="server" ID="ltlTotal">  </asp:Literal>
                            </td>
                        </tr>
                    </tfoot>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-12-12">
    </div>
    <div class="grid-8-12">
        <table>
            <asp:Repeater runat="server" ID="rptVersoesOrcamentoDeViagens" OnItemCommand="SelecionandoVersao">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna100px">
                                Or&ccedil;amento
                            </th>
                            <th class="coluna150px">
                                Centro de Custo
                            </th>
                            <th class="coluna100px">
                                Total Or&ccedil;ado
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex%2 == 0 ? "even" : "odd" %>">
                        <td class="coluna100px">
                            <asp:LinkButton runat="server" ID="lknOrcamento" CommandName="SelecionarVersao"
                                            CommandArgument='<%#Eval("Id") %>' Text='<%#Eval("versao") %>'></asp:LinkButton>
                        </td>
                        <td>
                            <%#Eval("CentroDeCusto") %>
                        </td>
                        <td class="coluna100px">
                            (<%#string.Format("{0:#,###;#,###;0}", Eval("ValorTotal"))%>)
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