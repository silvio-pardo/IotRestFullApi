﻿using IotRestFullApi.Dal;
using IotRestFullApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Repositories
{
    public class CommandRepository
    {
        private readonly IotContext iotContext;
        public CommandRepository(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public Command Get(int key)
        {
            if (iotContext == null)
                return null;
            Command foundValue = iotContext.Command.Where(_ => _.Id == key).FirstOrDefault();
            return foundValue;
        }
        public IList<Command> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<Command> foundValue = iotContext.Command.Select(_ => new Command() { Id = _.Id, Payload = _.Payload, Device = _.Device, Uid = _.Uid, Time = _.Time, Type = _.Type }).ToList();
            return foundValue;
        }
        public bool Insert(Command data)
        {
            return true;
        }
    }
}