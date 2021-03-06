using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using OS.Models;
using OS.ViewModels;
using Dapper;

namespace OS.DataAccess
{
    public class OSRepo
    {

        SYSRepo repo = new SYSRepo();

        string connStr = ConfigurationManager.ConnectionStrings["IMS_OS_DEFAULT"].ConnectionString;

        public string CheckDefectCriticalMode(string OSNo, string DefectID, string OSScope)
        {
            var ErrMsg = "";
            var rtn = "E";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    rtn = conn.Query<string>("spOs_CheckDefectCriticalMode", 
                                            new {
                                                    DefectID = DefectID,
                                                    OSScope = OSScope
                                                }, commandType: CommandType.StoredProcedure).SingleOrDefault().Trim();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "CheckDefectCriticalMode", OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string UpsertOSMain(os_Main os_Main, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpsertOSMain", 
                                new {
                                        OSNo = os_Main.OSNo,
                                        IssueDate = os_Main.IssueDate,
                                        IssueBy = os_Main.IssueByEmpNo,
                                        PkgID = os_Main.PkgID,
                                        LotCat = os_Main.LotCat,
                                        LotNo = os_Main.LotNo,
                                        ProdRank = os_Main.ProdRank,
                                        BaseProductName = os_Main.BaseProductName,
                                        LotQty = os_Main.LotQty,
                                        BinJudge = os_Main.BinJudge,
                                        CtmJudge = os_Main.CtmJudge,
                                        XrayJudge = os_Main.XrayJudge,
                                        AppJudge = os_Main.AppJudge,
                                        SemJudge = os_Main.SemJudge,
                                        OverAllJudge = os_Main.OverAllJudge,
                                        OverAllRemark = os_Main.OverAllRemark,
                                        UpdateBy = UpdateBy
                                    }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertOSMain", os_Main.OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string UpsertLotStatusDetails(os_LotStatusDetails os_LotStatusDetails, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpsertLotStatusDetails", 
                                new {
                                        OSNo = os_LotStatusDetails.OSNo,
                                        LotID = os_LotStatusDetails.LotID,
                                        StepCode = os_LotStatusDetails.StepCode,
                                        Inquiry = os_LotStatusDetails.Inquiry,
                                        Start = os_LotStatusDetails.Start,
                                        Finish = os_LotStatusDetails.Finish,
                                        UpdateBy = UpdateBy
                                    }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertLotStatusDetails", os_LotStatusDetails.OSNo, "OSRepo");
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
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpsertManualAnalysis", 
                                new {
                                        OSNo = os_ManualAnalysis.OSNo,
                                        PassQty = os_ManualAnalysis.PassQty,
                                        OpenQty = os_ManualAnalysis.OpenQty,
                                        ShortQty = os_ManualAnalysis.ShortQty,
                                        TtlAlyQty = os_ManualAnalysis.TtlAlyQty,
                                        DefectQty = os_ManualAnalysis.DefectQty,
                                        DefectID = os_ManualAnalysis.DefectID,
                                        Remark = os_ManualAnalysis.Remark,
                                        CAStatus = os_ManualAnalysis.CAStatus,
                                        UpdateBy = UpdateBy
                                    }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertManualAnalysis", os_ManualAnalysis.OSNo, "OSRepo");
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
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpsertXrayAnalysis", 
                                new {
                                        OSNo = os_XrayAnalysis.OSNo,
                                        DefectID1 = os_XrayAnalysis.DefectID1,
                                        DefectID2 = os_XrayAnalysis.DefectID2,
                                        DefectID3 = os_XrayAnalysis.DefectID3,
                                        DefectID4 = os_XrayAnalysis.DefectID4,
                                        DefectID5 = os_XrayAnalysis.DefectID5,
                                        DefectID6 = os_XrayAnalysis.DefectID6,
                                        DefectQty1 = os_XrayAnalysis.DefectQty1,
                                        DefectQty2 = os_XrayAnalysis.DefectQty2,
                                        DefectQty3 = os_XrayAnalysis.DefectQty3,
                                        DefectQty4 = os_XrayAnalysis.DefectQty4,
                                        DefectQty5 = os_XrayAnalysis.DefectQty5,
                                        DefectQty6 = os_XrayAnalysis.DefectQty6,
                                        DefectEtc1 = os_XrayAnalysis.DefectEtc1,
                                        DefectEtc2 = os_XrayAnalysis.DefectEtc2,
                                        DefectEtc3 = os_XrayAnalysis.DefectEtc3,
                                        DefectEtc4 = os_XrayAnalysis.DefectEtc4,
                                        DefectEtc5 = os_XrayAnalysis.DefectEtc5,
                                        DefectEtc6 = os_XrayAnalysis.DefectEtc6,
                                        Remark = os_XrayAnalysis.Remark,
                                        XAStatus = os_XrayAnalysis.XAStatus,
                                        UpdateBy = UpdateBy
                                }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertXrayAnalysis", os_XrayAnalysis.OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string UpsertRefFile(string OSNo, string OSScope, string FileName, string FilePath, string FileType, string Comment, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpsertRef", 
                                new {
                                        OSNo = OSNo,
                                        OSScope = OSScope,
                                        FileName = FileName,
                                        FilePath = FilePath,
                                        FileType = FileType,
                                        Comment = Comment,
                                        UpdateBy = UpdateBy
                                    }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpsertRefFile", OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string UpdateOSStatus(string OSNo, string OSScope)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpdateOSStatus", 
                                new {
                                        OSNo = OSNo,
                                        OSScope = OSScope
                                    }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpdateOSStatus", OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string UpdateLotMovementHist(string OSNo, string LotReceive, string LotReceiveBy, string UpdateBy)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spOs_UpdateLotMovementHist",
                                new
                                {
                                    OSNo = OSNo,
                                    LotReceive = LotReceive,
                                    LotReceiveBy = LotReceiveBy,
                                    UpdateBy = UpdateBy
                                }, commandType: CommandType.StoredProcedure);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpdateLotMovementHist", OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string UpdateRefFileComment(string OSNo, string OSScope, string FileName, string Comment)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                string sqlstr = "UPDATE [dbo].[os_Ref] SET Comment = '" + Comment + "' WHERE OSNo = '" + OSNo + "' "
                                + "AND OSScope = '" + OSScope + "' AND [FileName] = '" + FileName + "' ";

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sqlstr);
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "UpdateRefFileComment", OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public string DelRefFile(string OSNo, string OSScope, string FileName)
        {
            var ErrMsg = "";
            var rtn = "";

            try
            {
                string sqlstr = "DELETE  FROM [dbo].[os_Ref] WHERE OSNo = '"+ OSNo +"' AND OSScope = '"+ OSScope +"' AND [FileName] = '"+ FileName +"'";

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute(sqlstr); 
                }
                rtn = "OK";
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "DelRefFile", OSNo, "OSRepo");
                rtn = "E";
            }
            return rtn;
        }

        public IList<MainList> LoadMainList(string PosID)
        {
            var ErrMsg = "";
            IList<MainList> MainList = new List<MainList>();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    MainList = conn.Query<MainList>("spOs_LoadMainList", new { PosID = PosID }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadMainList", "", "OSRepo");
                MainList.Clear();
            }
            return MainList;
        }

        public os_Main LoadOsMain(string OSNo)
        {
            var ErrMsg = "";
            os_Main os_Main = new os_Main();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_Main = conn.Query<os_Main>("spOs_LoadOsMain", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadOsMain", OSNo, "OSRepo");
                os_Main = null;
            }
            return os_Main;
        }

        public os_LotStatusDetails LoadLotStatusDetails(string OSNo)
        {
            var ErrMsg = "";
            os_LotStatusDetails os_LotStatusDetails = new os_LotStatusDetails();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_LotStatusDetails = conn.Query<os_LotStatusDetails>("spOs_LoadLotStatusDetail", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadLotStatusDetails", OSNo, "OSRepo");
                os_LotStatusDetails = null;
            }
            return os_LotStatusDetails;
        }

        public os_LotMatDetail LoadLotMatDetail(string OSNo)
        {
            var ErrMsg = "";
            os_LotMatDetail os_LotMatDetail = new os_LotMatDetail();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_LotMatDetail = conn.Query<os_LotMatDetail>("spOs_LoadLotMatDetail", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadLotMatDetail", OSNo, "OSRepo");
                os_LotMatDetail = null;
            }
            return os_LotMatDetail;
        }

        public os_AssyDetail LoadAssyDetail(string OSNo)
        {
            var ErrMsg = "";
            os_AssyDetail os_AssyDetail = new os_AssyDetail();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_AssyDetail = conn.Query<os_AssyDetail>("spOs_LoadAssyDetail", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadAssyDetail", OSNo, "OSRepo");
                os_AssyDetail = null;
            }
            return os_AssyDetail;
        }

        public os_SpcBin LoadSpcBin(string OSNo)
        {
            var ErrMsg = "";
            os_SpcBin os_SpcBin = new os_SpcBin();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_SpcBin = conn.Query<os_SpcBin>("spOs_LoadSpcBin", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadSpcBin", OSNo, "OSRepo");
                os_SpcBin = null;
            }
            return os_SpcBin;
        }

        public os_ManualAnalysis LoadManualAnalysis(string OSNo)
        {
            var ErrMsg = "";
            os_ManualAnalysis os_ManualAnalysis = new os_ManualAnalysis();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_ManualAnalysis = conn.Query<os_ManualAnalysis>("spOs_LoadManualAnalysis", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadManualAnalysis", OSNo, "OSRepo");
                os_ManualAnalysis = null;
            }
            return os_ManualAnalysis;
        }

        public os_XrayAnalysis LoadXrayAnalysis(string OSNo)
        {
            var ErrMsg = "";
            os_XrayAnalysis os_XrayAnalysis = new os_XrayAnalysis();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_XrayAnalysis = conn.Query<os_XrayAnalysis>("spOs_LoadXrayAnalysis", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadXrayAnalysis", OSNo, "OSRepo");
                os_XrayAnalysis = null;
            }
            return os_XrayAnalysis;
        }

        public os_AppAnalysis LoadAppAnalysis(string OSNo)
        {
            var ErrMsg = "";
            os_AppAnalysis os_AppAnalysis = new os_AppAnalysis();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_AppAnalysis = conn.Query<os_AppAnalysis>("spOs_LoadAppAnalysis", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadAppAnalysis", OSNo, "OSRepo");
                os_AppAnalysis = null;
            }
            return os_AppAnalysis;
        }

        public os_SemAnalysis LoadSemAnalysis(string OSNo)
        {
            var ErrMsg = "";
            os_SemAnalysis os_SemAnalysis = new os_SemAnalysis();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    os_SemAnalysis = conn.Query<os_SemAnalysis>("spOs_LoadSemAnalysis", new { OSNo = OSNo }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadSemAnalysis", OSNo, "OSRepo");
                os_SemAnalysis = null;
            }
            return os_SemAnalysis;
        }

        public IList<OSRef> LoadRef(string OSNo, string OSScope)
        {
            var ErrMsg = "";
            IList<OSRef> OSRef = new List<OSRef>();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    OSRef = conn.Query<OSRef>("spOs_LoadRef", new { OSNo = OSNo, OSScope = OSScope }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                repo.UpsertSysErrLog("A", ErrMsg, "LoadRef", OSNo, "OSRepo");
                OSRef.Clear();
            }
            return OSRef;
        }

    }
}