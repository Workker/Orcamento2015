using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspeficicacaoFuncionarios
{
    public class MotorDeValidacaoDeFuncionario
    {
        public virtual CentrosDeCusto CentrosRepo { get; set; }
        public virtual Departamentos DepartamentosRepositorio { get; set; }
        public virtual List<Departamento> Departamentos { get; set; }
        public virtual CentrosDeCusto CentrosDeCustoRepo { get; set; }
        public virtual List<CentroDeCusto> CentrosDeCusto { get; set; }


        public void Validar(Carga carga, List<Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel> funcionarios)
        {
            ValidaCentrosDeCustos(carga,funcionarios);

            if (!carga.Ok())
                return;
           
            ValidaDepartamentos(carga, funcionarios);

            if (!carga.Ok())
                return;

            ValidaFuncionarios(funcionarios,carga);
        }

        public void ValidaCentrosDeCustos(Carga carga, List<Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel> funcionarios)
        {
            try
            {

            
            CentrosDeCustoRepo = new CentrosDeCusto();
            CentrosDeCusto = new List<CentroDeCusto>();

            foreach (var funcionarioExcel in funcionarios)
            {
                if (!CentrosDeCusto.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                {
                    AdicionarCentroDeCusto(carga, funcionarioExcel);
                }
                else
                {
                    funcionarioExcel.CentroDeCusto = CentrosDeCusto.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);

                }
            }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ValidaDepartamentos(Carga carga, List<Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel> funcionarios)
        {
            Departamentos = new List<Departamento>();
            DepartamentosRepositorio = new Departamentos();

            foreach (var funcionarioExcel in funcionarios)
            {
                if (!Departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                {
                    AdicionarDepartamento(carga, funcionarioExcel);
                }
                else
                {
                    funcionarioExcel.DepartamentoEntidade = Departamentos.FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);

                }
            }
        }

        private void AdicionarDepartamento(Carga carga, Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionarioExcel)
        {
            var departamento = DepartamentosRepositorio.ObterPor(funcionarioExcel.Departamento);

            var espeficicacaoDepartamento = FabricaDeEspeficicacaoDepartamento.ObterEspeficicacao(funcionarioExcel, departamento);

            if (espeficicacaoDepartamento.IsSatisfiedBy(carga))
            {
                Departamentos.Add(departamento);
                funcionarioExcel.DepartamentoEntidade = departamento;
            }
        }

        private void AdicionarCentroDeCusto(Carga carga, Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionarioExcel)
        {
            try
            {

            
            var centro = CentrosDeCustoRepo.ObterPor(funcionarioExcel.CodigoCentroDeCusto);

            var espeficicacaoCentro = FabricaDeEspeficicacaoCentroDeCusto.ObterEspeficicacao(funcionarioExcel, centro);

            if (espeficicacaoCentro.IsSatisfiedBy(carga))
            {
                CentrosDeCusto.Add(centro);
                funcionarioExcel.CentroDeCusto = centro;
            }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void ValidaFuncionarios(List<Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel> funcionarios, Carga carga)
        {
            foreach (var funcionarioExcel in funcionarios)
            {
                  var matriculaRepetida =
                    CentrosDeCusto.FirstOrDefault(c => c.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto).
                        Funcionarios.Any(f => f.Matricula == funcionarioExcel.NumeroMatricula);

                var especificacaoFuncionario = FabricaDeEspeficicacaoFuncionario.ObterEspeficicacao(funcionarioExcel,matriculaRepetida);
                especificacaoFuncionario.IsSatisfiedBy(carga);                    
            }
        }
    }
}
