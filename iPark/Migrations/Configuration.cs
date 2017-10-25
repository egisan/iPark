namespace iPark.Migrations
{
    using iPark.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    internal sealed class Configuration : DbMigrationsConfiguration<iPark.DAL.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(iPark.DAL.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Vehicles.AddOrUpdate(
              v => v.RegNo,
              new Vehicle { RegNo = "ABC123", Color = EnumEntities.Colors.BLACK, VehicleType = EnumEntities.Vtypes.CAR, Make = "FIAT", Model = "500", Wheels = 4, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC324", Color = EnumEntities.Colors.YELLOW, VehicleType = EnumEntities.Vtypes.BUS, Make = "VOLVO", Model = "95-3", Wheels = 8, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC456", Color = EnumEntities.Colors.WHITE, VehicleType = EnumEntities.Vtypes.MC, Make = "SUZUKI", Model = "TURBO", Wheels = 2, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC789", Color = EnumEntities.Colors.RED, VehicleType = EnumEntities.Vtypes.CAR, Make = "IVECO", Model = "4000", Wheels = 4, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC987", Color = EnumEntities.Colors.GRAY, VehicleType = EnumEntities.Vtypes.VAN, Make = "Nissan", Model = "2015", Wheels = 4, CheckIn = DateTime.Now }
            );

        }
    }
}
