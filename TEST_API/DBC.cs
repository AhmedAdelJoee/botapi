using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TEST_API.Models;

namespace TEST_API
{
    public class DBC : DbContext
    {
        public DBC()
            : base("Name=DataConnection")
        {
            Configuration.LazyLoadingEnabled = false;

            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = 380;
        }

        public DbSet<AxaProvider> axaProviders { get; set; }
        public DbSet<UserQuery> userQueries { get; set; }
    }
}