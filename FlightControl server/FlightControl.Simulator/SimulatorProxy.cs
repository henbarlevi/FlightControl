using FlightControl.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightControl.Contract.Entities.SimulatorServiceEntities;
using System.ServiceModel;

namespace FlightControl.Simulator
{
    public class SimulatorProxy : ISimulatorService, ISimulatorCallback
    {
        private ISimulatorService proxy; //channel to the service
        DuplexChannelFactory<ISimulatorService> factory;
        public event EventHandler PlaneParked = delegate { };

        //singelton implementation
        private static readonly object padlock = new object();
        private static SimulatorProxy instance = null;
        public static SimulatorProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SimulatorProxy();
                    }
                    return instance;
                }
            }

        }
        //ctor
        private SimulatorProxy()
        {
            factory = new DuplexChannelFactory<ISimulatorService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888"));
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service         
        }
      

        public void OnPlaneParked(OnPlaneParkedRequest request)
        {
            //raise event of parked plane (sender = parked plane )
            PlaneParked.Invoke(request.ParkedPlane, EventArgs.Empty);
        }

        public void SendSimulation(SimulationRequest simulationRequest)
        {
            try
            {
                proxy.SendSimulation(simulationRequest);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
