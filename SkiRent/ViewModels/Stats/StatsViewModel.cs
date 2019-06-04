using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiRent.Entities;
using SkiRent.Entities.FilterModels;


namespace SkiRent.ViewModels
{
	public class StatsViewModel
	{
        public StatsFilterModel FilterModel { get; set; }
        public int OrderNumber { get; set; }
        public int RentedItems { get; set; }
        public decimal PaymentsValue { get; set; }
        public int NewItems { get; set; }
    }
}