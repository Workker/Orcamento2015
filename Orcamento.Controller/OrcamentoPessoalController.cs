using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DTO.OrcamentoPessoal;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain.Servico.Pessoal;

namespace Orcamento.Controller
{

    public class OrcamentoPessoalController
    {
        #region Atributos

        private IDepartamentos departamentos;
        private ICentrosDeCusto centrosDeCusto;
        private ServicoObterTotalizadorDePessoal servico;
        #endregion

        #region Propriedades

        public IDepartamentos Departamentos
        {
            get { return departamentos ?? (departamentos = new Departamentos()); }
            set { departamentos = value; }
        }

        public ICentrosDeCusto CentrosDeCusto
        {
            get { return centrosDeCusto ?? (centrosDeCusto = new CentrosDeCusto()); }
            set { centrosDeCusto = value; }
        }

        public ServicoObterTotalizadorDePessoal ServicoObterTotalizadorDePessoal
        {
            get { return servico ?? (servico = new ServicoObterTotalizadorDePessoal()); }
            set { servico = value; }
        }

        #endregion

        #region Métodos

        public Departamento ObterDepartamentoPor(int id)
        {
            return Departamentos.Obter(id);
        }

        public IOrderedEnumerable<CentroDeCusto> ObterCentrosDeCustoPor(Departamento departamento)
        {
            return departamento.CentrosDeCusto.Where(f => f.Funcionarios != null
                && f.Funcionarios.Count > 0
                && f.Funcionarios.Where(fu => fu.Departamento.Id == departamento.Id) != null
                && f.Funcionarios.Where(fu => fu.Departamento.Id == departamento.Id).Count() > 0).OrderBy(x => x.Nome);
        }

        public void Salvar(Domain.Orcamento orcamento)
        {
            var orcamentos = new Orcamentos();

            orcamentos.Salvar(orcamento);
        }

        public IList<OrcamentoPessoalDTO> ObterTotalizadorDePessoalPor(int centroDeCustoId, Departamento departamento)
{
            var centroDeCusto = CentrosDeCusto.Obter<CentroDeCusto>(centroDeCustoId);
            var servicoCalculaPessoal = new ServicoGerarOrcamentoPessoalPorCentroDeCusto();
            servicoCalculaPessoal.CentroDeCusto = centroDeCusto;
            servicoCalculaPessoal.Departamento = departamento;

            servicoCalculaPessoal.Gerar("");
            return ServicoObterTotalizadorDePessoal.Obter(servicoCalculaPessoal.Orcamento);
        }

        public CentroDeCusto ObterCentroDeCustoPor(int centroDeCustoId)
        {
            return CentrosDeCusto.Obter<CentroDeCusto>(centroDeCustoId);
        }

        #endregion
    }
}
