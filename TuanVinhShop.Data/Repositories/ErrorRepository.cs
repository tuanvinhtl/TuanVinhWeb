using TeduShop.Data.Infrastructure;
using TuanVinhShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {

    }
    public class ErrorRepository:RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
