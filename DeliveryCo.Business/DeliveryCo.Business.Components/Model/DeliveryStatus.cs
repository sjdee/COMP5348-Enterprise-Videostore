using Common;
using DeliveryCo.Business.Entities;

namespace DeliveryCo.Business.Components.Model
{
    public class DeliveryStatus : IVisitable
    {
        public string Topic => "DeliveryStatus";
        public DeliveryInfo DeliveryInfo { get; set; }
        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}