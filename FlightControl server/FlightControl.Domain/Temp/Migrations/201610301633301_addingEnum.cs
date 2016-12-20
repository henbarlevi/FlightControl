namespace FlightControl.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planes", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Planes", "State");
        }
    }
}
