using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities.SimulatorServiceEntities
{
    /// <summary>
    /// sending request to simulator about the parked plane.
    /// the simulator will now know that this plane get randomizly departure
    /// </summary>
    [DataContract]
    public class OnPlaneParkedRequest
    {
        [DataMember]
        public PlaneDTO ParkedPlane { get; set; }
    }
}
