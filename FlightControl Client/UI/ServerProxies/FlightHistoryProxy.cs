using FlightControl.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FlightControl.Contract.Entities.FHServiceEntities;

namespace UI.ServerProxies
{
    public class FlightHistoryProxy : IFlightHistoryService,IFlightHistoryCallback
    {
        private IFlightHistoryService proxy; //channel to the service
        DuplexChannelFactory<IFlightHistoryService> factory;
        //singelton implementation
        private static readonly object padlock = new object();
        private static FlightHistoryProxy instance = null;
        public static FlightHistoryProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FlightHistoryProxy();
                    }
                    return instance;
                }
            }

        }
        public FlightHistoryProxy()
        {
            //open channel
            factory = new DuplexChannelFactory<IFlightHistoryService>(this,new WSDualHttpBinding(), new EndpointAddress("http://localhost:888"));
            factory.Open();
            proxy = factory.CreateChannel(); //creating channel to the service 
        }

        public GetArrivalsResponse GetArrivals(GetArrivalsRequest request)
        {
                GetArrivalsResponse response = proxy.GetArrivals(new GetArrivalsRequest { });
                return response;           
        }

        public GetDeparturesResponse GetDepartures(GetDeparturesRequest request)
        {
            GetDeparturesResponse response = proxy.GetDepartures(new GetDeparturesRequest { });
            return response;
        
        }

        public void OnArrivalAcquired(OnArrivalAcquired request)
        {
            throw new NotImplementedException();
        }
    }
}
