using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OS.DataAccess;
using OS.Models;
using OS.ViewModels;

namespace OS.Controllers
{
    public class InitController : Controller
    {

        OSRepo orepo = new OSRepo();
        SYSRepo repo = new SYSRepo();

#region Initialise

        [HttpGet]
        [ActionName("Init")]
        public ActionResult Init(string OSNo)
        {
            OSInit OSInit = new OSInit();
           
            if (Session["LoginUsr"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (OSNo != "" && OSNo != null)
                {
                    OSInit = GetOS(OSNo);
                }
                else
                {
                    OSInit = InitializeOS();
                }

                return View(OSInit);
            }

        }

        public OSInit InitializeOS()
        {
            var SysGoodMsg = "";
            var SysBadMsg = "";
            var LoginUser = (LoginUsr)Session["LoginUsr"];

            OSInit OSInit = new OSInit();
            OSInit.os_Main = new os_Main();
            OSInit.os_LotStatusDetails = new os_LotStatusDetails();
            OSInit.os_SpcBin = new os_SpcBin();
            OSInit.os_ManualAnalysis = new os_ManualAnalysis();
            OSInit.os_XrayAnalysis = new os_XrayAnalysis();
            OSInit.os_AppAnalysis = new os_AppAnalysis();
            OSInit.os_SemAnalysis = new os_SemAnalysis();

            OSInit.os_Main.IssueByEmpNo = LoginUser.EmpNo;
            OSInit.os_Main.IssueByEmpName = LoginUser.EmpName;
            OSInit.os_Main.IssueDate = DateTime.Now;
            OSInit.os_Main.StatusCode = "NEW";
            OSInit.os_Main.StatusDesc = "New Registration";
            OSInit.os_Main.BinJudge = "NA";
            OSInit.os_Main.CtmJudge = "NA";
            OSInit.os_Main.XrayJudge = "NA";
            OSInit.os_Main.AppJudge = "NA";
            OSInit.os_Main.SemJudge = "NA";
            OSInit.os_Main.OverAllJudge = "NA";

            ViewBag.SysGoodMsg = SysGoodMsg;
            ViewBag.SysBadMsg = SysBadMsg;

            return OSInit;
        }

        public OSInit GetOS(string OSNo)
        {
            var SysGoodMsg = "";
            var SysBadMsg = "";
            var LoginUser = (LoginUsr)Session["LoginUsr"];

            OSInit OSInit = new OSInit();
            OSInit.os_Main = orepo.LoadOsMain(OSNo);
            OSInit.os_LotStatusDetails = orepo.LoadLotStatusDetails(OSNo);
            OSInit.os_SpcBin = orepo.LoadSpcBin(OSNo);
            OSInit.os_ManualAnalysis = orepo.LoadManualAnalysis(OSNo);
            OSInit.os_XrayAnalysis = orepo.LoadXrayAnalysis(OSNo);
            OSInit.os_AppAnalysis = orepo.LoadAppAnalysis(OSNo);
            OSInit.os_SemAnalysis = orepo.LoadSemAnalysis(OSNo);

            if (OSInit.os_Main == null)
            {
                OSInit.os_Main = new os_Main();
                SysBadMsg = "Cannot find " + OSNo + " in the database";
            }

            if (OSInit.os_LotStatusDetails == null)
            {
                OSInit.os_LotStatusDetails = new os_LotStatusDetails();
            }

            if (OSInit.os_SpcBin == null)
            {
                OSInit.os_SpcBin = new os_SpcBin();
            }

            if (OSInit.os_ManualAnalysis == null)
            {
                OSInit.os_ManualAnalysis = new os_ManualAnalysis();
            }

            if (OSInit.os_XrayAnalysis == null)
            {
                OSInit.os_XrayAnalysis = new os_XrayAnalysis();
            }

            if (OSInit.os_AppAnalysis == null)
            {
                OSInit.os_AppAnalysis = new os_AppAnalysis();
            }

            if (OSInit.os_SemAnalysis == null)
            {
                OSInit.os_SemAnalysis = new os_SemAnalysis();
            }

            ViewBag.SysGoodMsg = SysGoodMsg;
            ViewBag.SysBadMsg = SysBadMsg;

            return OSInit;
        }

#endregion Initialise

#region Upsert

        [HttpPost]
        [ActionName("Init")]
        public ActionResult UpdateOS(OSInit OSInit)
        {
            var ErrMsg = "";
            var rtn = "";
            var OSNo = "";
            var SysGoodMsg = "";
            var SysBadMsg = "";
            var LoginUser = (LoginUsr)Session["LoginUsr"];

            OSNo = OSInit.os_Main.OSNo;
            OSInit.os_ManualAnalysis.OSNo = OSNo;
            OSInit.os_XrayAnalysis.OSNo = OSNo;

            try
            {
                rtn = UpsertOsMain(OSInit.os_Main, LoginUser.EmpNo);
                if (rtn != "E" && rtn != "")
                {
                    if (OSInit.os_Main.StatusCode == "CA_EXEC_REQ")
                    {
                        rtn = "";
                        rtn = UpsertManualAnalysis(OSInit.os_ManualAnalysis, LoginUser.EmpNo);
                    }

                    if (rtn != "E" && rtn != "" && OSInit.os_Main.StatusCode == "XA_EXEC_REQ")
                    {
                        rtn = "";
                        rtn = UpsertXrayAnalysis(OSInit.os_XrayAnalysis, LoginUser.EmpNo);
                    }
                }
                else
                {
                    SysBadMsg = "Exception Error has been threw in UpsertOSMain Function";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpdateOS", OSInit.os_Main.OSNo, "INIT");
            }

            ModelState.Clear();

            OSInit = new OSInit();
            OSInit = GetOS(OSNo);

            ViewBag.SysGoodMsg = SysGoodMsg;
            ViewBag.SysBadMsg = SysBadMsg;

            return View(OSInit);
        }

        public string UpsertOsMain(os_Main os_Main, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                if (!string.IsNullOrEmpty(os_Main.OSNo))
                {
                    if (string.IsNullOrEmpty(os_Main.IssueByEmpNo))
                    {
                        os_Main.IssueByEmpNo = "470";
                    }

                    if (string.IsNullOrEmpty(os_Main.IssueByEmpName))
                    {
                        os_Main.IssueByEmpName = "OS";
                    }

                    if (string.IsNullOrEmpty(os_Main.PkgID))
                    {
                        os_Main.PkgID = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.PkgDesc))
                    {
                        os_Main.PkgDesc = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.LotCat))
                    {
                        os_Main.LotCat = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.BaseProductName))
                    {
                        os_Main.BaseProductName = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.LotNo))
                    {
                        os_Main.LotNo = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.ProdRank))
                    {
                        os_Main.ProdRank = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.BinJudge))
                    {
                        os_Main.BinJudge = "NA";
                    }

                    if (string.IsNullOrEmpty(os_Main.CtmJudge))
                    {
                        os_Main.CtmJudge = "NA";
                    }

                    if (string.IsNullOrEmpty(os_Main.XrayJudge))
                    {
                        os_Main.XrayJudge = "NA";
                    }

                    if (string.IsNullOrEmpty(os_Main.AppJudge))
                    {
                        os_Main.AppJudge = "NA";
                    }

                    if (string.IsNullOrEmpty(os_Main.SemJudge))
                    {
                        os_Main.SemJudge = "NA";
                    }

                    if (string.IsNullOrEmpty(os_Main.OverAllJudge))
                    {
                        os_Main.OverAllJudge = "NA";
                    }

                    if (string.IsNullOrEmpty(os_Main.OverAllRemark))
                    {
                        os_Main.OverAllRemark = "";
                    }

                    if (string.IsNullOrEmpty(os_Main.StatusCode))
                    {
                        os_Main.StatusCode = "";
                    }

                    rtn = orepo.UpsertOSMain(os_Main, UpdateBy);
                }
                else
                {
                    repo.UpsertSysErrLog("A", "OSNo is empty or null", "UpsertOSMain", os_Main.OSNo, "INIT");
                    rtn = "E";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertOSMain", os_Main.OSNo, "INIT");
                rtn = "E";
            }
            return rtn;
        }

        public string UpsertManualAnalysis(os_ManualAnalysis os_ManualAnalysis, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                if (!string.IsNullOrEmpty(os_ManualAnalysis.OSNo))
                {
                    if (string.IsNullOrEmpty(os_ManualAnalysis.DefectID))
                    {
                        os_ManualAnalysis.DefectID = "";
                    }

                    if (string.IsNullOrEmpty(os_ManualAnalysis.Remark))
                    {
                        os_ManualAnalysis.Remark = "";
                    }

                    if (string.IsNullOrEmpty(os_ManualAnalysis.CompleteBy))
                    {
                        os_ManualAnalysis.CompleteBy = "";
                    }

                    if (string.IsNullOrEmpty(os_ManualAnalysis.DateComplete))
                    {
                        os_ManualAnalysis.DateComplete = "";
                    }

                    rtn = orepo.UpsertManualAnalysis(os_ManualAnalysis, UpdateBy);

                    if (os_ManualAnalysis.CAStatus == true)
                    {
                        orepo.UpdateOSStatus(os_ManualAnalysis.OSNo, "CA");
                        
                        //EAARelease
                    }
                }
                else
                {
                    repo.UpsertSysErrLog("A", "OSNo is empty or null", "UpsertManualAnalysis", os_ManualAnalysis.OSNo, "INIT");
                    rtn = "E";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertManualAnalysis", os_ManualAnalysis.OSNo, "INIT");
                rtn = "E";
            }
            return rtn;
        }

        public string UpsertXrayAnalysis(os_XrayAnalysis os_XrayAnalysis, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                if (!string.IsNullOrEmpty(os_XrayAnalysis.OSNo))
                {
                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectID1))
                    {
                        os_XrayAnalysis.DefectID1 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectID2))
                    {
                        os_XrayAnalysis.DefectID2 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectID3))
                    {
                        os_XrayAnalysis.DefectID3 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectID4))
                    {
                        os_XrayAnalysis.DefectID4 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectID5))
                    {
                        os_XrayAnalysis.DefectID5 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectID6))
                    {
                        os_XrayAnalysis.DefectID6 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectEtc1))
                    {
                        os_XrayAnalysis.DefectEtc1 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectEtc2))
                    {
                        os_XrayAnalysis.DefectEtc2 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectEtc3))
                    {
                        os_XrayAnalysis.DefectEtc3 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectEtc4))
                    {
                        os_XrayAnalysis.DefectEtc4 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectEtc5))
                    {
                        os_XrayAnalysis.DefectEtc5 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DefectEtc6))
                    {
                        os_XrayAnalysis.DefectEtc6 = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.Remark))
                    {
                        os_XrayAnalysis.Remark = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.CompleteBy))
                    {
                        os_XrayAnalysis.CompleteBy = "";
                    }

                    if (string.IsNullOrEmpty(os_XrayAnalysis.DateComplete))
                    {
                        os_XrayAnalysis.DateComplete = "";
                    }

                    rtn = orepo.UpsertXrayAnalysis(os_XrayAnalysis, UpdateBy);

                    if (os_XrayAnalysis.XAStatus == true)
                    {
                        orepo.UpdateOSStatus(os_XrayAnalysis.OSNo, "XA");

                        //EAARelease
                    }
                }
                else
                {
                    repo.UpsertSysErrLog("A", "OSNo is empty or null", "UpsertXrayAnalysis", os_XrayAnalysis.OSNo, "INIT");
                    rtn = "E";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertXrayAnalysis", os_XrayAnalysis.OSNo, "INIT");
                rtn = "E";
            }
            return ErrMsg;
        }

        public string GetCAResult(string OSNo, int TtlAlyQty)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                if (TtlAlyQty > 0)
                {
                    rtn = "NG";
                }
                else
                {
                    rtn = "OK";
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "GetCAResult", OSNo, "INIT");
                rtn = "E";
            }
            return rtn;
        }

        public string GetXAResult(string OSNo, string DefectID1, string DefectID2, string DefectID3, string DefectID4,
                                  string DefectID5, string DefectID6, int DefectQty1, int DefectQty2, int DefectQty3,
                                  int DefectQty4, int DefectQty5, int DefectQty6)
        {
            var ErrMsg = "";
            var rtn = "";
            var Mode = "";
            int High = 0;
            int Medium = 0;
            int Ga = 0;
            int Low = 0;
            int MediumQty = 0;
            string[] DefectID = new string[6];
            int[] DefectQty = new int[6];

            DefectID[0] = DefectID1;
            DefectID[1] = DefectID2;
            DefectID[2] = DefectID3;
            DefectID[3] = DefectID4;
            DefectID[4] = DefectID5;
            DefectID[5] = DefectID6;
            DefectQty[0] = DefectQty1;
            DefectQty[1] = DefectQty2;
            DefectQty[2] = DefectQty3;
            DefectQty[3] = DefectQty4;
            DefectQty[4] = DefectQty5;
            DefectQty[5] = DefectQty6;

            try
            {
                for (int i = 0; i < DefectID.Length; i++)
                {
                    if (DefectID[i].ToString() != "")
                    {
                        Mode = orepo.CheckDefectCriticalMode(OSNo, DefectID[i].ToString(), "XA");
                        if (Mode != "E" && Mode != "")
                        {
                            if (Mode == "H")
                            {
                                High++;
                            }
                            else if (Mode == "M")
                            {
                                Medium++;

                                if (Convert.ToInt32("0" + DefectQty[i].ToString()) > 1)
                                {
                                    MediumQty++;
                                }
                            }
                            else if (Mode == "G")
                            {
                                Ga++;
                            }
                            else
                            {
                                Low++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (Mode != "E" && Mode != "")
                {
                    if (High > 0 && Ga > 0)
                    {
                        rtn = "XA-NG";
                    }
                    else if (High > 0)
                    {
                        rtn = "OA-NG";
                    }
                    else if (Medium > 0 && Ga > 0)
                    {
                        if (MediumQty > 0)
                        {
                            rtn = "XA-NG";
                        }
                        else
                        {
                            rtn = "XA-OK";
                        }
                    }
                    else if (Medium > 0)
                    {
                        if (MediumQty > 0)
                        {
                            rtn = "OA-NG";
                        }
                        else
                        {
                            rtn = "XA-NG";
                        }
                    }
                    else if (Ga > 0)
                    {
                        rtn = "XA-OK";
                    }
                    else
                    {
                        rtn = "OA-OK";
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "GetXAResult", OSNo, "INIT");
                rtn = "E";
            }
            return rtn;
        }

        public string GetAAResult(string OSNo, string DefectID1, string DefectID2, string DefectID3, string DefectID4,
                          string DefectID5, string DefectID6, int DefectQty1, int DefectQty2, int DefectQty3,
                          int DefectQty4, int DefectQty5, int DefectQty6)
        {
            var ErrMsg = "";
            var rtn = "";
            var Mode = "";
            int High = 0;
            int Medium = 0;
            int Ga = 0;
            int Low = 0;
            int MediumQty = 0;
            string[] DefectID = new string[6];
            int[] DefectQty = new int[6];

            DefectID[0] = DefectID1;
            DefectID[1] = DefectID2;
            DefectID[2] = DefectID3;
            DefectID[3] = DefectID4;
            DefectID[4] = DefectID5;
            DefectID[5] = DefectID6;
            DefectQty[0] = DefectQty1;
            DefectQty[1] = DefectQty2;
            DefectQty[2] = DefectQty3;
            DefectQty[3] = DefectQty4;
            DefectQty[4] = DefectQty5;
            DefectQty[5] = DefectQty6;

            try
            {
                for (int i = 0; i < DefectID.Length; i++)
                {
                    if (DefectID[i].ToString() != "")
                    {
                        Mode = orepo.CheckDefectCriticalMode(OSNo, DefectID[i].ToString(), "AA");
                        if (Mode != "E" && Mode != "")
                        {
                            if (Mode == "H")
                            {
                                High++;
                            }
                            else if (Mode == "M")
                            {
                                Medium++;

                                if (Convert.ToInt32("0" + DefectQty[i].ToString()) > 1)
                                {
                                    MediumQty++;
                                }
                            }
                            else if (Mode == "G")
                            {
                                Ga++;
                            }
                            else
                            {
                                Low++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (Mode != "E" && Mode != "")
                {
                    if (High > 0 && Ga > 0)
                    {
                        rtn = "AA-NG";
                    }
                    else if (High > 0)
                    {
                        rtn = "OA-NG";
                    }
                    else if (Medium > 0 && Ga > 0)
                    {
                        if (MediumQty > 0)
                        {
                            rtn = "AA-NG";
                        }
                        else
                        {
                            rtn = "AA-OK";
                        }
                    }
                    else if (Medium > 0)
                    {
                        if (MediumQty > 0)
                        {
                            rtn = "OA-NG";
                        }
                        else
                        {
                            rtn = "AA-NG";
                        }
                    }
                    else if (Ga > 0)
                    {
                        rtn = "AA-OK";
                    }
                    else
                    {
                        rtn = "OA-OK";
                    }
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "GetAAResult", OSNo, "INIT");
                rtn = "E";
            }
            return rtn;
        }

#endregion Upsert

    }
}