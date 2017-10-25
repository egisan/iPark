namespace iPark.Migrations
{
    using iPark.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using iPark.DAL;
    

    internal sealed class Configuration : DbMigrationsConfiguration<GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public GarageContext context { get; set; }

        protected override void Seed(GarageContext context)
        {
            this.context = context;
            AddGarage();
            AddParking();
            AddVehicles();



        }

        private void AddGarage()
        {
            context.Garages.AddOrUpdate(
                v => v.Name,
                    new Garage { Name = "GarageNo 1", Capacity = 100 }
                 );
        }

        private void AddParking()
        {
            var garages = context.Garages.ToList();
           
            foreach(var garage in garages)
            {
                var garagNo = 100;
                for (int i = 0; i < garage.Capacity; i++ )
                {
                    context.Parkings.AddOrUpdate(
                      v => v.ParkingNo,
                      new Parking { GarageId = garage.Id, ParkingNo = garagNo }
                   );
                    garagNo++;
                }
            }
           
            
        }

        private void AddVehicles()
        {
            context.Vehicles.AddOrUpdate(
              v => v.RegNo,
              new Vehicle { RegNo = "ABC123", Color = EnumEntities.Colors.BLACK, VehichleType = EnumEntities.Vtypes.Car, Make = "FIAT", Model = "500", Wheels = 4, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC324", Color = EnumEntities.Colors.YELLOW, VehichleType = EnumEntities.Vtypes.Bus, Make = "VOLVO", Model = "95-3", Wheels = 8, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC456", Color = EnumEntities.Colors.WHITE, VehichleType = EnumEntities.Vtypes.MC, Make = "SUZUKI", Model = "TURBO", Wheels = 2, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC789", Color = EnumEntities.Colors.RED, VehichleType = EnumEntities.Vtypes.Car, Make = "IVECO", Model = "4000", Wheels = 4, CheckIn = DateTime.Now },
              new Vehicle { RegNo = "ABC987", Color = EnumEntities.Colors.GRAY, VehichleType = EnumEntities.Vtypes.Van, Make = "Nissan", Model = "2015", Wheels = 4, CheckIn = DateTime.Now }
              );
        }

        private void AddParkingVehicle()
        {
            
        }
    }
}
