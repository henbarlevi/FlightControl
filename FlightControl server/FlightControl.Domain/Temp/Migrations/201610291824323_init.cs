namespace FlightControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Planes",
                c => new
                    {
                        PlaneId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        StationId = c.Int(nullable: false),
                        ArrivalId = c.Int(nullable: false),
                        DepartureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaneId);
            
            CreateTable(
                "dbo.Arrivals",
                c => new
                    {
                        ArrivalId = c.Int(nullable: false, identity: true),
                        PlaneId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArrivalId)
                .ForeignKey("dbo.Planes", t => t.PlaneId, cascadeDelete: true)
                .Index(t => t.PlaneId);
            
            CreateTable(
                "dbo.Departures",
                c => new
                    {
                        DepartureId = c.Int(nullable: false, identity: true),
                        PlaneId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DepartureId)
                .ForeignKey("dbo.Planes", t => t.PlaneId, cascadeDelete: true)
                .Index(t => t.PlaneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departures", "PlaneId", "dbo.Planes");
            DropForeignKey("dbo.Arrivals", "PlaneId", "dbo.Planes");
            DropIndex("dbo.Departures", new[] { "PlaneId" });
            DropIndex("dbo.Arrivals", new[] { "PlaneId" });
            DropTable("dbo.Departures");
            DropTable("dbo.Arrivals");
            DropTable("dbo.Planes");
        }
    }
}
