using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.ComponentModel.DataAnnotations;

namespace XavSpace.Website.ViewModels.Settings
{
    public class IndexViewModel
    {
        [Display(Name="First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Post")]
        public string Post { get; set; }

        [Display(Name = "UID Number")]
        
        [Range(12000, 141999)]
        public int UidNumber { get; set; }

        [Display(Name = "Emp ID")]
       
        [DataType(DataType.Text)]
        [Range(2001,2180)]
        public int EmpId { get; set; }

        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
        public bool HasPassword { get; set; }
    }
}