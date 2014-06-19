using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico.Hospitalar;

namespace Orcamento.Controller
{
    public class DepartamentoController
    {
        private IDepartamentos departamentos;
        private IHospitais hospitais;
        private IServicoSalvarDepartamento servico;
        private ICentrosDeCusto centrosDeCusto;

        public IDepartamentos Departamentos
        {
            private get { return departamentos ?? (departamentos = new Departamentos()); }
            set { departamentos = value; }
        }

        public IHospitais Hospitais
        {
            private get { return hospitais ?? (hospitais = new Hospitais()); }
            set { hospitais = value; }
        }

        public IServicoSalvarDepartamento Servico
        {
            private get { return servico ?? (servico = new ServicoSalvarDepartamento()); }
            set { servico = value; }
        }

        public ICentrosDeCusto CentrosDeCusto
        {
            private get { return centrosDeCusto ?? (centrosDeCusto = new CentrosDeCusto()); }
            set { centrosDeCusto = value; }
        }

        public IList<Hospital> BuscarTodosOsHospitais()
        {
            return Hospitais.Todos();
        }

        public IList<Setor> BuscarTodosOsDepartamentos()
        {
            return Departamentos.Todos<Setor>();
        }

        public Hospital BuscarHospitalPor(int id)
        {
            return Hospitais.ObterPor(id);
        }

        public IList<CentroDeCusto> BuscarTodosOsCentrosDeCusto()
        {
            return CentrosDeCusto.Todos<CentroDeCusto>();
        }

        public IList<CentroDeCusto> BuscarTodosOsCentrosDeCustoDeUmDepartamento(int id)
        {
            return Departamentos.Obter(id).CentrosDeCusto;
        }

        public Departamento BuscarDepartamento(int id)
        {
            return Departamentos.Obter(id);
        }

        public void SalvarDepartamento(Departamento departamento)
        {
            Servico.Salvar(departamento);
        }

        public CentroDeCusto ObterCentroDeCustoPor(int centroDeCustoId)
        {
            return CentrosDeCusto.Obter<CentroDeCusto>(centroDeCustoId);
        }
    }
}