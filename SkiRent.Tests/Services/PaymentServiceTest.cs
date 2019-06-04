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
using SkiRent.ViewModels.Payment;

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class PaymentServiceTest
    {
        private IMapper _mapper;
        private Faker<Payment> _paymentFaker;

        [TestInitialize]
        public void Init()
        {
            _paymentFaker = new Faker<Payment>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.Currency, f => f.Finance.Currency().Code)
                .RuleFor(x => x.ExchangeRate, f => f.Random.Decimal())
                .RuleFor(x => x.PaymentDate, f => f.Date.Past(1))
                .RuleFor(x => x.OrderID, (f, x) => x.ID)
                .RuleFor(x => x.Amount, f => f.Random.Decimal());
            _mapper = MapperService.GetMapperInstance();
        }


        [TestMethod]
        public void GetAll_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _paymentFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Payment>>()
                    .SetupData(input);

                var service = kernel.Get<SkiRent.Services.PaymentService>();
                var result = service.GetAll();

                result.Count.ShouldBe(input.Count);
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].ID.ShouldBe(input[i].ID);
                    result[i].Amount.ShouldBe(input[i].Amount);
                    result[i].ExchangeRate.ShouldBe(input[i].ExchangeRate);
                    result[i].OrderID.ShouldBe(input[i].OrderID);
                    result[i].PaymentDate.ShouldBe(input[i].PaymentDate);
                    result[i].Currency.ShouldBe(input[i].Currency);
                }
            }
        }

        [TestMethod]
        public void Get_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _paymentFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Payment>>()
                    .SetupData(input);

                var service = kernel.Get<SkiRent.Services.PaymentService>();
                var result = service.Get(1);

                result.ID.ShouldBe(input[1].ID);
                result.Amount.ShouldBe(input[1].Amount);
                result.ExchangeRate.ShouldBe(input[1].ExchangeRate);
                result.OrderID.ShouldBe(input[1].OrderID);
                result.PaymentDate.ShouldBe(input[1].PaymentDate);
                result.Currency.ShouldBe(input[1].Currency);
            }
        }

        [TestMethod]
        public void Add_Should_Add_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _paymentFaker.Generate(1).FirstOrDefault();
                var data = _paymentFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Payment>>()
                    .SetupData(data);

                var service = kernel.Get<SkiRent.Services.PaymentService>();

                Action act = () => { service.Add(_mapper.Map<PaymentBasicViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);
                

                data.Count.ShouldBe(6);
                var result = data.LastOrDefault();

                result.ID.ShouldBe(input.ID);
                result.Amount.ShouldBe(input.Amount);
                result.ExchangeRate.ShouldBe(input.ExchangeRate);
                result.OrderID.ShouldBe(input.OrderID);
                result.PaymentDate.ShouldBe(input.PaymentDate);
                result.Currency.ShouldBe(input.Currency);
            }
        }

        [TestMethod]
        public void Delete_Should_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _paymentFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Payment>>()
                    .SetupData(data);

                var service = kernel.Get<PaymentService>();
                var input = _mapper.Map<PaymentBasicViewModel>(service.Get(1));
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

                var data = _paymentFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Payment>>()
                    .SetupData(data);

                var service = kernel.Get<PaymentService>();
                var input = _paymentFaker.Generate(1).LastOrDefault();
                service.Delete(_mapper.Map<PaymentBasicViewModel>(input));

                data.Count.ShouldBe(5);
            }
        }

        [TestMethod]
        public void Update_Should_Update_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _paymentFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Payment>>()
                    .SetupData(data);

                var service = kernel.Get<PaymentService>();
                var inputID = service.Get(1).ID;
                var input = _paymentFaker.Generate(1).LastOrDefault();
                input.ID = inputID;

                Action act = () => { service.Update(_mapper.Map<PaymentBasicViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);
                var result = data[1];

                data.Count.ShouldBe(5);
                result.ID.ShouldBe(input.ID);
                result.Amount.ShouldBe(input.Amount);
                result.ExchangeRate.ShouldBe(input.ExchangeRate);
                result.OrderID.ShouldBe(input.OrderID);
                result.PaymentDate.ShouldBe(input.PaymentDate);
                result.Currency.ShouldBe(input.Currency);
            }
        }
    }
}