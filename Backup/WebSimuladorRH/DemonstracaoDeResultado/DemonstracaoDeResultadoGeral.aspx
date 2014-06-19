<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="DemonstracaoDeResultadoGeral.aspx.cs" Inherits="WebSimuladorRH.DemonstracaoDeResultado.DemonstracaoDeResultadoGeral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="grid-12-12">
        <h2>DRE - Demonstra&ccedil;&atilde;o de Resultado</h2>
        <hr/>
    </div>
    <div runat="server" id="divDre" class="grid-12-12">
        <table >
            <thead >
                <tr>
                    <th class="coluna150px" style="background-color: #FFFFFF; color: #696969 !important; font-size: 9px;">
                        R$ Mil
                    </th>
                    <th class="coluna88px">
                        Jan
                    </th>
                    <th class="coluna88px">
                        Fev
                    </th>
                    <th class="coluna88px">
                        Mar
                    </th>
                    <th class="coluna88px">
                        Abr
                    </th>
                    <th class="coluna88px">
                        Mai
                    </th>
                    <th class="coluna88px">
                        Jun
                    </th>
                    <th class="coluna88px">
                        Jul
                    </th>
                    <th class="coluna88px">
                        Ago
                    </th>
                    <th class="coluna88px">
                        Set
                    </th>
                    <th class="coluna88px">
                        Out
                    </th>
                    <th class="coluna88px">
                        Nov
                    </th>
                    <th class="coluna88px">
                        Dez
                    </th>
                    <th class="coluna100px" style="border-left: 1px solid #dfe8f7 !important;">
                        Or&ccedil;ado 2013
                    </th>
                     <th class="coluna100px" style="border-left: 1px solid #dfe8f7 !important;">
                        Realizado 2012
                    </th>
                </tr>
            </thead>
            <tbody   clientidmode="Static" class="gridBorder">
                <tr style="background-color: #595959; color: #FFFFFF;">
                    <td class="coluna150px bold">Receita Bruta</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaBruta12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlReceitaBrutaTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlReceitaBrutaTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 1%;">Dedu&ccedil;&otilde;es</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDeducoes12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDeducoesTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDeducoesTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Glosa Interna</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInterna12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaInternaTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaInternaTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">% R.B</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaInternaRB12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaInternaRBTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaInternaRBTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Glosa Externa</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExterna12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaExternaTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaExternaTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">% R.B</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlGlosaExternaRB12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaExternaRBTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlGlosaExternaRBTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Impostos</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostos12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlImpostosTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlImpostosTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">% R.B.</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlImpostosRB12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlImpostosRBTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlImpostosRBTotal2012"></asp:Literal></td>
                </tr>
                <tr style="background-color: #595959; color: #FFFFFF;">
                    <td class="coluna150px bold">Receita L&iacute;quida</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlReceitaLiquida12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlReceitaLiquidaTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlReceitaLiquidaTotal2012"></asp:Literal></td>
                </tr>
                <tr style="background-color: #595959; color: #FFFFFF;">
                    <td class="coluna150px bold">Despesas Operacionais</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasOperacionais12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDespesasOperacionaisTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDespesasOperacionaisTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Pessoal</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlPessoal12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlPessoalTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlPessoalTotal2012"></asp:Literal></td>
                </tr>
                 <tr>
                    <td class="coluna150px" style="padding-left: 2%">ServicosMedicos</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosMedicos12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlServicosMedicosTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlServicosMedicosTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Insumos</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumos12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlInsumosTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlInsumosTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">% R.L.</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlInsumosRL12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlInsumosRLTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlInsumosRLTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Servi&ccedil;os Contratados</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlServicosContratados12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlServicosContratadosTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlServicosContratadosTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Ocupa&ccedil;&atilde;o</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlOcupacao12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlOcupacaoTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlOcupacaoTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Utilidades e Servi&ccedil;os</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlUtilidadesEServicos12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlUtilidadesEServicosTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlUtilidadesEServicosTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Despesas Gerais</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDespesasGerais12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDespesasGeraisTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDespesasGeraisTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Marketing</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMarketing12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlMarketingTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlMarketingTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <td class="coluna150px" style="padding-left: 2%">Descontos Obtidos</td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlDescontosObtidos12"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDescontosObtidosTotal"></asp:Literal></td>
                    <td class="coluna100px"><asp:Literal runat="server" ID="ltlDescontosObtidosTotal2012"></asp:Literal></td>
                </tr>
            </tbody>
            <tfoot >
                <tr>
                    <th class="coluna150px" style="border-right: 1px solid #dfe8f7 !important;">
                        Margem Bruta
                    </th>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta1"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta2"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta3"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta4"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta5"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta6"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta7"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta8"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta9"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta10"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta11"></asp:Literal></td>
                    <td class="coluna88px"><asp:Literal runat="server" ID="ltlMargemBruta12"></asp:Literal></td>
                    <td class="coluna100px" style="border-left: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaTotal"></asp:Literal></td>
                    <td class="coluna100px" style="border-left: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaTotal2012"></asp:Literal></td>
                </tr>
                <tr>
                    <th class="coluna150px" style="border-right: 1px solid #dfe8f7 !important; border-top: 1px solid #dfe8f7 !important;">
                        Margem Bruta (%)
                    </th>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem1"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem2"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem3"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem4"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem5"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem6"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem7"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem8"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem9"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem10"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem11"></asp:Literal></td>
                    <td class="coluna88px" style="border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagem12"></asp:Literal></td>
                    <td class="coluna100px" style="border-left: 1px solid #dfe8f7 !important; border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagemTotal"></asp:Literal></td>
                    <td class="coluna100px" style="border-left: 1px solid #dfe8f7 !important; border-top: 1px solid #dfe8f7 !important;"><asp:Literal runat="server" ID="ltlMargemBrutaPorcentagemTotal2012"></asp:Literal></td>
                </tr>
            </tfoot>
        </table>
    </div>

     <div class="grid-12-12">
            <table>
                <asp:Repeater ID="rptCoorporativo" runat="server" OnItemDataBound="TotalCoorporativo">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th class="coluna150px">
                                   Coorporativo
                                </th>
                                <th class="coluna77px">
                                     <asp:Literal ID="ltlCoorporativoMes1" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes2" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                     <asp:Literal ID="ltlCoorporativoMes3" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes4" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes5" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes6" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes7" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes8" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes9" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes10" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes11" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMes12" runat="server" Text="0"></asp:Literal>
                                </th>
                                <th class="coluna77px">
                                    <asp:Literal ID="ltlCoorporativoMesTotal" runat="server" Text="0"></asp:Literal>
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
                            <asp:Repeater ID="rptIncrementos" runat="server" DataSource='<%#Eval("Valores") %>'>
                                <ItemTemplate>
                                    <td class="coluna77px">
                                     
                                        <%#String.Format("{0:#,###;#,###;0}", Eval("Valor"))%>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td class="coluna77px">
                                 <%#String.Format("{0:#,###;#,###;0}", Eval("ValorTotal"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td class="coluna150px bold">
                                    EBITDA
                                </td>
                      
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes1" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes2" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                                     
                                    <asp:Literal ID="ltlEBITDAMes3" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes4" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes5" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes6" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes7" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes8" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes9" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes10" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes11" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAMes12" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltlEBITDAAno" runat="server" Text="0"></asp:Literal>
                                </td>
                            </tr>
                             <tr>
                                <td class="coluna150px bold">
                                    EBITDA(%)
                                </td>
                      
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes1" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes2" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes3" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes4" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes5" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes6" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes7" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes8" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes9" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes10" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes11" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemMes12" runat="server" Text="0"></asp:Literal>
                                </td>
                                <td class="coluna77px">
                                    <asp:Literal ID="ltEBITDAPorcentagemAno" runat="server" Text="0"></asp:Literal>
                                </td>
                            </tr>
                        </tfoot>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
</asp:Content>
