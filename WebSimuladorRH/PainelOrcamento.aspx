<%@ Page AutoEventWireup="true" CodeBehind="PainelOrcamento.aspx.cs" Inherits="WebSimuladorRH.PainelOrcamento"
    Language="C#" MasterPageFile="Principal.Master" Title="Or&ccedil;amento 2014 - Sele&ccedil;&atilde;o de M&oacute;dulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="center" align="center">
        <div class="grid-12-12 workkerForm-no-lbl">
            <h2>
                Sele&ccedil;&atilde;o de M&oacute;dulos</h2>
            <hr />
        </div>
        <div id="divReceita" runat="server" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnReceita" Text="Receita" OnClick="IndoParaOSimuladorDeReceita"
                Height="100px" Width="100%" />
        </div>
        <div runat="server" id="divOutrasDespesas" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnOutrasDespesas" Text="Outras Despesas" OnClick="IndoParaOSimuladorDeDespesas"
                Height="100px" Width="100%" />
        </div>
        <div runat="server" id="divPessoal" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnPessoal" Text="Pessoal" Height="100px" Width="100%"
                OnClick="IndoParaOSimuladorDePessoal" />
        </div>
        <div id="divViagemHospedagem" runat="server" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnViagemHospedagem" Text="Viagem / Hospedagem" OnClick="IndoParaOSimuladorDeViagemHospedagem"
                Height="100px" Width="100%" />
        </div>
        <%--
        <div class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnTotalizadorPorCentroDeCusto" Text="Totalizador por Centro de Custo" 
                        Height="100px" Width="100%" OnClick="IndoParaOTotalizadorPorCentroDeCusto" />
        </div>
        --%>
        <%--
        <div class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnTotalizadorPorDespesa" Text="Totalizador por Despesa" 
                        Height="100px" Width="100%" OnClick="IndoParaOTotalizadorPorDespesa" />
        </div>
        --%>
        <%--
        <div class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button ID="btnTotalizadorPorGrupoDeConta" runat="server" Text="Totalizador por Grupo de Conta" 
                        Height="100px" Width="100%" onclick="IndoParaOTotalizadorPorGrupoDeConta"/>
        </div>
        --%>
        <div runat="server" id="divFuncionario" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnFuncionario" Text="Funcion&aacute;rio" Height="100px"
                Width="100%" OnClick="IndoParaOSimuladorDeFuncionario" />
        </div>
        <div runat="server" visible="false" id="divAlocacao" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnAlocacao" Text="Alterar Alocacão de Funcion&aacute;rio"
                Height="100px" Width="100%" OnClick="IndoParaAlocacao" />
        </div>
        <div runat="server" id="divInsumos" visible="False" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnInsumos" Text="Insumos" Height="100px" Width="100%"
                OnClick="IndoParaInsumos" />
        </div>
        <div runat="server" id="divResultadoOrcado" class="grid-12-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnResultadoOrcado" Text="Resultado Or&ccedil;ado"
                Height="100px" Width="99%" OnClick="IndoParaOResultadoOrcado" />
        </div>
        <div runat="server" id="divDRE" class="grid-12-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnDRE" Text="DRE" Height="100px" Width="99%" OnClick="IndoParaODRE" />
        </div>
        <div runat="server" visible="false" id="divDRETotal" class="grid-12-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btndreTotal" Text="DRE Total" Height="100px" Width="99%"
                OnClick="IndoParaODRETotal" />
        </div>
        <div runat="server" visible="false" id="divCadastroDeTicketsDePessoal" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastroDeTicketsDePessoal" Text="Cadastrar Tickets de Pessoal"
                Height="100px" Width="100%" OnClick="IndoParaOCadastroDeTicketsDePessoal" />
        </div>
        <div runat="server" id="divCadastroDeTicketsDeViagem" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastroDeTicketDeViagem" Text="Cadastrar Tickets de Viagem"
                Height="100px" Width="100%" OnClick="IndoParaOCadastroDeTicketDeViagem" />
        </div>
        <div runat="server" visible="False" id="divCadastroDeTicketDeProducao" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastroDeTicketDeProducao" Text="Cadastrar Ticket de Produ&ccedil;&atilde;o"
                Height="100px" Width="100%" OnClick="IndoParaOCadastroDeTicketDeProducao" />
        </div>
        <%--<div runat="server"  Visible="False" ID="DivControleDeCentroDeCusto" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btncontroleDeCentro" Text="Controle de Centro de Custo"
                        Height="100px" Width="100%" OnClick="IndoParaControle" />
        </div>--%>
        <div runat="server" visible="False" id="divCadastroUsuario" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastroUsuario" Text="Cadastrar Usu&aacute;rio"
                Height="100px" Width="100%" OnClick="IndoParaOCadastroDeusuario" />
        </div>
        <div runat="server" visible="False" id="divCadstroDeHospital" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastroHospital" Text="Cadastro de Hospital" Height="100px"
                Width="100%" OnClick="IndoParaOCadastroDeHospital" />
        </div>
        <div runat="server" visible="False" id="divALterarHospital" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnAlterarHospital" Text="Alteração de Hospital" Height="100px"
                Width="100%" OnClick="IndoParaAlteracaoDeHospital" />
        </div>
        <div runat="server" visible="False" id="divAlterarCentroDeCusto" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnAlterarCentroDeCusto" Text="Alteração de centro de custo"
                Height="100px" Width="100%" OnClick="IndoParaAlteracaoDeCentroDeCusto" />
        </div>
        <div runat="server" visible="False" id="divCadastrarCentroDeCusto" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastrarCentroDeCusto" Text="Cadastro de centro de custo" Height="100px"
                Width="100%" OnClick="IndoParaCadastrarCentroDeCusto" />
        </div>

          <div runat="server" visible="False" id="divCadastroDeConta" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastroDeConta" Text="Cadastro de conta" Height="100px"
                Width="100%" OnClick="IndoParaCadastroDeConta" />
        </div>

        <div runat="server" visible="False" id="divAlterarConta" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnAlterarConta" Text="Alteração de conta" Height="100px"
                Width="100%" OnClick="IndoParaAlteracaoDeConta" />
        </div>


          <div runat="server" visible="False" id="divCadastrarUsuario" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastrarUsuario" Text="Cadastro de Usuário" Height="100px"
                Width="100%" OnClick="IndoParaCadastroDeUsuario" />
        </div>

        <div runat="server" visible="False" id="divAlteracaoUsuario" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnAlterarUsuario" Text="Alteração de Usuário" Height="100px"
                Width="100%" OnClick="IndoParaAlteracaoDeUsuario" />
        </div>

            <div runat="server" visible="False" id="divCadastrarDepartamento" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnCadastrarDepartamento" Text="Cadastro de Departamento" Height="100px"
                Width="100%" OnClick="IndoParaCadastroDeDepartamento" />
        </div>

        <div runat="server" visible="False" id="divAlterarDepartamento" class="grid-6-12 workkerForm-no-lbl homeButton">
            <asp:Button runat="server" ID="btnAlterarDepartamento" Text="Alteração de Departamento" Height="100px"
                Width="100%" OnClick="IndoParaAlteracaoDeDepartamento" />
        </div>
    </div>
</asp:Content>
