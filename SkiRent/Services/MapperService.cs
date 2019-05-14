using AutoMapper;
using SkiRent.Entities;

using SkiRent.ViewModels.Category;
using SkiRent.ViewModels.Customer;
using SkiRent.ViewModels.Employee;
using SkiRent.ViewModels.Item;
using SkiRent.ViewModels.Order;
using SkiRent.ViewModels.Payment;

namespace SkiRent.Services
{
    public static class MapperService
    {
        public static IMapper GetMapperInstance()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
//	            config.AllowNullCollections = true;
	            config.CreateMap<Employee, EmployeeBasicViewModel>();
	            config.CreateMap<Employee, EmployeeDetailViewModel>()
		            .ForMember(dest => dest.Password, opt => opt.Ignore());
	            config.CreateMap<EmployeeDetailViewModel, Employee>()
		            .ForSourceMember(src => src.IsAdmin, opt => opt.DoNotValidate())
		            .ForMember(src => src.Orders, opt => opt.PreCondition((src) => src.Orders != null));

                config.CreateMap<Item, ItemBasicViewModel>();
                config.CreateMap<Item, ItemDetailViewModel>();
				config.CreateMap<ItemDetailViewModel, Item>()
					.ForMember(src => src.Rented_Items, opt => opt.PreCondition((src) => src.Rented_Items != null));

				config.CreateMap<Category, CategoryBasicViewModel>();
				config.CreateMap<Category, CategoryDetailViewModel>();
				config.CreateMap<CategoryDetailViewModel, Category>()
					.ForMember(src => src.Items, opt => opt.PreCondition((src) => src.ItemList != null))
					.ForMember(src => src.SubCategories, opt => opt.PreCondition((src) => src.SubCategories != null));

				config.CreateMap<Customer, CustomerBasicViewModel>();
				config.CreateMap<Customer, CustomerDetailViewModel>();
				config.CreateMap<CustomerDetailViewModel, Customer>()
					.ForMember(src => src.Orders, opt => opt.PreCondition((src) => src.Orders != null));

				config.CreateMap<Order, OrderBasicViewModel>();
				config.CreateMap<Order, OrderDetailViewModel>();
				config.CreateMap<OrderDetailViewModel, Order>()
					.ForMember(src => src.Rented_Items, opt => opt.PreCondition((src) => src.Rented_Items != null));


				config.CreateMap<Payment, PaymentBasicViewModel>();
				config.CreateMap<PaymentBasicViewModel, Payment>();
				config.CreateMap<RentedItem, RentedItemBasicViewModel>();
				config.CreateMap<RentedItemBasicViewModel, RentedItem>();
			});

            return mapperConfiguration.CreateMapper();
        }
    }
}