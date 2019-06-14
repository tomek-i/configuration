using System.Linq;
using System.Data.Entity;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Interfaces;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace Configuration.SQL
{
   
  public class SQLConfigStorage : SQLStorage<ConfigurationContext>
    {
        //private new ConfigurationContext _context => (ConfigurationContext)base._context;

        public SQLConfigStorage(ConfigurationContext context, ConfigMode mode):base(context,mode)
        {
           // _context = context;
            
        }
     
        public new void Set<T>(T instance) where T:SQLAppConfig
        {
            base.Set(instance);
            //if (!_context.Set<T>().Any(x => x.Code == instance.Code))
            //{
            //    _context.Set<T>().Add(instance);
            //}
            //else
            //{
            //    // no need, if you update the instance and call set with the instance, it will update
            //}
            //_context.SaveChanges();
        }
        public new T Get<T>(string code) where T : SQLAppConfig
        {
            return base.Get<T>(code,x=>x.Settings);
            //var instance = _context.Set<SQLAppConfig>().Include(x => x.Settings).Where(x => x.Code == code).SingleOrDefault();
            //if (instance == null)
            //{
            //    instance = new SQLAppConfig(code) { Code = code };
            //    _context.Set<SQLAppConfig>().Add(instance);
            //}
            //return (T)instance;
        }
       
    }
    public class SQLStorage<TContext> : IConfigStorage  where TContext : DbContext
    {
        protected TContext _context;

        public SQLStorage(TContext context,ConfigMode mode)
        {
            _context = context;
        }
       
        public void Set<T>(T instance) where T : class, IConfiguration
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
        public T Get<T>(string name, Expression<Func<T, object>> include) where T : class, IConfiguration
        {
            return  _context.Set<T>().Include(include).Where(x => x.Code == name).SingleOrDefault();
        }
        public T Get<T>(string name) where T : class,IConfiguration
        {
            var instance = _context.Set<T>().Where(x => x.Code == name).SingleOrDefault();
            return instance;
        }

    
       
    }
}
