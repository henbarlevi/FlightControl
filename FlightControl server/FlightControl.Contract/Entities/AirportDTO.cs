using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities
{
    /// <summary>
    /// represent the airport points and the planes in them
    /// </summary>
    [DataContract]
    public class AirportDTO
    {
        /// <summary>
        /// contain all the planes in the air port
        /// </summary>
        [DataMember]
        public List<PlaneDTO> Planes{ get; set; }
       
    }
}
