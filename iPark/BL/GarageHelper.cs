using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iPark.Models;
using iPark.DAL;

namespace iPark.BL
{
    public class GarageHelper
    {
        private GarageContext context;

        public GarageHelper(GarageContext context)
        {
            this.context = context;
        }
        public List<Parking> GetFreeParking(string vType)
        {
            var results = new List<Parking>();
            var vehicleType = GetVehicleType(vType);
            if (vehicleType != null)
            {
                results = GetFreeParking(vehicleType);
            }
            return results;
        }

        private VehicleType GetVehicleType(string vType)
        {
            return context.VehicleTypes.Where(v => v.Name.ToLower().Trim() == vType.ToLower().Trim()).FirstOrDefault();
        }

        private List<Parking> GetFreeParking(VehicleType vehicleType)
        {
            var results = new List<Parking>();
            var spacesRequired = (vehicleType.SpacesRequired >= 3)? vehicleType.SpacesRequired / 3 : vehicleType.SpacesRequired;
            if (vehicleType.Name.ToLower().Trim() == "mc")
            {

                var mcList = context.Vehicles.Where(v => v.VehicleType.ToString().ToUpper() == EnumEntities.Vtypes.MC.ToString().ToUpper() && v.CheckOut == null).ToList();
                if (mcList.Count == 0)
                {
                    // no mc is parked
                    //results = from parking in context.Parkings from parVeh in context.ParkingVehicles on 
                }
                else
                {

                }
                    
                           
                            
                
            }
            else
            {

            }

            return results;
        }


    }
}