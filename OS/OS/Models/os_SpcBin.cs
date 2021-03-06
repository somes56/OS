using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OS.Models
{
    public class os_SpcBin
    {
        public string OSNo { get; set; }
        public string StepCode { get; set; }
        public int Bin2 { get; set; }
        public int Bin7 { get; set; }
        public int Bin8 { get; set; }
        public int TtlDefQty { get; set; }
        public string SBStatus { get; set; }
    }
}