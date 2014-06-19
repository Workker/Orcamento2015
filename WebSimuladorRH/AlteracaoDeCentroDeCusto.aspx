<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AlteracaoDeCentroDeCusto.aspx.cs" Inherits="WebSimuladorRH.AlteracaoDeCentroDeCusto" %>

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
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>
            Centro de custo</h2>
        <hr />
    </div>

    <div class="grid-12-12">
        <label class="workkerForm-lbl-value bold">
            Hospital:</label>
        <br />
        <asp:DropDownList runat="server" ID="ddlHospitais" AutoPostBack="true" 
            onselectedindexchanged="ddlHospitais_SelectedIndexChanged" />
    </div>

    <div class="grid-12-12">
        <label class="workkerForm-lbl-value bold">
            Centro de custo:</label>
        <br />
        <asp:DropDownList runat="server" ID="ddlCentrosDeCusto" />
    </div>
    <div class="grid-12-12">
        <asp:Button ID="btnCopiarContas" runat="server" Text="Copiar contas" 
            onclick="btnCopiarContas_Click" />
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater runat="server" ID="rptContas" 
                onitemdatabound="rptContas_ItemDataBound">
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
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" 
            onclick="btnSalvar_Click" />
        <asp:Button ID="btnNovo" runat="server" Text="Novo" />
    </div>
    <asp:HiddenField ID="hdnCodigoCentroDeCusto" runat="server" />
</asp:Content>
