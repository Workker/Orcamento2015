using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class TipoUsuarioMap : ClassMap<TipoUsuario>
    {
        public  TipoUsuarioMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
