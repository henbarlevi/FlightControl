using FlightControl.Contract.Entities;
using FlightControl.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightControl.FlightsSimulator
{
    public class Simulator
    {
        public List<PlaneDTO> PlanesParking { get; set; } //contain planes that are parking
        public List<string> RandomCompanyNames { get; set; } //will use to create random plane
        private Random rnd;
        public Simulator()
        {
            rnd = new Random();
            PlanesParking = new List<PlaneDTO>();
            RandomCompanyNames = new List<string> { "EL-AL", "Turkish-Airlines", "Blugaria-Airlines", "France-Air", "Arkia" };
        }
        /// <summary>
        /// randomize arrival - will randomize new arrival plane each time 
        /// randomize departure - will randomize new departure plane from the PlanesParking prop
        /// he return that plane. the tower will know if it (arrival/departure)
        /// by the station value
        /// </summary>
        public PlaneDTO Simulate()
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
        public PlaneDTO SimulateArrival()
        {
            //randomizing plane
            PlaneDTO randomPlane = new PlaneDTO { PlaneId = rnd.Next(1, 100000), CompanyName = RandomCompanyNames[rnd.Next(0, RandomCompanyNames.Count)], StationId = 8 };
           // PlanesParking.Add(randomPlane);
            return randomPlane;
            ////if the plane not already in process of landing/departuring:
            //if (!PlanesParking.Contains(randomPlane))
            //{
            //    //if the plane is in the air/on parking lot return the plane         
            //    if (StationsInfoService.IsInAir(randomPlane.StationId)
            //       || StationsInfoService.IsParking(randomPlane.StationId))
            //    {
            //        return randomPlane;
            //    }
            //}
            ////called recursivly until plane not in process is returned

            //return Simulate(allplanes);
        }
        public PlaneDTO SimulateDeparture()
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
