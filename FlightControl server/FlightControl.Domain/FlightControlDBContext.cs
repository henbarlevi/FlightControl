namespace FlightControl.Domain
{
    using Contract.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class FlightControlDBContext : DbContext
    {

        public FlightControlDBContext()
            : base("name=FlightControlDBContext")
        {
        }

        /// <summary>
        /// planes table will show all existing planes status:
        /// station Id = 8 - the plane is not in the airport and not attend to land
        /// station Id = 0 - 2 the plane is not in the airport and attend to land
        /// station Id = 3 - the plane landed in the airport / the plane is going to departure
        /// station Id = 4 - the plane is going to park
        /// station Id = 5-6 - the plane is parking
        /// station Id = 7 - the plane is leaving the parking lot and going to departure
        /// </summary>
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Arrival> Arrivals { get; set; }
        public virtual DbSet<Departure> Departures { get; set; }


    }

    //public class Arrival
    // {
    //     public DateTime Time { get; set; }
    // }
    // public class Departure
    // {
    //     public DateTime Time { get; set; }
    // }
    //public class Airport
    //{

    //}

    public class Arrival
    {
        public int PlaneNumber { get; set; }
        //[ForeignKey("PlaneId")]
        //public virtual Plane Plane { get; set; }

        [Key]
        public int ArrivalId { get; set; }
        public DateTime Time { get; set; }


    }

    public class Departure
    {
        public int PlaneNumber { get; set; }
        //[ForeignKey("PlaneId")]
        //public virtual Plane Plane { get; set; }

        [Key]
        public int DepartureId { get; set; }
        public DateTime Time { get; set; }
    }
    public class Plane
    {
        [Key]
        public int PlaneId { get; set; }
        public string CompanyName { get; set; }
        public int StationId { get; set; }
        public PlaneState State { get; set; }


        //public virtual ICollection<Arrival> Arrivals { get; set; }
        //public virtual ICollection<Departure> Departures { get; set; }
        //public Plane()
        //{
        //    Arrivals = new List<Arrival>();
        //    Departures = new List<Departure>();
        //}
    }
}