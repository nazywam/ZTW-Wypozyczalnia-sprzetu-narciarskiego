using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Entities.FilterModels
{
	public class OrderFilterModel
	{
        [DisplayName("[[[Order number]]]")]
        public int? S_ID { get; set; }

        [StringLength(30)]
        [DisplayName("[[[Returned]]]")]
        public string S_Returned { get; set; }

        [StringLength(30)]
        [DisplayName("[[[Customer]]]")]
        public string S_Customer_ID { get; set; }

        public bool IsFiltered
		{
			get
			{
				if (S_ID != null || S_Returned != null || S_Customer_ID != null)
					return true;
				else
					return false;
			}
		}
	}
}