using FlightControl.Contract.Entities;
using System.Collections.Generic;

namespace FlightControl.Domain.Repositories
{
    public interface IPlaneRepository //TODO : add methods
    {
        void AddPlane(PlaneDTO planeDTO);
        void ChangePlaneStatus(PlaneDTO planeDTO);
        List<PlaneDTO> GetAirportPlanes();
    }
}