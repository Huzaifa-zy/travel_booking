using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        TravelDBEntities1 tDB = new TravelDBEntities1();
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(user user)
        {


            if (ModelState.IsValid)
            {
                tDB.users.Add(user);
                tDB.SaveChanges();
                return RedirectToAction("ForgotPassword");
            }


            return View(user);
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}