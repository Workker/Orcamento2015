using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Servico.ServicoClonarObjetos
{
    public class ServicoClonarOrcamentoHospitalar
    {
        public Orcamento Clonar( Orcamento orcamento) 
        {
            var orcamentoNovo = new OrcamentoHospitalar(orcamento);
            orcamentoNovo.VersaoFinal = false;
            return orcamentoNovo;
        }

        //private void ZerarIds(ref Orcamento orcamento)
        //{
        //    orcamento.Id = 0;

        //    foreach (var servico in orcamento.Servicos)
        //    {
        //        servico.Id = 0;

        //        foreach (var valor in servico.Valores)
        //        {
        //            valor.Id = 0;
        //        }
        //    }

        //    foreach (var fator in orcamento.FatoresReceita)
        //    {
        //        fator.Id = 0;

        //        foreach (var incremento in fator.Incrementos)
        //        {
        //            incremento.Id = 0;
        //        }
        //    }
        //}
    }
}
