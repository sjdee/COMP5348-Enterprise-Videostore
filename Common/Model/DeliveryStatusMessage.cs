using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class DeliveryStatusMessage : Message
    {
        [DataMember]
        public String SourceAddress { get; set; }
        [DataMember]
        public String DestinationAddress { get; set; }
        [DataMember]
        public String OrderNumber { get; set; }
        [DataMember]
        public Guid DeliveryIdentifier { get; set; }
        [DataMember]
        public String DeliveryNotificationAddress { get; set; }
        [DataMember]
        public Int32 Status { get; set; }
    }
}
