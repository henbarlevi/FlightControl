﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities.FHServiceEntities
{
    [DataContract]
    public class GetArrivalsResponse : BaseResponse
    {
        [DataMember]
        public List<ArrivalDTO> Arrivals { get; set; }
    }
}