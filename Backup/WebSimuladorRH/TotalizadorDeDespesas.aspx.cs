using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public class DespesaTotalizadorDespesaDTO
    {
        public int Id { get; set; }

        public long ValorTotalDespesa { get; set; }

        public string NomeDespesa { get; set; }
    }


    public partial class TotalizadorDeDespesas : BasePage
    {
        private Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos;

        public Departamento SetorSelecionado
        {
            get { return (Departamento) Session["SetorSelecionado"]; }
            set { Session["SetorSelecionado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Departamentos setores = new Departamentos();
                this.SetorSelecionado = setores.Obter((int) Session["DepartamentoLogadoId"]);

                orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
                var listaOrcamentos = orcamentos.TodosPor(SetorSelecionado);

                PreencherOrcamentos(listaOrcamentos);
            }
        }

        public void PreencherOrcamentos(IList<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            List<object> objetos = new List<object>();

            var despesaDeViagem =
                listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.Viagem && a.VersaoFinal).FirstOrDefault();
            var despesasOperacionais =
                listaOrcamentos.Where(a => a.Tipo == TipoOrcamentoEnum.DespesaOperacional && a.VersaoFinal).GroupBy(
                    a => a.DespesasOperacionais);

            DespesaTotalizadorDespesaDTO despesaDeViagemDTO = new DespesaTotalizadorDespesaDTO();
            despesaDeViagemDTO.NomeDespesa = "Despesa De Viagem";

            List<DespesaTotalizadorDespesaDTO> despesas = new List<DespesaTotalizadorDespesaDTO>();

            if (despesaDeViagem != null)
                despesaDeViagemDTO.ValorTotalDespesa = despesaDeViagem.ValorTotal;
            else
                despesaDeViagemDTO.ValorTotalDespesa = 0;

            despesas.Add(despesaDeViagemDTO);

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
                                DespesaTotalizadorDespesaDTO despesaDto = new DespesaTotalizadorDespesaDTO();

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

                            DespesaTotalizadorDespesaDTO despesaDto = new DespesaTotalizadorDespesaDTO();
                            despesaDto.NomeDespesa = item1.Nome;
                            despesaDto.ValorTotalDespesa = 0;
                            despesaDto.Id = item1.Id;

                            despesas.Add(despesaDto);
                        }

                    }
                }
            }
            //ltlDespesaTutalOrcada.Text = despesas.Sum(x => x.ValorTotalDespesa).ToString() + ",00";
            rptOrcamentos.DataSource = despesas.OrderBy(x => x.NomeDespesa);
            rptOrcamentos.DataBind();
        }
    }
}