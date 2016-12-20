using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities.FHServiceEntities
{
    [DataContract]
    public class GetDeparturesResponse : BaseResponse
    {
        [DataMember]
        public List<DepartureDTO> Departures { get; set; }
    }
}
