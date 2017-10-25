using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPark.Models
{
    public class ParkingVehicle
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ParkingId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Parking Parking { get; set; }
    }
}