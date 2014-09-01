using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeitorDeExcel
{
    public abstract class EspecificacaoItemImportadoExcel<T> : ISpecification<T>
    {
        private List<string> mensagens;

        public List<string> Mensagens
        {
            get { return mensagens ?? (mensagens = new List<string>()); }
            private set { mensagens = value; }
        }

        public void Adicionar(string mensagem)
        {
            Mensagens.Add(mensagem);
        }

        public abstract bool IsSatisfiedBy(T candidate);

        internal void Limpar()
        {
            mensagens = new List<string>();
        }
    }
}
