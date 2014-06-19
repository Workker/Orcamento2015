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
    public class CentroCustoDTO
    {
        public int Id { get; set; }

        public long ValorTotalCentroDeCusto { get; set; }

        public string NomeCentroDeCusto { get; set; }
    }

    public partial class TotalizadorCentroDeCusto : BasePage
    {
        private Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos;

        public Departamento SetorSelecionado
        {
            get
            {
                return (Departamento)Session["SetorSelecionado"];
            }
            set
            {
                Session["SetorSelecionado"] = value;
            }
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
            }
        }

        public void PreencherOrcamentos(IList<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            List<object> objetos = new List<object>();

            var centrosDeCusto = listaOrcamentos.Where(a => a.Tipo != TipoOrcamentoEnum.Hospitalar && a.VersaoFinal).GroupBy(a => a.CentroDeCusto);

            List<CentroCustoDTO> centros = new List<CentroCustoDTO>();

            foreach (var centro in centrosDeCusto.Select(a => a).ToList())
            {
                CentroCustoDTO centroDto = new CentroCustoDTO();

                centroDto.ValorTotalCentroDeCusto = centro.Sum(c => c.ValorTotal);
                centroDto.NomeCentroDeCusto = centro.FirstOrDefault().CentroDeCusto.Nome;
                centroDto.Id = centro.FirstOrDefault().CentroDeCusto.Id;

                centros.Add(centroDto);
            }

            foreach (var centro in this.SetorSelecionado.CentrosDeCusto.Where(c => centros.All(b => b.Id != c.Id)).ToList())
            {
                CentroCustoDTO centroDto = new CentroCustoDTO();

                centroDto.ValorTotalCentroDeCusto = 0;
                centroDto.NomeCentroDeCusto = centro.Nome;
                centroDto.Id = centro.Id;
                centros.Add(centroDto);
            }

            rptOrcamentos.DataSource = centros.OrderBy(x => x.NomeCentroDeCusto);
            rptOrcamentos.DataBind();
        }

       
    }
}