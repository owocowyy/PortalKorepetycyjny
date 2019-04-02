using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PortalKorepetycyjny.Models;

namespace PortalKorepetycyjny.Controllers
{
    public class AdvertismentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Advertisments
        public ActionResult Index()
        {
            return View(db.Advertisments.ToList());
        }

        // GET: Advertisments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisment advertisment = db.Advertisments.Find(id);
            if (advertisment == null)
            {
                return HttpNotFound();
            }
            return View(advertisment);
        }

        // GET: Advertisments/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advertisments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,PublicationDate,Description")] Advertisment advertisment)
        {
            if (ModelState.IsValid)
            {
                advertisment.Id = Guid.NewGuid();
                advertisment.PublicationDate = DateTime.Now;
                var userName = User.Identity.GetUserId();
                advertisment.CoachId = db.Coaches.FirstOrDefault(c => c.Id == userName);
                db.Advertisments.Add(advertisment);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertisment);
        }

        // GET: Advertisments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisment advertisment = db.Advertisments.Find(id);
            if (advertisment == null)
            {
                return HttpNotFound();
            }
            return View(advertisment);
        }

        // POST: Advertisments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,PublicationDate,Description")] Advertisment advertisment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertisment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advertisment);
        }

        // GET: Advertisments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisment advertisment = db.Advertisments.Find(id);
            if (advertisment == null)
            {
                return HttpNotFound();
            }
            return View(advertisment);
        }

        // POST: Advertisments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Advertisment advertisment = db.Advertisments.Find(id);
            db.Advertisments.Remove(advertisment);
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
