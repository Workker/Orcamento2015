using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento
{
    public class UnitariosDeInsumo : ProcessaCarga
    {
        public override void Processar(Carga carga, bool salvar = false)
        {
            throw new NotImplementedException();
        }

        internal override void SalvarDados()
        {
            throw new NotImplementedException();
        }
    }
}
