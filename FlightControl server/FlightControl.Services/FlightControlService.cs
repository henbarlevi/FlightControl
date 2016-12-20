using FlightControl.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FlightControl.Contract.Entities.FCServiceEntities;
using FlightControl.BL;
using FlightControl.Contract.Entities.SimulatorServiceEntities;
using FlightControl.BL.EventArguments;
using System.Diagnostics;
using FlightControl.Contract.Entities;
using FlightControl.Contract.Entities.FHServiceEntities;

namespace FlightControl.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FlightControlService : IFlightControlService,IFlightHistoryService,ISimulatorService
    {
        private List<IFlightControlCallback> callbacks;
        private ISimulatorCallback simulatorCallback;
        //private AirportManager airportManager;
        public FCServiceLogic serviceLogic { get; set; }
        //ctor
        public FlightControlService()
        {
            //init callbacks:
            callbacks = new List<IFlightControlCallback>();
            //airportManager = new AirportManager();
            serviceLogic = new FCServiceLogic();
            //assign to AirportChanged event:
            serviceLogic.AirportChanged += OnAirportChanged;
            serviceLogic.PlaneParked += OnPlaneParked;
        }
        //telling to simulator about parked plane so he can simulate that departure (some time after)
        #region IFlightControlService Methods
        /// <summary>
        /// in each change in the airport the server will send callback to the client 
        /// about the change
        /// </summary>
        /// <param name="sender">airport manager</param>
        /// <param name="e">contain the changed airportDTO</param>
        private void OnAirportChanged(object sender, AirportEventArgs e)
        {
            for (int i = 0; i < callbacks.Count; i++)
            {
                callbacks[i].OnAirportChanged(new OnAirportChangedRequest { ChangedAirport = e.ChangedAirport });
            }
        }        
        public ConnectResponse Connect(ConnectRequest connectRequest)
        {
            //adding client to callbacks:
            IFlightControlCallback callback = OperationContext.Current.GetCallbackChannel<IFlightControlCallback>();
            if (!callbacks.Contains(callback))
            {
                callbacks.Add(callback);
                //var res = new ConnectResponse { IsSuccess = true, Message = "connection succeded", Airport = serviceLogic.GetAirportState() };
                //Debug.WriteLine(res.IsSuccess + " " + res.Message);
                //returning that the login succeeded and the airport current state:
                return new ConnectResponse { IsSuccess = true, Message = "connection succeded", Airport = serviceLogic.GetAirportState() };
            }
            else // client didnt added to callbacks - connection failed:
            {
                return new ConnectResponse { IsSuccess = false, Message = "connection failed", Airport = null };
            }


        }
        public void Disconnect(DisconnectRequest disconnectRequest)
        {
            //removing client from callbacks:
            IFlightControlCallback callback = OperationContext.Current.GetCallbackChannel<IFlightControlCallback>();
            callbacks.Remove(callback);
        }
        #endregion 
        #region IHistoryService Methods
        //FlightHistoryService - gives client the history of all arrivals in airport
        public GetArrivalsResponse GetArrivals(GetArrivalsRequest request)
        {
            //get history arrivals from db:
           List<ArrivalDTO> arrivals = serviceLogic.GetArrivalsHistory();
            //returning response:
            return new GetArrivalsResponse { IsSuccess = true, Message = "arrivals pulled succesfuly", Arrivals = arrivals };
        }
        //FlightHistoryService - gives client the history of all departures in airport
        public GetDeparturesResponse GetDepartures(GetDeparturesRequest request)
        {
            //get history departures from db:
            List<DepartureDTO> departures = serviceLogic.GetDeparturesHistory();
            //returning response:
            return new GetDeparturesResponse { IsSuccess = true, Message = "departures pulled succesfuly", Departures = departures };
        }
        #endregion
        #region ISimulatorService Methods
        /// <summary>
        ///telling to simulator about parked plane so he can simulate that departure (some time after)
        /// </summary>
        /// <param name="sender">parked plane</param>
        /// <param name="e">empty</param>
        private void OnPlaneParked(object sender, EventArgs e)
        {
           if(simulatorCallback != null)
            {
                PlaneDTO plane = sender as PlaneDTO;
                simulatorCallback.OnPlaneParked(new OnPlaneParkedRequest { ParkedPlane = plane });
            }
        }
        /// <summary>
        /// the simulator sending simulation request of arrival/departure plane:
        /// </summary>
        /// <param name="simulationRequest"></param>
        public void SendSimulation(SimulationRequest simulationRequest)
        {
           //adding callback of simulator:
           if(simulatorCallback == null)
            {
                simulatorCallback = OperationContext.Current.GetCallbackChannel<ISimulatorCallback>();
            }
           //telling the airport about the simulation:
            serviceLogic.GetSimulation(simulationRequest.SimulatedPlane);
        }
        #endregion
    }
}
