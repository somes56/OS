using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OS.ViewModels
{
    public class LoginUsr
    {
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LoginPsswrd { get; set; }

        public string UsrName { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string PosID { get; set; }
    }
}