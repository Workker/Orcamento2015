using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.DTO;

namespace Orcamento.Domain
{
    [Serializable]
    public class OrcamentoHospitalar : Orcamento
    {
        public override TipoOrcamentoEnum Tipo
        {
            get
            {
                return TipoOrcamentoEnum.Hospitalar;
            }
            set
            {
                base.Tipo = value;
            }
        }

        public OrcamentoHospitalar(Orcamento orcamentoASerClonado)
        {
            this.Ano = orcamentoASerClonado.Ano;
            this.Setor = orcamentoASerClonado.Setor;
            AdicionarServicos(orcamentoASerClonado);

            AdicionarFatores(orcamentoASerClonado);

        }

        private void AdicionarFatores(Orcamento orcamentoASerClonado)
        {
            this.FatoresReceita = new List<FatorReceita>();


            foreach (var fator in orcamentoASerClonado.FatoresReceita)
            {
                FatorReceita fatorNovo = new FatorReceita(fator.Setor, fator.SubSetor,true);
                fatorNovo.Incrementos = new List<IncrementoDaComplexidade>();
                foreach (var incremento in fator.Incrementos)
                {
                    IncrementoDaComplexidade incrementoNovo = new IncrementoDaComplexidade(incremento.Mes);

                    incrementoNovo.Negativo = incremento.Negativo;
                    incrementoNovo.ReceitaBruta = incremento.ReceitaBruta;
                    incrementoNovo.ReceitaLiquida = incremento.ReceitaLiquida;
                    incrementoNovo.Ticket = incremento.Ticket;
                    incrementoNovo.Complexidade = incremento.Complexidade;

                    fatorNovo.Incrementos.Add(incrementoNovo);
                }

                this.FatoresReceita.Add(fatorNovo);
            }
        }

        private void AdicionarServicos(Orcamento orcamentoASerClonado)
        {
            this.Servicos = new List<ServicoHospitalar>();

            foreach (var servico in orcamentoASerClonado.Servicos)
            {
                ServicoHospitalar servicoNovo = new ServicoHospitalar(servico.Conta, servico.SubSetor, servico.Setor,true);

                servicoNovo.Valores = new List<ProducaoHospitalar>();
                foreach (var valor in servico.Valores)
                {
                    ProducaoHospitalar producaoNova = new ProducaoHospitalar(valor.Mes);

                    producaoNova.Valor = valor.Valor;

                    servicoNovo.Valores.Add(producaoNova);
                   
                }

                this.Servicos.Add(servicoNovo);
            }
        }

        public OrcamentoHospitalar(Departamento departamento, int ano)
        {
            base.InformarDepartamento(departamento);
            this.Ano = ano;
            this.CriarServicosHospitalaresNoAno();
            this.Servicos = new List<ServicoHospitalar>();
            this.FatoresReceita = new List<FatorReceita>();
            CriarServicosHospitalaresNoAno();
            CriarFatoresDeReceita();
        }

        protected OrcamentoHospitalar()
        { }

        public virtual void CriarServicosHospitalaresNoAno()
        {
            foreach (var setor in this.Setor.Setores)
            {
                foreach (var conta in setor.Contas)
                {
                    foreach (var subSetor in setor.SubSetores)
                    {
                        CriarServico(conta, subSetor, setor);
                    }
                }
            }
        }

        public virtual void CriarFatoresDeReceita()
        {
            foreach (var setor in this.Setor.Setores)
            {
                foreach (var subSetor in setor.SubSetores)
                {
                    CriarFatorReceita(subSetor, setor);
                }
            }
        }

        public virtual void CriarFatorReceita(SubSetorHospital subSetor, SetorHospitalar setor)
        {
            this.FatoresReceita.Add(new FatorReceita(setor, subSetor));
        }

        public virtual void CriarServico(ContaHospital conta, SubSetorHospital subSetor, SetorHospitalar setor)
        {
            this.Servicos.Add(new ServicoHospitalar(conta, subSetor, setor));
        }

        public override void CalcularReceitaBruta(List<TicketDeProducao> tickets) 
        {
            foreach (var  fator in this.FatoresReceita)
            {
                fator.CalcularReceitaBruta(Servicos.Where(s => s.Setor.Id == fator.Setor.Id && s.SubSetor.Id == fator.SubSetor.Id).ToList(),tickets);
            }
        }

        public override void CalcularReceitaLiquida(List<TicketDeProducao> tickets,List<TicketParcela> parcelas)
        {
            foreach (var fator in this.FatoresReceita)
            {
                fator.CalcularReceitaLiquida(Servicos.Where(s => s.Setor.Id == fator.Setor.Id && s.SubSetor.Id == fator.SubSetor.Id).ToList(),tickets,parcelas);
            }
        }

        public override void CalcularTotalDRE()
        {
            this.Legenda = "Orçamento Hospitalar";
            TicketsDeProducao tickets = new TicketsDeProducao();
            TicketsDeReceita ticketsDeReceita = new TicketsDeReceita();

            var ticketDeReceita = ticketsDeReceita.Obter(this.Setor, TipoTicketDeReceita.ReajusteDeConvenios);


            this.CalcularReceitaLiquida(tickets.Todos(this.Setor).ToList(), ticketDeReceita.Parcelas.ToList());
            this.ValorTotalDRE = FatoresReceita.Sum(x => x.Incrementos.Sum(y => y.ReceitaLiquida));
        }

        public override void CalcularCustoHospitalar(TicketDeReceita ticket, List<CustoUnitario> custosUnitarios, List<ContaHospitalarDTO> contasUnitarias)
        {
            this.CustosUnitariosTotal = new List<CustoUnitarioTotal>();
            foreach (var setor in this.Setor.Setores)
            {
               
                    foreach (var subSetor in setor.SubSetores)
                    {
                        CriarCustoTotal(subSetor, setor, ticket, custosUnitarios,contasUnitarias);
                    }
            }
        }

        private void CriarCustoTotal( SubSetorHospital subSetor, SetorHospitalar setor,TicketDeReceita ticket,List<CustoUnitario> custosUnitarios,List<ContaHospitalarDTO> contasUnitarias)
        {
            this.CustosUnitariosTotal.Add(new CustoUnitarioTotal(subSetor, setor, contasUnitarias.Where(s=> s.Subsetor == subSetor.NomeSetor && s.Setor == setor.NomeSetor).FirstOrDefault().Valores,
                custosUnitarios.Where(s =>  s.SubSetor.Id == subSetor.Id && s.Setor.Id == setor.Id).FirstOrDefault().Valores.ToList(), ticket));
        }
    }
}
