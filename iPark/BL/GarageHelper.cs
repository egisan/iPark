﻿using System;
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

       
        public Parking CheckFreeParking(int id)
        {
            //var result = context.Parkings.Join(context.ParkingVehicles, p => p.Id, pv => pv.ParkingId,
            //   (p, pv) => new
            //   {
            //       Parking = p,
            //       ParkingVehicle = pv
            //   }).Where(p => p.Parking.Id ==1).ToList();


            var query = (from p in context.Parkings
                        join pv in context.ParkingVehicles on p.Id equals pv.ParkingId
                        join v in context.Vehicles on pv.VehicleId equals v.Id
                        where p.Id == id && v.CheckOut == null
                        select p).ToList().FirstOrDefault();

            return query;
        }

        public bool VehicleAlreadyParked(string regNo)
        {
            bool isParked = true;
            var vehicle = context.Vehicles.Where(v => v.RegNo.ToLower() == regNo.ToLower() && v.CheckOut == null).FirstOrDefault();
            if (vehicle == null)
                isParked = false;
            return isParked;   
        }

        private string GetVehicleParkingNo(Vehicle vehicle)
        {
            var result = "";
            

            return result;
        }

        private Parking GetFirstFreeParking()
        {
            foreach (var parking in context.Parkings)
            {
                var freeParking = CheckFreeParking(parking.Id);
                if (freeParking != null && freeParking.Id != 0)
                    return parking;
            }
             return new Parking();
        }

        private Parking GetFirstFreeParking(List<Parking> freeParking)
        {

            foreach (var parking in context.Parkings)
            {
                if (freeParking.Where(fp => fp.Id == parking.Id).Count() == 0 )
                {
                    foreach (var parkingVehicle in context.ParkingVehicles.Where(pv => pv.ParkingId == parking.Id).ToList())
                    {
                        if (context.Vehicles.Where(v => v.Id == parkingVehicle.VehicleId && v.CheckOut == null).ToList().Count == 0)
                        {
                            return parking;
                        }
                    }
                }
                
            }
            return new Parking();
        }
        private Parking GetFirstFreeParking(List<Parking> freeParking, Parking parking)
        {

            if(parking != null && parking.Id > 0)
            {
                if (freeParking.Where(fp => fp.Id == parking.Id).Count() == 0)
                {
                    foreach (var parkingVehicle in context.ParkingVehicles.Where(pv => pv.ParkingId == parking.Id).ToList())
                    {
                        if (context.Vehicles.Where(v => v.Id == parkingVehicle.VehicleId && v.CheckOut == null).ToList().Count == 0)
                        {
                            return parking;
                        }
                    }
                }

            }
            return new Parking();
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

        public VehicleType GetVehicleType(string vType)
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
                    // get first free parking
                    var parking = GetFirstFreeParking();
                    if (parking != null)
                    {
                        results.Add(parking);
                        return results;
                    }
                    else
                    {
                        //full message
                    }
                    
                }
                else
                {
                    //MC parked and not checke out then try to find a not full mc parking

                }
            }
            else
            {
                // other vehicle types
                bool isNotFull = true;
                var lastUsedParkingNo = 0;
                while(results.Count < spacesRequired && isNotFull)
                {
                    Parking parking = null;
                    if (lastUsedParkingNo == 0)
                     parking = GetFirstFreeParking();
                    else
                    {
                        parking = GetAdjacentFreeParking(lastUsedParkingNo);
                        if (parking != null)
                        {
                            parking = GetFirstFreeParking(results, parking);
                        }

                    }
                    if (parking != null && parking.Id > 0)
                    {
                        results.Add(parking);
                        lastUsedParkingNo = parking.ParkingNo + 1;
                        var adjacentFreeList = context.Parkings.Where(p => p.ParkingNo > parking.ParkingNo && p.ParkingNo < parking.ParkingNo + spacesRequired).ToList();
                        foreach (var aParking in adjacentFreeList)
                        {
                           var foundFreeParking =  GetFirstFreeParking(results, aParking);
                            if (foundFreeParking != null && foundFreeParking.Id > 0)
                            {
                                //free found
                                results.Add(foundFreeParking);
                            }
                            else
                            {
                                lastUsedParkingNo = aParking.ParkingNo;
                                break;
                            }
                                
                        }
                        if (results.Count != spacesRequired)
                        {
                            results.Clear();
                        }
                        else
                        {
                            return results;
                        }
                        

                    }
                    else
                    {
                        isNotFull = false;
                    }
                }
            }

            return results;
        }

        private Parking GetAdjacentFreeParking(int lastParkingNo)
        {
            Parking parking = new Parking();
            parking = context.Parkings.Where(p => p.ParkingNo == lastParkingNo + 1).FirstOrDefault();
            return parking;
        }
    }
}