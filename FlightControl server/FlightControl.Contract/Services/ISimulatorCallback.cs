using FlightControl.Contract.Entities.SimulatorServiceEntities;
using System.ServiceModel;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// the simulator will callback each time a plane finished to park
    /// and the simulator will know that he can simulate a departure of this plane
    /// </summary>
    [ServiceContract]
    public interface ISimulatorCallback
    {
        [OperationContract]
        void OnPlaneParked(OnPlaneParkedRequest request);
    }
}