using System.Collections.Generic;
using System.Linq;
using IotRestFullApi.Dal;
using IotRestFullApi.Entities;
namespace IotRestFullApi.Repositories
{
    public class ActionRepository
    {
        private readonly IotContext iotContext;
        public ActionRepository(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public Action Get(int key)
        {
            if (iotContext == null)
                return null;
            Action foundValue = iotContext.Action.Where(_ => _.Id == key).FirstOrDefault();
            return foundValue;
        }
        public IList<Action> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<Action> foundValue = iotContext.Action.Select(_ => new Action() { Id = _.Id, Status = _.Status, Payload = _.Payload, Device = _.Device, Uid = _.Uid}).ToList();
            return foundValue;
        }
        public bool Insert(Action data)
        {
            return true;
        }
    }
}
