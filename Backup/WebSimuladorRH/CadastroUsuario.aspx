<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="CadastroUsuario.aspx.cs" Inherits="WebSimuladorRH.CadastroUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
//        function ValidarCamposDaPagina() {

//            var campoDeNome = document.getElementById("txtNome");

//            if (!VerificarSeOCampoEstaVazio(campoDeNome)) {
//                alert("O campo nome deve ser preenchido.");
//                return false;
//            }

//            var campoDeTipo = document.getElementById("ddlTipo");

//            if (!VerificarSeOCampoEstaVazio(campoDeTipo)) {
//                alert("O campo tipo deve ser preenchido.");
//                return false;
//            }

//            var campoDeLogin = document.getElementById("txtLogin");

//            if (!VerificarSeOCampoEstaVazio(campoDeLogin)) {
//                alert("O campo login deve ser preenchido.");
//                return false;
//            }

//            return true;
//        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid-12-12">
        <h2>
            Cadastro de Usu&aacute;rio</h2>
        <hr />
    </div>
    <div id="divUsuarioAlteracao" runat="server" visible="False">
        <div class="grid-12-12">
            <label class="workkerForm-lbl-value bold">
                Usuario:</label>
            <br />
            <asp:DropDownList runat="server" ID="ddlUsuarios"  onselectedindexchanged="ddlUsuario_SelectedIndexChanged" 
                AutoPostBack="True"  />
        </div>
    </div>
    <div id="divUsuarioNome" runat="server"  class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            Nome:</label>
        <br />
        <asp:TextBox ID="txtNome" CssClass="workkerForm-txt" MaxLength="255" runat="server"
            ClientIDMode="Static"></asp:TextBox>
    </div>
    
    <div id="divUsuarioLogin" runat="server"  class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            Login:</label>
        <br />
        <asp:TextBox ID="txtLogin" CssClass="workkerForm-txt" MaxLength="255" runat="server"
            ClientIDMode="Static"></asp:TextBox>
    </div>
    <div id="divUsuarioTipo" runat="server" visible="False" class="grid-3-12">
        <label class="workkerForm-lbl-value bold">
            Tipo:</label>
        <br />
        <asp:DropDownList ID="ddlTipo" runat="server" OnDataBound="DdlTipoOnDataBound" AutoPostBack="True"
            ClientIDMode="Static" OnSelectedIndexChanged="DdlTipoSelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="grid-12-12">
        <table>
            <asp:Repeater runat="server" ID="rptDepartamento" OnItemDataBound="rptDepartamento_ItemDataBound">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th style="background-color: #000; text-align: center;">
                                Departamentos
                            </th>
                            <th style="background-color: #000; width: 0%;">
                            </th>
                        </tr>
                        <tr>
                            <th>
                                Departamento
                            </th>
                            <th class="coluna100px">
                                Selecionar
                            </th>
                        </tr>
                    </thead>
                    <tbody class="gridBorder">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#Container.ItemIndex % 2 == 0? "even" : "odd"%>">
                        <td>
                            <%#Eval("Nome") %>
                        </td>
                        <td class="coluna100px">
                            <asp:CheckBox ID="CheckBoxidDepartamento" runat="server" Text="" />
                            <asp:HiddenField ID="HiddenFieldidDepartamento" runat="server" Value='<%# Eval("Id")%>' />
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
        <asp:Button ID="btnSalvar" runat="server"
            Text="Salvar" OnClick="BtnSalvarClick" />
    </div>
</asp:Content>
