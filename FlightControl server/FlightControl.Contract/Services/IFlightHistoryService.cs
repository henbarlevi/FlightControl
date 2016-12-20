using FlightControl.Contract.Entities.FHServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// this service will give the client info about all
    /// the history flights (arrivals and departures)
    /// include planeId Time Of arrival/departure etc.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IFlightHistoryCallback))]
    public interface IFlightHistoryService
    {
        [OperationContract]
        GetArrivalsResponse GetArrivals(GetArrivalsRequest request);
        [OperationContract]
        GetDeparturesResponse GetDepartures(GetDeparturesRequest request);
    }
}
