using FlightControl.Contract.Entities;
using FlightControl.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.BL
{
    /// <summary>
    /// updating the plane state by using the plane station
    /// </summary>
    public static class PlaneStateUpdater
    {
        public static void Update(PlaneDTO plane)
        {
            if (StationsInfoService.IsParking(plane.StationId))
            {
                plane.State = PlaneState.Parking;
            }
            else if (StationsInfoService.IsAttendingToLand(plane.StationId))
            {
                plane.State = PlaneState.Landing;
            }
            else if(StationsInfoService.IsAttendingToDepart(plane.StationId)) 
            {
                plane.State = PlaneState.Departuring;
            }
        }

        public static void Update(AirportDTO airport)
        {
            if (airport != null)
            {
                foreach (var plane in airport.Planes)
                {
                    Update(plane);
                }
            }
        }
    }
}
