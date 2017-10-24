using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iPark.DAL;
using iPark.Models;

namespace iPark.Controllers
{
    public class VehiclesController : Controller
    {
        private GarageContext db = new GarageContext();


        // Shows the Partial View of vehicles which are in the Garage
        // GET: Vehicles
        public ActionResult Index(string sort, string searchRegNo, string searchVehichleType, string searchCheckIn, string searchCheckOut)
        {
            ViewBag.sort = String.IsNullOrEmpty(sort) ? "" : sort;
            ViewBag.searchRegNo = String.IsNullOrEmpty(searchRegNo) ? "" : searchRegNo;
            ViewBag.searchVehichleType = String.IsNullOrEmpty(searchVehichleType) ? "" : searchVehichleType;
            ViewBag.searchCheckIn = String.IsNullOrEmpty(searchCheckIn) ? "" : searchCheckIn;
            ViewBag.searchCheckOut = String.IsNullOrEmpty(searchCheckOut) ? "" : searchCheckOut;
            var dbVehicles = db.Vehicles;
            List<Vehicle> vehicles;
            if (sort != null)
            {
                vehicles = GetSorted(sort);
            }
            else
            {
                vehicles = dbVehicles.ToList();
            }
            if (!String.IsNullOrEmpty(searchRegNo))
            {
                vehicles = vehicles.Where(e => e.RegNo.ToLower().StartsWith(searchRegNo.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(searchVehichleType))
            {

                if (EnumEntities.Vtypes.BUS.ToString().ToLower() == searchVehichleType.ToLower())
                    vehicles = vehicles.Where(e => e.VehichleType.ToString().ToLower() == EnumEntities.Vtypes.BUS.ToString().ToLower()).ToList();
                if (EnumEntities.Vtypes.CAR.ToString().ToLower() == searchVehichleType.ToLower())
                    vehicles = vehicles.Where(e => e.VehichleType.ToString().ToLower() == EnumEntities.Vtypes.CAR.ToString().ToLower()).ToList();
                if (EnumEntities.Vtypes.MC.ToString().ToLower() == searchVehichleType.ToLower())
                    vehicles = vehicles.Where(e => e.VehichleType.ToString().ToLower() == EnumEntities.Vtypes.MC.ToString().ToLower()).ToList();
                if (EnumEntities.Vtypes.VAN.ToString().ToLower() == searchVehichleType.ToLower())
                    vehicles = vehicles.Where(e => e.VehichleType.ToString().ToLower() == EnumEntities.Vtypes.VAN.ToString().ToLower()).ToList();


            }
            if (!String.IsNullOrEmpty(searchCheckIn))
            {
               vehicles = vehicles.Where(e => e.CheckIn.CompareTo(System.DateTime.Parse(searchCheckIn)) > 0).ToList();
            }
            if (!String.IsNullOrEmpty(searchCheckOut))
            {
                vehicles = vehicles.Where(e => e.CheckOut != null && System.DateTime.Parse(e.CheckOut.ToString()).CompareTo(System.DateTime.Parse(searchCheckOut)) > 0).ToList();
            }
            return View(vehicles);
        }

        // Shows the Detailed View of vehicles parked in Garage
        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // Shows the "Park a new vehicle" View
        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // Parks a new vehicle - the data are created HERE!!
        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegNo,Color,VehichleType,Make,Model,Wheels")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.CheckIn = System.DateTime.Now;  // We add the TimeStamp!
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // Shows "Modify Vehicle data View"
        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // Modify the Vehicle data (but NOT Check out/in timestamps!)
        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNo,Color,VehichleType,Make,Model,Wheels,CheckIn,CheckOut")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if (vehicle.CheckIn == null)
                {
                    vehicle.CheckIn = System.DateTime.Now;
                }
                vehicle.CheckOut = null;
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5  (DELETE)
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5 (DELETE)
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut([Bind(Include = "Id,RegNo,Color,VehichleType,Make,Model,Wheels")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.CheckOut = System.DateTime.Now;
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["Receipt"] == "on")
                {
                    return RedirectToAction("Receipt");
                }
                //else
               // {
                    return RedirectToAction("Index");
               // }
                
            }
            return View(vehicle);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<Vehicle> GetSorted(string sort)
        {
            List<Vehicle> results = new List<Vehicle>();
            switch (sort)
            {
                case "0":
                    results = db.Vehicles.OrderBy(e => e.RegNo).ToList();
                    break;
                case "1":
                    results = db.Vehicles.OrderBy(e => e.VehichleType).ToList();
                    break;
                case "2":
                    results = db.Vehicles.OrderBy(e => e.CheckIn).ToList();
                    break;
                case "3":
                    results = db.Vehicles.OrderBy(e => e.CheckOut).ToList();
                    break;
                default:
                    results = db.Vehicles.ToList();
                    break;

            }

            return results;
        }
    }
}
