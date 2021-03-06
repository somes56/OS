using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OS.Models;

namespace OS.ViewModels
{
    public class OSInit
    {
        public os_Main os_Main { get; set; }
        public os_LotStatusDetails os_LotStatusDetails { get; set; }
        public os_SpcBin os_SpcBin { get; set; }
        public os_ManualAnalysis os_ManualAnalysis { get; set; }
        public os_XrayAnalysis os_XrayAnalysis { get; set; }
        public os_AppAnalysis os_AppAnalysis { get; set; }
        public os_SemAnalysis os_SemAnalysis { get; set; }
    }
}