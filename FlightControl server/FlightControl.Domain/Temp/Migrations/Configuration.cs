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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Planes.AddOrUpdate(

              new Plane { CompanyName = "El-Al", StationId = 5 , State= PlaneState.Parking }
        

            );

        }
    }
}
