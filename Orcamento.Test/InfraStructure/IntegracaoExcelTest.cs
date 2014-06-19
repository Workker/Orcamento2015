using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data.OleDb;
using System.Data;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;

namespace Orcamento.Test.InfraStructure
{
  

    [TestFixture]
    [Ignore]
    public class IntegracaoExcelTest
    {
        [Test]
        [Ignore]
        public void Inicializar()
        {
            //string cnn = @"Provider=CRU.Jet.OLEDB.4.0; Data Source=C:\\preco.xls;Extended Properties='Excel 8.0;HDR=NO;'";
            //string cnn = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\preco.xls;Extended Properties='Excel 8.0;HDR=NO;'";
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Hospital2.xls");
            //_conectionstring += "Data Source=" + Server.mappath("~/adm/xls/preço.xls") + ";";
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [HOSPITAL x CENTRO CUSTO MOD$]", cn);

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            int i = 0;

            List<Hospital> registrosDeIntegracao = new List<Hospital>();

            string ultimoNomeDeHospital = string.Empty;

            Hospitais hospitais = new Hospitais();

            foreach (DataRow row in dt.Rows)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                Hospital hospital = null;

                if (string.IsNullOrEmpty(ultimoNomeDeHospital) || ultimoNomeDeHospital != (string)row[0])
                {
                    hospital = new Hospital();
                    hospital.Nome = (string)row[0];
                }
                else
                    hospital = hospitais.ObterPor((string)row[0]);

                CentroDeCusto centroDeCusto = hospital.ObterCentroDeCustoPor(codigoDoCentroCusto: (string)row[1]);

                if (centroDeCusto == null)
                {
                    CentrosDeCusto centrosDeCusto = new CentrosDeCusto();
                    centroDeCusto = centrosDeCusto.ObterPor(codigo: (string)row[1]);

                    if (centroDeCusto == null)
                    {
                        centroDeCusto = new CentroDeCusto(nome: (string)row[2]);

                        centroDeCusto.CodigoDoCentroDeCusto = (string)row[1];

                        Conta conta = centroDeCusto.ObterContaPor((string)row[4]);

                        if (conta == null)
                        {
                            conta = CriarContaInexistente(row, centroDeCusto, conta);
                        }

                        hospital.Adicionar(centroDeCusto);
                        hospitais.Adicionar(hospital);
                    }
                    else
                    {
                        CriarContaComCentroDeCustoEncontrado(hospitais, row, hospital, centroDeCusto);
                    }
                }
                else
                {
                    CriarContaComCentroDeCustoEncontrado(hospitais, row, hospital, centroDeCusto);
                }

                ultimoNomeDeHospital = hospital.Nome;
            }

            //var hospitalBarraDor = registrosDeIntegracao.Where(x => x.NomeHospital == "BARRA D'OR");

            cn.Close();
            cn.Dispose();
            cmd.Dispose();
        }

        private static Conta CriarContaInexistente(DataRow row, CentroDeCusto centroDeCusto, Conta conta)
        {
            Contas contas = new Contas();

            conta = contas.ObterContaPor(codigo: (string)row[4]);

            if (conta == null)
            {
                conta = new Conta(nome: (string)row[3]);

                conta.CodigoDaConta = (string)row[4];

                centroDeCusto.AdicionarConta(conta);
            }
            else
            {
                centroDeCusto.AdicionarConta(conta);
            }

            return conta;
        }

        private static void CriarContaComCentroDeCustoEncontrado(Hospitais hospitais, DataRow row, Hospital hospital, CentroDeCusto centroDeCusto)
        {
            Conta conta = centroDeCusto.ObterContaPor((string)row[4]);

            if (conta == null)
            {
                Contas contas = new Contas();

                conta = contas.ObterContaPor(codigo: (string)row[4]);

                if (conta == null)
                {
                    conta = new Conta(nome: (string)row[3]);

                    conta.CodigoDaConta = (string)row[4];

                    centroDeCusto.AdicionarConta(conta);
                }
                else
                {
                    centroDeCusto.AdicionarConta(conta);
                }
            }

            hospital.Remover(centroDeCusto);
            hospital.Adicionar(centroDeCusto);
            hospitais.Adicionar(hospital);
        }

    }
}
