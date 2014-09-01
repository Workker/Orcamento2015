using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeitorDeExcel
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // SETAR AS SUAS CONFIGURAÇÕES AQUI
            string pathDoExcel = @"C:\projects\ExcelReader\ExcelReader\bin\Debug\4.xls;";
            string planilha = "Plan1";

            // CRIA AS ESPECIFICAÇÕES PARA VALIDAÇÕES
            var listaDeEspecificacoes = new List<EspecificacaoItemImportadoExcel<Pessoa>>();

            var especificacao1 = new PrimeiraLetraDoNomeDeveSerM();
            listaDeEspecificacoes.Add(especificacao1);

            var especificacao2 = new EstadoDeveSerRioDeJaneiro();
            listaDeEspecificacoes.Add(especificacao2);

            // OBTEM OS REGISTROS DO EXCEL
            List<Pessoa> pessoas = ImportadorDeExcel<Pessoa>.ObterItensValidadosDeUmExcel(pathDoExcel, planilha, listaDeEspecificacoes);

            // PRINTA NA TELA OS RESULTADOS
            foreach (Pessoa registro in pessoas)
                Console.WriteLine(registro.Nome + " " +
                    registro.Idade.ToString() + " " +
                    registro.Estado + ": " +
                    registro.MensagensDeErro);

            Console.ReadLine();
        }
    }

    public class Pessoa : ItemRegistroExcel
    {
        [Required(ErrorMessage = "O campo do nome não foi preenchido")]
        public string Nome { get; set; }

        [Required]
        public double Idade { get; set; }

        [Required]
        public string Estado { get; set; }
    }

    public class PrimeiraLetraDoNomeDeveSerM : EspecificacaoItemImportadoExcel<Pessoa>
    {
        public override bool IsSatisfiedBy(Pessoa candidate)
        {
            var satisfeito = candidate.Nome[0] == 'M';

            if (!satisfeito)
                Adicionar("A primeira letra do nome não é M!");

            return satisfeito;
        }
    }

    public class EstadoDeveSerRioDeJaneiro : EspecificacaoItemImportadoExcel<Pessoa>
    {
        public override bool IsSatisfiedBy(Pessoa candidate)
        {
            var satisfeito = candidate.Estado == "RJ";

            if (!satisfeito)
                Adicionar("O estado não é RJ!");

            return satisfeito;
        }
    }
}