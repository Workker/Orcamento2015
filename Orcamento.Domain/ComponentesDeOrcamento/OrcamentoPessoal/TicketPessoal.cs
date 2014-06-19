using Orcamento.Domain.Gerenciamento;
namespace Orcamento.Domain
{
    public enum TipoTicketDePessoal : short
    {
        Alimentação = 1,
        AssistenciaMedica,
        AssistenciaOdontologica,
        OutrosBeneficios,
        TreinamentoPessoal,
        ValeDeTransporte,
        OutrasDespesas,
        AdicionalNoturno,
        AdicionalDeInsalubridade,
        AdicionaDePericulosidade,
        Gratificacoes,
        HorasExtras,
        AdicionalDeSobreaviso,
        Indenizacao,
        BolsaDeEstagio,
        FGTS,
        INSS
    }

    public class TicketDeOrcamentoPessoal : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Valor { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual TipoTicketDePessoal Ticket { get; set; }

        public TicketDeOrcamentoPessoal(Departamento departamento)
        {
            this.Departamento = departamento;
        }

        protected TicketDeOrcamentoPessoal() { }
    }
}
