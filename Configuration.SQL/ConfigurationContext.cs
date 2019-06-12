using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Text;

namespace Configuration.SQL
{

    //[DbConfigurationType(typeof(WarburnEstateDatabaseConfiguration))]
    public class ConfigurationContext : DbContext
    {

        public virtual DbSet<SQLAppConfig> AppConfigs { get; set; }
        public virtual DbSet<SQLAppConfigSetting> AppConfigSettings { get; set; }

        public ConfigurationContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SQLAppConfigSetting>().HasKey(x => x.Id);
            modelBuilder.Entity<SQLAppConfigSetting>().Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SQLAppConfigSetting>().Property(x => x.Code).IsRequired();

            modelBuilder.Entity<SQLAppConfig>().HasKey(x => x.Id);
            modelBuilder.Entity<SQLAppConfig>().Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SQLAppConfig>().HasMany(x => x.Settings).WithRequired(x => x.Config).HasForeignKey(x => x.AppConfigId);
            modelBuilder.Entity<SQLAppConfig>().Property(x => x.Code).IsRequired();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <exception cref="DbUpdateException">Entity Update Failed - errors follow:\n +
        ///                     ex.InnerException?.InnerException</exception>
        /// <exception cref="DbEntityValidationException">Entity Validation Failed - errors follow:\n +
        ///                     sb.ToString()</exception>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(
                    "Entity Update Failed - errors follow:\n" +
                    ex.InnerException?.InnerException, ex
                    ); // Add the original exception as the innerException
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }


    }
}
