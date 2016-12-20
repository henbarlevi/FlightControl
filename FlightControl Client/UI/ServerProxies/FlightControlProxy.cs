using FlightControl.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightControl.Contract.Entities.FCServiceEntities;
using System.ServiceModel;
using UI.EventArguments;

namespace UI.ServerProxies
{
    public class FlightControlProxy : IFlightControlService, IFlightControlCallback
    {
        private IFlightControlService proxy; //channel to the service
        DuplexChannelFactory<IFlightControlService> factory;
        public event EventHandler<AirportEventArgs> AirportChanged = delegate { };

        //singelton implementation
        private static readonly object padlock = new object();
        private static FlightControlProxy instance = null;
        public static FlightControlProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FlightControlProxy();
                    }
                    return instance;
                }
            }

        }
        //ctor
        private FlightControlProxy()
        {
            factory = new DuplexChannelFactory<IFlightControlService>(this, new WSDualHttpBinding(), new EndpointAddress("http://localhost:888"));
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service         
        }
        //client send request to connect server in order to get callback each time airport state change
        public ConnectResponse Connect(ConnectRequest connectRequest)
        {
            try
            {
                ConnectResponse response = proxy.Connect(connectRequest);
                return response;
            }
            catch(Exception e)
            {
                return new ConnectResponse { IsSuccess = false, Message = "failed to connect " + e.Message };
            }
        }

        public void Disconnect(DisconnectRequest disconnectRequest)
        {
            try
            {
                proxy.Disconnect(disconnectRequest);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void OnAirportChanged(OnAirportChangedRequest request)
        {
            AirportChanged.Invoke(this, new AirportEventArgs { ChangedAirport = request.ChangedAirport });
        }
    }
}
