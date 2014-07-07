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
        public virtual string Status { get; set; } //Altera 
        public virtual string NomeArquivo { get; set; }
        public virtual IList<Detalhe> Detalhes { get; set; }   

        public virtual void AdicionarDetalhe(string nome, string descricao, int linha, TipoDetalheEnum detalheEnum)
        {

            if (Detalhes == null)
                Detalhes = new List<Detalhe>();

            var detalhe = new Detalhe() { Nome = nome, Descricao = descricao , Linha = linha, TipoDetalhe = detalheEnum};

            Detalhes.Add(detalhe);
        }

        public virtual void Processar()
        {
            _processaCarga.Processar(this);
        }

        public virtual bool Ok()
        {
            return this.Detalhes == null || this.Detalhes.Count == 0 || this.Detalhes.Where(d=> d.TipoDetalhe == TipoDetalheEnum.erroDeProcesso || d.TipoDetalhe == TipoDetalheEnum.erroLeituraExcel) != null
                && this.Detalhes.Where(d=> d.TipoDetalhe == TipoDetalheEnum.erroDeProcesso || d.TipoDetalhe == TipoDetalheEnum.erroLeituraExcel).Count() == 0;
        }
    }
}
