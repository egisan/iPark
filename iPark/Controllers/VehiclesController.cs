﻿using System;
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
        public ActionResult Index()
        {
            return View(db.Vehicles.ToList());
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
    }
}
