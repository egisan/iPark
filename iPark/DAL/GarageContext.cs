using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iPark.DAL
{
    public class GarageContext : DbContext
    {
        public GarageContext() : base("Garage2")
        {
        }
        public DbSet<Models.Garage> Garages { get; set; }
        public DbSet <Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.Parking> Parkings { get; set; }
    }
}