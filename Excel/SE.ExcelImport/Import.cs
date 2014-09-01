using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Model;

namespace SE.EasyExcelImporter
{
    [NotifyPropertyChanged]
    public class ImportInfo<T> where T : class
    {
        public static List<T> ObterItensValidadosDeUmExcel(string pathDoExcel, string planilha, EspecificacaoItemImportadoExcel<T> especificacao)
        {
            return CarregarDados<T>(pathDoExcel, planilha, especificacao);
        }

        private static List<T> CarregarDados<T>(string excelPath, string planilha,
                                                EspecificacaoItemImportadoExcel<T> especificacao)
        {
            var connection =
                new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath +
                                    "Extended Properties=\"Excel 8.0;HDR=YES\"");

            try
            {
                DataSet dadosDoExcel = ObterDadosDoExcel<T>(planilha, connection);
                List<string> colunas = ObterColunasDoDataTable(dadosDoExcel);

                return ObterRegistrosValidados(dadosDoExcel, colunas, especificacao);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        private static DataSet ObterDadosDoExcel<T>(string planilha, OleDbConnection connection)
        {
            var command = new OleDbCommand("SELECT * FROM [" + planilha + "$]", connection);
            connection.Open();
            var objAdapter1 = new OleDbDataAdapter();
            objAdapter1.SelectCommand = command;
            var objDataset1 = new DataSet();
            objAdapter1.Fill(objDataset1);
            return objDataset1;
        }

        private static List<string> ObterColunasDoDataTable(DataSet objDataset1)
        {
            var colunas = new List<string>();
            foreach (DataColumn coluna in objDataset1.Tables[0].Columns)
                colunas.Add(coluna.ColumnName);

            return colunas;
        }

        private static List<T> ObterRegistrosValidados<T>(DataSet objDataset1, List<string> colunas,
                                                          EspecificacaoItemImportadoExcel<T> especificacao)
        {
            var colecao = new List<T>();

            foreach (DataRow linha in objDataset1.Tables[0].Rows)
            {
                object itemRegistro;
                Type tipoItem = CriaUmNovoObjeto<T>(out itemRegistro);

                SetaValoresDoExcelNoObjetoCriado<T>(colunas, tipoItem, linha, itemRegistro);
                ValidarNotacaoDeDados(itemRegistro);
                ValidarEspecificacoes(especificacao, itemRegistro);
                AdicionaONovoObjetoNaColecao(colecao, itemRegistro);
            }

            return colecao;
        }

        private static void ValidarEspecificacoes<T>(EspecificacaoItemImportadoExcel<T> especificacao,
                                                     object itemRegistro)
        {
            if (especificacao != null)
            {
                especificacao.IsSatisfiedBy((T)itemRegistro);
                ((ItemRegistroExcel)itemRegistro).AdicionarErro(especificacao.Mensagens);
            }
        }

        private static void AdicionaONovoObjetoNaColecao<T>(List<T> colecao, object itemRegistro)
        {
            colecao.Add((T)itemRegistro);
        }

        private static void SetaValoresDoExcelNoObjetoCriado<T>(List<string> colunas, Type tipoItem, DataRow linha,
                                                                object itemRegistro)
        {
            foreach (string coluna in colunas)
            {
                PropertyInfo atributo = tipoItem.GetProperty(coluna);
                object valor = linha[coluna];

                if (!string.IsNullOrEmpty(valor.ToString()))
                    atributo.SetValue(itemRegistro, valor, null);
            }
        }

        private static Type CriaUmNovoObjeto<T>(out object itemRegistro)
        {
            Type tipoItem = Type.GetType(typeof(T).FullName);
            ConstructorInfo construtorItem = tipoItem.GetConstructor(Type.EmptyTypes);
            itemRegistro = construtorItem.Invoke(new object[] { });
            return tipoItem;
        }

        private static void ValidarNotacaoDeDados(object item)
        {
            var resultadosDaValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(item, null, null);
            Validator.TryValidateObject(item, contexto, resultadosDaValidacao, true);

            foreach (ValidationResult validationResult in resultadosDaValidacao)
                ((ItemRegistroExcel)item).AdicionarErro(validationResult.ErrorMessage);
        }
    }
}
