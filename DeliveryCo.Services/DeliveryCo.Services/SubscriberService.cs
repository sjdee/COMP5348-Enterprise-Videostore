using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using DeliveryCo.Services.Transformations; 

namespace DeliveryCo.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Message pMessage)
        {
            if (pMessage is DeliveryInfoMessage)
            {
                var lMessage = pMessage as DeliveryInfoMessage;
                var lVisitor = new DeliveryInfoMessageToDeliveryInfo();
                var dService = new DeliveryService(); 
                lMessage.Accept(lVisitor);
                dService.SubmitDelivery(lVisitor.Result);
            }
        }
    }
}
