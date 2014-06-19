using System.Collections.Generic;
using System;

namespace Orcamento.Domain
{
    [Serializable]
    public abstract class DespesaDeViagem : IAggregateRoot<int>
    {
        #region Propriedades Mapeadas
        public virtual MesEnum Mes { get; set; }
        public virtual long Quantidade { get; set; }
        #endregion

        #region Propriedades da Abstração

        public virtual string Despesa { get; set; }
        public virtual string NomeCidade { get; set; }
        public virtual long ValorTotal { get; set; }
        public virtual long ValorTotalPassagem { get; set; }
        public virtual long ValorTotalTaxi { get; set; }
        public virtual long ValorTotalRefeicao { get; set; }
        public virtual long ValorTotalDiaria { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        #endregion

        public virtual int Id
        {
            get;
            set;
        }
    }
}
