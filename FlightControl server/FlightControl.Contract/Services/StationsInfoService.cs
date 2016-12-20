using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Contract.Services
{
    /// <summary>
    /// class that contain information about the airport stations index
    /// </summary>
    public class StationsInfoService
    {
        public static readonly int InAir = 8;
        public static readonly int[] AttendingToLand = new[] { 0, 1, 2 };
        public static readonly int Track = 3;
        public static readonly int AttendToPark = 4;
        public static readonly int AttendToDepart = 7;
        public static readonly int[] Parking = new[] { 5, 6 };

        /// <summary>
        /// all methods below are returning bool value
        /// if the plane is in a specific location,
        /// the methods get the index of the plane location
        /// and determine if the plane is in the loacation asked
        /// </summary>
        /// <param name="planeStationId"></param>
        /// <returns></returns>
        public static bool IsInAir(int planeStationId)
        {
            if (planeStationId == InAir)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsAttendingToLand(int planeStationId)
        {
            return AttendingToLand.Contains(planeStationId);
        }
        public static bool IsOnTrack(int planeStationId)
        {
            if (planeStationId == Track)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsAttendingToPark(int planeStationId)
        {
            if (planeStationId == AttendToPark)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsAttendingToDepart(int planeStationId)
        {
            if (planeStationId == AttendToDepart)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsParking(int planeStationId)
        {
            return Parking.Contains(planeStationId);
        }
    }
}
