using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.ViewModels;
using SkiRent.ViewModels.Category;

namespace SkiRent.Services
{
	public class StatsService : BaseService<Model>
	{

        public StatsService(Model context) : base(context)
		{
        }

        public StatsViewModel GetStats(DateTime dFrom, DateTime dTo)
        {
            StatsViewModel vm = new StatsViewModel();
            vm.NewItems = m_Context.Items.Where(i => dTo > i.Purchase_date && dFrom < i.Purchase_date).Count();
            var orders = m_Context.Orders.Where(i => dTo > i.Date_Rented && dFrom < i.Date_Rented);
            vm.OrderNumber = orders.Count();
            if (vm.OrderNumber > 0)
            {
                vm.RentedItems = orders.Sum(i => i.Rented_Items.Count());
                vm.PaymentsValue = orders.Sum(i => i.Payments.Sum(j => j.Amount * j.ExchangeRate));
            }
            else
            {
                vm.RentedItems = 0;
                vm.PaymentsValue = 0;
            }
            return vm;
        }


        private bool DateBetween(DateTime dFrom, DateTime dTo, DateTime? dChecked)
        {
            return dTo > dChecked.Value && dFrom < dChecked.Value;
        }
    }
}