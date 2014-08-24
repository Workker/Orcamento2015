using Orcamento.Domain.Robo.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Entities.Monitoramento
{
    public abstract class ProcessaCarga
    {
        public abstract void Processar(Carga carga, bool salvar);
        internal Carga carga;
        internal Processo processo;

        internal abstract void SalvarDados();

        public void SalvarAlteracoes(bool salvar)
        {
            if (carga.Ok() && salvar)
            {
                SalvarDados();
            }
        }

        internal bool CargaContemErros()
        {
            return !carga.Ok();
        }
    }
}
