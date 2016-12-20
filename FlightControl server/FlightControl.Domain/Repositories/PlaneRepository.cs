using FlightControl.Contract.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.Domain.Repositories
{
    public class PlaneRepository : IPlaneRepository
    {
        public void AddPlane(PlaneDTO planeDTO)
        {
            using (var context = new FlightControlDBContext())
            {
                Plane plane = ConvertToModel(planeDTO);
                context.Planes.Add(plane);
                context.SaveChanges();
            }
        }
        public List<PlaneDTO> GetAirportPlanes()
        {
            using (var context = new FlightControlDBContext())
            {
                //if (context.Planes != null)
                //{
                    List<Plane> allplanes = context.Planes.ToList();
                    return allplanes.Select(p => ConvertToDTO(p)).ToList();
                //}
                //else
                //{
                //    return new List<PlaneDTO>();
                //}
            }
        }
        public void ChangePlaneStatus(PlaneDTO planeDTO)
        {
            using (var context = new FlightControlDBContext())
            {
                Plane plane = ConvertToModel(planeDTO);
                context.Entry(plane).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private Plane ConvertToModel(PlaneDTO planeDTO)
        {
            if (planeDTO != null)
            {
                return new Plane { PlaneId = planeDTO.PlaneId, StationId = planeDTO.StationId, CompanyName = planeDTO.CompanyName, State = planeDTO.State };
            }
            return null;
        }
        private PlaneDTO ConvertToDTO(Plane plane)
        {
            if (plane != null)
            {
                return new PlaneDTO { PlaneId = plane.PlaneId, CompanyName = plane.CompanyName, StationId = plane.StationId, State = plane.State };

            }
            return null;
        }
    }
}
