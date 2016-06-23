using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        TravelDBEntities1 tDB = new TravelDBEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult AdminPanelDest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminPanelDest(destination d)
        {
            string id = Request["Id"];
            destination dest = tDB.destinations.Find(id);
            if (dest != null)
            {
                ModelState.AddModelError("id", "Destination Already Exists!");
                return View(d);
            }
            if (ModelState.IsValid)
            {
                //for (int i = 0; i < Request.Files.Count; i++)
                //{
                //    HttpPostedFileBase file = Request.Files[i];
                //    file.SaveAs(Server.MapPath(@"~\Files\" + file.FileName));
                //    user.imgpath = Server.MapPath(@"~\Files\" + file.FileName);
                //}
                tDB.destinations.Add(d);
                tDB.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(d);
        }
        public ActionResult AdminPanelOffer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminPanelOffer(offer o)
        {
            string id = Request["Id"];
            offer of = tDB.offers.Find(id);
            if (of != null)
            {
                ModelState.AddModelError("id", "Destination Already Exists!");
                return View(o);
            }
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    file.SaveAs(Server.MapPath(@"~\Files\" + file.FileName));
                    o.imgpath = file.FileName;
                }
                tDB.offers.Add(o);
                tDB.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(o);
        }
        public ActionResult AdminPanelUser()
        {
            return View(tDB.users.ToList());
        }
        public ActionResult Edit(string email)
        {
            user u = tDB.users.Find(email);
            if (u == null)
                return HttpNotFound();
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(user u)
        {
            tDB.Entry(u).State = EntityState.Modified;
            tDB.SaveChanges();
            return RedirectToAction("AdminPanelUser");
        }
        public JsonResult checkUser()
        {
            string email = Request["email"];
            user u = tDB.users.Find(email);


            if (u == null)
            {
                return this.Json(false, JsonRequestBehavior.AllowGet);
            }
            else
                return this.Json(u.qID, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPass()
        {
            string email = Request["email"];
            string answer = Request["ans"];
            user u = tDB.users.Find(email);
            if (answer.Equals(u.answer))
                return this.Json(u.password, JsonRequestBehavior.AllowGet);
            else
                return this.Json("Invalid Answer", JsonRequestBehavior.AllowGet);

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user user)
        {
            string userNameCheck = Request["email"];
            string pass = Request["password"];

            user u = tDB.users.Find(userNameCheck);
            string type = "user";

            //if (ModelState.IsValid)
            //if (userNameCheck == null || pass == null)
            //{
            //    ModelState.AddModelError("email", "This field is required");
            //    return View(user);
            //}
            if (u != null)
            {
                if (userNameCheck.Equals(u.email) && pass.Equals(u.password))
                {
                    if (type.Equals(u.type))
                    {
                        u.Session_Start(u);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("AdminPanel");
                    }
                }

                else
                {
                    return View(user);
                }
            }
            return View(user);
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(user user)
        {
            string email = Request["email"];
            user u = tDB.users.Find(email);
            if (u != null)
            {
                ModelState.AddModelError("email", "User Already Exists!");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    file.SaveAs(Server.MapPath(@"~\Files\" + file.FileName));
                    user.imgpath = file.FileName;
                }
                tDB.users.Add(user);
                tDB.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Plans()
        {
            return View(tDB.offers.ToList());
        }
        public ActionResult Services()
        {
            return View();
        }
    }
}