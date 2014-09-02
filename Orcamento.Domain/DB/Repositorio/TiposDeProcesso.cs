using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class TiposDeProcesso : BaseRepository
    {
        public TipoProcesso ObterPor(TipoProcessoEnum processo)
        {
            return base.Obter<TipoProcesso>((int) processo);
        }
    }
}
