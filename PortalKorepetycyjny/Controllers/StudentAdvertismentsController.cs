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
    public class StudentAdvertismentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentAdvertisments
        public ActionResult Index()
        {
            return View(db.StudentAdvertisments.ToList());
        }

        // GET: StudentAdvertisments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAdvertisment studentAdvertisment = db.StudentAdvertisments.Find(id);
            if (studentAdvertisment == null)
            {
                return HttpNotFound();
            }
            return View(studentAdvertisment);
        }

        // GET: StudentAdvertisments/Create
        public ActionResult Create()
        {
            //ViewData["Subjects"] = db.Subjects.Select(s => s.Name).ToList();
            List<SelectListItem> subjects = new List<SelectListItem>();
            foreach(var sub in db.Subjects)
            {
                subjects.Add(new SelectListItem { Text = sub.Name, Value = sub.Id.ToString() });
            }
            ViewBag.SbujectName = subjects;
            return View();
        }

        // POST: StudentAdvertisments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Descryption,Title")] StudentAdvertisment studentAdvertisment,int SbujectName)
        {
            if (ModelState.IsValid)
            {
                studentAdvertisment.CreatorId = User.Identity.GetUserId();
                studentAdvertisment.AdvertismentDate = DateTime.Now;
                studentAdvertisment.SbujectName = db.Subjects.FirstOrDefault(s => s.Id == SbujectName);
                db.StudentAdvertisments.Add(studentAdvertisment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        // GET: StudentAdvertisments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAdvertisment studentAdvertisment = db.StudentAdvertisments.Find(id);
            if (studentAdvertisment == null)
            {
                return HttpNotFound();
            }
            return View(studentAdvertisment);
        }

        // POST: StudentAdvertisments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descryption,Title,AdvertismentDate")] StudentAdvertisment studentAdvertisment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAdvertisment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentAdvertisment);
        }

        // GET: StudentAdvertisments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAdvertisment studentAdvertisment = db.StudentAdvertisments.Find(id);
            if (studentAdvertisment == null)
            {
                return HttpNotFound();
            }
            return View(studentAdvertisment);
        }

        // POST: StudentAdvertisments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAdvertisment studentAdvertisment = db.StudentAdvertisments.Find(id);
            db.StudentAdvertisments.Remove(studentAdvertisment);
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
