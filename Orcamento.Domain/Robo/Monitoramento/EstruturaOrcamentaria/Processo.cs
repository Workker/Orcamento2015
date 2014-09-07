using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria
{
    public class Processo : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string Status { get; set; }
        public virtual string Nome { get; set; }
        public virtual TipoProcesso TipoProcesso { get; set; }
        public virtual DateTime? Iniciado { get; set; }
        public virtual DateTime? Finalizado { get; set; }
        public virtual string MsgDeErro { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
