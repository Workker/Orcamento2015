using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain;
using System.Linq;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Servico;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Servico.Pessoal;
namespace WebSimuladorRH
{
    public partial class ResultadoOrcado : BasePage
    {
        public ServicoMapperOrcamentoView ServicoMapperOrcamentoView { get { return new ServicoMapperOrcamentoView(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            if (!this.IsPostBack)
            {
                Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
                Departamentos setores = new Departamentos();
                var departamento = setores.Obter((int)Session["DepartamentoLogadoId"]);
                var dres = orcamentos.ObterDRE(departamento);
                orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
                var listaOrcamentos = orcamentos.TodosPor(departamento);
                var viagens = PreencherResultadoOrçadoViagens(listaOrcamentos);
                var outrasDespesas = dres.Where(x => x.Nome == "Orçamento de outras despesas").Sum(v => v.ValorTotal);

                if (viagens > 0)
                {
                    outrasDespesas += listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Sum(a => a.Despesas.Sum(d => d.ValorTotalRefeicao + d.ValorTotalTaxi));
                }

                var producoes = dres.Where(x => x.Nome == "Orçamento Hospitalar").FirstOrDefault();

                PreencherOutrasDespesas(outrasDespesas);
                PreencherViagens(viagens);
                PreencherPessoal(orcamentos, departamento);
                Receita(producoes);

                trViagens.Visible = departamento.GetType() == typeof(Setor);
                trReceita.Visible = departamento.GetType() == typeof(Hospital);
                if (departamento.GetType() == typeof(Hospital) && producoes != null && producoes.ValorTotal > 0)
                {
                    Insumos insumos = new Insumos();
                    var insumo = insumos.ObterInsumo(departamento);

                    trInsumos.Visible = true;
                    var orcamentoDeProducao = orcamentos.ObterOrcamentoHospitalarFinal(departamento);

                    if (orcamentoDeProducao != null)
                    {
                        var contasUnitarias = ServicoMapperOrcamentoView.TransformarProducaoDeInsumos(
                            orcamentoDeProducao.Servicos.Where(s => s.Conta.Nome != "Salas" && s.Conta.TipoValorContaEnum == TipoValorContaEnum.Quantidade && s.Conta.Calculado == false).ToList(),
                            orcamentoDeProducao.Servicos.Where(s => s.Conta.TipoValorContaEnum == TipoValorContaEnum.Porcentagem).ToList());

                        TicketsDeReceita tickets = new TicketsDeReceita();
                        var ticketsDeReceita = tickets.Todos(departamento);
                        var ticket = ticketsDeReceita.Where(t => t.TipoTicket == global::Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao.TipoTicketDeReceita.ReajusteDeInsumos).FirstOrDefault();

                        orcamentoDeProducao.CalcularCustoHospitalar(ticket, insumo.CustosUnitarios.ToList(), contasUnitarias);

                        txtInsumos.Text += "(";
                        txtInsumos.Text += orcamentoDeProducao.CustosUnitariosTotal.Sum(c => c.Valores.Sum(v => v.Valor)).ToString("#,###,###,###0"); ;
                        txtInsumos.Text += ")";
                    }
                }
                else
                {
                    txtInsumos.Text += "(";
                    txtInsumos.Text += 0.ToString();
                    txtInsumos.Text += ")";
                }



            }
        }

        private void PreencherPessoal(Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos, Orcamento.Domain.Gerenciamento.Departamento departamento)
        {
            var orcamentosNovos = new NovosOrcamentosPessoais();

            var OrcamentosDeDespesasOperacionais = new List<NovoOrcamentoPessoal>();

            foreach (var centro in departamento.CentrosDeCusto)
            {
                var orcamentoOperacional = orcamentos.ObterOrcamentoFinalOrcamentoOperacional(centro, departamento);

                var servicoCalculaPessoal = new ServicoGerarOrcamentoPessoalPorCentroDeCusto();
                servicoCalculaPessoal.CentroDeCusto = centro;
                servicoCalculaPessoal.Departamento = departamento;

                servicoCalculaPessoal.Gerar("");
                OrcamentosDeDespesasOperacionais.Add(servicoCalculaPessoal.Orcamento);
            }

            double somaPessoal = OrcamentosDeDespesasOperacionais.Where(d => d.Despesas != null).Sum(o => o.Despesas.Where(de=> de.Parcelas != null)
                .Sum(d => d.Parcelas.Sum(p => p.Valor)));
            PreencherPessoal(somaPessoal);
        }

        private List<NovoOrcamentoPessoal> ObterOrcamentos(Orcamento.Domain.Gerenciamento.Departamento departamento)
        {
            List<NovoOrcamentoPessoal> orcamentos = new List<NovoOrcamentoPessoal>();

            foreach (var centro in departamento.CentrosDeCusto)
            {
                var servicoCalculaPessoal = new ServicoGerarOrcamentoPessoalPorCentroDeCusto();
                servicoCalculaPessoal.CentroDeCusto = centro;
                servicoCalculaPessoal.Departamento = departamento;

                servicoCalculaPessoal.Gerar("");
                orcamentos.Add(servicoCalculaPessoal.Orcamento);
            }

            return orcamentos;
        }

        private double PreencherResultadoOrçadoViagens(List<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            if (listaOrcamentos == null || listaOrcamentos != null && listaOrcamentos.Count == 0 || listaOrcamentos != null && listaOrcamentos.Count > 0 && listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Count() == 0)
                return 0;

            return listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Sum(a => a.Despesas.Sum(d => d.ValorTotalPassagem + d.ValorTotalDiaria));
        }

        private void Receita(DRE producoes)
        {
            if (producoes != null)
                txtReceita.Text = producoes.ValorTotal.ToString("#,###,###,###0");
            else
                txtReceita.Text = "0";
        }

        private void PreencherPessoal(double pessoais)
        {
            txtPessoal.Text = "(";
            txtPessoal.Text += pessoais.ToString("#,###,###,###0");
            txtPessoal.Text += ")";
        }

        private void PreencherViagens(double viagens)
        {
            txtViagens.Text = "(";
            txtViagens.Text += viagens.ToString("#,###,###,###0");
            txtViagens.Text += ")";
        }

        private void PreencherOutrasDespesas(double outrasDespesas)
        {
            txtOutrasDespesas.Text = "(";
            txtOutrasDespesas.Text += outrasDespesas.ToString("#,###,###,###0");
            txtOutrasDespesas.Text += ")";
        }
    }
}