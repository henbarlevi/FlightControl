using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Entities
{
    /// <summary>
    /// plane entity
    /// plane
    /// </summary>
    [DataContract]
    public class PlaneDTO : IPlaneDTO
    {
        [DataMember]
        public int PlaneId { get; set; }
        [DataMember]
        public string CompanyName { get;  set; }
        [DataMember]
        public int StationId { get; set; }
        [DataMember]
        public PlaneState State { get; set; }

    }
    //telling the plane state
    [DataContract]
    public enum PlaneState
    {
        [EnumMember]
        Landing,
        [EnumMember]
        Departuring,
        [EnumMember]
        Parking
    }
}
