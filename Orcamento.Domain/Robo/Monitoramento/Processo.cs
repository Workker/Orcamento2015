using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento
{
    public class Processo
    {
        private OleDbConnection Cn { get; set; }
        private OleDbCommand Cmd { get; set; }


        public OleDbDataReader InicializarCarga(Carga carga)
        {
            try
            {
                string _conectionstring;
                _conectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;";
                _conectionstring += String.Format("Data Source={0};", carga.Diretorio);
                _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

                Cn = new OleDbConnection(_conectionstring);
                Cmd = new OleDbCommand("Select * from [carga$]", Cn);
                Cn.Open();
                var reader = Cmd.ExecuteReader();
                return reader;
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro na leitura", "Nao foi possivel ler o excel, por favor verifque se o layout esta correto (colunas, valores, nome da aba(carga) )", 0,
                                          TipoDetalheEnum.erroLeituraExcel);
                return null;
            }
        }

        public void FinalizarCarga()
        {
            Cmd.Clone();
            Cmd.Dispose();

            Cn.Close();
            Cn.Dispose();
            
        }
    }
}
