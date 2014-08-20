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
        public virtual IList<DetalheModel> Detalhes { get; set; }

    }

    [Serializable]
    public class CargasModel
    {
        public IList<CargaModel> Cargas { get; set; }

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