using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;

namespace Orcamento.Domain.DB.Repositorio
{
//    public interface IDespesasPessoais
//    {
//        void Adicionar(DespesaPessoal despesaPessoal);
//        DataSet Listar();
//        DataSet Listar(string p);
//        void Salvar(IAggregateRoot<int> root);
//        void Deletar(IAggregateRoot<int> root);
//        IList<T> Todos<T>();
//        T Obter<T>(int id);
//    }

//    public class DespesasPessoais : BaseRepository, IDespesasPessoais
//    {
////        public void Adicionar(DespesaPessoal despesaPessoal)
////        {
////            base.Salvar(despesaPessoal);
////        }

////        public DataSet Listar()
////        {
////            StringBuilder query = new StringBuilder();

////            query.Append(@"
////                    select gc.Nome as Nome, round(sum(p.Valor),2) as Valor
////                    from DespesaPessoal dp,
////                    parcela p,
////                    GrupoDeConta gc
////                    where dp.Id = p.DespesaPessoal_Id
////                    and dp.GrupoDeCOnta_Id = gc.Id
////                    group by gc.Nome");

////            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["Conexao"]);

////            SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), cnn);

////            cnn.Open();

////            DataSet dataSet = new DataSet();

////            adapter.Fill(dataSet);

////            return dataSet;
////        }

////        public DataSet Listar(string p)
////        {
////            StringBuilder query = new StringBuilder();

////            query.Append(@"
////                    select gc.Nome as Nome, round(sum(p.Valor),2) as Valor
////                    from DespesaPessoal dp,
////                    parcela p,
////                    GrupoDeConta gc,
////                    CentroDeCusto cc
////                    where dp.Id = p.DespesaPessoal_Id
////                    and dp.GrupoDeCOnta_Id = gc.Id
////                    and cc.Id = dp.CentroDeCusto_id
////                    and cc.Id = ");
////            query.Append(p);
////            query.Append(" group by gc.Nome order by gc.Nome asc");

////            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["Conexao"]);

////            SqlDataAdapter adapter = new SqlDataAdapter(query.ToString(), cnn);

////            cnn.Open();

////            DataSet dataSet = new DataSet();

////            adapter.Fill(dataSet);

////            return dataSet;
////        }

////        public void Remover(DespesaPessoal despesa)
////        {
////            base.Deletar(despesa);
////        }
//    }
}
