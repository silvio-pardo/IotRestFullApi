using IotRestFullApi.Dal;

namespace IotRestFullApi.Repositories
{
    public class BaseRepositories<T>
    {
        protected readonly IotContext iotContext;
        public BaseRepositories(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public T Insert(T data)
        {
            iotContext.Add(data);
            iotContext.SaveChanges();
            return data;
        }
        public T Modify(T data)
        {
            iotContext.Update(data);
            iotContext.SaveChanges();
            return data;
        }
        public bool Delete(T data)
        {
            iotContext.Remove(data);
            iotContext.SaveChanges();
            return true;
        }
    }
}
