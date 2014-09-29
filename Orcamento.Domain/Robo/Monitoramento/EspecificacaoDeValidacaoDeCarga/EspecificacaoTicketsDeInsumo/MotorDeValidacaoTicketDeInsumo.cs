using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao;
using Orcamento.Domain.Util.Specification;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsDeInsumo
{
    public class MotorDeValidacaoTicketDeInsumo
    {
        private readonly Carga carga;
        private readonly List<TicketDeInsumoExcel> ticketsInsumo;

        public MotorDeValidacaoTicketDeInsumo(Carga carga, List<TicketDeInsumoExcel> ticketsInsumo)
        {
            this.carga = carga;
            this.ticketsInsumo = ticketsInsumo;
        }

        private Departamentos departamentos;
        public virtual Departamentos DepartamentosRepositorio
        {
            get
            {
                if (departamentos == null)
                    departamentos = new Departamentos();

                return departamentos;
            }
        }

        public virtual List<Departamento> Departamentos { get; set; }
        public virtual List<CustoUnitario> CustosUnitarios { get; set; }
        public virtual List<Insumo> Insumos { get; set; }
        public virtual List<Setor> Setores { get; set; }
        public virtual List<SubSetorHospital> SubSetores { get; set; }

        public void Validar()
        {
            Departamentos = DepartamentosRepositorio.TodosComSetoresESubSetores();
            Contract.Requires(Departamentos != null, "Departamentos não encontrados");

            ValidaDepartamentos();

            if (!carga.Ok())
                return;

            ValidarSetores();

            if (!carga.Ok())
                return;

            ValidarSubSetores();

            if (!carga.Ok())
                return;

            ValidarInsumos();

            if (!carga.Ok())
                return;

            ValidarCustoUnitario();

            if (!carga.Ok())
                return;

            ValidarProducaoHospitalar();
        }

        private void ValidarSetores()
        {
            foreach (TicketDeInsumoExcel ticketDeInsumoExcel in ticketsInsumo)
            {
                var setores = ticketDeInsumoExcel.DepartamentoEntidade.Setores.ToList();

                Especificacao especificacaoTicketParcela =
                    FabricaDeEspeficicacaoSetor.ObterEspeficicacao(ticketDeInsumoExcel, setores);

                especificacaoTicketParcela.IsSatisfiedBy(carga);
            }
        }
        private void ValidarSubSetores()
        {
            foreach (TicketDeInsumoExcel ticketDeInsumoExcel in ticketsInsumo)
            {
                var subSetores = ticketDeInsumoExcel.DepartamentoEntidade.Setores.SelectMany(s => s.SubSetores).ToList();

                Especificacao especificacaoTicketParcela =
                    FabricaDeEspeficicacaoSubSetor.ObterEspeficicacao(ticketDeInsumoExcel, subSetores);

                especificacaoTicketParcela.IsSatisfiedBy(carga);

            }
        }

        private void ValidarProducaoHospitalar()
        {
            foreach (TicketDeInsumoExcel ticketDeInsumoExcel in ticketsInsumo)
            {
                List<ProducaoHospitalar> producoes = ticketDeInsumoExcel.CustoUnitario.Valores.ToList();

                Especificacao especificacaoTicketParcela =
                    FabricaDeEspeficicacaoProducaoHospitalar.ObterEspeficicacao(ticketDeInsumoExcel, producoes);

                if (especificacaoTicketParcela.IsSatisfiedBy(carga))
                {
                    ticketDeInsumoExcel.ProducaoHospitalar = ticketDeInsumoExcel.CustoUnitario.Valores.FirstOrDefault(p => ticketDeInsumoExcel.mes == (int)p.Mes);
                }
            }
        }

        private void ValidarCustoUnitario()
        {
            var tickets = new CustosUnitarios();

            CustosUnitarios = new List<CustoUnitario>();

            foreach (TicketDeInsumoExcel ticketDeInsumo in ticketsInsumo)
            {
                if (!CustosUnitarios.Any(t =>  t.Setor.NomeSetor == ticketDeInsumo.setor && t.SubSetor.NomeSetor == ticketDeInsumo.subSetor))
                    CustosUnitarios.AddRange(Insumos.Where(i=> i.Departamento.Nome == ticketDeInsumo.Departamento).SelectMany(u=> u.CustosUnitarios).ToList());

                Especificacao especificacaoTicket =
                    FabricaDeEspeficicacaoCustoUnitario.ObterEspeficicacao(ticketDeInsumo, CustosUnitarios);

                if (especificacaoTicket.IsSatisfiedBy(carga))
                {
                    CustoUnitario custoUnitario =
                        CustosUnitarios.FirstOrDefault(t => t.Setor.NomeSetor == ticketDeInsumo.setor && t.SubSetor.NomeSetor == ticketDeInsumo.subSetor);
                    ticketDeInsumo.SetorHospitalar = custoUnitario.Setor;
                    ticketDeInsumo.SubSetorHospital = custoUnitario.SubSetor;
                    ticketDeInsumo.CustoUnitario = custoUnitario;
                }
            }
        }

        private void ValidarInsumos()
        {
            var insumos = new Insumos();

            Insumos = new List<Insumo>();

            foreach (TicketDeInsumoExcel ticketDeInsumo in ticketsInsumo)
            {
                if (!Insumos.Any(t => t.Departamento.Nome == ticketDeInsumo.Departamento))
                    Insumos.Add(insumos.ObterInsumo(ticketDeInsumo.DepartamentoEntidade));

                Especificacao especificacaoTicket =
                    FabricaDeEspeficicacaoInsumo.ObterEspeficicacao(ticketDeInsumo, Insumos);

                if (especificacaoTicket.IsSatisfiedBy(carga))
                {
                    Insumo insumo =
                        Insumos.FirstOrDefault(t => t.Departamento.Nome == ticketDeInsumo.Departamento);
                    ticketDeInsumo.Insumo = insumo;
                }
            }
        }

        #region Departamentos

        public void ValidaDepartamentos()
        {
            foreach (TicketDeInsumoExcel ticketProducaoExcel in ticketsInsumo)
            {
                if (!Departamentos.Any(d => d.Nome == ticketProducaoExcel.Departamento))
                    AdicionarDepartamento(carga, ticketProducaoExcel);
                else
                    ticketProducaoExcel.DepartamentoEntidade =
                        Departamentos.FirstOrDefault(d => d.Nome == ticketProducaoExcel.Departamento);
            }
        }

        private void AdicionarDepartamento(Carga carga, TicketDeInsumoExcel ticketDeProducaoExcel)
        {
            Departamento departamento = Departamentos.FirstOrDefault(p => p.Nome == ticketDeProducaoExcel.Departamento);

            Especificacao espeficicacaoDepartamento = FabricaDeEspeficicacaoDepartamento.ObterEspeficicacao(
                ticketDeProducaoExcel, departamento);

            if (espeficicacaoDepartamento.IsSatisfiedBy(carga))
                ticketDeProducaoExcel.DepartamentoEntidade = departamento;
        }

        #endregion
    }
}
