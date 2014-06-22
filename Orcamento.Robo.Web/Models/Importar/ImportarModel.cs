using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MS.Internal.Xml.XPath;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Robo.Web.Models.Importar
{
    [Serializable]
    public class TipoImportacaoModel
    {
        public string Tipo { get; set; }
        public TipoEstrategiaDeCargaEnum Id { get; set; }
        public bool Selecionado { get; set; }
    }

    [Serializable]
    public class ImportarModel
    {
        public virtual string NomeImportacao { get; set; }
        public virtual List<TipoImportacaoModel> TiposImportacao { get; set; }

        public ImportarModel()
        {
            this.TiposImportacao = new List<TipoImportacaoModel>();
        }
    }
}