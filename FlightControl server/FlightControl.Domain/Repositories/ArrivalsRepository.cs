using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightControl.Contract.Entities;

namespace FlightControl.Domain.Repositories
{
    public class ArrivalsRepository : IArrivalsRepository
    {
        public void AddArrival(int planeId)
        {
            using (var context = new FlightControlDBContext())
            {
                //Arrival arrival = new Arrival { PlaneId = planeId, Time = DateTime.Now };
                //context.Arrivals.Add(arrival);      
                //context.SaveChanges();

                Arrival arrival = new Arrival { PlaneNumber = planeId, Time = DateTime.Now };
                context.Arrivals.Add(arrival);
                context.SaveChanges();
            }
        }

        public List<ArrivalDTO> GetAllArrivals()
        {
            using (var context = new FlightControlDBContext())
            {
                var arrivals = context.Arrivals.ToList(); //TODO: can select be before to list without cast
                return arrivals.Select(u => ConvertToDTO(u)).ToList();
            }
        }

        private ArrivalDTO ConvertToDTO(Arrival arrival)
        {
            if (arrival != null)
            {
                return new ArrivalDTO { ArrivalId = arrival.ArrivalId, PlaneNumber = arrival.PlaneNumber, Time = arrival.Time };
            }
            return null;
        }
        private Arrival ConvertToModel(ArrivalDTO arrivalDTO)
        {
            if (arrivalDTO != null)
            {
                return new Arrival { ArrivalId = arrivalDTO.ArrivalId, PlaneNumber = arrivalDTO.PlaneNumber, Time = arrivalDTO.Time };
            }
            return null;
        }
    }
}
