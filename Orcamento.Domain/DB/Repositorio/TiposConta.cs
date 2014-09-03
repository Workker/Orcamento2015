
using NHibernate.Criterion;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public class TiposConta : BaseRepository
    {
        public void Adicionar(TipoConta tipoConta)
        {
            Session.SaveOrUpdate(tipoConta);
        }

        public TipoConta ObterPor(int id)
        {
            return Session.QueryOver<TipoConta>().Where(c => c.Id == id).SingleOrDefault();
        }

        public TipoConta ObterPor(string nome)
        {
            var criterio = Session.CreateCriteria<TipoConta>();
            criterio.Add(Expression.Eq("Nome", nome));

            return criterio.UniqueResult<TipoConta>();
        }

       
    }
}
