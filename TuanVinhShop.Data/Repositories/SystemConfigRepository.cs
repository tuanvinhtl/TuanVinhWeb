using TeduShop.Data.Infrastructure;
using TuanVinhShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface ISystemConfigRepository:IRepository<SystemConfig>
    {

    }
    public class SystemConfigRepository : RepositoryBase<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }

}
