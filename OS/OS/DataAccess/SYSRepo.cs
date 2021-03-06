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
    public class SYSRepo
    {

        string connStr = ConfigurationManager.ConnectionStrings["IMS_OS_DEFAULT"].ConnectionString;

        public LoginUsr LoginValidation(string LoginID, string LoginPsswrd)
        {
            var ErrMsg = "";
            LoginUsr LoginUsr = new LoginUsr();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    LoginUsr = conn.Query<LoginUsr>("spSys_LoginValidation", new { LoginID = LoginID, LoginPsswrd = LoginPsswrd }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                LoginUsr = null;
            }
            return LoginUsr;
        }

        public IEnumerable<OSLot> AdvSearchOS(string str)
        {
            var ErrMsg = "";
            IEnumerable<OSLot> OSLot = new List<OSLot>();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    OSLot = conn.Query<OSLot>("spMst_AdvSearchOS", new { SearchBy = str }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                UpsertSysErrLog("A", ErrMsg, "AdvSearchOS", "", "SYSRepo");
                OSLot = Enumerable.Empty<OSLot>();
            }
            return OSLot;
        }

        public IEnumerable<CodeDescription> AdvSearchDefect(string str, string p1)
        {
            var ErrMsg = "";
            IEnumerable<CodeDescription> CodeDescription = new List<CodeDescription>();

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    CodeDescription = conn.Query<CodeDescription>("spMst_AdvSearchDefMode", new { SearchBy = str, OSScope = p1 }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
                UpsertSysErrLog("A", ErrMsg, "AdvSearchDefect", "", "SYSRepo");
                CodeDescription = Enumerable.Empty<CodeDescription>();
            }
            return CodeDescription;
        }

        public void UpsertSysErrLog(string SysFlag, string ErrDesc, string FuncName, string OSNo, string OSScope)
        {
            var ErrMsg = "";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Execute("spSys_UpsertSysErrLog",
                                new
                                {
                                    SysFlag = SysFlag,
                                    ErrDesc = ErrDesc,
                                    FuncName = FuncName,
                                    OSNo = OSNo,
                                    OSScope = OSScope
                                }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                ErrMsg = e.ToString();
            }
        }

    }
}