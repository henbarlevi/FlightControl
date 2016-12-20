using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities
{
    [DataContract]
    public class DepartureDTO
    {
        [DataMember]
        public int DepartureId { get; set; }
        [DataMember]
        public int PlaneNumber { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
    }
}
