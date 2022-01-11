using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryPortal.Models;

namespace InventoryManagement.Controllers
{
    public class ItemsController : Controller
    {
        private InventoryManagementEntities db = new InventoryManagementEntities();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.DeviceType).Include(i => i.Location).Include(i => i.Status);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.DeviceTypeId = new SelectList(db.DeviceTypes, "Id", "Type");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "SatutsName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Barcode,SerialNumber,Manufacturer,Model,Description,ReceivingDate,WarrantyExpiryDate,Remarks,DateCreated,DateModified,DeviceTypeId,LocationId,StatusId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceTypeId = new SelectList(db.DeviceTypes, "Id", "Type", item.DeviceTypeId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName", item.LocationId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "SatutsName", item.StatusId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceTypeId = new SelectList(db.DeviceTypes, "Id", "Type", item.DeviceTypeId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName", item.LocationId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "SatutsName", item.StatusId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Barcode,SerialNumber,Manufacturer,Model,Description,ReceivingDate,WarrantyExpiryDate,Remarks,DateCreated,DateModified,DeviceTypeId,LocationId,StatusId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceTypeId = new SelectList(db.DeviceTypes, "Id", "Type", item.DeviceTypeId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "LocationName", item.LocationId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "SatutsName", item.StatusId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
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
