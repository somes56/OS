using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OS.ViewModels
{
    public class MainList
    {
        public string OSNo { get; set; }
        public string LotID { get; set; }
        public string StatusDesc { get; set; }
        public string IssueDate { get; set; }
        public int LeadTime { get; set; }
        public string TestCat { get; set; }
        public bool LotRcv { get; set; }
        public string LotRcvHist { get; set; }
        public string ABLNo { get; set; }
        public string ABLDefect { get; set; }
    }
}