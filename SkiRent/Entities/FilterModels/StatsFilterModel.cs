using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Entities.FilterModels
{
	public class StatsFilterModel
	{
        [Required]
        [DisplayName("[[[Date from]]]")]
        public string S_DateFrom { get; set; }
        [Required]
        [DisplayName("[[[Date to]]]")]
        public string S_DateTo { get; set; }
    }
}