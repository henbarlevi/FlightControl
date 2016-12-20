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
    /// check if the airport field is clear for landing/departuring
    /// </summary>
    public class AirportFieldValidator
    {
        // plane can start departure only if stations 0,1,2,3,7 are empty.
        public static bool IsClearForDeparturing(AirportDTO airport)
        {
            foreach (var plane in airport.Planes)
            {
                if(StationsInfoService.IsAttendingToLand(plane.StationId) ||
                   StationsInfoService.IsOnTrack(plane.StationId) ||
                   StationsInfoService.IsAttendingToDepart(plane.StationId))
                {
                    return false; // the field isnt clear for departuring
                }
            }
            return true; //the field is clear for departuring
        }

        //plane can start arrival/landing only if sations 0,1,2,3,7,4 are empty(4-can start parking)
        public static bool IsClearForLanding(AirportDTO airport)
        {
          
            // same rules as IsClearForDeparture with the exception of station 4
            if (AirportFieldValidator.IsClearForDeparturing(airport)) // if all is true, then can check station 4
            {
                PlaneDTO plane = airport.Planes.FirstOrDefault(p => StationsInfoService.IsAttendingToPark(p.StationId));
                if (plane == null) //no 
                {
                    return true;
                }
            }
            return false;
         
        }
        
    }
}
