using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH
{
    public class DespesaTotalizadorDTO
    {
        public int Id { get; set; }

        public long ValorTotalDespesa { get; set; }

        public string NomeDespesa { get; set; }
    }

    public class CentroCustoTotalizadorDTO
    {
        public int Id { get; set; }

        public long ValorTotalCentroDeCusto { get; set; }

        public string NomeCentroDeCusto { get; set; }
    }

    public partial class Totalizador : BasePage
    {
        private Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos;

        public Departamento SetorSelecionado
        {
            get { return (Departamento)Session["SetorSelecionado"]; }
            set { Session["SetorSelecionado"] = value; }
        }

        public List<DespesaTotalizadorDTO> Despesas
        {
            get { return (List<DespesaTotalizadorDTO>)Session["DespesasTotais"]; }
            set { Session["DespesasTotais"] = value; }
        }

        public List<CentroCustoTotalizadorDTO> CentrosDeCusto
        {
            get { return (List<CentroCustoTotalizadorDTO>)Session["CentrosDeCustoTotais"]; }
            set { Session["CentrosDeCustoTotais"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            if (!Page.IsPostBack)
            {
                Departamentos setores = new Departamentos();
                this.SetorSelecionado = setores.Obter((int)Session["DepartamentoLogadoId"]);

                orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
                var listaOrcamentos = orcamentos.TodosPor(SetorSelecionado);

                PreencherOrcamentos(listaOrcamentos);

                PreencherCentrosDeCusto(listaOrcamentos);
            }
        }

        public void PreencherOrcamentos(IList<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            List<object> objetos = new List<object>();
            List<DespesaTotalizadorDTO> despesas = new List<DespesaTotalizadorDTO>();

            var despesaDeViagem =
                listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Sum(c => c.ValorTotal);
            var despesasOperacionais =
                listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.DespesaOperacional && a.VersaoFinal).GroupBy(
                    a => a.DespesasOperacionais);


            foreach (
                var despesa in
                    despesasOperacionais.Select(a => a).Where(i => i.Key != null).GroupBy(
                        b => b.Key.GroupBy(c => c.Conta)).ToList())
            {
                foreach (var item in despesa)
                {
                    foreach (var item1 in item)
                    {
                        foreach (var item2 in item1.DespesasOperacionais)
                        {
                            if (despesas.Exists(d => d.Id == item2.Conta.Id))
                            {
                                despesas.Where(d => d.Id == item2.Conta.Id).FirstOrDefault().ValorTotalDespesa +=
                                    item2.Valor;
                            }
                            else
                            {
                                DespesaTotalizadorDTO despesaDto = new DespesaTotalizadorDTO();

                                despesaDto.ValorTotalDespesa = item2.Valor;
                                despesaDto.NomeDespesa = item2.Conta.Nome;
                                despesaDto.Id = item2.Conta.Id;

                                despesas.Add(despesaDto);
                            }

                        }

                    }
                }

            }

            var contas = this.SetorSelecionado.CentrosDeCusto.GroupBy(c => c.Contas.GroupBy(u => u)).ToList();

            var contasFiltradas =
                contas.Where(c => c.All(b => despesas.All(d => d.Id != b.Id))).GroupBy(c => c.GroupBy(u => u)).ToList();

            foreach (var conta in contasFiltradas)
            {
                foreach (var conta1 in conta)
                {
                    foreach (var item in conta1)
                    {
                        foreach (var item1 in item.Contas)
                        {
                            if (despesas.Exists(d => d.Id == item1.Id))
                                continue;

                            DespesaTotalizadorDTO despesaDto = new DespesaTotalizadorDTO();
                            despesaDto.NomeDespesa = item1.Nome;
                            despesaDto.ValorTotalDespesa = 0;
                            despesaDto.Id = item1.Id;

                            despesas.Add(despesaDto);
                        }

                    }
                }
            }


            if (SetorSelecionado.GetType() == typeof(Setor))
            {
                //  AdicionarDespesaDeViagem(despesas, despesaDeViagem);
                DespesaTotalizadorDTO ViagemHospedagem = new DespesaTotalizadorDTO();
                ViagemHospedagem.NomeDespesa = "VIAGEM/HOSPEDAGEM";
                if (despesaDeViagem > 0)
                {
                    ViagemHospedagem.ValorTotalDespesa = listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Sum(a => a.Despesas.Sum(d => d.ValorTotalPassagem + d.ValorTotalDiaria));
                    despesas.Where(d => d.NomeDespesa == "CONDUCAO (TAXI,METRO,ONIBUS E TREM)").FirstOrDefault().ValorTotalDespesa += listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Sum(a => a.Despesas.Sum(d => d.ValorTotalTaxi));
                    despesas.Where(d => d.NomeDespesa == "LANCHES E REFEICOES").FirstOrDefault().ValorTotalDespesa += listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).Sum(a => a.Despesas.Sum(d => d.ValorTotalRefeicao));
                }
                else
                    ViagemHospedagem.ValorTotalDespesa = 0;

                despesas.Add(ViagemHospedagem);

            }


            Despesas = despesas;


            rptOrcamentos.DataSource = Despesas.OrderBy(x => x.NomeDespesa);
            rptOrcamentos.DataBind();
        }

        private static void AdicionarDespesaDeViagem(List<DespesaTotalizadorDTO> despesas, long despesaDeViagem)
        {
            DespesaTotalizadorDTO despesaDeViagemDTO = new DespesaTotalizadorDTO();
            despesaDeViagemDTO.NomeDespesa = "Despesa De Viagem";


            if (despesaDeViagem != null)
                despesaDeViagemDTO.ValorTotalDespesa = despesaDeViagem;
            else
                despesaDeViagemDTO.ValorTotalDespesa = 0;

            despesas.Add(despesaDeViagemDTO);


        }

        public void PreencherCentrosDeCusto(IList<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            List<object> objetos = new List<object>();

            var centrosDeCusto = listaOrcamentos.Where(a => a.Tipo != TipoOrcamentoEnum.Hospitalar && a.VersaoFinal).GroupBy(a => a.CentroDeCusto);

            List<CentroCustoTotalizadorDTO> centros = new List<CentroCustoTotalizadorDTO>();

            foreach (var centro in centrosDeCusto.Select(a => a).ToList())
            {
                CentroCustoTotalizadorDTO centroDto = new CentroCustoTotalizadorDTO();

                centroDto.ValorTotalCentroDeCusto = centro.Sum(c => c.ValorTotal);
                centroDto.NomeCentroDeCusto = centro.FirstOrDefault().CentroDeCusto.Nome;
                centroDto.Id = centro.FirstOrDefault().CentroDeCusto.Id;

                centros.Add(centroDto);
            }

            foreach (var centro in this.SetorSelecionado.CentrosDeCusto.Where(c => centros.All(b => b.Id != c.Id)).ToList())
            {
                CentroCustoTotalizadorDTO centroDto = new CentroCustoTotalizadorDTO();

                centroDto.ValorTotalCentroDeCusto = 0;
                centroDto.NomeCentroDeCusto = centro.Nome;
                centroDto.Id = centro.Id;
                centros.Add(centroDto);
            }

            CentrosDeCusto = centros;

            rptCentroDecusto.DataSource = CentrosDeCusto.OrderBy(x => x.NomeCentroDeCusto);
            rptCentroDecusto.DataBind();
        }

        protected void rptOrcamentos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var total = (Literal)e.Item.FindControl("ltlTotal");
                total.Text = "(";
                total.Text += Despesas.Sum(d => d.ValorTotalDespesa).ToString("#,###,###,###0");
                total.Text += ")";
            }
        }

        protected void rptCentroDecusto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var total = (Literal)e.Item.FindControl("ltlTotal");
                total.Text = "(";
                total.Text += CentrosDeCusto.Sum(d => d.ValorTotalCentroDeCusto).ToString("#,###,###,###0");
                total.Text += ")";
            }
        }
    }
}