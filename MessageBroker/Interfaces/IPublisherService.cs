﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common.Model;

namespace MessageBroker.Interfaces
{
    [ServiceContract]
    public interface IPublisherService
    {
        [OperationContract(IsOneWay = true)]
        void Publish(Message pMessage);
    }
}
