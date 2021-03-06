using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OS.DataAccess;
using OS.Models;
using OS.ViewModels;

namespace OS.Controllers
{
    public class CmnController : Controller
    {

        SYSRepo repo = new SYSRepo();

        OSRepo orepo = new OSRepo();

        public ActionResult AdvSearchOS(string str)
        {
            IEnumerable<OSLot> OSLot = repo.AdvSearchOS(str);

            return View("PartialAdvSearchOS", OSLot);
        }

        public ActionResult AdvSearchDefect(string str, string p1)
        {
            IEnumerable<CodeDescription> CodeDescription = repo.AdvSearchDefect(str, p1);

            return View("PartialAdvSearchOSDefect", CodeDescription);
        }

        public ActionResult LoadUploadFile(string OSNo, string OSScope)
        {
            IEnumerable<OSRef> OSRef = orepo.LoadRef(OSNo, OSScope);
            return View("PartialXARefFile", OSRef);

        }

        public void UploadFile(string OSNo, string OSScope)
        {
            var ErrMsg = "";
            var rtn = "";
            var LoginUser = (LoginUsr)Session["LoginUsr"];
            var RefFileServerPhysicalPath = Server.MapPath("~/OSRefFiles/" + OSNo);
            var RefFileServerVirtualPath = @"~/OSRefFiles/" + OSNo + @"/";
            
            try
            {
                foreach (string F in Request.Files)
                {
                    var FContent = Request.Files[F];

                    if (FContent != null && FContent.ContentLength > 0)
                    {
                        var RefFileName = Path.GetFileName(FContent.FileName);
                        var RefFileExt = Path.GetExtension(FContent.FileName);

                        if (System.IO.File.Exists(RefFileServerPhysicalPath + @"/" + RefFileName) != true)
                        {
                            if (!Directory.Exists(RefFileServerPhysicalPath))
                            {
                                Directory.CreateDirectory(RefFileServerPhysicalPath);
                            }

                            FContent.SaveAs(Path.Combine(RefFileServerPhysicalPath, RefFileName));

                            if (System.IO.File.Exists(RefFileServerPhysicalPath + @"/" + RefFileName) == true)
                            {
                                rtn = orepo.UpsertRefFile(OSNo, OSScope, RefFileName, RefFileServerVirtualPath, RefFileExt, "", LoginUser.EmpNo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UploadFile", OSNo, "CMN");
            }
        }

        public void UpdateRefFileComment(string OSNo, string OSScope, string FileName, string Comment)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                rtn = orepo.UpdateRefFileComment(OSNo, OSScope, FileName, Comment);
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpdateRefFileComment", OSNo, "CMN");
            }
        }

        public void DeleteUploadFile(string OSNo, string OSScope, string FileName)
        {
            var ErrMsg = "";
            var rtn = "";
            var RefFileServerPhysicalPath = Server.MapPath("~/OSRefFiles/" + OSNo);

            try
            {
                if (System.IO.File.Exists(RefFileServerPhysicalPath + @"/" + FileName) != false)
                {
                    System.IO.File.Delete(RefFileServerPhysicalPath + @"/" + FileName);

                    if (System.IO.File.Exists(RefFileServerPhysicalPath + @"/" + FileName) != true)
                    {
                        rtn = orepo.DelRefFile(OSNo, OSScope, FileName);
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "DeleteUploadFile", OSNo, "CMN");
            }
        }
    }
}