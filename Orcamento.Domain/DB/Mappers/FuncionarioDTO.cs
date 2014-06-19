using System;

namespace Orcamento.Domain.DB.Mappers
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        public string Identificacao { get; set; }
        public string Hospital { get; set; }
        public string CentroDeCusto { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        //public string Funcionario { get; set; }
        
        public double SalarioBase { get; set; }
        public int DataDeAdmissao { get; set; }
        public bool Demitido { get; set; }
        public int MesDeDemissao { get; set; }
        public bool Aumentado { get; set; }
        public double PercentualDeAumento { get; set; }
        public int MesDeAumento { get; set; }
        public DateTime DataDeSaidaDeFerias { get; set; }
        public string Cargo { get; set; }
        public int AnoAdmissao { get; set; }
        public int NumeroDeVaga { get; set; }
        public bool Excluido { get; set; }
        public bool FuncionarioFoiAumentado { get; set; }

        public string InicialNumeroMatricula { get; set; }


        public int ObterNumeroSequencial
        {
            get
            {
                return string.IsNullOrEmpty(InicialNumeroMatricula) ? 0 :InicialNumeroMatricula == "N"? 999999 + NumeroDeVaga:555555 + NumeroDeVaga;
            }
        }

        public int FuncionarioReposicao { get; set; }
    }
}
