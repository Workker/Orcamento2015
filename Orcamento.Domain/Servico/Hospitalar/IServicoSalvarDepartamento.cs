using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.Servico.Hospitalar
{
    public interface IServicoSalvarDepartamento
    {
        void Salvar(Departamento departamento);
    }
}
