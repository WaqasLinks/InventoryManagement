using InventoryPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{

    public class LocationsController : Controller
    {
        private InventoryManagementEntities db = new InventoryManagementEntities();
        // GET: Locations
        public ActionResult Index()
        {
            List<Location> locations = db.Locations.ToList<Location>();
            return View(locations);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Location location)
        {
            //location.Id = Guid.NewGuid().ToString();
            location.DateCreated = DateTime.Now;

            db.Locations.Add(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            Location location = db.Locations.Find(id);
            return View(location);
        }
        [HttpPost]
        public ActionResult Edit(Location location)
        {
            location.DateModified = DateTime.Now;

            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Location location = db.Locations.Find(id);
            return View(location);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Location location = db.Locations.Find(id);

            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}