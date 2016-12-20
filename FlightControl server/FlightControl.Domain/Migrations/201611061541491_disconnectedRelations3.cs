namespace FlightControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disconnectedRelations3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departures", "PlaneId", "dbo.Planes");
            DropIndex("dbo.Departures", new[] { "PlaneId" });
            RenameColumn(table: "dbo.Departures", name: "PlaneId", newName: "Plane_PlaneId");
            AddColumn("dbo.Departures", "PlaneNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Departures", "Plane_PlaneId", c => c.Int());
            CreateIndex("dbo.Departures", "Plane_PlaneId");
            AddForeignKey("dbo.Departures", "Plane_PlaneId", "dbo.Planes", "PlaneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departures", "Plane_PlaneId", "dbo.Planes");
            DropIndex("dbo.Departures", new[] { "Plane_PlaneId" });
            AlterColumn("dbo.Departures", "Plane_PlaneId", c => c.Int(nullable: false));
            DropColumn("dbo.Departures", "PlaneNumber");
            RenameColumn(table: "dbo.Departures", name: "Plane_PlaneId", newName: "PlaneId");
            CreateIndex("dbo.Departures", "PlaneId");
            AddForeignKey("dbo.Departures", "PlaneId", "dbo.Planes", "PlaneId", cascadeDelete: true);
        }
    }
}
