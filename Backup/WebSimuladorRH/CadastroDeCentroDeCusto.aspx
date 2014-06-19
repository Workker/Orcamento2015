<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="CadastroDeCentroDeCusto.aspx.cs" Inherits="WebSimuladorRH.CadastroDeCentroDeCusto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $("[id*=chkTodos]").click(function () {

                var object = this;

                $("[id*=chkSelecionado]").each(function () {

                    if (object.checked == true) {

                        this.checked = true;
                    } else {

                        this.checked = false;
                    }
                });
            });

//            $("[id*=btnSalvar]").click(function () {

//                return validarCampos();
//            });
        });

        function validarCampos() {

            if ($("[id*=ddlCentroDeCustoASerAlterado]").val() == "") {
                alert("Selecione um centro de custo.");
                return false;
                
            } else {
                
                if ($("[id*=txtDescricao]").val() == "") {
                    alert("O campo descrição deve ser preenchido.");
                    return false;

                } else {

                    if ($("[id*=txtCodigo]").val() == "") {
                        alert("O campo código deve ser preenchido.");
                        return false;
                        
                    } else {

                        return true;
                    }
                }
            }
        }        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>
            Centro de custo</h2>
        <hr />
    </div>

    <div id="divAlteracao" runat="server" visible="false">
        <div class="grid-12-12"></div>

        <div class="grid-3-12">
            <label class="workkerForm-lbl-value bold">
                Centro de custo:</label>
            <br />
            <asp:DropDownList ID="ddlCentroDeCustoASerAlterado" runat="server" 
                onselectedindexchanged="ddlCentroDeCustoASerAlterado_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </div>
    </div>

    <div class="grid-12-12">
    </div>

    <div class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            Descri&ccedil;&atilde;o:</label>
        <br />
        <asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox>
    </div>
    <div class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            C&oacute;digo:</label>
        <br />
        <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
        <asp:HiddenField ID="hdnIdCentroDeCusto" runat="server" />
    </div>
    <br />
    <div class="grid-12-12">
        <h2>Copiar contas</h2>
        <hr />
    </div>

    <div class="grid-3-12">
        <label class="workkerForm-lbl-value bold">Hospital:</label>
        <br />
        <asp:DropDownList ID="ddlHospitais" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlHospitais_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="grid-12-12"></div>
    <div class="grid-3-12">
        <label class="workkerForm-lbl-value bold">Centro de custo:</label>
        <br />
        <asp:DropDownList ID="ddlCentrosDeCusto" runat="server">
        </asp:DropDownList>
    </div>

    <div class="grid-12-12">
        <asp:Button ID="btnCopiarContas" runat="server" Text="Copiar contas" 
            onclick="btnCopiarContas_Click" />
    </div>
        
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater runat="server" ID="rptContas" OnItemDataBound="rptContas_ItemDataBound">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th>
                                Conta
                            </th>
                            <th>
                                Todas<asp:CheckBox ID="chkTodos" runat="server" />
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td>
                            <asp:Label ID="lblNomeConta" runat="server" Text='<%#Eval("Nome") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSelecionado" runat="server" />
                            <asp:HiddenField ID="hdnIdConta" runat="server" Value='<%#Eval("Id")%>' />
                            <asp:HiddenField ID="hdnCodigoDaConta" runat="server" Value='<%#Eval("CodigoDaConta")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
    </div>
    <asp:HiddenField ID="hdnCodigoCentroDeCusto" runat="server" />
</asp:Content>
