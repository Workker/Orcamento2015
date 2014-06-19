<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AlteracaoDeHospital.aspx.cs" Inherits="WebSimuladorRH.AlteracaoDeHospital" %>

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

            $("[id*=btnSalvar]").click(function () {

                return ValidarCampos();
            });
        });

        function ValidarCampos() {

            if ($("[id*=txtNome]").val() == "") {

                alert("O campo nome deve ser preenchido.");
                return false;
            } else {
                return true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>
            Hospital</h2>
        <hr />
    </div>
     
    <div id="divAlteracao" runat="server" Visible="False">
        <div class="grid-12-12">
            <label class="workkerForm-lbl-value bold">
                Hospital:</label>
            <br />
            <asp:DropDownList runat="server" ID="ddlHospitais" 
                onselectedindexchanged="ddlHospitais_SelectedIndexChanged" 
                AutoPostBack="True" />
        </div>
    </div>

    <div class="grid-12-12"></div>

    <div class="grid-12-12">
        <label class="workkerForm-lbl-value bold">
            Nome:</label>
        <br />
        <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
    </div>
    
    <div class="grid-12-12"></div>
    
    
    <h2>Copiar centros de custo</h2>
    <hr/>
    <div class="grid-12-12"></div>

    <div class="grid-12-12">
        <label class="workkerForm-lbl-value bold">
            Hospital:</label>
        <br />
        <asp:DropDownList runat="server" ID="ddlHospitalASerCopiado" />
    </div>
    
    <div class="grid-12-12"></div>

    <div class="grid-12-12">
        <asp:Button ID="btnCopiarCentrosDeCusto" runat="server" Text="Copiar centros de custo"
            OnClick="btnCopiarCentrosDeCusto_Click" />
    </div>
    <div class="grid-12-12 workkerForm-no-lbl">
        <table>
            <asp:Repeater runat="server" ID="rptCentrosDeCusto" 
                OnItemDataBound="rptCentrosDeCusto_ItemDataBound">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th>
                                Centro de custo
                            </th>
                            <th>
                                Código
                            </th>
                            <th>
                                Todos<asp:CheckBox ID="chkTodos" runat="server" />
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                        <td>
                            <%#Eval("Nome") %>
                        </td>
                        <td>
                            <asp:Label ID="lblCodigoSetor" runat="server" Text='<%#Eval("CodigoDoCentroDeCusto")%>' />
                            <asp:HiddenField ID="hdnIdSetor" runat="server" Value='<%#Eval("Id")%>' />
                            <asp:HiddenField ID="hdnNomeSetor" runat="server" Value='<%#Eval("Nome")%>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSelecionado" runat="server" />
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
    <asp:HiddenField ID="txtIdHospital" runat="server" />
</asp:Content>
