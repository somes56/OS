using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OS.ViewModels
{

    public class OSLot
    {
        public string OSNo { get; set; }
        public string LotID { get; set; }
        public string IssueDate { get; set; }
        public string StepCode { get; set; }
        public string StatusDesc { get; set; }
    }

    public class CodeDescription
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

}