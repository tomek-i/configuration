using System.Data.Entity.Infrastructure;

namespace Configuration.SQL
{
    public class WarburnEstateDatabaseFactory : IDbContextFactory<ConfigurationContext>
    {
        private const string DatabaseNameDevelopment = "Development";
        private const string DatabaseNameLive = "WarburnEstate";

        #region Implementation of IDbContextFactory<out WarburnDatabaseContext>

        public ConfigurationContext Create()
        {
            string connection = null;
#if DEBUG
            connection = $@"Data Source=BRITTANY;Initial Catalog={DatabaseNameDevelopment};Persist Security Info=True;Trusted_Connection=True";
#else
            connection = $@"Data Source=BRITTANY;Initial Catalog={DatabaseNameLive};Persist Security Info=True;USer ID=warburn;Password=warburn";
#endif
            return Create(connection);
        }

        public static ConfigurationContext Create(string nameOrConnectionString)
        {
            if (string.IsNullOrWhiteSpace(nameOrConnectionString))
            {
                return new WarburnEstateDatabaseFactory().Create();
            }
            else
            {
                return new ConfigurationContext(nameOrConnectionString);
            }
        }



        #endregion

    }
}
