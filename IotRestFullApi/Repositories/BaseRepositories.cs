using IotRestFullApi.Dal;
using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IotRestFullApi.Repositories
{
    public class BaseRepositories<T> where T : BaseEntities
    {
        protected readonly IotContext iotContext;
        protected DbSet<T> DbSet { get { return iotContext.Set<T>(); } }
        public BaseRepositories(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public virtual T Single(int id)
        {
            return DbSet.Find(id);
        }
        public virtual T Insert(T data)
        {
            iotContext.Add(data);
            iotContext.SaveChanges();
            return data;
        }
        public virtual T Modify(T data)
        {
            iotContext.Update(data);
            iotContext.SaveChanges();
            return data;
        }
        public virtual bool Delete(T data)
        {
            iotContext.Remove(data);
            iotContext.SaveChanges();
            return true;
        }
    }
}
