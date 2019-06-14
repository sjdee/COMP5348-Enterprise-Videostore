using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Common.Model
{
    public class DeliverySubmittedMessage : Message
    {
        [DataMember]
        public Guid OrderNumber { get; set; }

        [DataMember]
        public Guid DeliveryId { get; set; }

    }
}
