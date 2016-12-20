using FlightControl.BL.EventArguments;
using FlightControl.Contract.Entities;
using FlightControl.Contract.Services;
using FlightControl.Domain.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.BL
{
    public class FCServiceLogic
    {
        public AirportManager AirportManager { get; set; }
        public IUnityContainer Container { get; set; }
        // repositories
        public IPlaneRepository PlaneRepository { get; set; }
        public IArrivalsRepository ArrivalRepository { get; set; }
        public IDepartureRepository DepartureRepository { get; set; }
        //events
        public event EventHandler<AirportEventArgs> AirportChanged = delegate { };
        public event EventHandler PlaneParked = delegate { };

        public FCServiceLogic()
        {
            //Unity IOC Containter Init:
            InitContainer();
            //resolve repositories:
            InitRepositories();
            //init AirportManager:
            AirportManager = Container.Resolve<AirportManager>();
            //subscribe to airportManager events:
            AirportManager.Tower.PlaneArriving += OnPlaneArriving;
            AirportManager.Tower.PlaneDeparturing += OnPlaneDeparturing;
            AirportManager.Tower.PlaneParked += OnPlaneParked; //subscribe to event in order to bubble event to FlightControlService WCF
            AirportManager.AirportChanged += OnAirportChanged; //subscribe to event in order to bubble event to FlightControlService WCF
           
            //set airport state by Data From DB:
            List<PlaneDTO> airportPlanes = PlaneRepository.GetAirportPlanes();
            AirportManager.GetAirportStateFromDB(airportPlanes);         
        }
        //bubble the PlaneParked event up to the WCF service, 
        private void OnPlaneParked(object sender, EventArgs e)
        {
            PlaneParked.Invoke(sender, e);
        }
        //bubble the AirportChanged event up to the WCF service, 
        private void OnAirportChanged(object sender, AirportEventArgs e)
        {
            AirportChanged.Invoke(sender, e);
        }
        //will write the departuring details to DB
        private void OnPlaneDeparturing(object sender, EventArgs e)
        {
            PlaneDTO plane = sender as PlaneDTO;
            //write the new departuring to DB:
            DepartureRepository.AddDeparture(plane.PlaneId);
        }
        //will write the arriving details to DB
        private void OnPlaneArriving(object sender, EventArgs e)
        {
            PlaneDTO plane = sender as PlaneDTO;
            //write the new arrival to DB:
            ArrivalRepository.AddArrival(plane.PlaneId);

        }

        //get simulation of event (arrival/depature) from the simulator send it to the airportManager and write the event to the DB
        public void GetSimulation(PlaneDTO plane)
        {
            //send it to the airport Manager:
            AirportManager.GetSimulation(plane);
            
        }
        public AirportDTO GetAirportState() //return AirportDTO that contain the airport current state
        {
            return AirportManager.Airport;
        }
        //pulling data from db about all history departures
        public List<DepartureDTO> GetDeparturesHistory()
        {
            return DepartureRepository.GetAllDepartures();
        }
        //pulling data from db about all history arrivals
        public List<ArrivalDTO> GetArrivalsHistory()
        {
            return ArrivalRepository.GetAllArrivals();
        }
        private void InitRepositories()
        {
            PlaneRepository = Container.Resolve<IPlaneRepository>();
            ArrivalRepository = Container.Resolve<IArrivalsRepository>();
            DepartureRepository = Container.Resolve<IDepartureRepository>();
        } //create repositories that handle DB
        private void InitContainer()
        {
            Container = new UnityContainer();
            Container.RegisterType<IPlaneRepository, PlaneRepository>();
            Container.RegisterType<IArrivalsRepository, ArrivalsRepository>();
            Container.RegisterType<IDepartureRepository, DepartureRepository>();
            //Container.RegisterType<IList<IPlaneDTO>, IPlaneDTO[]>();
            //Container.RegisterType<IPlaneDTO, PlaneDTO>();
            Container.RegisterType<AirportDTO>();
            Container.RegisterType<Tower>();
            Container.RegisterType<AirportManager>();
        } //containter instance and register types



    }
}
