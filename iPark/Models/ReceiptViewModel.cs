using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPark.Models
{
    public class ReceiptViewModel
    {
        public EnumEntities.Vtypes VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegNo { get; set; }
        public EnumEntities.Colors Color { get; set; }
       
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        // System.TimeSpan diff1 = date2.Subtract(date1);

        public int ParkingTime;


    }
}