using FlightControl.Contract.Entities.FCServiceEntities;
using System.ServiceModel;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// the client will get callback each time the airport state
    /// change
    /// </summary>
    [ServiceContract]
    public interface IFlightControlCallback
    {
        [OperationContract]
        void OnAirportChanged(OnAirportChangedRequest request);
    }
}