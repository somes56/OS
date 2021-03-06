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
    public class MainController : Controller
    {

        SYSRepo repo = new SYSRepo();
        OSRepo orepo = new OSRepo();

        public ActionResult Main()
        {
            if (Session["LoginUsr"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult LoadMainList()
        {
            var LoginUsr = (LoginUsr)Session["LoginUsr"];
            var PosID = LoginUsr.PosID.ToString();

            IList<MainList> MainList = orepo.LoadMainList(PosID);

            return View("PartialMainList", MainList);
        }

        public string UpdateLotMovementHist(string OSNo)
        {
            var rtn = "";
            var UpdateBy = "";
            var ErrMsg = "";
            var LoginUser = (LoginUsr)Session["LoginUsr"];

            UpdateBy = LoginUser.EmpNo;

            try
            {
                rtn = orepo.UpdateLotMovementHist(OSNo, "Y", UpdateBy, UpdateBy);
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpdateLotMovementHist", "", "MAIN");
                rtn = "E";
            }
            return rtn;
        }
    }
}