using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Importar
{
    [Serializable]
    public class DetalheImportacaoModel
    {
        public string Nome { get; set; }
        public int Linha { get; set; }
        public string Descricao { get; set; }
    }

    [Serializable]
    public class DetalhesDaImportacaoModel
    {
        public List<DetalheImportacaoModel> Detalhes { get; set; }
        public bool ImportacaoOk
        {
            get { return this.Detalhes == null || this.Detalhes.Count == 0; }
        }

        public DetalhesDaImportacaoModel()
        {
            this.Detalhes = new List<DetalheImportacaoModel>();
        }
    }
}