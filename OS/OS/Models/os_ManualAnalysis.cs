using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OS.Models
{
    public class os_ManualAnalysis
    {
        public string OSNo { get; set; }
        public int PassQty { get; set; }
        public int OpenQty { get; set; }
        public int ShortQty { get; set; }
        public int TtlAlyQty { get; set; }
        public string DefectID { get; set; }
        public string DefectDesc { get; set; }
        public int DefectQty { get; set; }
        public string Remark { get; set; }
        public bool CAStatus { get; set; }
        public string CompleteBy { get; set; }
        public string DateComplete { get; set; }
    }
}