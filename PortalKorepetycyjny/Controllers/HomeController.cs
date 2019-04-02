using PagedList;
using PortalKorepetycyjny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalKorepetycyjny.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchTerm, int page = 1)
        {
            /*var model = db.Advertisments
                .OrderBy(r => r.PublicationDate)
                .Where(r => searchTerm == null || r.Description.Contains(searchTerm))
                .Select(r => new AdvertisementDTO
                {
                    Id = r.CoachId,
                    CoachNameAndSurname = r.Coach.Name + " " + r.Coach.Surname,
                    Title = r.Title,
                    Description = r.Description
                }).ToPagedList(page, 10);*/

            /*var model = (from a in db.Advertisments
                         join c in db.Coaches on a.CoachId equals c.Id
                         orderby a.Title
                        where a.CoachId == c.Id
                         select new AdvertisementDTO
                         {
                             Id = a.CoachId,
                             CoachNameAndSurname = c.Name + " " + c.Surname,
                             PublicationDate = a.PublicationDate,
                             Title = a.Title,
                             Description = a.Description
                         }).ToPagedList(page, 10);*/

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Advertisements", db.Advertisments.OrderBy(r => r.Title).ToPagedList(page,10));
            }
            return View(db.Advertisments.OrderBy(r => r.Title).ToPagedList(page,10));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}