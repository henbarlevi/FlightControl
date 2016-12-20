using FlightControl.Contract.Entities;
using FlightControl.Contract.Services;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FlightControl.BL
{
    /// <summary>
    /// the tower is managing the mechanisem in the airport
    /// it tell all the planes were to go
    /// -------flightControl rules---------
    ///1.a plane can start departure only if stations 0,1,2,3,7 are empty.
    ///2.a plane can start arrival/landing only if sations 0,1,2,3,7,4 are empty (4-can start parking)
    ///3 a) a plane that is in station 4 can only be in station 5\6 if one of them is empty.
    ///3 b) station 5 comes first if empty, then 6.
    ///4.departure planesOnHold always have higher priority than arrival planesOnHold
    ///planesInAirport - all planes that are currently in airport
    ///planesOnHold - planes that are attending to arrival/departure and waiting their turn
    /// </summary>   
    public class Tower
    {
        public Queue<PlaneDTO> DeparturePlanesOnHold { get; set; } //planes attending to confirmation that they can depart
        public Queue<PlaneDTO> ArrivalPlanesOnHold { get; set; } //planes attending to confirmation that they can land (not part of the airport for now)
        public event EventHandler PlaneParked = delegate { }; //telling the simulator that the plane parked.
        public event EventHandler PlaneArriving = delegate { };//tower raise event when deciding to let plane land in airport
        public event EventHandler PlaneDeparturing = delegate { };//tower raise event when deciding to let plane departure from airport
        public Tower()
        {
            DeparturePlanesOnHold = new Queue<PlaneDTO>();
            ArrivalPlanesOnHold = new Queue<PlaneDTO>();
        }
        //getting  request from plane that want to arrival/departure
        public void AcceptPlaneRequest(PlaneDTO plane)
        {
            if (plane != null)
            {
                if (StationsInfoService.IsInAir(plane.StationId)) //if plane is in air
                {
                    //means plane want to land:
                    ArrivalPlanesOnHold.Enqueue(plane);
                }
                else if (StationsInfoService.IsParking(plane.StationId)) //if plane parking
                {
                    //means plane want to departure:
                    DeparturePlanesOnHold.Enqueue(plane);
                }
            }

        }
        //managing planes mechanisem:
        public void ManageAirport(AirportDTO airport)
        {
            //removing planes from airport that already departured:
            RemoveDeparturedPlanesFromAirport(airport);
            //telling the planes in airport where to go
            //(planes that are already in process of landing/parking/departuring):
            MangePlanesInAirport(airport);
            //telling planes on hold if they can land/departure
            //(telling them to start the process)
            ManagePlanesOnHold(airport);
            //updating planes status:
            PlaneStateUpdater.Update(airport);
        }
        //removing planes from airport that already departured:
        private void RemoveDeparturedPlanesFromAirport(AirportDTO airport)
        {
            for (int i = 0; i < airport.Planes.Count; i++)
            {
                PlaneDTO plane = airport.Planes[i];
                //if the plane already left the airport
                if (StationsInfoService.IsInAir(plane.StationId))
                {
                    //remove the plane from the airport:
                    airport.Planes.Remove(plane);
                }
            }
        }

        private void MangePlanesInAirport(AirportDTO airport)
        {
            //telling planes in airport where to move:
            for (int i = 0; i < airport.Planes.Count; i++)
            {
                PlaneDTO plane = airport.Planes[i];
                //if plane landing
                if (StationsInfoService.IsAttendingToLand(plane.StationId))
                {
                    plane.StationId++;
                }
                //if plane on track
                else if (StationsInfoService.IsOnTrack(plane.StationId))
                {
                    if (plane.State == PlaneState.Landing)
                    {
                        plane.StationId++;
                    }
                    else//departuring
                    {
                        plane.StationId = StationsInfoService.InAir; //tell the plane departure to air
                        //and from the plansOnHold queue:
                        DeparturePlanesOnHold.Dequeue();
                    }
                }
                //if attending to park
                else if (StationsInfoService.IsAttendingToPark(plane.StationId))
                {
                    //if the parking slots are empty -tell the plane to move there:
                    PlaneDTO firstParkingSlot = airport.Planes.FirstOrDefault(u => u.StationId == StationsInfoService.Parking[0]);
                    PlaneDTO secondParkingSlot = airport.Planes.FirstOrDefault(u => u.StationId == StationsInfoService.Parking[1]);
                    if (firstParkingSlot == null)
                    {
                        plane.StationId = StationsInfoService.Parking[0];
                        //raise event about the parked plane:
                        PlaneParked.Invoke(plane, EventArgs.Empty);
                    }
                    else if (secondParkingSlot == null)
                    {
                        plane.StationId = StationsInfoService.Parking[1];
                        //raise event about the parked plane:
                        PlaneParked.Invoke(plane, EventArgs.Empty);
                    }

                }
                else if (StationsInfoService.IsAttendingToDepart(plane.StationId))
                {
                    plane.StationId = StationsInfoService.Track;
                }
            }
        }

        private void ManagePlanesOnHold(AirportDTO airport)
        {
            //mangaing the planes that want to departure/arrival:
            //check if the airport field is clean for departure:
            if (AirportFieldValidator.IsClearForDeparturing(airport))
            {
                // if there is plane that want to departure:
                if (DeparturePlanesOnHold.Count != 0)
                {
                    //tell the plane to departure:
                    PlaneDTO plane = DeparturePlanesOnHold.Peek();
                    PlaneDTO departuringPlane = airport.Planes.Find(p => p.PlaneId == plane.PlaneId);
                    departuringPlane.StationId = StationsInfoService.AttendToDepart;
                    //raise event that plane is departuring:
                    PlaneDeparturing.Invoke(departuringPlane, EventArgs.Empty);
                }
                else //if there isnt planes that want to depart, check if there is plane that want land
                {
                    // if there is plane that want to land:
                    if (ArrivalPlanesOnHold.Count != 0)
                    {
                        //check if the field clear for landing:
                        if (AirportFieldValidator.IsClearForLanding(airport))
                        {
                            //if field clear tell the plane to land:
                            PlaneDTO plane = ArrivalPlanesOnHold.Dequeue();
                            plane.StationId = StationsInfoService.AttendingToLand.First();
                            //adding plane to airport :
                            airport.Planes.Add(plane);
                            //raise event that plane is arriving:
                            PlaneArriving.Invoke(plane, EventArgs.Empty);

                        }
                    }
                }
            }


        }
    }

}