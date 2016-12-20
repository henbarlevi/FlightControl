namespace FlightControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisconnectRelationsFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Arrivals", "Plane_PlaneId", "dbo.Planes");
            DropForeignKey("dbo.Departures", "Plane_PlaneId", "dbo.Planes");
            DropIndex("dbo.Arrivals", new[] { "Plane_PlaneId" });
            DropIndex("dbo.Departures", new[] { "Plane_PlaneId" });
            DropColumn("dbo.Arrivals", "Plane_PlaneId");
            DropColumn("dbo.Departures", "Plane_PlaneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departures", "Plane_PlaneId", c => c.Int());
            AddColumn("dbo.Arrivals", "Plane_PlaneId", c => c.Int());
            CreateIndex("dbo.Departures", "Plane_PlaneId");
            CreateIndex("dbo.Arrivals", "Plane_PlaneId");
            AddForeignKey("dbo.Departures", "Plane_PlaneId", "dbo.Planes", "PlaneId");
            AddForeignKey("dbo.Arrivals", "Plane_PlaneId", "dbo.Planes", "PlaneId");
        }
    }
}
