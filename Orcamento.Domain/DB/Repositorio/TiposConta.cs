
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public class TiposConta : BaseRepository
    {
        public void Adicionar(TipoConta tipoConta)
        {
            Session.Save(tipoConta);
        }
    }
}
