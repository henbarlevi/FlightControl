using FlightControl.Contract.Entities.SimulatorServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// service that the simulator use in order to
    /// send a simulation of plane that arrivaling /departuring.
    /// the simulator will get callback when plane has finished to park (can departure)
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ISimulatorCallback))]
    public interface ISimulatorService
    {
        [OperationContract]
        void SendSimulation(SimulationRequest simulationRequest);
    }
}
