<%@ Page Title="Orçamento 2014 - Funcion&aacute;rio" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Funcionario.aspx.cs" Inherits="WebSimuladorRH.Pessoal.Funcionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $(".sonumero").keydown(function (event) {
                PermitirSomenteADigitacaoDeNumerosComVirgula(event);
            });


            $("[id*=txtDataDeAdmissao]").blur(function (event) {

                if (!ValidarData(this.value)) {
                    alert("Data inválida.");
                    this.value = "";
                }
            });

            $('[id*=txtSalario]').priceFormat();

            ConfiguracaoDaCaixaDeTextoDeSalario();
        });

        function ConfiguracaoDaCaixaDeTextoDeSalario() {


            $("[id*=txtSalario]").focus(function (event) {
                this.select();
            });
        }

        function exibirCamposDemissao(chkDemitido) {

            // alert("Será necessário repor esta vaga");

            var id = chkDemitido.id;
            var idArray = id.split("_");
            var prefixo = idArray[0] + "_" + idArray[1] + "_" + idArray[2] + "_" + idArray[3] + "_";

            if (chkDemitido.checked) {
                $("#" + prefixo + "chkAumentado").attr('checked', false);

                $("#" + prefixo + "ddlMesesDeAumento").attr('disabled', true);
                $("#" + prefixo + "ddlMesesDeAumento").attr('value', '0');
                $("#" + prefixo + "txtPercentualDeAumento").attr('disabled', true);
                $("#" + prefixo + "txtPercentualDeAumento").attr('value', '0');
                $("#" + prefixo + "ddlMesDeDemissao").attr('disabled', false);
            } else {

                $("#" + prefixo + "ddlMesDeDemissao").attr('disabled', true);
            }
        }

        //            } else {
        //                $("#" + prefixo + "chkDemitido").attr('disabled', true);
        //                $("#" + prefixo + "chkAumentado").attr('disabled', false);
        //                // $("#" + prefixo + "chkAumentado").attr('checked', true);
        //                $("#" + prefixo + "ddlMesesDeAumento").attr('disabled', false);
        //                $("#" + prefixo + "txtPercentualDeAumento").attr('disabled', false);
        //                $("#" + prefixo + "ddlMesDeDemissao").attr('disabled', true);
        //            }
        //        }

        function exibirCamposAumento(element) {

            var id = element.id;
            var idArray = id.split("_");
            var prefixo = idArray[0] + "_" + idArray[1] + "_" + idArray[2] + "_" + idArray[3] + "_";

            if (element.checked) {
                //$("#" + prefixo + "chkAumentado").attr('disabled', false);
                $("#" + prefixo + "ddlMesesDeAumento").attr('disabled', false);
                $("#" + prefixo + "txtPercentualDeAumento").attr('disabled', false);
                $("#" + prefixo + "ddlMesDeDemissao").attr('disabled', true);
                $("#" + prefixo + "ddlMesDeDemissao").attr('value', '0');
                $("#" + prefixo + "chkDemitido").attr('checked', false);

            } else {
                $("#" + prefixo + "txtPercentualDeAumento").attr('disabled', true);
                $("#" + prefixo + "ddlMesesDeAumento").attr('disabled', true);
                /*
                    $("#" + prefixo + "chkAumentado").attr('disabled', true);
                    $("#" + prefixo + "ddlMesesDeAumento").attr('disabled', true);
                    $("#" + prefixo + "txtPercentualDeAumento").attr('disabled', true);
                    $("#" + prefixo + "ddlMesDeDemissao").attr('disabled', false);
                    $("#" + prefixo + "chkDemitido").attr('disabled', false);
                    //$("#" + prefixo + "chkDemitido").attr('checked', true);*/
            }
        }

        //function CarregarVaga() {
        //    alert('Ta la');
        //    debugger;
        //    $.ajax({
        //        type: "POST",
        //        url: "/Funcionarios.aspx/CarregarVaga",
        //        //    data: "{'parametro':'" + ddlDataDeAdmissao.val() + "'}",
        //        data: "",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (msg) {
        //        }
        //    });
        //}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MyScriptManager" runat="server" />
    <div class="grid-12-12">
        <h2>Funcion&aacute;rio</h2>
        <hr />
    </div>
    <div class="grid-6-12">
        <label class="workkerForm-lbl-value bold">
            Centro de Custo:
        </label>
        <asp:DropDownList runat="server" ID="ddlCentrosDeCusto" AutoPostBack="True" OnSelectedIndexChanged="SelecionandoCentroDeCusto" />
    </div>
   
    <div class="grid-12-12"></div>
    <div class="grid-12-12" runat="server" id="divHeadCount" visible="False">
        <label class="workkerForm-lbl-value bold">
            Head Count:</label>
        <asp:Literal runat="server" ID="HeadCount">
        </asp:Literal>
    </div>
    <%--    <div class="grid-2-12">
        <label class="workkerForm-lbl-value">Matr&iacute;cula:&nbsp;</label>
        <asp:TextBox runat="server" ID="txtMatricula"></asp:TextBox>
    </div>--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="grid-2-12">
                <label class="workkerForm-lbl-value">Motivo:&nbsp;</label><br />
                <asp:DropDownList AutoPostBack="true"
                    OnSelectedIndexChanged="Dll_Motivo" ID="txtNome" runat="server">
                    <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Aumento de Quadro" Value="Aumento de Quadro" />
                    <asp:ListItem Text="Reposição de Vaga" Value="Reposição de Vaga" />
                </asp:DropDownList>

            </div>
            <div class="grid-4-12">
                <label class="workkerForm-lbl-value">
                    M&ecirc;s de admiss&atilde;o:&nbsp;
                </label>
                <br />

                <asp:DropDownList ID="ddlDataDeAdmissao" runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="Dll_DataAdmissaoChange">
                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                    <asp:ListItem Text="Janeiro" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Fevereiro" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Mar&ccedil;o" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Maio" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Junho" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Julho" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                    <asp:ListItem Text="Setembro" Value="9"></asp:ListItem>
                    <asp:ListItem Text="Outubro" Value="10"></asp:ListItem>
                    <asp:ListItem Text="Novembro" Value="11"></asp:ListItem>
                    <asp:ListItem Text="Dezembro" Value="12"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div runat="server" id="DivVaga" visible="False" class="grid-2-12">
                <label class="workkerForm-lbl-value">Vaga:&nbsp;</label><br />
                <asp:DropDownList ID="ddlVaga" runat="server">
                    <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="grid-1-12">
                <label class="workkerForm-lbl-value">Cargo:</label><br />
                <asp:TextBox runat="server" ID="txCargo"></asp:TextBox>
            </div>
            <div class="grid-2-12">
                <label class="sonumero workkerForm-lbl-value">Sal&aacute;rio:&nbsp;</label><br />
                <asp:TextBox runat="server" ID="txtSalario" MaxLength="9" Text="0,00"></asp:TextBox>
            </div>
            <div class="grid-12-12"></div>


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlDataDeAdmissao" EventName="SelectedIndexChanged" />
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtNome" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="grid-12-12"></div>
    <div class="grid-3-12">

        <asp:Button runat="server" ID="btnIncluirNovoFuncionario"
            Text="Incluir Novo Funcion&aacute;rio" OnClick="IncluindoNovoFuncionario" />
    </div>
     <div class="grid-3-12">
        <asp:Button ID="Button1" runat="server" OnClick="Salvando" Text="Salvar" />
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater runat="server" ID="rptFuncionarios" OnItemCommand="DeletarFuncionario">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="coluna190px">Nº de vaga 
                            </th>
                            <th class="coluna55px">Matr&iacute;cula
                            </th>
                            <th class="coluna190px">Funcion&aacute;rio
                            </th>
                            <th class="coluna190px">Cargo
                            </th>

                            <th class="coluna77px">Sal&aacute;rio Base
                            </th>
                            <th class="coluna66px">Ano Admiss&atilde;o
                            </th>
                            <th class="coluna66px">M&ecirc;s Admiss&atilde;o
                            </th>
                            <th class="coluna66px">Demitido
                            </th>
                            <th class="coluna66px">M&ecirc;s de Demiss&atilde;o
                            </th>
                            <th class="coluna66px">Aumentado
                            </th>
                            <th class="coluna66px">% de Aumento
                            </th>
                            <th class="coluna66px">M&ecirc;s do Aumento
                            </th>
                            <th class="coluna55px">&nbsp;
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>" style="background-color: <%#Convert.ToBoolean( Eval("Excluido"))? "red" : "" %>">
                        <td class="coluna190px">
                            <%#Eval("NumeroDeVaga") %>
                            <asp:HiddenField ID="HiddenNumeroVaga"  Value='<%#Eval("NumeroDeVaga") %>' runat="server" />
                        </td>
                        <td class="coluna55px">
                            <asp:HiddenField runat="server" ID="hdnId" Value='<%#Eval("Id") %>' />
                            <asp:HiddenField runat="server" ID="Sequencial" Value='<%#Eval("ObterNumeroSequencial") %>' />
                            
                            <%#Convert.ToString( Eval("InicialNumeroMatricula")).Length > 0?Eval("InicialNumeroMatricula") + string.Format("{0:D5}",int.Parse( Convert.ToString( Eval("Matricula")).Substring(1))):Eval("Matricula")  %>
                        </td>
                        <td class="coluna190px">
                            <%#Eval("Nome") %>
                        </td>
                        <td class="coluna190px">
                            <%#Eval("Cargo") %>
                        </td>

                        <td class="coluna77px">
                            <%#string.Format("{0:#,###;#,###;0}", Eval("SalarioBase")) %>
                        </td>
                        <td class="coluna66px">
                            <%#Eval("AnoAdmissao", "{0:d}") %>
                        </td>
                        <td class="coluna66px">
                            <%#Eval("DataDeAdmissao", "{0:d}") %>
                        </td>
                        <td class="coluna66px">
                            <%--Enabled='<%# Convert.ToInt32( Eval("FuncionarioReposicao")) == 0 %>'--%>
                            <asp:CheckBox runat="server" ID="chkDemitido" Checked='<%#Eval("Demitido") %>' onclick="exibirCamposDemissao(this);" />
                        </td>
                        <td class="coluna66px">
                            <asp:DropDownList runat="server" ID="ddlMesDeDemissao" SelectedValue='<%#Eval("MesDeDemissao") %>' Enabled='<%#Convert.ToBoolean( Eval("Demitido")) %>'>
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                                <asp:ListItem Value="3">Mar&ccedil;o</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="5">Maio</asp:ListItem>
                                <asp:ListItem Value="6">Junho</asp:ListItem>
                                <asp:ListItem Value="7">Julho</asp:ListItem>
                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                <asp:ListItem Value="9">Setembro</asp:ListItem>
                                <asp:ListItem Value="10">Outrubro</asp:ListItem>
                                <asp:ListItem Value="11">Novembro</asp:ListItem>
                                <asp:ListItem Value="12">Dezembro</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="coluna66px">
                            <asp:CheckBox runat="server" ID="chkAumentado" Checked='<%#Eval("Aumentado") %>'
                                onclick="exibirCamposAumento(this);" />
                        </td>
                        <td class="coluna66px">
                            <asp:TextBox runat="server" ID="txtPercentualDeAumento" MaxLength="4" Text='<%#Eval("PercentualDeAumento") %>'
                                class="sonumero"></asp:TextBox>
                        </td>
                        <td class="coluna66px">
                            <asp:DropDownList runat="server" ID="ddlMesesDeAumento" SelectedValue='<%#Eval("MesDeAumento") %>' Enabled='<%#Convert.ToBoolean( Eval("Aumentado")) %>'>
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="10">Outrubro</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="coluna55px">
                            <asp:ImageButton runat="server" ImageUrl="~/imagens/delete.png" CommandName="DeletarUsuario" CommandArgument='<%#  Eval("ObterNumeroSequencial") %>' />
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
        <label class="workkerForm-lbl-value">Justificativa para aumento do quadro de funcion&aacute;rios:</label><br />
        <asp:TextBox runat="server" ID="txtJustificativa" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <asp:Button ID="btnSalvar" runat="server" OnClick="Salvando" Text="Salvar" />
    </div>
</asp:Content>
