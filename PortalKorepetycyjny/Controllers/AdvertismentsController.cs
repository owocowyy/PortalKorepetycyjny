using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PortalKorepetycyjny.Models;

namespace PortalKorepetycyjny.Controllers
{
    [Authorize]
    public class AdvertismentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Advertisments
        public ActionResult Index()
        {
            var currentUser = db.Coaches.Find(User.Identity.GetUserId());   //Jeśli użytkownik inny niż Coach to redirect na Home
            if(currentUser == null)
            {
                return Redirect("/home");
            }

            return View(db.Advertisments.ToList());
        }

        // GET: Advertisments/Details/5
        [Authorize]
        public ActionResult Details(int? id)
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

            string currentId = id.ToString();

            var model = (from a in db.Advertisments
                         join c in db.Coaches on a.CoachId equals c.Id
                         orderby a.Title
                         where a.CoachId == currentId
                         select new AdvertisementDTO
                         {
                             Id = a.CoachId,
                             CoachNameAndSurname = c.Name + " " + c.Surname,
                             PublicationDate = a.PublicationDate,
                             Title = a.Title,
                             Description = a.Description
                         });

            //return View(model);

            return View(advertisment);
        }

        // GET: Advertisments/Create
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
        public ActionResult Create([Bind(Include = "Title,Description")] Advertisment advertisment)
        {
            if (ModelState.IsValid && User is Coach)
            {

                advertisment.CoachId = User.Identity.GetUserId();
                advertisment.PublicationDate = DateTime.Now;
                db.Advertisments.Add(advertisment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertisment);
        }

        // GET: Advertisments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            var loggedInUser = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisment advertisment = db.Advertisments.Find(id);
            if (advertisment == null)
            {
                return HttpNotFound();
            }
            

            if(advertisment.CoachId!= loggedInUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else {
                return View(advertisment);

            }
            

        }

        // POST: Advertisments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CoachId,Title,PublicationDate,Description")] Advertisment advertisment)
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
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Advertisment advertisment = db.Advertisments.Find(id);
            var loggedInUser = User.Identity.GetUserId();

            if (advertisment.CoachId != loggedInUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else
            {
                return View(advertisment);

            }
            if (advertisment == null)
            {
                return HttpNotFound();
            }
            return View(advertisment);
        }

        // POST: Advertisments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
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
