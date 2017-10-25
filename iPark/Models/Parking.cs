using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPark.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public int ParkingNo { get; set; }
        public int GarageId { get; set; }
        public Garage Garage { get; set; }
        //public ParkingSize Size { get; set; }
        //public EnumEntities.Vtypes ParkingType { get; set; }
    }
}