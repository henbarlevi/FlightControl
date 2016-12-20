using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities.SimulatorServiceEntities
{
    [DataContract]
    public class SimulationRequest
    {
        [DataMember]
        public PlaneDTO SimulatedPlane { get; set; }
    }
}
