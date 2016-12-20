using FlightControl.FlightsSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using FlightControl.Contract.Entities;
using FlightControl.Domain.Repositories;

using Microsoft.Practices.Unity;
using System.ComponentModel;
using System.Collections;
using FlightControl.BL.EventArguments;

namespace FlightControl.BL
{
    public class AirportManager // 
    {
        public Tower Tower { get; set; } // responsible for telling the planes where to go
        public Timer TowerTimer { get; set; } // in each tick the tower will manage the airport
        //public Timer SimulatorTimer { get; set; } // in each tick the simulator will create a new departure\arrival
        //public Simulator Simulator { get; set; }// simulate arrival/departure events
       // public List<PlaneDTO> AllPlanes { get; set; } // all existing planes ind DB (include those not in the airport)
        public AirportDTO Airport { get; set; } //contain all planes in airport
        public event EventHandler<AirportEventArgs> AirportChanged  = delegate { }; //event raised each time the airport state (planes locations) cahnge
        //public IPlaneRepository PlaneRepository { get; set; }
        //public IArrivalsRepository ArrivalRepository { get; set; }
        //public IDepartureRepository DepartureRepository { get; set; }
        [Dependency]
        public IUnityContainer Container { get; set; } //Ioc
        //ctor
        public AirportManager(IUnityContainer Container)
        {
            
            //Unity IOC Containter Init:
            //InitContainer();
            //init repositories
            //PlaneRepository = Container.Resolve<IPlaneRepository>();
            //ArrivalRepository = Container.Resolve<IArrivalsRepository>();
            //DepartureRepository = Container.Resolve<IDepartureRepository>();
            //get all existing planes from airport:
            Airport = Container.Resolve<AirportDTO>();
            Airport.Planes = new List<PlaneDTO>(); //TODO:Change to Unity Container
            //Airport.Planes = PlaneRepository.GetAirportPlanes();
            //init tower:
            Tower = Container.Resolve<Tower>();
            //Tower.PlaneParked += OnPlaneParked;
            //init simulator:
            //Simulator = Container.Resolve<Simulator>();      
            //init timers:
            InitTimers();

        }
        public void GetAirportStateFromDB(List<PlaneDTO> planes)
        {
            Airport.Planes = planes;
        }
        //getting simulation and telling to the tower about it
        public void GetSimulation(PlaneDTO plane)
        {
            Tower.AcceptPlaneRequest(plane);
        }
        //private void OnPlaneParked(object sender, EventArgs e)
        //{
        //    PlaneDTO plane = sender as PlaneDTO;
        //    Simulator.PlanesParking.Add(plane);
        //}

        //private void InitContainer()
        //{
        //    Container = new UnityContainer();
        //    Container.RegisterType<IPlaneRepository, PlaneRepository>();
        //    Container.RegisterType<IArrivalsRepository, ArrivalsRepository>();
        //    Container.RegisterType<IDepartureRepository, DepartureRepository>();
        //    //Container.RegisterType<IList<IPlaneDTO>, IPlaneDTO[]>();
        //    //Container.RegisterType<IPlaneDTO, PlaneDTO>();
        //    Container.RegisterType<AirportDTO>();
        //    Container.RegisterType<Tower>();
        //    Container.RegisterType<Simulator>();
        //} //containter instance and register types
        private void InitTimers()
        {
            TowerTimer = new Timer(2000);
            TowerTimer.Elapsed += TowerTimer_Elapsed;
            TowerTimer.Enabled = true;
            //SimulatorTimer = new Timer(2000);
            //SimulatorTimer.Elapsed += SimulatorTimer_Elapsed;
            //SimulatorTimer.Enabled = true;
        }
        //each tick the simulator will raise an airport event and the tower will get it
        //private void SimulatorTimer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    PlaneDTO plane = Simulator.Simulate();
        //    //sending request to the tower about arrvial/departure plane:
        //    Tower.AcceptPlaneRequest(plane);
        //}
        //each tick the tower will manage the planes:
        private void TowerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tower.ManageAirport(Airport);
            //raising event that the airport changed:
            AirportChanged.Invoke(this, new AirportEventArgs { ChangedAirport = Airport });
        }
        
    }
}
