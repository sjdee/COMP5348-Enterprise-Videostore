﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class FundsTransferErrorMessage : Message
    {
        [DataMember]
        public Exception Error { get; set; }

        [DataMember]
        public Guid OrderGuid { get; set; }
    }
}
