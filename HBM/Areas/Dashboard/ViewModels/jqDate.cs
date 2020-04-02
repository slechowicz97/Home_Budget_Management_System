using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class jqDate
    {
        [Required]
        [Display(Name = "Select Date")]
        public DateTime JoinDate { get; set; }
    }
}