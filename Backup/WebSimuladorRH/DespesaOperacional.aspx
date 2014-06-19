<%@ Page Title="Or&ccedil;amento 2013 - Outras Despesas" Language="C#" MasterPageFile="Principal.Master"
    AutoEventWireup="true" CodeBehind="DespesaOperacional.aspx.cs" Inherits="WebSimuladorRH.DespesaOperacional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            ConfiguracaoDasCaixasDeTextoDaGrid();

            IniciarModal(document);

        });

        function MostrarIconeDeMemoriaDeCalculo(object) {

            var idImagemMemoriaDeCalculo = $("#idImagemMemoriaDeCalculo").val();


            var valorDaMemoriaDeCalculo = $("#" + object.id).parent().find("textarea").val();

            if (valorDaMemoriaDeCalculo === "") {
                $("#" + idImagemMemoriaDeCalculo).hide();
            }
            else {
                $("#" + idImagemMemoriaDeCalculo).show();
            }
        }

        function Carregar(caixaDeTexto, conta) {

            var memoriaDeCalculo = document.getElementById(caixaDeTexto).value;

            $.ajax({
                type: "POST",
                url: "/DespesaOperacional.aspx/InformarMemoriaDeCalculo",
                data: "{'parametro':'" + memoriaDeCalculo + "','texto':'" + conta + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                }
            });
        }

        function ConfiguracaoDasCaixasDeTextoDaGrid() {

            $(".sonumero").keyup(function (event) {


                //InsereMascaraDeMilhar(this);

                if (event.keyCode == 9) {
                    this.select();
                }
            });

            $(".sonumero").keydown(function (event) {
                PermitirSomenteADigitacaoDeNumeros(event);
            });

            $('.sonumero').focus(function () {

                this.value = RemoverTodosOsCaracteresNaoNumericos(this.value);

                var nosTotalizadores = $(this).parent().parent().children(":last").get(0).children;
                nosTotalizadores[1].value = this.value;

                var index = $(this.parentNode).index();
                document.getElementById("somaAnteriorMes" + index).value = this.value;
                document.getElementById("somaAnteriorAnualGeral").value = RemoverTodosOsCaracteresNaoNumericos(document.getElementById("somaAnteriorMes" + index).value);

                this.select();
            });

            $('.sonumero').blur(function () {

                CalcularTotalDeUmaContaPorAno(this);

                CalcularOQuantoFoiGastoComOPagamentoDeContasEmUmMes(this);

                CalcularOQuantoFoiGastoComOPagamentoDeContasEmUmAno(this);

                if (this.value == "") {

                    this.value = "(0)";
                }
                else {

                    if (this.value.indexOf(".") < 0) {

                        this.value = "(" + AplicarMascaraDeMilhar(this.value) + ")";
                    }
                    else {

                        this.value = "(" + this.value + ")";
                    }
                }
            });
        }


        function CalcularTotalDeUmaContaPorAno(object) {

            var nosTotalizadores = $(object).parent().parent().children(":last").get(0).children;
            var numeroAtual = RemoverTodosOsCaracteresNaoNumericos(object.value);
            var numeroAnterior = nosTotalizadores[1].value;

            if (numeroAtual != numeroAnterior) {

                var total = 0;

                total = (RemoverTodosOsCaracteresNaoNumericos(nosTotalizadores[0].innerText) * 1);

                total += ((numeroAtual * 1) - numeroAnterior);

                nosTotalizadores[0].innerText = "(" + AplicarMascaraDeMilhar(total) + ")";
            }
        }

        function CalcularOQuantoFoiGastoComOPagamentoDeContasEmUmMes(object) {

            var index = $(object.parentNode).index();
            var numeroAtual = object.value.replace(/\.|\,00|\(|\)+/g, '');

            var numeroAnterior = document.getElementById("somaAnteriorMes" + index).value;

            if (numeroAtual != numeroAnterior) {

                var total = (document.getElementById("totalmes" + index).value.replace(/\.|\,00|\(|\)+/g, ''));

                total = (total * 1) + ((numeroAtual * 1) - (numeroAnterior * 1));
                document.getElementById("totalmes" + index).value = "(" + AplicarMascaraDeMilhar(total) + ")";
            }
        }

        function CalcularOQuantoFoiGastoComOPagamentoDeContasEmUmAno(object) {

            var numeroAtual = object.value.replace(".", "");
            var numeroAnterior = document.getElementById("somaAnteriorAnualGeral").value;
            var numeroComMascaraDeMilhar = "";

            if (numeroAtual != numeroAnterior) {

                var total = (document.getElementById("totalAnualGeral").value.replace(/\.|\,00|\(|\)+/g, ''));
                total = total.replace(".", "");

                total = (total * 1) + ((numeroAtual * 1) - (numeroAnterior * 1));

                document.getElementById("totalAnualGeral").value = "(" + AplicarMascaraDeMilhar(total) + ")";
            }
        }

        function InsereMascaraDeMilhar(object) {

            var x = 0;
            var num = RemoverTodosOsCaracteresNaoNumericos(object.value);

            if (num.length == object.maxLength) {
                num = num.substring(0, (num.length - 2));
            }

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

        $(window).on('beforeunload', function () {

            $("[id*=btnApagarVersao]").attr('disabled', 'disabled');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12 workkerForm-no-lbl">
        <h2>
            Despesas Operacionais</h2>
        <hr />
    </div>
    <div>
        <input type="text" id="idImagemMemoriaDeCalculo" style="display: none;" />
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Centro de Custo:</label>
        <asp:DropDownList runat="server" ID="dropCentroDeCusto" AutoPostBack="True" OnSelectedIndexChanged="SelecionandoCentroDeCusto">
        </asp:DropDownList>
    </div>
    <div runat="server" id="divVersao" visible="False" class="grid-12-12">
        <label class="workkerForm-lbl-value bold">
            Vers&atilde;o:</label>
        <asp:Literal runat="server" ID="ltlVersaoDoOrcamento"></asp:Literal>
    </div>
    <div class="grid-12-12">
        <div id="boxes">
            <div id="mask">
            </div>
            <table>
                <asp:Repeater runat="server" ID="rptDespesasOperacionais" OnItemDataBound="TratandoOCarregamentoDasDespesasOperacionaisItemAItem">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th class="coluna150px">
                                    Contas
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
                                    2013
                                </th>
                            </tr>
                        </thead>
                        <tbody class="gridBorder">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                            <td class="coluna150px">
                                <div id='<%#Eval("DespesaID") %>' class="window dialog">
                                    <label for="lbMemoriaDeCalculo">
                                        Mem&oacute;ria de C&aacute;lculo:</label>
                                    <br />
                                    <asp:TextBox runat="server" Text='<%#Eval("MemoriaDeCalculo") %>' TextMode="MultiLine"
                                        Style="height: 150px; max-height: 150px; max-width: 370px; width: 370px;" ID="txtMemoriaDeCalculo"></asp:TextBox>
                                    <br />
                                    <asp:Button class="close" runat="server" ID="lknOrcamento" Text="Salvar" OnClientClick="MostrarIconeDeMemoriaDeCalculo(this);" />
                                    <input type="button" class="close" value="Cancelar" />
                                </div>
                                </div>
                                <asp:Image runat="server" ID="imgInformation" Style="display: none;" ImageUrl="Imagem/information.png"
                                    CssClass="teste" />
                                <a href='#<%#Eval("DespesaID") %>' name="modal">
                                    <%#Eval("Conta") %></a>
                                <asp:HiddenField runat="server" ID="idConta" Value='<%#Eval("ContaID") %>' />
                                <asp:HiddenField runat="server" ID="idDespesaOperacional" Value='<%#Eval("DespesaOperacionalID") %>' />
                                <asp:HiddenField runat="server" ID="idDespesaContaID" Value='<%#Eval("DespesaID") %>' />
                            </td>
                            <asp:Repeater runat="server" ID="rptContas" DataSource='<%#Eval("Despesas") %>' OnItemDataBound="CarregandoContas">
                                <ItemTemplate>
                                    <td class="coluna66px">
                                        <asp:HiddenField runat="server" ID="idDespesa" Value='<%#Eval("DespesaId") %>' />
                                        <asp:TextBox runat="server" MaxLength="7" class="sonumero" ID="valorDespesa" Text='<%#string.Format("{0:#,###;#,###;0}", Eval("Valor"))%>'></asp:TextBox>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td class="coluna66px">
                                <asp:Label ID="valorDespesa" runat="server" class="sonumero" Text='<%#string.Format("{0:#,###;#,###;0}", Eval("ValorTotal"))%>'></asp:Label>
                                <input type="text" id="hdSomaVerdadeira" style="display: none;" />
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
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes1" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes1" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes2" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes2" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes3" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes3" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes4" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes4" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes5" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes5" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes6" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes6" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes7" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes7" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes8" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes8" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes9" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes9" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes10" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes10" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes11" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes11" style="display: none;" value="0" />
                                </td>
                                <td class="coluna77px">
                                    <asp:TextBox ID="totalmes12" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorMes12" style="display: none;" value="0" />
                                </td>
                                <td class="coluna88px">
                                    <asp:TextBox ID="totalAnualGeral" ReadOnly="True" ClientIDMode="Static" runat="server"
                                        Text="(0)"></asp:TextBox>
                                    <input type="text" id="somaAnteriorAnualGeral" style="display: none;" value="0" />
                                </td>
                            </tr>
                        </tfoot>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div runat="server" id="divBotaoSalvarOrcamento" visible="False" class="grid-1-12">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar Or&ccedil;amento" OnClick="Salvando"
            OnClientClick=" return confirm('Deseja salvar esse or&ccedil;amento?'); " />
    </div>
    <div runat="server" id="divBotaoAtribuirComoVersaoFinal" visible="False" class="grid-1-12">
        <asp:Button ID="btnVersaoFInal" runat="server" Text="Atribuir Vers&atilde;o Final"
            OnClick="AtribuindoVersaoFinalDoOrcamento" OnClientClick=" return confirm('Deseja atribuir este or&ccedil;amento como vers&atilde;o final?'); " />
    </div>
    <div runat="server" id="divBotaoApagarVersao" visible="False" class="grid-1-12">
        <asp:Button ClientIDMode="Static" ID="btnApagarVersao" runat="server" Text="Apagar Vers&atilde;o"
            OnClick="ApagandoVersao" OnClientClick=" return confirm('Deseja apagar esta vers&atilde;o do or&ccedil;amento?'); " />
    </div>
    <div runat="server" id="divBotaoIncluirNovaVersao" visible="False" class="grid-1-12">
        <asp:Button runat="server" ID="btnIncluirNovaVersao" Text="Incluir Nova Vers&atilde;o"
            OnClick="IncluindoNovaVersao" />
    </div>
    <div runat="server" id="div1" class="grid-1-12">
        <asp:Button runat="server" ID="btnTotalizador" Text="Total Or&ccedil;ado" OnClick="IrParaTotalizador" />
    </div>
    <div class="grid-1-12">
    </div>
    <div class="grid-12-12">
    </div>
    <div class="grid-8-12">
        <table>
            <asp:Repeater runat="server" ID="rptVersoesDeOrcamento" OnItemCommand="SelecionandoVersoesDeOrcamento">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna100px">
                                Vers&atilde;o
                            </th>
                            <th>
                                Centro de Custo
                            </th>
                            <th class="coluna66px">
                                Valor Total
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td class="coluna100px">
                            <asp:LinkButton runat="server" ID="lknOrcamento" CommandName="SelecionarVersao" CommandArgument='<%#Eval("Id") %>'
                                Text='<%#Eval("versao") %>'></asp:LinkButton>
                        </td>
                        <td>
                            <%#Eval("CentroDeCusto") %>
                        </td>
                        <td class="coluna66px">
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
    <asp:Label runat="server" ID="lblValorDespesa"></asp:Label>
</asp:Content>
