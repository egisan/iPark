namespace iPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "VehicleType", c => c.Int(nullable: false));
            DropColumn("dbo.Vehicles", "VehichleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "VehichleType", c => c.Int(nullable: false));
            DropColumn("dbo.Vehicles", "VehicleType");
        }
    }
}
