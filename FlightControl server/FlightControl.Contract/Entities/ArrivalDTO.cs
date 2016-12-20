using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities
{
    /// <summary>
    /// describe arrival of plane.
    /// </summary>
    [DataContract]
    public class ArrivalDTO
    {
        [DataMember]
        public int ArrivalId { get; set; }
        [DataMember]
        public int PlaneNumber { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
    }
}
