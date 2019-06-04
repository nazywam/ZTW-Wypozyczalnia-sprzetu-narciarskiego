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
using SkiRent.ViewModels.Category;
using SkiRent.ViewModels.Customer;

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        private IMapper _mapper;

        private Faker<Customer> _customerFaker;

        [TestInitialize]
        public void Init()
        {
            _customerFaker = new Faker<Customer>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.FirstName, f => f.Random.String(5, 8))
                .RuleFor(x => x.LastName, f => f.Random.String(5, 8))
                .RuleFor(x => x.Address, f => f.Random.String(5, 30))
                .RuleFor(x => x.PhoneNumber, f => f.Random.Int(9, 9))
                .RuleFor(x => x.IdentyficationNumber, f => f.Random.String(10, 10))
                .RuleFor(x => x.Orders, f => new List<Order>());
            _mapper = MapperService.GetMapperInstance();
        }


        [TestMethod]
        public void GetAll_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _customerFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Customer>>()
                    .SetupData(input);

                var service = kernel.Get<CustomerService>();
                var result = service.GetAll();

                result.Count.ShouldBe(input.Count);
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].ID.ShouldBe(input[i].ID);
                    result[i].FirstName.ShouldBe(input[i].FirstName);
                    result[i].LastName.ShouldBe(input[i].LastName);
                    result[i].Address.ShouldBe(input[i].Address);
                    result[i].PhoneNumber.ShouldBe(input[i].PhoneNumber);
                    result[i].IdentyficationNumber.ShouldBe(input[i].IdentyficationNumber);
                }
            }
        }

        [TestMethod]
        public void Get_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _customerFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Customer>>()
                    .SetupData(input);

                var service = kernel.Get<CustomerService>();
                var result = service.Get(1);

                result.ID.ShouldBe(input[1].ID);
                result.FirstName.ShouldBe(input[1].FirstName);
                result.LastName.ShouldBe(input[1].LastName);
                result.Address.ShouldBe(input[1].Address);
                result.PhoneNumber.ShouldBe(input[1].PhoneNumber);
                result.IdentyficationNumber.ShouldBe(input[1].IdentyficationNumber);
                result.Orders.Count.ShouldBe(input[1].Orders.Count);
            }
        }

        [TestMethod]
        public void Add_Should_Add_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _customerFaker.Generate(1).FirstOrDefault();
                var data = _customerFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Customer>>()
                    .SetupData(data);

                var service = kernel.Get<CustomerService>();

                Action act = () => { service.Add(_mapper.Map<CustomerDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);

                data.Count.ShouldBe(6);
                var result = data.LastOrDefault();

                result.ID.ShouldBe(input.ID);
                result.FirstName.ShouldBe(input.FirstName);
                result.LastName.ShouldBe(input.LastName);
                result.Address.ShouldBe(input.Address);
                result.PhoneNumber.ShouldBe(input.PhoneNumber);
                result.IdentyficationNumber.ShouldBe(input.IdentyficationNumber);
                result.Orders.Count.ShouldBe(input.Orders.Count);
            }
        }

        [TestMethod]
        public void Delete_Should_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _customerFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Customer>>()
                    .SetupData(data);

                var service = kernel.Get<CustomerService>();
                var input = _mapper.Map<CustomerDetailViewModel>(service.Get(1));
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

                var data = _customerFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Customer>>()
                    .SetupData(data);

                var service = kernel.Get<CustomerService>();
                var input = _customerFaker.Generate(1).LastOrDefault();
                service.Delete(_mapper.Map<CustomerDetailViewModel>(input));

                data.Count.ShouldBe(5);
            }
        }

        [TestMethod]
        public void Update_Should_Update_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _customerFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Customer>>()
                    .SetupData(data);

                var service = kernel.Get<CustomerService>();
                var inputID = service.Get(1).ID;
                var input = _customerFaker.Generate(1).LastOrDefault();
                input.ID = inputID;

                Action act = () => { service.Update(_mapper.Map<CustomerDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);
                var result = data[1];

                data.Count.ShouldBe(5);
                result.ID.ShouldBe(input.ID);
                result.FirstName.ShouldBe(input.FirstName);
                result.LastName.ShouldBe(input.LastName);
                result.Address.ShouldBe(input.Address);
                result.PhoneNumber.ShouldBe(input.PhoneNumber);
                result.IdentyficationNumber.ShouldBe(input.IdentyficationNumber);
                result.Orders.Count.ShouldBe(input.Orders.Count);
            }
        }
    }
}