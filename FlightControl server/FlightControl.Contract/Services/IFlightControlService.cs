using FlightControl.Contract.Entities;
using FlightControl.Contract.Entities.FCServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// service that the client use in order to get notifications
    /// about the changes in the airport.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IFlightControlCallback))]
    [ServiceKnownType(typeof(PlaneState))] // PlaneState enum serialization
    public interface IFlightControlService
    {
        [OperationContract]
        ConnectResponse Connect(ConnectRequest connectRequest); //sign the client to callbacks
        [OperationContract]
        void Disconnect(DisconnectRequest disconnectRequest); //singOut the client from callbacks
    }
}
