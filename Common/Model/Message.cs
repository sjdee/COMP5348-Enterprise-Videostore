using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;



namespace Common.Model
{
    [DataContract]
    [KnownType(typeof(RequestFundsTransferMessage))]
    [KnownType(typeof(FundsTransferSuccessfulMessage))]
    [KnownType(typeof(FundsTransferErrorMessage))]
    [KnownType(typeof(DeliveryStatusMessage))]
    [KnownType(typeof(DeliveryInfoMessage))]
    [KnownType(typeof(DeliverySubmittedMessage))]
    [KnownType(typeof(SendEmailMessage))]

    public abstract class Message : IVisitable
    {
        [DataMember]
        public String Topic { get; set; }


        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
