namespace iPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Wheels", c => c.Int(nullable: false));
            AlterColumn("dbo.Vehicles", "RegNo", c => c.String(nullable: false, maxLength: 7));
            AlterColumn("dbo.Vehicles", "Make", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Vehicles", "Model", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Vehicles", "NrWheels");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "NrWheels", c => c.Int(nullable: false));
            AlterColumn("dbo.Vehicles", "Model", c => c.String());
            AlterColumn("dbo.Vehicles", "Make", c => c.String());
            AlterColumn("dbo.Vehicles", "RegNo", c => c.String());
            DropColumn("dbo.Vehicles", "Wheels");
        }
    }
}
