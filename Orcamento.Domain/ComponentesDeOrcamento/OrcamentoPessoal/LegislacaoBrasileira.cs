using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Aumentado;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal
{
    public class LegislacaoBrasileira
    {
        public static DespesaPessoal ObterDespesasParaO(Funcionario funcionario, Conta conta,NovoOrcamentoPessoal orcamento)
        {
            if (funcionario.Demitido)
                return ObterDespesaDeFuncionarioDemitido(conta, funcionario,orcamento);

            return funcionario.Aumentado ? ObterDespesaDeFuncionarioAumentado(conta, funcionario,orcamento) : ObterDespesaDeFuncionarioSemAumento(conta,orcamento);
        }

        private static DespesaPessoal ObterDespesaDeFuncionarioDemitido(Conta conta, Funcionario funcionario,NovoOrcamentoPessoal orcamento)
        {
            switch (conta.TipoConta.TipoContaEnum)
            {
                case TipoContaEnum.FGTS:
                    return new FgtsRecisao(funcionario.MesDeDemissao, conta, orcamento);
                case TipoContaEnum.DecimoTerceiro:
                    return new DecimoTerceiroRecisao(funcionario.MesDeDemissao, conta, orcamento);
                case TipoContaEnum.Ferias:
                    return new FeriasRecisao(funcionario.MesDeDemissao, conta, orcamento);
                case TipoContaEnum.Indenizacao:
                    return new Recisao(funcionario.MesDeDemissao, conta, orcamento);
                case TipoContaEnum.Salario:
                    return new SalarioRecisao(funcionario.MesDeDemissao,conta, orcamento);
                case TipoContaEnum.INSS:
                    return new INSSRecisao(funcionario.MesDeDemissao, conta, orcamento);
                case TipoContaEnum.Extras:
                    return new Extras(conta,orcamento,funcionario.MesDeDemissao);
                default:
                    return null;
            }
        }

        private static DespesaPessoal ObterDespesaDeFuncionarioAumentado(Conta conta, Funcionario funcionario, NovoOrcamentoPessoal orcamento)
        {
            switch (conta.TipoConta.TipoContaEnum)
            {
                case TipoContaEnum.FGTS:
                    return new FGTSComAumento(funcionario.Aumento, funcionario.MesDeAumento, conta, orcamento);
                case TipoContaEnum.DecimoTerceiro:
                    return new DecimoTerceiroComAumento(funcionario.Aumento, funcionario.MesDeAumento, conta, orcamento);
                case TipoContaEnum.Ferias:
                    return new FeriasComAumento(funcionario.Aumento, funcionario.MesDeAumento, conta, orcamento);
                case TipoContaEnum.Salario:
                    return new SalarioComAumento(funcionario.Aumento, funcionario.MesDeAumento, conta, orcamento);
                case TipoContaEnum.INSS:
                    return new INSSComAumento(funcionario.Aumento, funcionario.MesDeAumento, conta, orcamento);
                case TipoContaEnum.Extras:
                    return new Extras(conta,orcamento);
                default:
                    return null;
            }
        }

        private static DespesaPessoal ObterDespesaDeFuncionarioSemAumento(Conta conta,NovoOrcamentoPessoal orcamento)
        {
            switch (conta.TipoConta.TipoContaEnum)
            {
                case TipoContaEnum.FGTS:
                    return new FGTS(conta,orcamento);
                case TipoContaEnum.DecimoTerceiro:
                    return new DecimoTerceiro(conta, orcamento);
                case TipoContaEnum.Ferias:
                    return new Ferias(conta, orcamento);
                case TipoContaEnum.Salario:
                    return new Salario(conta, orcamento);
                case TipoContaEnum.INSS:
                    return new INSS(conta, orcamento);
                case TipoContaEnum.Extras:
                    return new Extras(conta,orcamento);
                default:
                    return null;
            }
        }
    }
}