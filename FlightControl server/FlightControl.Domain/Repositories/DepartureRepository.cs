using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightControl.Contract.Entities;

namespace FlightControl.Domain.Repositories
{
    public class DepartureRepository : IDepartureRepository
    {
        public void AddDeparture(int planeId)
        {
            using (var context = new FlightControlDBContext())
            {
                Departure departure = new Departure { PlaneNumber = planeId, Time = DateTime.Now };
                context.Departures.Add(departure);
                context.SaveChanges();
            }
        }

        public List<DepartureDTO> GetAllDepartures()
        {
            using (var context = new FlightControlDBContext())
            {
                var departures = context.Departures.ToList(); //TODO: can select be before to list without cast
                return departures.Select(u => ConvertToDTO(u)).ToList();
            }
        }

        private DepartureDTO ConvertToDTO(Departure departure)
        {
            if (departure != null)
            {
                return new DepartureDTO { DepartureId = departure.DepartureId, PlaneNumber = departure.PlaneNumber, Time = departure.Time };
            }
            return null;
        }
        private Departure ConvertToModel(DepartureDTO departureDTO)
        {
            if (departureDTO != null)
            {
                return new Departure { DepartureId = departureDTO.DepartureId, PlaneNumber = departureDTO.PlaneNumber, Time = departureDTO.Time };
            }
            return null;
        }
    }
}
