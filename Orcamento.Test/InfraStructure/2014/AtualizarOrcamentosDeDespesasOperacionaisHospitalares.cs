using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain.Servico.OutrasDespesas;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Test.InfraStructure._2014
{
    [TestFixture]
    public class AtualizarOrcamentosDeDespesasOperacionaisHospitalares
    {
        public ServicoOrcamentoOperacionalVersao ServicoOrcamento { get { return new ServicoOrcamentoOperacionalVersao(); } }
        [Test]
        public void Executar()
        {
            var tiposConta = new TiposConta();
            var tiposDeconta = new TiposConta();
            var tipoContaOutras = tiposDeconta.Obter<TipoConta>(1);

            string _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\CargaOrcamento2014\\DepartamentoCentroDeCustoContaHospitalarNew.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [c$]", cn);
            cn.Open();
            var reader = cmd.ExecuteReader();

            var documento = new List<RegistroExcel>();

            var listaContas = new List<Conta>();
            var listaCentrosDeCusto = new List<CentroDeCusto>();

            var repositorioDeHospitais = new Hospitais();
            var repositorioDeCusto = new CentrosDeCusto();
            var repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                if (reader[0] == null || reader[0] == DBNull.Value || string.IsNullOrEmpty(reader[0].ToString()))
                    break;

                var registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[5],
                    CodigoCentroDeCusto = Convert.ToString(reader[0]),
                    DescricaoCentroDeCusto = (string)reader[1]
                };

                if (reader[5] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[3];
                    registroExcel.CodigoConta = Convert.ToString(reader[2]);
                    registroExcel.GrupoResumoNome = (string)reader[4];
                }

                registroExcel.Mes = (string)reader[7];
                registroExcel.Valor = Convert.ToInt64(reader[6]);

                documento.Add(registroExcel);
            }

            cn.Close();
            cn.Dispose();
            cmd.Dispose();

            var gruposDeConta = documento.Select(x => x.GrupoResumoNome).Distinct();


            var codigosDeConta = documento.Select(x => x.CodigoConta).Distinct();

            foreach (var codigoDeConta in codigosDeConta)
            {

            }


            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();


            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();
            var listaHospitais = new List<Departamento>();
            var orcamentoNao = new List<Orcamento.Domain.Orcamento>();
            foreach (var nomeHospital in hospitais)
            {
                var hospital = repositorioDeHospitais.ObterPor(nomeHospital);
                if (hospital == null)
                    throw new Exception();

                listaCentrosDeCusto = new List<CentroDeCusto>();
                listaContas = new List<Conta>();

                if (!listaHospitais.Any(l => l.Nome == nomeHospital))
                {
                    listaHospitais.Add(hospital);

                    foreach (var documentoHospital in documento.Where(d => d.NomeHospital == nomeHospital))
                    {
                        var centroDeCusto = repositorioDeCusto.ObterPor(documentoHospital.CodigoCentroDeCusto);
                        if (centroDeCusto == null)
                            throw new Exception();

                        if (!listaCentrosDeCusto.Any(l => l.CodigoDoCentroDeCusto == documentoHospital.CodigoCentroDeCusto))
                            listaCentrosDeCusto.Add(centroDeCusto);

                        string descricaoDaConta = documento.Where(x => x.CodigoConta == documentoHospital.CodigoConta).Select(y => y.DescricaoConta).Distinct().First();

                        var conta = repositorioContas.ObterContaPor(documentoHospital.CodigoConta);
                        if (conta == null)
                            throw new Exception();

                        if (!listaContas.Any(l => l.CodigoDaConta == documentoHospital.CodigoConta))
                            listaContas.Add(conta);


                    }


                    foreach (var centroDeCusto in listaCentrosDeCusto)
                    {
                        Orcamentos orcamentos = new Orcamentos();
                        var orcamento = orcamentos.ObterOrcamentoFinalOrcamentoOperacional(centroDeCusto, hospital);
                        if (orcamento == null)
                        {
                            var orcamentosGerenciamento = orcamentos.TodosOrcamentosOperacionaisPor(centroDeCusto, hospital);

                            bool podeCriarMaisUmaVersaoDeOrcamento = PodeCriarMaisUmaVersaoDeOrcamento(hospital, centroDeCusto.Id, orcamentosGerenciamento);

                            if (podeCriarMaisUmaVersaoDeOrcamento)
                                orcamento = this.ServicoOrcamento.CriarOrcamentoOperacional(orcamentosGerenciamento, hospital, centroDeCusto, 2014);
                            else
                                throw new Exception();

                            foreach (var despesa in orcamento.DespesasOperacionais)
                            {
                                var valor = documento.FirstOrDefault(
                                    d =>
                                    d.CodigoCentroDeCusto == centroDeCusto.CodigoDoCentroDeCusto &&
                                    d.CodigoConta == despesa.Conta.CodigoDaConta && d.NomeHospital == nomeHospital && d.Mes == ObterMes(despesa.Mes));

                                if (valor != null)
                                    despesa.Valor = valor.Valor;
                            }
                            orcamento.AtribuirVersaoFinal();
                            orcamentos.Salvar(orcamento);
                        }

                    }

                }




            }

            //ServicoAtualizarDespesasOperacionais servico = new ServicoAtualizarDespesasOperacionais();
            //var centrosAbuscar = listaCentrosDeCusto.Distinct();
            //foreach (var centro in centrosAbuscar)
            //{
            //    servico.AtualizarDespesas(centro);
            //}



        }
        private bool PodeCriarMaisUmaVersaoDeOrcamento(Departamento setor, int centroDeCustoId, List<Orcamento.Domain.Orcamento> orcamentosGerenciamento)
        {
            var gerenciamento = new GerenciadorDeOrcamentos();

            return gerenciamento.PodeCriarOrcamento(orcamentosGerenciamento, setor,
                                                    setor.ObterCentroDeCustoPor(
                                                        centroDeCustoId),
                                                    TipoOrcamentoEnum.
                                                        DespesaOperacional);
        }
        private string ObterMes(MesEnum mesEnum)
        {
            switch (mesEnum)
            {
                case MesEnum.Janeiro:
                    return "JANEIRO";
                case MesEnum.Fevereiro:
                    return "FEVEREIRO";
                case MesEnum.Marco:
                    return "MARÇO";
                case MesEnum.Abril:
                    return "ABRIL";
                case MesEnum.Maio:
                    return "MAIO";
                case MesEnum.Junho:
                    return "JUNHO";
                case MesEnum.Julho:
                    return "JULHO";
                case MesEnum.Agosto:
                    return "AGOSTO";
                case MesEnum.Setembro:
                    return "SETEMBRO";
                case MesEnum.Outubro:
                    return "OUTUBRO";
                case MesEnum.Novembro:
                    return "NOVEMBRO";
                case MesEnum.Dezembro:
                    return "DEZEMBRO";
            }
            throw new Exception();
        }
    }

    public class DepartamentoTest
    {
        public string Nome { get; set; }
        public List<CentroDeCustoTest> Centros { get; set; }
    }

    public class CentroDeCustoTest
    {
        public string CodCentro { get; set; }
        public List<ContaTest> Contas { get; set; }
    }

    public class ContaTest
    {
        public string CodConta { get; set; }
        public string Mes { get; set; }

    }
}

