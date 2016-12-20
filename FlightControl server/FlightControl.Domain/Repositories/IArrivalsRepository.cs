using FlightControl.Contract.Entities;
using System.Collections.Generic;

namespace FlightControl.Domain.Repositories
{
    public interface IArrivalsRepository
    {
        void AddArrival(int planeId);
        List<ArrivalDTO> GetAllArrivals();
    }
}