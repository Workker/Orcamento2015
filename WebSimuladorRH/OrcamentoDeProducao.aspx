<%@ Page Title="Or&ccedil;amento 2016 - Receita" Language="C#" MasterPageFile="Principal.Master"
    AutoEventWireup="true" CodeBehind="OrcamentoDeProducao.aspx.cs" Inherits="WebSimuladorRH.OrcamentoDeProducao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="Scripts/OrcamentoDeProducao.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            IniciarModal(document);

            sonumero();


            $("[id*=Complexidade]").keydown(function (event) {

                PermitirSomenteADigitacaoDeNumerosComVirgulaESinalNegativo(event);

            });
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
                url: "/OrcamentoDeProducao.aspx/InformarMemoriaDeCalculoComplexidade",
                data: "{'parametro':'" + memoriaDeCalculo + "','texto':'" + conta + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

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
                success: function (msg) {

                }
            });
        }

        $(window).on('beforeunload', function () {

            $("[id*=btnApagarVersao]").attr('disabled', 'disabled');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="boxes">
        <div id="mask">
        </div>
        <asp:DropDownList Visible="false" runat="server" ID="dropSetor">
        </asp:DropDownList>
        <div class="grid-12-12 workkerForm-no-lbl">
            <h2>
                Or&ccedil;amento de Receita</h2>
            <hr />
        </div>
        <div>
            <input type="text" id="idImagemMemoriaDeCalculo" style="display: none;" />
        </div>
        <div class="grid-12-12">
            <label class="workkerForm-lbl-value bold">
                Hospital:</label>
            <asp:Literal runat="server" ID="ltNomehospital"></asp:Literal>
        </div>
        <div class="grid-12-12">
            <label class="workkerForm-lbl-value bold">
                <asp:Literal runat="server" ID="ltlVersao" Text="Vers&atilde;o:"></asp:Literal>
            </label>
            <asp:Literal runat="server" ID="versaoDoOrcamento"></asp:Literal>
        </div>
        <div class="grid-12-12">
            <table id="tblProducao">
                <asp:Repeater ID="rptUtiUnidades" runat="server" OnItemDataBound="TotalizarMedia">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th style="background-color: #000;" class="coluna150px">
                                    <asp:Image runat="server" ID="imgInformation" Style="display: none;" ImageUrl="Imagem/information.png"/>
                                    <a href="#divReceita" name="modal" class="bold">Produção</a>
                                    <div id="divReceita" class="window dialog">
                                        <label for="lbMemoriaDeCalculo">
                                            Produ&ccedil;&atilde;o:</label>
                                        <br />
                                        <asp:TextBox runat="server" Text='' TextMode="MultiLine" Style="height: 150px; max-height: 150px;
                                            max-width: 370px; width: 370px;" ID="txtMemoriaDeCalculo"></asp:TextBox>
                                        <br />
                                        <asp:Button class="close" runat="server" ID="lknOrcamento" Text="Salvar" OnClientClick="MostrarIconeDeMemoriaDeCalculo(this);" />
                                        <input type="button" class="close" value="Cancelar" />
                                    </div>
                                </th>
                                <th style="background-color: #000;" class="coluna150px">
                                </th>
                                <th style="background-color: #000;" class="coluna150px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                            </tr>
                            <tr>
                                <th class="coluna150px">
                                    Setor
                                </th>
                                <th class="coluna150px">
                                    Sub Setor
                                </th>
                                <th class="coluna150px">
                                    Conta
                                </th>
                                <th class="coluna77px">
                                    Jan
                                </th>
                                <th class="coluna77px">
                                    Fev
                                </th>
                                <th class="coluna77px">
                                    Mar
                                </th>
                                <th class="coluna77px">
                                    Abr
                                </th>
                                <th class="coluna77px">
                                    Mai
                                </th>
                                <th class="coluna77px">
                                    Jun
                                </th>
                                <th class="coluna77px">
                                    Jul
                                </th>
                                <th class="coluna77px">
                                    Ago
                                </th>
                                <th class="coluna77px">
                                    Set
                                </th>
                                <th class="coluna77px">
                                    Out
                                </th>
                                <th class="coluna77px">
                                    Nov
                                </th>
                                <th class="coluna77px">
                                    Dez
                                </th>
                                <th class="coluna77px">
                                    Média
                                </th>
                            </tr>
                        </thead>
                        <tbody class="gridBorder">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                            <td class="coluna150px">
                                <asp:HiddenField runat="server" ID="IdServico" Value='<%#Eval("IdServico") %>' />
                                <%#Eval("Setor") %>
                            </td>
                            <td class="coluna150px">
                                <%#Eval("Subsetor") %>
                            </td>
                            <td class="coluna150px">
                                <%#Eval("Conta") %>
                            </td>
                            <asp:Repeater ID="rptValoresDosSetores" runat="server" DataSource='<%#Eval("Valores") %>'
                                OnItemDataBound="CarregandoValoresDosSetores">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <td class="coluna77px">
                                        <asp:HiddenField runat="server" ID="tipoValor" Value='<%#Eval("TipoValor") %>' />
                                        <asp:HiddenField runat="server" ID="ValorID" Value='<%#Eval("ValorID") %>' />
                                        <asp:HiddenField runat="server" ID="Mes" Value='<%#Eval("Mes") %>' />
                                        <asp:HiddenField runat="server" ID="Calculado" Value='<%#Eval("Calculado") %>' />
                                        <asp:HiddenField runat="server" ID="contaId" Value='<%#Eval("ContaId") %>' />
                                        <asp:TextBox ID="txtValor" Visible='<%#! Convert.ToBoolean(Eval("Calculado"))  %>'
                                            CssClass="sonumero" runat="server" Text='<%#string.Format("{0:#.###;#,###;0}", Eval("Valor"))%>' MaxLength="10" />
                                        <asp:Literal runat="server" Visible='<%# Convert.ToBoolean(Eval("Calculado")) %>'
                                            ID="ltlValor" Text='<%#string.Format("{0:#,###;#,###;0}", Eval("Valor"))%>'></asp:Literal>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td class="coluna77px">
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
        <div class="grid-12-12">
            <table id="Unitarios">
                <asp:Repeater ID="RptUnitarios" runat="server">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th style="background-color: #000;" class="coluna150px">
                                    <asp:Literal runat="server" ID="ltlUnitario" Text="Unitarios"></asp:Literal>
                                </th>
                                <th style="background-color: #000;" class="coluna150px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                            </tr>
                            <tr>
                                <th class="coluna150px">
                                    Setor
                                </th>
                                <th class="coluna150px">
                                    Sub setor
                                </th>
                                <th class="coluna77px">
                                    Jan
                                </th>
                                <th class="coluna77px">
                                    Fev
                                </th>
                                <th class="coluna77px">
                                    Mar
                                </th>
                                <th class="coluna77px">
                                    Abr
                                </th>
                                <th class="coluna77px">
                                    Mai
                                </th>
                                <th class="coluna77px">
                                    Jun
                                </th>
                                <th class="coluna77px">
                                    Jul
                                </th>
                                <th class="coluna77px">
                                    Ago
                                </th>
                                <th class="coluna77px">
                                    Set
                                </th>
                                <th class="coluna77px">
                                    Out
                                </th>
                                <th class="coluna77px">
                                    Nov
                                </th>
                                <th class="coluna77px">
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
                                    <td class="coluna77px">
                                        <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                                        <asp:Literal runat="server" ID="Valor" Text='<%#String.Format("{0:#,###;#,###;0}", Eval("Valor")) %>'></asp:Literal>
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
        <div class="grid-12-12">
            <table id="complexidade">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="CarregarTotalizadorComplexidade">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th style="background-color: #000;" class="coluna150px">
                                    <asp:Image runat="server" ID="imgInformation" Style="display: none;" ImageUrl="Imagem/information.png"
                                        CssClass="teste" />
                                    <a href="#divComplex" name="modal" class="bold">Complexidade</a>
                                    <div id="divComplex" class="window dialog">
                                        <label for="lbMemoriaDeCalculo">
                                            Mem&oacute;ria de C&aacute;lculo:</label>
                                        <br />
                                        <asp:TextBox runat="server" TextMode="MultiLine" Style="height: 150px; max-height: 150px;
                                            max-width: 370px; width: 370px;" ID="txtMemoriaDeCalculo"></asp:TextBox>
                                        <br />
                                        <asp:Button class="close" runat="server" ID="lknOrcamento" Text="Salvar" OnClientClick="MostrarIconeDeMemoriaDeCalculo(this);" />
                                        <input type="button" class="close" value="Cancelar" />
                                    </div>
                                </th>
                                <th style="background-color: #000;" class="coluna150px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                            </tr>
                            <tr>
                                <th class="coluna150px">
                                    Setor
                                </th>
                                <th class="coluna150px">
                                    Sub setor
                                </th>
                                <th class="coluna77px">
                                    Jan
                                </th>
                                <th class="coluna77px">
                                    Fev
                                </th>
                                <th class="coluna77px">
                                    Mar
                                </th>
                                <th class="coluna77px">
                                    Abr
                                </th>
                                <th class="coluna77px">
                                    Mai
                                </th>
                                <th class="coluna77px">
                                    Jun
                                </th>
                                <th class="coluna77px">
                                    Jul
                                </th>
                                <th class="coluna77px">
                                    Ago
                                </th>
                                <th class="coluna77px">
                                    Set
                                </th>
                                <th class="coluna77px">
                                    Out
                                </th>
                                <th class="coluna77px">
                                    Nov
                                </th>
                                <th class="coluna77px">
                                    Dez
                                </th>
                                <th class="coluna77px">
                                    Total
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
                                <%# Eval("Subsetor.NomeSetor") %>
                            </td>
                            <asp:Repeater ID="rptIncrementos" OnItemDataBound="PreencherComplexidade" runat="server"
                                DataSource='<%#Eval("Incrementos") %>'>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <td class="coluna77px">
                                        <asp:HiddenField runat="server" ID="Id" Value='<%#Eval("Id") %>' />
                                        <asp:TextBox ID="Complexidade" CssClass="sonumero" runat="server" Text="" />
                                        <asp:HiddenField runat="server" ID="Mes" Value='<%#Eval("Mes") %>' />
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td class="coluna77px">
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtTotal"></asp:TextBox>
                                <%-- <input type="text" value="0" readonly="readonly" />--%>
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
        <div class="grid-1-12" id="divBotaoSalvar" runat="server" visible="False">
            <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" Text="Salvar Orcamento"
                OnClientClick=" return confirm('Deseja salvar esse orçamento?'); " />
            
        </div>
          <div class="grid-1-12" id="divBotaoClonar" runat="server"  visible="False">
            <asp:Button ID="btnClonar" runat="server" OnClick="btnClonar_Click" Text="Clonar Orçamento"
                OnClientClick=" return confirm('Deseja clonar esse orçamento?'); " />
                   </div>
        <div class="grid-1-12" id="divBotaoVersaoFinal" runat="server" visible="False">
            <asp:Button OnClick="VersaoFinal_Click" ID="btnVersaoFInal" runat="server" Text="Atribuir versão final"
                OnClientClick=" return confirm('Deseja atribuir este orçamento como versão final?'); " />
        </div>
        <div class="grid-1-12" id="divBotaoApagandoVersao" runat="server" visible="False">
            <asp:Button ID="btnApagarVersao" OnClick="ApagarVersao_Click" runat="server" Text="Apagar Versão"
                OnClientClick=" return confirm('Deseja apagar esta versão do orçamento?'); " />
        </div>
        <div class="grid-1-12" id="divBotaoIncluirNovaVersao" runat="server" visible="False">
            <asp:Button ID="btnIncluirNovaVersao" runat="server" Text="Incluir Nova Vers&atilde;o"
                OnClick="IncluindoNovaVersao" />
        </div>
        <div class="grid-12-12">
            <table>
                <asp:Repeater ID="rptReceitaBruta" runat="server" OnItemDataBound="TotalReceitaBruta">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th style="background-color: #000;" class="coluna150px">
                                    Receita Bruta
                                </th>
                                <th style="background-color: #000;" class="coluna150px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                                <th style="background-color: #000;" class="coluna77px">
                                </th>
                            </tr>
                            <tr>
                                <th class="coluna150px">
                                    Setor
                                </th>
                                <th class="coluna150px">
                                    Sub setor
                                </th>
                                <th class="coluna77px">
                                    Jan
                                </th>
                                <th class="coluna77px">
                                    Fev
                                </th>
                                <th class="coluna77px">
                                    Mar
                                </th>
                                <th class="coluna77px">
                                    Abr
                                </th>
                                <th class="coluna77px">
                                    Mai
                                </th>
                                <th class="coluna77px">
                                    Jun
                                </th>
                                <th class="coluna77px">
                                    Jul
                                </th>
                                <th class="coluna77px">
                                    Ago
                                </th>
                                <th class="coluna77px">
                                    Set
                                </th>
                                <th class="coluna77px">
                                    Out
                                </th>
                                <th class="coluna77px">
                                    Nov
                                </th>
                                <th class="coluna77px">
                                    Dez
                                </th>
                                <th class="coluna77px">
                                    Total
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
                                <%#Eval("SubSetor")%>
                            </td>
                            <asp:Repeater ID="rptIncrementos" runat="server" DataSource='<%#Eval("Incrementos") %>'>
                                <ItemTemplate>
                                    <td class="coluna77px">
                                                         
                                        <%#String.Format("{0:#,###;#,###;0}", Eval("ReceitaLiquida"))%>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td class="coluna77px">
                                <asp:Literal runat="server" ID="ltlTotal" Text="0"></asp:Literal>
                                <asp:HiddenField runat="server" ID="id" Value='<%#Eval("Id") %>' />
                                 <asp:HiddenField runat="server" ID="SubSetor" Value='<%#Eval("SubSetor") %>' />
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
                                    Orçado
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes1" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes2" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes3" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes4" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes5" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes6" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes7" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes8" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes9" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes10" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes11" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalMes12" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlTotalAno" runat="server" Text="0"></asp:Literal>
                                </td>
                            </tr>
                        </tfoot>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="grid-8-12">
        <table>
            <asp:Repeater runat="server" ID="rptOrcamentosDeProducao" OnItemCommand="SelecionandoVersoesDeOrcamento">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna100px">
                                Vers&atilde;o
                            </th>
                            <th class="coluna77px">
                                Valor Total
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td class="coluna100px">
                            <asp:LinkButton runat="server" ID="lknOrcamento" CommandName="SelecionarVersao" CommandArgument='<%#Eval("Id") %>'
                                Text='<%#Eval("versao") %>'></asp:LinkButton>
                        </td>
                        <td class="coluna77px">

                            <%#string.Format("{0:#,###;#,###;0}", Eval("Total"))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div runat="server" id="espacamento" style="margin-top: 40%" visible="False">
        &nbsp;
    </div>
    <%--<div runat="server" clientidmode="Static" id="footer" visible="False">
        <div style="float: left; padding-top: 1%;">
            <asp:Button ID="btnCalcularTotalMensal" runat="server" Text="Calcular" OnClick="btnCalcularTotalMensal_Click" />
        </div>
        <div style="float: right; width: 85%">
            <table runat="server" id="tabelaDeRodapeFixo" style="border-spacing: 0px; border-collapse: collapse;">
                <thead>
                    <tr style="background-color: #595959; color: #FFF !important;">
                        <th class="coluna77px">
                            Jan
                        </th>
                        <th class="coluna77px">
                            Fev
                        </th>
                        <th class="coluna77px">
                            Mar
                        </th>
                        <th class="coluna77px">
                            Abr
                        </th>
                        <th class="coluna77px">
                            Mai
                        </th>
                        <th class="coluna77px">
                            Jun
                        </th>
                        <th class="coluna77px">
                            Jul
                        </th>
                        <th class="coluna77px">
                            Ago
                        </th>
                        <th class="coluna77px">
                            Set
                        </th>
                        <th class="coluna77px">
                            Out
                        </th>
                        <th class="coluna77px">
                            Nov
                        </th>
                        <th class="coluna77px">
                            Dez
                        </th>
                        <th class="coluna77px">
                            Total
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes1" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes2" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes3" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes4" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes5" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes6" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes7" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes8" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes9" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes10" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes11" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalMes12" runat="server" Text="0"></asp:Literal>
                        </td>
                        <td style="border: 1px solid #dfe8f7;" class="coluna77px">
                            <asp:Literal ID="ltlTotalAno" runat="server" Text="0"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>--%>
</asp:Content>
