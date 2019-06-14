using System.Linq;
using System.Data.Entity;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Interfaces;

namespace Configuration.SQL
{
   
  public class SQLConfigStorage : SQLStorage
    {
        private ConfigurationContext _context;

        public SQLConfigStorage(ConfigurationContext context, ConfigMode mode):base(context,mode)
        {
            _context = context;
            
        }
        public new void Set<T>(T instance) where T:SQLAppConfig
        {
            if (!_context.Set<T>().Any(x => x.Code == instance.Code))
            {
                _context.Set<T>().Add(instance);
            }
            else
            {
                // no need, if you update the instance and call set with the instance, it will update
            }
            _context.SaveChanges();
        }
        public new T Get<T>(string code) where T : SQLAppConfig
        {
            var instance = _context.Set<SQLAppConfig>().Include(x => x.Settings).Where(x => x.Code == code).SingleOrDefault();
            if (instance == null)
            {
                instance = new SQLAppConfig(code) { Code = code };
                _context.Set<SQLAppConfig>().Add(instance);
            }
            return (T)instance;
        }
    }
    public class SQLStorage : IConfigStorage 
    {
        private DbContext _context;

        public SQLStorage(DbContext context,ConfigMode mode)
        {
            _context = context;
        }
       
        public void Set<T>(T instance) where T : class, IConfiguration
        {
            if (!_context.Set<T>().Any(x => x.Name == instance.Name))
            {
                _context.Set<T>().Add(instance);
            }
            else
            {
                // no need, if you update the instance and call set with the instance, it will update
            }
            _context.SaveChanges();
        }
       
        public T Get<T>(string name) where T : class,IConfiguration
        {
            var instance = _context.Set<T>().Where(x => x.Name == name).SingleOrDefault();
            return instance;
            return (T)(IConfiguration)instance;
        }

    
       
    }
}
