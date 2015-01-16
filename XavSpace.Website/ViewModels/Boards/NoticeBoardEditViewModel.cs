using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XavSpace.Website.ViewModels.Boards
{
    public class NoticeBoardEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name="Official?")]
        public bool IsOfficial { get; set; }
        public bool IsMandatory { get; set; }
    }
}
