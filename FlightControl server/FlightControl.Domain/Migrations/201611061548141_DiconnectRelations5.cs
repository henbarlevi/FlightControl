namespace FlightControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiconnectRelations5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Arrivals", "PlaneId", "dbo.Planes");
            DropIndex("dbo.Arrivals", new[] { "PlaneId" });
            RenameColumn(table: "dbo.Arrivals", name: "PlaneId", newName: "Plane_PlaneId");
            AddColumn("dbo.Arrivals", "PlaneNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Arrivals", "Plane_PlaneId", c => c.Int());
            CreateIndex("dbo.Arrivals", "Plane_PlaneId");
            AddForeignKey("dbo.Arrivals", "Plane_PlaneId", "dbo.Planes", "PlaneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Arrivals", "Plane_PlaneId", "dbo.Planes");
            DropIndex("dbo.Arrivals", new[] { "Plane_PlaneId" });
            AlterColumn("dbo.Arrivals", "Plane_PlaneId", c => c.Int(nullable: false));
            DropColumn("dbo.Arrivals", "PlaneNumber");
            RenameColumn(table: "dbo.Arrivals", name: "Plane_PlaneId", newName: "PlaneId");
            CreateIndex("dbo.Arrivals", "PlaneId");
            AddForeignKey("dbo.Arrivals", "PlaneId", "dbo.Planes", "PlaneId", cascadeDelete: true);
        }
    }
}
