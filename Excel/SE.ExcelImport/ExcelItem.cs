using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.EasyExcelImporter
{
    internal class ExcelItem
    {
        private List<string> erros;

        private List<string> Erros
        {
            get { return erros ?? (erros = new List<string>()); }
            set { erros = value; }
        }

        internal string MensagensDeErro
        {
            get
            {
                var mensagensDeErro = new StringBuilder();

                foreach (string erro in Erros)
                    mensagensDeErro.Append(erro);

                return mensagensDeErro.ToString();
            }
        }

        internal void AdicionarErro(string mensagem)
        {
            Erros.Add(mensagem);
        }

        internal void AdicionarErro(List<string> mensagens)
        {
            Erros.AddRange(mensagens);
        }
    }
}