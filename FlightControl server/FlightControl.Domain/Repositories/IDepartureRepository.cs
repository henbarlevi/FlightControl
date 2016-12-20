using FlightControl.Contract.Entities;
using System.Collections.Generic;

namespace FlightControl.Domain.Repositories
{
    public interface IDepartureRepository
    {
       void AddDeparture(int planeId);
        List<DepartureDTO> GetAllDepartures();
    }
}