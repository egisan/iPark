namespace iPark.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "CheckIn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "CheckIn", c => c.DateTime());
        }
    }
}
