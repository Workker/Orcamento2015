using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DTO;
using System.Diagnostics.Contracts;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace Orcamento.Domain.Servico
{
    public class ServicoMapperOrcamentoView
    {
        public Orcamentos Orcamentos { get { return new Orcamentos(); } }

        public List<ContaHospitalarDTO> TransformarProducao(Orcamento orcamento)
        {
            Contract.Requires(orcamento != null, "Orcamento não informado.");
            Contract.Requires(orcamento.Servicos != null, "Serviços não informado.");
            Contract.Requires(orcamento.Servicos.All(s => s.Valores != null && s.Valores.Count == 12), "Valores não podem ser nulos.");


            List<ContaHospitalarDTO> contasDTO = new List<ContaHospitalarDTO>();

            foreach (var servico in orcamento.Servicos)
            {
                ContaHospitalarDTO conta = new ContaHospitalarDTO();

                conta.Conta = servico.Conta.Nome;
                conta.Setor = servico.Setor.NomeSetor;
                conta.Subsetor = servico.SubSetor.NomeSetor;
                conta.IdServico = servico.Id;
                conta.Valores = new List<ValorContaDTO>();
                conta.ContaID = servico.Conta.Id;
                conta.ContasAxenadas = new List<int>();

                foreach (var contaAnexada in servico.Conta.ContasAnexadas)
                {
                    conta.ContasAxenadas.Add(contaAnexada.Id);
                }

                foreach (var valor in servico.Valores)
                {
                    conta.Valores.Add(new ValorContaDTO() { Mes = valor.Mes, TipoValor = servico.Conta.TipoValorContaEnum, Valor = valor.Valor, ValorID = valor.Id, Calculado = servico.Conta.Calculado, ContaId = servico.Conta.Id });
                }

                contasDTO.Add(conta);
            }

            return contasDTO;
        }

        public List<ContaHospitalarDTO> TransformarProducao(List<ServicoHospitalar> servicos)
        {
            Contract.Requires(servicos != null, "Orcamento não informado.");
            Contract.Requires(servicos != null, "Serviços não informado.");
            Contract.Requires(servicos.All(s => s.Valores != null && s.Valores.Count == 12), "Valores não podem ser nulos.");


            List<ContaHospitalarDTO> contasDTO = new List<ContaHospitalarDTO>();

            foreach (var servico in servicos)
            {
                ContaHospitalarDTO conta = new ContaHospitalarDTO();

                conta.Conta = servico.Conta.Nome;
                conta.Setor = servico.Setor.NomeSetor;
                conta.Subsetor = servico.SubSetor.NomeSetor;
                conta.IdServico = servico.Id;
                conta.Valores = new List<ValorContaDTO>();
                conta.ContaID = servico.Conta.Id;
                conta.ContasAxenadas = new List<int>();

                foreach (var contaAnexada in servico.Conta.ContasAnexadas)
                {
                    conta.ContasAxenadas.Add(contaAnexada.Id);
                }

                foreach (var valor in servico.Valores)
                {
                    conta.Valores.Add(new ValorContaDTO() { Mes = valor.Mes, TipoValor = servico.Conta.TipoValorContaEnum, Valor = valor.Valor, ValorID = valor.Id, Calculado = servico.Conta.Calculado, ContaId = servico.Conta.Id });
                }

                contasDTO.Add(conta);
            }

            return contasDTO;
        }

        public List<ContaHospitalarDTO> TransformarProducaoDeInsumos(List<ServicoHospitalar> servicos, List<ServicoHospitalar> Porcentagens)
        {
            Contract.Requires(servicos != null, "Orcamento não informado.");
            Contract.Requires(servicos != null, "Serviços não informado.");
            Contract.Requires(servicos.All(s => s.Valores != null && s.Valores.Count == 12), "Valores não podem ser nulos.");


            List<ContaHospitalarDTO> contasDTO = new List<ContaHospitalarDTO>();

            foreach (var servico in servicos)
            {
                ContaHospitalarDTO conta = new ContaHospitalarDTO();

                conta.Conta = servico.Conta.Nome;
                conta.Setor = servico.Setor.NomeSetor;
                conta.Subsetor = servico.SubSetor.NomeSetor;
                conta.IdServico = servico.Id;
                conta.Valores = new List<ValorContaDTO>();
                conta.ContaID = servico.Conta.Id;
                conta.ContasAxenadas = new List<int>();

                foreach (var contaAnexada in servico.Conta.ContasAnexadas)
                {
                    conta.ContasAxenadas.Add(contaAnexada.Id);
                }

                foreach (var valor in servico.Valores)
                {
                    var valorConta = new ValorContaDTO() { Mes = valor.Mes, Valor = valor.Valor, TipoValor = servico.Conta.TipoValorContaEnum, ValorID = valor.Id, Calculado = servico.Conta.Calculado, ContaId = servico.Conta.Id };

                    if (servico.Conta.Nome == "Capacidade Operacional")
                    {
                        var servicoPorcentagem = Porcentagens.Where(d => d.SubSetor.NomeSetor == servico.SubSetor.NomeSetor && d.Conta.TipoValorContaEnum == TipoValorContaEnum.Porcentagem).FirstOrDefault();

                        foreach (var valorPorcentagem in servicoPorcentagem.Valores.Where(v => v.Mes == valor.Mes).ToList())
                        {
                            if (valorPorcentagem.Valor != 0 && valorPorcentagem.Valor != 100)
                                valorConta.Valor = CalcularPorcentagem(valorConta.Valor, valorPorcentagem);
                            else if(valorPorcentagem.Valor == 0)
                                valorConta.Valor = 0;
                        }

                        valorConta.Valor = valorConta.Valor * DateTime.DaysInMonth(2015, (int)valor.Mes);

                        conta.Conta = "Pac Dia";
                    }
                    conta.Valores.Add(valorConta);
                }

                contasDTO.Add(conta);
            }

            return contasDTO;
        }

        private double CalcularPorcentagem(double ValorServico, ProducaoHospitalar valor)
        {
            var variavelMumtiplicada = (ValorServico * valor.Valor);
            var variavelDividida = (variavelMumtiplicada / 100);

            return variavelDividida;
        }

        public List<ContaHospitalarDTO> TransformarProducao(List<CustoUnitario> custos)
        {
            Contract.Requires(custos != null, "Orcamento não informado.");
            Contract.Requires(custos != null, "Serviços não informado.");
            Contract.Requires(custos.All(s => s.Valores != null && s.Valores.Count == 12), "Valores não podem ser nulos.");


            List<ContaHospitalarDTO> contasDTO = new List<ContaHospitalarDTO>();

            foreach (var servico in custos)
            {
                ContaHospitalarDTO conta = new ContaHospitalarDTO();

                conta.Setor = servico.Setor.NomeSetor;
                conta.Subsetor = servico.SubSetor.NomeSetor;
                conta.IdServico = servico.Id;
                conta.Valores = new List<ValorContaDTO>();
                conta.ContasAxenadas = new List<int>();



                foreach (var valor in servico.Valores)
                {
                    conta.Valores.Add(new ValorContaDTO() { Mes = valor.Mes, Valor = valor.Valor, ValorID = valor.Id, });
                }

                contasDTO.Add(conta);
            }

            return contasDTO;
        }

        public List<ContaHospitalarDTO> TransformarProducao(List<CustoUnitarioTotal> custos)
        {
            Contract.Requires(custos != null, "Orcamento não informado.");
            Contract.Requires(custos != null, "Serviços não informado.");
            Contract.Requires(custos.All(s => s.Valores != null && s.Valores.Count == 12), "Valores não podem ser nulos.");


            List<ContaHospitalarDTO> contasDTO = new List<ContaHospitalarDTO>();

            foreach (var servico in custos)
            {
                ContaHospitalarDTO conta = new ContaHospitalarDTO();


                conta.Setor = servico.Setor.NomeSetor;
                conta.Subsetor = servico.SubSetor.NomeSetor;
                conta.IdServico = servico.Id;
                conta.Valores = new List<ValorContaDTO>();
                conta.ContasAxenadas = new List<int>();


                foreach (var valor in servico.Valores)
                {
                    conta.Valores.Add(new ValorContaDTO() { Mes = valor.Mes, Valor = valor.Valor, ValorID = valor.Id });
                }

                contasDTO.Add(conta);
            }

            return contasDTO;
        }

        public void Salvar(Orcamento orcamento, List<ContaHospitalarDTO> contas)
        {
            Contract.Requires(orcamento != null, "Orcamento não informado.");
            Contract.Requires(orcamento.Servicos != null, "Produção não informada.");
            Contract.Requires(contas != null, "Contas não foram informadas.");

            foreach (var servico in orcamento.Servicos)
            {
                foreach (var valor in servico.Valores)
                {
                    valor.Valor = contas.Where(c => c.IdServico == servico.Id).FirstOrDefault().Valores.Where(t => t.Mes == valor.Mes).FirstOrDefault().Valor;
                }
            }

            Orcamentos.Salvar(orcamento);
        }


        public void Salvar(Insumo insumo, List<ContaHospitalarDTO> contas, bool custoUnitario)
        {
            Contract.Requires(insumo != null, "Insumo não informado.");
            Contract.Requires(insumo.CustosUnitarios != null, "Produção não informada.");
            Contract.Requires(contas != null, "Contas não foram informadas.");

            Insumos insumos = new Insumos();

            foreach (var custo in insumo.CustosUnitarios)
            {
                foreach (var valor in custo.Valores)
                {
                    valor.Valor = contas.Where(c => c.IdServico == custo.Id).FirstOrDefault().Valores.Where(t => t.Mes == valor.Mes).FirstOrDefault().Valor;
                }
            }

            insumos.Salvar(insumo);
        }
    }
}
