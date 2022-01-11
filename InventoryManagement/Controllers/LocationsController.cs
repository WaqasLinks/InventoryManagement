using InventoryPortal.Models;
using System;
using System.Collections.Generic;
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
            List<Location> locations=  db.Locations.ToList<Location>();
            return View(locations);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Location location)
        {
            location.DateCreated = DateTime.Now;
            
            db.Locations.Add(location);
            db.SaveChanges();
            return View();
        }


    }
}