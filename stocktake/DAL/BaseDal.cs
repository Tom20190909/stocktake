using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using stocktake.Entity;
using stocktake.DAL;
using System.Data.Common;

namespace stocktake
{
       public abstract partial class BaseDal<T>
        where T:class 
    {
        DbContext dbContext = new MyContext();
      
        public IQueryable<T> GetList()
        {
            return dbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>()
                .Where(GetByIdKey(id))
                .FirstOrDefault();
        }

        public int Add(T t)
        {
            dbContext.Set<T>().Add(t);
            return dbContext.SaveChanges();
        }

        public int Edit(T t)
        {
            dbContext.Set<T>().Attach(t);
            dbContext.Entry(t).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Remove(int id)
        {
            var t = GetById(id);
            dbContext.Set<T>().Remove(t);
            return dbContext.SaveChanges();
        }
        public int RemoveAll()
        {
            foreach(T t in GetList().AsQueryable())
            {
                dbContext.Set<T>().Remove(t);
            }
            return dbContext.SaveChanges();
        }

        public abstract Expression<Func<T, int>> GetKey();
        public abstract Expression<Func<T, bool>> GetByIdKey(int id);

        public int GetCount()
        {
            return dbContext.Set<T>().Count();
        }
    }
}
