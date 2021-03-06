using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OS.Models
{
    public class os_Main
    {
        public string OSNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueByEmpNo { get; set; }
        public string IssueByEmpName { get; set; }
        public string PkgID { get; set; }
        public string PkgDesc { get; set; }
        public string LotCat { get; set; }
        public string BaseProductName { get; set; }
        public int LotQty { get; set; }
        public string LotNo { get; set; }
        public string ProdRank { get; set; }
        public string BinJudge { get; set; }
        public string CtmJudge { get; set; }
        public string XrayJudge { get; set; }
        public string AppJudge { get; set; }
        public string SemJudge { get; set; }
        public string OverAllJudge { get; set; }
        public string OverAllRemark { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
    }
}