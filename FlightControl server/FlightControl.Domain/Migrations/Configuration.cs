namespace FlightControl.Domain.Migrations
{
    using Contract.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FlightControl.Domain.FlightControlDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FlightControl.Domain.FlightControlDBContext context)
        {
            //context.Planes.AddOrUpdate(
            //      p => p.PlaneId,
            //      new Plane { CompanyName = "El Al", StationId = 5, State = PlaneState.Parking } // parking
            //      //new Plane { CompanyName = "El Al", StationId = 8 },
            //      //new Plane { CompanyName = "El Al", StationId = 8 },
            //      //new Plane { CompanyName = "Air-Bulgaria", StationId = 8 },
            //      //new Plane { CompanyName = "Air-Bulgaria", StationId = 8 },
            //      //new Plane { CompanyName = "Air-Bulgaria", StationId = 8 },
            //      //new Plane { CompanyName = "Air-Bulgaria", StationId = 8 },
            //      //new Plane { CompanyName = "Turkish-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "Turkish-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "Turkish-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "Turkish-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "Turkish-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "USA-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "USA-Airlines", StationId = 8 },
            //      //new Plane { CompanyName = "USA-Airlines", StationId = 8 }
            //    );

            //context.Arrivals.AddOrUpdate(
            //   new Arrival { PlaneNumber = 1, Time = DateTime.Now }
                
            //    );
        }
    }
}
