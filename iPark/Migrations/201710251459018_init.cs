namespace iPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleTypes", "SpacesRequiredx", c => c.Int(nullable: false));
            DropColumn("dbo.VehicleTypes", "SpacesRequired");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleTypes", "SpacesRequired", c => c.Int(nullable: false));
            DropColumn("dbo.VehicleTypes", "SpacesRequiredx");
        }
    }
}
