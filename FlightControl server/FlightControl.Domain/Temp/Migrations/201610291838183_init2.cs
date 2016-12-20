namespace FlightControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Planes", "ArrivalId");
            DropColumn("dbo.Planes", "DepartureId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planes", "DepartureId", c => c.Int(nullable: false));
            AddColumn("dbo.Planes", "ArrivalId", c => c.Int(nullable: false));
        }
    }
}
