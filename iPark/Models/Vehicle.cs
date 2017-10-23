using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPark.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public EnumEntities.Colors Color { get; set; }
        public EnumEntities.Vtypes VehichleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int NrWheels { get; set; }

        public DateTime CheckIn { get; set; } 
        public DateTime? CheckOut { get; set; }
    }
}