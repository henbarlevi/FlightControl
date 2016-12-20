using FlightControl.Contract.Entities;
using FlightControl.Contract.Entities.SimulatorServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FlightControl.Simulator
{
    /// <summary>
    /// randomize arrival - will randomize new arrival plane each time 
    /// randomize departure - will randomize new departure plane from the PlanesParking prop
    /// he return that plane. the tower will know if it (arrival/departure)
    /// by the station value
    /// </summary>
    public class Simulator
    {
        private SimulatorProxy proxy;
        private Timer SimulatorTimer { get; set; } // in each tick the simulator will create a new departure\arrival
        public List<PlaneDTO> PlanesParking { get; set; } //contain planes that are parking
        public List<string> RandomCompanyNames { get; set; } //will use to create random plane
        private Random rnd;

        public Simulator()
        {
            rnd = new Random();
            PlanesParking = new List<PlaneDTO>();
            RandomCompanyNames = new List<string> { "EL-AL", "Turkish-Airlines", "Blugaria-Airlines", "France-Air", "Arkia" ,"Italy-Airlines","USA-Airlines"};
            //init proxy:
            proxy = SimulatorProxy.Instance;
            proxy.PlaneParked += OnPlaneParked;
        }
        //the simulator starting to create simulations:
        public void Run()
        {
            //init simulationTimer:
            SimulatorTimer = new Timer(2000);
            SimulatorTimer.Elapsed += SimulatorTimer_Elapsed;
            SimulatorTimer.Enabled = true;
        }
        private void OnPlaneParked(object sender, EventArgs e)
        {
            PlaneDTO parkedPlane = sender as PlaneDTO;
            PlanesParking.Add(parkedPlane);
        }
        //each tick the simualtor send simulation to the server:
        private void SimulatorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PlaneDTO plane = Simulate();
            //send simulation to server :
            proxy.SendSimulation(new SimulationRequest { SimulatedPlane = plane });
        }
        //create simulation randomaly (arrival/departure)
        private PlaneDTO Simulate()
        {
            //randomize if the event is arrival or departure
            if (rnd.Next(0, 2) == 0) //arrival event
            {
                return SimulateArrival();
            }
            else //departure event
            {
                return SimulateDeparture();
            }
        }
        //simulate an arrival plane:
        private PlaneDTO SimulateArrival()
        {
            //randomizing plane
            PlaneDTO randomPlane = new PlaneDTO { PlaneId = rnd.Next(1, 100000), CompanyName = RandomCompanyNames[rnd.Next(0, RandomCompanyNames.Count)], StationId = 8 };
            // PlanesParking.Add(randomPlane);
            return randomPlane;
        
        }
        //simulate a departure plane:
        private PlaneDTO SimulateDeparture()
        {
            if (PlanesParking.Count != 0)
            {
                PlaneDTO randomPlane = PlanesParking[rnd.Next(0, PlanesParking.Count)];
                PlanesParking.Remove(randomPlane); //removing plane from the parkingPlanes list
                return randomPlane;
            }
            return null;
        }
    }
}
