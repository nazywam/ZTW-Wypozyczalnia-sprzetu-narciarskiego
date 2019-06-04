using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using AutoMapper;
using Bogus;
using EntityFramework.Testing.Moq.Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;
using Shouldly;
using SkiRent.Entities;
using SkiRent.Services;
using SkiRent.ViewModels.Customer;
using SkiRent.ViewModels.Order;

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class OrderServiceTest
    {
        private IMapper _mapper;
        private Faker<Order> _orderFaker;

        [TestInitialize]
        public void Init()
        {
            var testCustomer = new Customer() { Orders = new List<Order>(), IdentyficationNumber = "1231231232", Address = "adres", PhoneNumber = 777666777, LastName = "mm", ID = 1, FirstName = "name"};
            var testEmployee = new Employee() {Orders = new List<Order>(), PhoneNumber = 123123123, LastName = "123", Address = "123", ID = 1, FirstName = "123", Login = "1414", PermissionLevel = 1, Salary = 100, Password = "123321"};

            _orderFaker = new Faker<Order>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.Customer, f => testCustomer)
                .RuleFor(x => x.Comment, f => f.Random.String(1, 10))
                .RuleFor(x => x.Date_Rented, f => f.Date.Past(1))
                .RuleFor(x => x.Rented_Items, f => new List<RentedItem>())
                .RuleFor(x => x.Date_Return, f => null)
                .RuleFor(x => x.Employee, f => testEmployee)
                .RuleFor(x => x.EmployeeID, f => testEmployee.ID)
                .RuleFor(x => x.CustomerID, (f, x) => testCustomer.ID);
            _mapper = MapperService.GetMapperInstance();
        }

        [TestMethod]
        public void GetAll_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _orderFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Order>>()
                    .SetupData(input);

                var service = kernel.Get<OrderService>();
                var result = service.GetAll();

                result.Count.ShouldBe(input.Count);
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].ID.ShouldBe(input[i].ID);
                    result[i].Customer.IdentyficationNumber.ShouldBe(input[i].Customer.IdentyficationNumber);
                    result[i].CustomerID.ShouldBe(input[i].CustomerID);
                    result[i].Comment.ShouldBe(input[i].Comment);
                    result[i].Date_Rented.ShouldBe(input[i].Date_Rented);
                    result[i].Date_Return.ShouldBe(input[i].Date_Return);
                    result[i].Employee.Login.ShouldBe(input[i].Employee.Login);
                    result[i].EmployeeID.ShouldBe(input[i].EmployeeID);
                }
            }
        }

        [TestMethod]
        public void Get_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _orderFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Order>>()
                    .SetupData(input);

                var service = kernel.Get<OrderService>();
                var result = service.Get(1);

                result.ID.ShouldBe(input[1].ID);
                result.Customer.IdentyficationNumber.ShouldBe(input[1].Customer.IdentyficationNumber);
                result.CustomerID.ShouldBe(input[1].ID);
                result.Comment.ShouldBe(input[1].Comment);
                result.Date_Rented.ShouldBe(input[1].Date_Rented);
                result.Date_Return.ShouldBe(input[1].Date_Return);
                result.Employee.Login.ShouldBe(input[1].Employee.Login);
                result.EmployeeID.ShouldBe(input[1].EmployeeID);
            }
        }

        [TestMethod]
        public void Add_Should_Add_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _orderFaker.Generate(1).FirstOrDefault();
                var data = _orderFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Order>>()
                    .SetupData(data);

                var service = kernel.Get<OrderService>();

                Action act = () => { service.Add(_mapper.Map<OrderDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);

                data.Count.ShouldBe(6);
                var result = data.LastOrDefault();

                result.ID.ShouldBe(input.ID);
                result.Customer.IdentyficationNumber.ShouldBe(input.Customer.IdentyficationNumber);
                result.CustomerID.ShouldBe(input.ID);
                result.Comment.ShouldBe(input.Comment);
                result.Date_Rented.ShouldBe(input.Date_Rented);
                result.Date_Return.ShouldBe(input.Date_Return);
                result.Employee.Login.ShouldBe(input.Employee.Login);
                result.EmployeeID.ShouldBe(input.EmployeeID);
            }
        }

        [TestMethod]
        public void Delete_Should_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _orderFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Order>>()
                    .SetupData(data);

                var service = kernel.Get<OrderService>();
                var input = _mapper.Map<OrderDetailViewModel>(service.Get(1));
                Action act = () => { service.Delete(input); };
                Assert.ThrowsException<FileLoadException>(act);

                data.Count.ShouldBe(4);
                data.FirstOrDefault(x => x.ID == input.ID).ShouldBeNull();
            }
        }

        [TestMethod]
        public void Delete_Should_Not_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _orderFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Order>>()
                    .SetupData(data);

                var service = kernel.Get<OrderService>();
                var input = _orderFaker.Generate(1).LastOrDefault();
                service.Delete(_mapper.Map<OrderDetailViewModel>(input));

                data.Count.ShouldBe(5);
            }
        }

        [TestMethod]
        public void Update_Should_Update_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _orderFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Order>>()
                    .SetupData(data);

                var service = kernel.Get<OrderService>();
                var inputID = service.Get(1).ID;
                var input = _orderFaker.Generate(1).LastOrDefault();
                input.ID = inputID;

                Action act = () => { service.Update(_mapper.Map<OrderDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);
                var result = data[1];

                result.ID.ShouldBe(input.ID);
                result.Customer.IdentyficationNumber.ShouldBe(input.Customer.IdentyficationNumber);
                result.CustomerID.ShouldBe(input.ID);
                result.Comment.ShouldBe(input.Comment);
                result.Date_Rented.ShouldBe(input.Date_Rented);
                result.Date_Return.ShouldBe(input.Date_Return);
                result.Employee.Login.ShouldBe(input.Employee.Login);
                result.EmployeeID.ShouldBe(input.EmployeeID);
            }
        }
    }
}