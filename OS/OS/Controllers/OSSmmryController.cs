using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OS.Models;
using OS.ViewModels;

namespace OS.Controllers
{
    public class OSSmmryController : Controller
    {
        // GET: OSSmmry
        public ActionResult OSSmmry()
        {
            OSSmmry OSSmmry = new OSSmmry();
            OSSmmry.os_Main = new os_Main();
            OSSmmry.os_Main.StatusDesc = "NEW";

            return View(OSSmmry);
        }
    }
}