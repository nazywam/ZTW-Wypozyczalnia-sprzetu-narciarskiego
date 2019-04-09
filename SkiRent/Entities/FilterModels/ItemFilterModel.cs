using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Entities.FilterModels
{
	public class ItemFilterModel
	{
		[StringLength(30)]
		[DisplayName("Producent")]
		public string S_Manufacturer { get; set; }
		[StringLength(255)]
		[DisplayName("Model")]
		public string S_Model { get; set; }
		[StringLength(30)]
		[DisplayName("Dostępny")]
		public string S_Avaible { get; set; }
		[StringLength(30)]
		[DisplayName("Kategoria")]
		public string S_CategoryID { get; set; }

		public bool IsFiltered
		{
			get
			{
				if (S_Manufacturer != null || S_Model != null || S_Avaible != null || S_CategoryID != null)
					return true;
				else
					return false;
			}
		}
	}
}