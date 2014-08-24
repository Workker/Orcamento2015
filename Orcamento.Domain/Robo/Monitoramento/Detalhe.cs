using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento
{
    public class Detalhe
    {
        public virtual Guid Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Excecao { get; set; }
        public virtual int Linha { get; set; }
        public virtual TipoDetalheEnum TipoDetalhe { get; set; }

        public  Detalhe()
        {
        }
    }

    public enum TipoDetalheEnum : short
    {
        erroLeituraExcel = 0,
        erroDeProcesso = 1,
        detalhe = 2,
        sucesso = 3,
        erroDeValidacao
    }
}
