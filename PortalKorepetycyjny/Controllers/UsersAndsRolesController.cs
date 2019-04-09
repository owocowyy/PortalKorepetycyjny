using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PortalKorepetycyjny.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PortalKorepetycyjny.Controllers
{
    public class UsersAndRolesController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(RegisterViewModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            ApplicationUser user = userManager.FindByName(model.Email);
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = model.Email;
                user.Email = model.Email;

                IdentityResult result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersWithRoles", "UsersAndRoles");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteUser(ApplicationUser model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(model.Id);
            if (User.Identity.GetUserId() == model.Id)
                return RedirectToAction("UsersWithRoles", "UsersAndRoles");

            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            ApplicationUser user = userManager.FindById(id);
            if (user != null)
            {
                IdentityResult result = userManager.Delete(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("UsersWithRoles", "UsersAndRoles");
                }
            }
            return RedirectToAction("UsersWithRoles", "UsersAndRoles");
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            ApplicationUser user = userManager.FindById(id);


            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in roleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(ApplicationUser user, string RoleName)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser savedUser = userManager.FindById(user.Id);

                if (User.Identity.GetUserId() == user.Id)
                {
                    if (!(savedUser.Email.Equals(user.Email)) || !(savedUser.UserName.Equals(user.UserName)))
                        return RedirectToAction("UsersWithRoles", "UsersAndRoles");
                }


                savedUser.Email = user.Email;
                savedUser.UserName = user.UserName;

                userManager.RemoveFromRoles(savedUser.Id, userManager.GetRoles(savedUser.Id).ToArray());
                userManager.AddToRole(savedUser.Id, RoleName);

                userManager.Update(savedUser);

                return RedirectToAction("UsersWithRoles", "UsersAndRoles");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(IdentityRole model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists(model.Name))
            {
                IdentityResult result = roleManager.Create(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfRoles", "UsersAndRoles");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteRole(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role = new IdentityRole(name);

            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        public ActionResult DeleteRole(IdentityRole model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityResult result = roleManager.Delete(roleManager.FindByName(model.Name));

            if (result.Succeeded)
            {
                return RedirectToAction("ListOfRoles", "UsersAndRoles");
            }

            return RedirectToAction("ListOfRoles", "UsersAndRoles");
        }

        [HttpGet]
        public ActionResult EditRole(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationDbContext db = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role = roleManager.FindByName(name);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                IdentityRole savedRole = roleManager.FindById(role.Id);
                savedRole.Name = role.Name;
                roleManager.Update(savedRole);

                return RedirectToAction("ListOfRoles", "UsersAndRoles");
            }
            return View(role);
        }

        public ActionResult UsersWithRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var model = (from p in db.Users select p).ToList();
            var usersWithRoles = (from user in model
                                  select new UsersInRoleViewModel
                                  {
                                      UserId = user.Id,
                                      Email = user.Email,
                                      Name = user.Name,
                                      Surname = user.Surname,
                                  }).ToList();


           for(int i = 0; i < usersWithRoles.Count; i++)
            {
                usersWithRoles.ElementAt(i).AccountType = model.ElementAt(i).GetAccountType();
            }

            return View(usersWithRoles);
        }

        public ActionResult ListOfRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            return View(roleManager.Roles.ToList());
        }
    }
}