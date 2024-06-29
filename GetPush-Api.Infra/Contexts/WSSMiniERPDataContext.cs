using GetPush_Api.Shared;
using System.Data.Entity;

namespace GetPush_Api.Infra.Contexts
{
    public class ProjetoAPICRMDataContext : DbContext
    {
        public ProjetoAPICRMDataContext() : base(Runtime.ConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
    }
}
