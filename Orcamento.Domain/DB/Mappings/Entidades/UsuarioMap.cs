using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class UsuarioMap: ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
            Map(x => x.Login);
            Map(x => x.Senha);
            References(x => x.TipoUsuario).Not.Nullable();
            HasManyToMany(x => x.Departamentos)
                .Cascade.None();
        }
    }
}
