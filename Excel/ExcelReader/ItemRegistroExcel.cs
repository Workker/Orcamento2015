using System.Collections.Generic;
using System.Text;

namespace LeitorDeExcel
{
    public class ItemRegistroExcel
    {
        private List<string> erros;

        private List<string> Erros
        {
            get { return erros ?? (erros = new List<string>()); }
            set { erros = value; }
        }

        public string MensagensDeErro
        {
            get
            {
                var mensagensDeErro = new StringBuilder();

                foreach (string erro in Erros)
                    mensagensDeErro.Append(erro);

                return mensagensDeErro.ToString();
            }
        }

        public void AdicionarErro(string mensagem)
        {
            Erros.Add(mensagem);
        }

        public void AdicionarErro(List<string> mensagens)
        {
            Erros.AddRange(mensagens);
        }
    }
}