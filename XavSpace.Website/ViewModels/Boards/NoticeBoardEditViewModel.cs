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
        [Required]
        public string Title { get; set; }
        [StringLength(75, MinimumLength = 10)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Description { get; set; }

        public bool IsMandatory { get; set; }
    }
}
