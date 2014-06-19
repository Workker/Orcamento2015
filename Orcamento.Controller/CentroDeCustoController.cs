using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico.Hospitalar;
using Orcamento.Domain;
using Orcamento.Domain.Servico.OutrasDespesas;

namespace Orcamento.Controller
{
    public class CentroDeCustoController
    {
        private ICentrosDeCusto centrosDeCusto;
        private IContas contas;
        private IHospitais hospitais;
        private IDepartamentos departamentos;

        public ICentrosDeCusto CentrosDeCusto
        {
            private get { return centrosDeCusto ?? (centrosDeCusto = new CentrosDeCusto()); }
            set { centrosDeCusto = value; }
        }

        public IContas Contas
        {
            private get { return contas ?? (contas = new Contas()); }
            set { contas = value; }
        }

        public IHospitais Hospitais
        {
            private get { return hospitais ?? (hospitais = new Hospitais()); }
            set { hospitais = value; }
        }

        public IDepartamentos Departamentos
        {
            private get { return departamentos ?? (departamentos = new Departamentos()); }
            set { departamentos = value; }
        }

        public IList<CentroDeCusto> BuscarTodosOsCentrosDeCusto()
        {
            return CentrosDeCusto.Todos<CentroDeCusto>();
        }

        public IList<Conta> BuscarTodasAsContasDeUmCentroDeCusto(string codigo)
        {
            return CentrosDeCusto.ObterPor(codigo).Contas;
        }

        public IList<Conta> BuscarTodasAsContasDeUmCentroDeCusto(int id)
        {
            return CentrosDeCusto.Obter<CentroDeCusto>(id).Contas;
        }

        public CentroDeCusto BuscarCentroDeCustoPor(int id)
        {
            return CentrosDeCusto.Obter<CentroDeCusto>(id);
        }

        public CentroDeCusto BuscarCentroDeCustoPor(string codigo)
        {
            return CentrosDeCusto.ObterPor(codigo);
        }

        public void Salvar(CentroDeCusto centroDeCusto)
        {
            CentrosDeCusto.Salvar(centroDeCusto);
            ServicoAtualizarDespesasOperacionais servico = new ServicoAtualizarDespesasOperacionais();

            servico.AtualizarDespesas(centroDeCusto);
        }

        public IList<Conta> BuscarTodosAsContas()
        {
            return Contas.Todos();
        }

        public Conta BuscarContaPor(string codigo)
        {
            return Contas.ObterContaPor(codigo);
        }

        public Conta BuscarContaPor(int id)
        {
            return Contas.ObterContaPor(id);
        }

        public IList<Hospital> BuscarTodosOsHospitais()
        {
            return Hospitais.Todos();
        }

        public IList<CentroDeCusto> BuscarTodosOsCentrosDeCustoDeUmDepartamento(int id)
        {
            return Departamentos.Obter(id).CentrosDeCusto;
        }

        public Departamento BuscarDepartamento(int id)
        {
            return Departamentos.Obter(id);
        }
    }
}
