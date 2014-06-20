using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orcamento.Domain.Robo.Monitoramento;

namespace Orcamento.Domain.Entities.Monitoramento
{
    public  class Carga : IAggregateRoot<Guid>
    {
        private IProcessaCarga _processaCarga;

        public virtual Guid Id { get; set; }

        public Carga()
        {
            
        }

        public Carga(IProcessaCarga processaCarga)
        {
            _processaCarga = processaCarga;
        }

        public virtual DateTime DataCriacao { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual string Diretorio { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual IList<Detalhe> Detalhes { get; set; }

        public virtual void AdicionarDetalhe(string nome, string descricao, int linha)
        {

            if (Detalhes == null)
                Detalhes = new List<Detalhe>();

            var detalhe = new Detalhe() { Nome = nome, Descricao = descricao , Linha = linha};

            Detalhes.Add(detalhe);
        }

        public virtual void Processar()
        {
            _processaCarga.Processar(this);
        }
    }
}
