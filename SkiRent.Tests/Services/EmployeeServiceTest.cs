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
using SkiRent.Authorization;
using SkiRent.Entities;
using SkiRent.Extensions;
using SkiRent.Services;
using SkiRent.ViewModels.Employee;

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private IMapper _mapper;
        private Faker<Employee> _employeeFaker;

        [TestInitialize]
        public void Init()
        {
            _employeeFaker = new Faker<Employee>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.Address, f => f.Address.StreetAddress(true))
                .RuleFor(x => x.EmploymentDate, f => f.Date.Recent())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Login, f => f.Internet.UserName())
                .RuleFor(x => x.Password, (f, x) => AuthorizationHelper.HashPassword(x.Login, f.Internet.Password()))
                .RuleFor(x => x.PermissionLevel, f => f.Random.Int(0, 1))
                .RuleFor(x => x.PhoneNumber, f => f.Random.Int(100000000, 999999999))
                .RuleFor(x => x.Salary, f => f.Random.Decimal())
                .RuleFor(x => x.Orders, f => new HashSet<Order>());
            _mapper = MapperService.GetMapperInstance();
        }

        [TestMethod]
        public void GetUserByUserNameAndPassword_Should_Get_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _employeeFaker.Generate(5).ToList();
                input[2].Login = "Login";
                input[2].Password = "83C25C0E3AD21D5D2EBDD6E76AD3A3EB";     //Password should be updated with change of Hashing algorithm

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(input);

                var service = kernel.Get<EmployeeService>();
                var result = service.GetUserByUserNameAndPassword("Login", "P4SSw0rd");

                result.ID.ShouldBe(input[2].ID);
                result.Address.ShouldBe(input[2].Address);
                result.EmploymentDate.ShouldBe(input[2].EmploymentDate);
                result.FirstName.ShouldBe(input[2].FirstName);
                result.LastName.ShouldBe(input[2].LastName);
                result.Login.ShouldBe(input[2].Login);
                result.PermissionLevel.ShouldBe(input[2].PermissionLevel);
                result.PhoneNumber.ShouldBe(input[2].PhoneNumber);
                result.Salary.ShouldBe(input[2].Salary);
            }
        }
        
        [TestMethod]
        public void GetUserByUserNameAndPassword_Should_Fail()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(input);

                var service = kernel.Get<EmployeeService>();
                var result = service.GetUserByUserNameAndPassword("Login", "Haslo");

                result.ShouldBeNull();
            }
        }

        [TestMethod]
        public void GetAll_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(input);

                var service = kernel.Get<EmployeeService>();
                var result = service.GetAll();

                result.Count.ShouldBe(input.Count);
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].ID.ShouldBe(input[i].ID);
                    result[i].Address.ShouldBe(input[i].Address);
                    result[i].EmploymentDate.ShouldBe(input[i].EmploymentDate);
                    result[i].FirstName.ShouldBe(input[i].FirstName);
                    result[i].LastName.ShouldBe(input[i].LastName);
                    result[i].Login.ShouldBe(input[i].Login);
                    result[i].PermissionLevel.ShouldBe(input[i].PermissionLevel);
                    result[i].PhoneNumber.ShouldBe(input[i].PhoneNumber);
                    result[i].Salary.ShouldBe(input[i].Salary);
                }
            }
        }

        [TestMethod]
        public void Get_Should_Get_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(data);

                var input = data.SingleOrDefault(x => x.ID == 1);

                var service = kernel.Get<EmployeeService>();
                var result = service.Get(1);

                result.ID.ShouldBe(input.ID);
                result.Address.ShouldBe(input.Address);
                result.EmploymentDate.ShouldBe(input.EmploymentDate);
                result.FirstName.ShouldBe(input.FirstName);
                result.LastName.ShouldBe(input.LastName);
                result.Login.ShouldBe(input.Login);
                result.Password.ShouldBeNull();
                result.PermissionLevel.ShouldBe(input.PermissionLevel);
                result.PhoneNumber.ShouldBe(input.PhoneNumber);
                result.Salary.ShouldBe(input.Salary);
                result.Orders.Count.ShouldBe(input.Orders.Count);
            }
        }

        [TestMethod]
        public void Add_Should_Add_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(data);

                var input = _employeeFaker.Generate(1).FirstOrDefault();

                var service = kernel.Get<EmployeeService>();
                
                Action act = () => { service.Add(_mapper.Map<EmployeeDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);

                data.Count.ShouldBe(6);
                var result = data.LastOrDefault();

                result.ID.ShouldBe(input.ID);
                result.Address.ShouldBe(input.Address);
                result.EmploymentDate.ShouldBe(input.EmploymentDate);
                result.FirstName.ShouldBe(input.FirstName);
                result.LastName.ShouldBe(input.LastName);
                result.Login.ShouldBe(input.Login);
//                result.Password.ShouldBe(AuthorizationHelper.HashPassword(input.Login, input.Password));
                result.PermissionLevel.ShouldBe(input.PermissionLevel);
                result.PhoneNumber.ShouldBe(input.PhoneNumber);
                result.Salary.ShouldBe(input.Salary);
                result.Orders.Count.ShouldBe(input.Orders.Count);
            }
        }

        [TestMethod]
        public void Delete_Should_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(data);

                var service = kernel.Get<EmployeeService>();
                var input = service.Get(1);
                Action act = () => { service.Delete(_mapper.Map<EmployeeDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);

                data.Count.ShouldBe(4);
                data.FirstOrDefault(x => x.Login == input.Login).ShouldBeNull();
            }
        }

        [TestMethod]
        public void Delete_Should_Not_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(data);

                var service = kernel.Get<EmployeeService>();
                var mapper = MapperService.GetMapperInstance();
                var input = _employeeFaker.Generate(6).LastOrDefault();
                service.Delete(mapper.Map<EmployeeDetailViewModel>(input));

                data.Count.ShouldBe(5);
            }
        }

        [TestMethod]
        public void Update_Should_Update_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _employeeFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Employee>>()
                    .SetupData(data);

                var service = kernel.Get<EmployeeService>();
                var inputID = service.Get(1).ID;
                var input = _employeeFaker.Generate(1).FirstOrDefault();
                input.ID = inputID;

                Action act = () => { service.Update(_mapper.Map<EmployeeDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);
                var result = data[1];

                data.Count.ShouldBe(5);
                result.ID.ShouldBe(input.ID);
                result.Address.ShouldBe(input.Address);
                result.EmploymentDate.ShouldBe(input.EmploymentDate);
                result.FirstName.ShouldBe(input.FirstName);
                result.LastName.ShouldBe(input.LastName);
                result.Login.ShouldBe(input.Login);
//                result.Password.ShouldBe(AuthorizationHelper.HashPassword(input.Login, input.Password));
                result.PermissionLevel.ShouldBe(input.PermissionLevel);
                result.PhoneNumber.ShouldBe(input.PhoneNumber);
                result.Salary.ShouldBe(input.Salary);
                result.Orders.Count.ShouldBe(input.Orders.Count);
            }
        }
    }
}