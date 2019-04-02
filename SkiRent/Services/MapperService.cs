using AutoMapper;
using SkiRent.Entities;
using SkiRent.Entities.DTO;

namespace SkiRent.Services
{
    public static class MapperService
    {
        public static IMapper GetMapperInstance()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Employee, EmployeeDTO>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());
                config.CreateMap<EmployeeDTO, Employee>()
                    .ForSourceMember(src => src.IsAdmin, opt => opt.DoNotValidate());

                config.CreateMap<Category, CategoryDTO>();
                config.CreateMap<Customer, CustomerDTO>();
                config.CreateMap<Item, ItemDTO>();
                config.CreateMap<Order, OrderDTO>();
                config.CreateMap<Payment, PaymentDTO>();
                config.CreateMap<RentedItem, RentedItemDTO>(); 
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}