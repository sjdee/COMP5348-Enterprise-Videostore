﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BookStore.Business.Entities.Model
{
    public class DeliveryInfoItem : IVisitable
    {
        public string Topic => "DeliveryRequest";
        public String SourceAddress { get; set; }
        public String DestinationAddress { get; set; }
        public Guid OrderNumber { get; set; }
        public Guid DeliveryIdentifier { get; set; }
        public String DeliveryNotificationAddress { get; set; }
        public Int32 Status { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}