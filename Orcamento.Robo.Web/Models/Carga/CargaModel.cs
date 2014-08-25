using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Carga
{
    [Serializable]
    public class CargaModel
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual DateTime? DataInicio { get; set; }
        public virtual DateTime? DataFim { get; set; }
        public virtual string Diretorio { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual string Status { get; set; }
        public virtual string NomeArquivo { get; set; }
        public virtual string TipoDaCarga { get; set; }
        public virtual IList<DetalheModel> Detalhes { get; set; }

    }

    [Serializable]
    public class Pagina
    {
        public int numeroPaginacao { get; set; }
        public bool selecionado { get; set; }
    }

    [Serializable]
    public class CargasModel
    {
        public IList<CargaModel> Cargas { get; set; }
        public IList<Pagina> Paginas { get; set; }

        public void CarregarPaginas()
        {
            if (Cargas == null || Cargas.Count == 0)
                return;

            if (Paginas != null && Paginas.Count > 0)
                return;

            if (Paginas == null)
                Paginas = new List<Pagina>();


            var NumeroDePaginas = Cargas.Count / 50;

            for (int i = 1; i < NumeroDePaginas; i++)
            {
                var pagina = new Pagina() { numeroPaginacao = i };
                pagina.selecionado = i == 1;
                Paginas.Add(pagina);
            }
        }

        public CargasModel()
        {
            this.Cargas = new List<CargaModel>();
        }

    }

    [Serializable]
    public class DetalheModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int Linha { get; set; }
    }
}