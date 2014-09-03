using System.Linq;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Beneficios;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;

namespace Orcamento.Domain.Servico.Pessoal
{
    public class ServicoGerarOrcamentoPessoalPorCentroDeCusto
    {
        #region Atributos

      //  private IDespesasPessoais despesasPessoais;
        private INovosOrcamentosPessoais _novosOrcamentosPessoais;

        #endregion

        #region Propriedades

        //public IDespesasPessoais DespesasPessoais
        //{
        //    get { return despesasPessoais ?? (despesasPessoais = new DespesasPessoais()); }
        //    set { despesasPessoais = value; }
        //}

        public Departamento Departamento { get; set; }

        public CentroDeCusto CentroDeCusto { get; set; }
        public NovoOrcamentoPessoal Orcamento { get; set; }

        public INovosOrcamentosPessoais NovosOrcamentosPessoais
        {
            get { return _novosOrcamentosPessoais ?? (_novosOrcamentosPessoais = new NovosOrcamentosPessoais()); }
            set { _novosOrcamentosPessoais = value; }
        }

        #endregion

        #region Métodos

        public virtual void Gerar(string justificativa)
        {
            InicializarOrcamento(justificativa);

            CalcularDespesas();

            NovosOrcamentosPessoais.Salvar(Orcamento);
        }

        private void InicializarOrcamento(string justificativa)
        {
            Orcamento = NovosOrcamentosPessoais.ObterPor(Departamento.Id, CentroDeCusto.Id);

            if (Orcamento == null)
                ConstruirOrcamentoComOsDadosBasicos(justificativa);
            else
            {
                if (!string.IsNullOrEmpty(justificativa))
                    Orcamento.Justificativa = justificativa;

                NovosOrcamentosPessoais.Salvar(Orcamento);
            }
        }

        private void ConstruirOrcamentoComOsDadosBasicos(string justificativa)
        {
            Orcamento = new NovoOrcamentoPessoal(Departamento, CentroDeCusto, 2015);
            Orcamento.Justificativa = justificativa;

            var tickets = new TicketsDeOrcamentoPessoal().Todos(Departamento);

            foreach (var ticketDeOrcamentoPessoal in tickets)
                Orcamento.Adicionar(ticketDeOrcamentoPessoal);        
        }

        private void CalcularDespesas()
        {
            AcordosDeConvencao acordos = new AcordosDeConvencao();
            var acordo = acordos.ObterAcordoDeConvencao(Departamento);

            foreach (var grupoDeConta in CentroDeCusto.GrupoDeContas)
            {
                if (grupoDeConta.Nome == "Benefícios")
                {
                    foreach (var conta in grupoDeConta.Contas.Where(conta => conta.TipoConta.TipoContaEnum == TipoContaEnum.Beneficios))
                    {
                        CalcularBeneficios(conta, Orcamento);
                    }
                }
                else
                {
                    foreach (var funcionario in CentroDeCusto.Funcionarios.Where(c => c.Departamento.Id == Departamento.Id))
                        foreach (var conta in grupoDeConta.Contas)
                        {
                            if (conta.TipoConta.TipoContaEnum != TipoContaEnum.Beneficios)
                            {
                                funcionario.CalcularDespesa(CentroDeCusto, conta, Orcamento, funcionario.DataAdmissao, acordo.Porcentagem, acordo.MesAumento);
                            }
                        }

                }
            }
        }

        private void CalcularBeneficios(Conta conta, NovoOrcamentoPessoal orcamento)
        {
            var headCount = CentroDeCusto.Funcionarios.Where(c => c.Departamento.Id == Departamento.Id).ToList().Count;

            foreach (var funcionario in CentroDeCusto.Funcionarios.Where(f => f.Departamento.Id == Departamento.Id))
            {
                Beneficios beneficio = new Beneficios(conta, orcamento);
                beneficio.Calcular(headCount, funcionario);
            }
        }

        #endregion
    }
}