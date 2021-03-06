using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using OS.DataAccess;
using System.Web.Mvc;
using OS.ViewModels;

namespace OS.Controllers
{
    public class LoginController : Controller
    {

        SYSRepo repo = new SYSRepo();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUsr Usr)
        {
            if (Usr.LoginID == null && Usr.LoginPsswrd == null)
            {
                return View();
            }

            LoginUsr LoginUsr = repo.LoginValidation(Usr.LoginID, Usr.LoginPsswrd);
            if (LoginUsr == null)
            {
                ViewBag.SysBadMsg = "NG";
                return View(LoginUsr);
            }
            else
            {
                Session["LoginUsr"] = LoginUsr;

                if (LoginUsr.PosID == "OPR")
                {
                    return RedirectToAction("Init", "Init");
                }
                else
                {
                    return RedirectToAction("Main", "Main");
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View("Login");
        }
    }
}