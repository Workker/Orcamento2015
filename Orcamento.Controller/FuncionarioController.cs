using System.Collections.Generic;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Mappers;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain.Servico.Pessoal;

namespace Orcamento.Controller
{

    public class FuncionarioController
    {
        #region Atributos

        private IFuncionarios _funcionarios;
        private IDepartamentos _departamentos;
        private ICentrosDeCusto _centrosDeCusto;
        private ServicoGerarOrcamentoPessoalPorCentroDeCusto _servicoGerarOrcamentoPessoalPor;

        #endregion

        #region Propriedades

        public IDepartamentos Departamentos
        {
            get { return _departamentos ?? (_departamentos = new Departamentos()); }
            set { _departamentos = value; }
        }

        public ICentrosDeCusto CentrosDeCusto
        {
            get { return _centrosDeCusto ?? (_centrosDeCusto = new CentrosDeCusto()); }
            set { _centrosDeCusto = value; }
        }

        public IFuncionarios Funcionarios
        {
            get { return _funcionarios ?? (_funcionarios = new Funcionarios()); }
            set { _funcionarios = value; }
        }

        public ServicoGerarOrcamentoPessoalPorCentroDeCusto ServicoGerarOrcamentoPessoalPor
        {
            get { return _servicoGerarOrcamentoPessoalPor ?? (_servicoGerarOrcamentoPessoalPor = new ServicoGerarOrcamentoPessoalPorCentroDeCusto()); }
            set { _servicoGerarOrcamentoPessoalPor = value; }
        }

        public Departamento Departamento { get; set; }
        public string CodigoDoCentroDeCusto { get; set; }

        #endregion

        #region Métodos

        public void SalvarFuncionario(Funcionario funcionario)
        {
            CentroDeCusto centroDeCusto = ObterCentroDeCustoPor(this.CodigoDoCentroDeCusto);

            centroDeCusto.Adicionar(funcionario);

            SalvarCentroDeCusto(centroDeCusto);
        }

        public IList<CentroDeCusto> ObterCentrosDeCustoPor(int departamentoId)
        {
            return ObterDepartamentoPor(departamentoId).CentrosDeCusto;
        }

        public Departamento ObterDepartamentoPor(int id)
        {
            return Departamentos.Obter(id);
        }

        public Funcionario ObterFuncionarioPor(int id)
        {
            return Funcionarios.Obter<Funcionario>(id);
        }

        public List<FuncionarioDTO> ObterTodosFuncionariosPor(int departamentoId, string codigoDeCentroDeCusto)
        {
            return Funcionarios.ObterTodos(departamentoId, codigoDeCentroDeCusto);
        }

        public void Salvar(Funcionario funcionario)
        {
            Funcionarios.Salvar(funcionario);
        }

        public NovoOrcamentoPessoal ObterOrcamento(int centroDeCustoid, int departamentoID)
        {
            NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();

            return orcamentos.ObterPor(departamentoID, centroDeCustoid);
        }

        public void SalvarOrcamento(string justificativa)
        {
            var centroDeCusto = CentrosDeCusto.ObterPor(CodigoDoCentroDeCusto);
            ServicoGerarOrcamentoPessoalPor.CentroDeCusto = centroDeCusto;
            ServicoGerarOrcamentoPessoalPor.Departamento = Departamento;

            ServicoGerarOrcamentoPessoalPor.Gerar(justificativa);

            //ControlesCentroDeCusto controles = new ControlesCentroDeCusto();
            //var controle = controles.ObterPor(Departamento, centroDeCusto);
            //controle.Salvo = true;
            //controles.Salvar(controle);
        }

        public CentroDeCusto ObterCentroDeCustoPor(string codigoDoCentroDeCusto)
        {
            return CentrosDeCusto.ObterPor(codigoDoCentroDeCusto);
        }

        public void SalvarCentroDeCusto(CentroDeCusto centroDeCusto)
        {
            CentrosDeCusto.Salvar(centroDeCusto);
        }

        #endregion
    }
}
