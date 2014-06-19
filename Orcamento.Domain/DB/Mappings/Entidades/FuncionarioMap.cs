using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class FuncionarioMap : ClassMap<Funcionario>
    {
        public FuncionarioMap()
        {
            Id(f => f.Id);
            Map(f => f.AnoAdmissao);
            Map(f => f.DataSaidaFerias);
            Map(f => f.DataAdmissao);
            Map(f => f.Salario);
            Map(f => f.Matricula);
            Map(f => f.CargaHoraria);
            Map(f => f.Demitido);
            Map(f => f.MesDeDemissao);
            Map(f => f.Aumento);
            Map(f => f.Aumentado);
            Map(f => f.MesDeAumento);
            Map(f => f.Nome);
            //HasMany(f => f.Despesas)
            //    .Cascade
            //    .All();
            References(d => d.Departamento);
            Map(f => f.Cargo);
            Map(f => f.InicialNumeroMatricula);
            Map(f => f.NumeroDeVaga);
            Map(f => f.FuncionarioReposicao);
        }
    }
}
