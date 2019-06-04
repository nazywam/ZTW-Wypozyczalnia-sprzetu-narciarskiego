using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using SkiRent;
using SkiRent.Entities;
using SkiRent.Services;
using SkiRent.ViewModels.Employee;

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class MapperServiceTest
    {
        private IMapper _mapper;
        private Faker<Employee> _employeeFaker;

        [TestInitialize]
        public void Init()
        {
            _mapper = MapperService.GetMapperInstance();
            _employeeFaker = new Faker<Employee>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.Address, f => f.Address.StreetAddress(true))
                .RuleFor(x => x.EmploymentDate, f => f.Date.Recent())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Login, f => f.Internet.UserName())
                .RuleFor(x => x.Password, f => f.Internet.Password())
                .RuleFor(x => x.PermissionLevel, f => f.Random.Int(0, 1))
                .RuleFor(x => x.PhoneNumber, f => f.Random.Int(100000000, 999999999))
                .RuleFor(x => x.Salary, f => f.Random.Decimal())
                .RuleFor(x => x.Orders, f => new HashSet<Order>());
        }

        [TestMethod]
        public void Convert_Employee_To_EmployeeDTO_Should_Convert_Properly()
        {
            var input = _employeeFaker.Generate(1).FirstOrDefault();

            var result = _mapper.Map<EmployeeDetailViewModel>(input);

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

//        [TestMethod]
//        public void Convert_EmployeeDTO_To_Employee_Should_Convert_Properly()
//        {
//            var input = _employeeFaker.Generate(1).FirstOrDefault();
//
//            var result = _mapper.Map<Employee>(input);
//
//            result.ID.ShouldBe(input.ID);
//            result.Address.ShouldBe(input.Address);
//            result.EmploymentDate.ShouldBe(input.EmploymentDate);
//            result.FirstName.ShouldBe(input.FirstName);
//            result.LastName.ShouldBe(input.LastName);
//            result.Login.ShouldBe(input.Login);
//            result.Password.ShouldBe(input.Password);
//            result.PermissionLevel.ShouldBe(input.PermissionLevel);
//            result.PhoneNumber.ShouldBe(input.PhoneNumber);
//            result.Salary.ShouldBe(input.Salary);
//            result.Orders.Count.ShouldBe(input.Orders.Count);
//        }

        [TestMethod]
        public void Convert_Employee_List_To_EmployeeDTO_List_Should_Convert_Properly()
        {
            var input = _employeeFaker.Generate(2).ToList();
        
            var result = _mapper.Map<List<EmployeeDetailViewModel>>(input);
        
            for (int i = 0; i < input.Count; i++)
            {
                result[i].ID.ShouldBe(input[i].ID);
                result[i].Address.ShouldBe(input[i].Address);
                result[i].EmploymentDate.ShouldBe(input[i].EmploymentDate);
                result[i].FirstName.ShouldBe(input[i].FirstName);
                result[i].LastName.ShouldBe(input[i].LastName);
                result[i].Login.ShouldBe(input[i].Login);
                result[i].Password.ShouldBeNull();
                result[i].PermissionLevel.ShouldBe(input[i].PermissionLevel);
                result[i].PhoneNumber.ShouldBe(input[i].PhoneNumber);
                result[i].Salary.ShouldBe(input[i].Salary);
                result[i].Orders.Count.ShouldBe(input[i].Orders.Count);
            }
        }
        
//        [TestMethod]
//        public void Convert_EmployeeDTO_List_To_Employee_List_Should_Convert_Properly()
//        {
//            var input = _employeeFaker.Generate(2).ToList();
//        
//            var result = _mapper.Map<List<Employee>>(input);
//        
//            for (int i = 0; i < input.Count; i++)
//            {
//                result[i].ID.ShouldBe(input[i].ID);
//                result[i].Address.ShouldBe(input[i].Address);
//                result[i].EmploymentDate.ShouldBe(input[i].EmploymentDate);
//                result[i].FirstName.ShouldBe(input[i].FirstName);
//                result[i].LastName.ShouldBe(input[i].LastName);
//                result[i].Login.ShouldBe(input[i].Login);
//                result[i].Password.ShouldBe(input[i].Password);
//                result[i].PermissionLevel.ShouldBe(input[i].PermissionLevel);
//                result[i].PhoneNumber.ShouldBe(input[i].PhoneNumber);
//                result[i].Salary.ShouldBe(input[i].Salary);
//                result[i].Orders.Count.ShouldBe(input[i].Orders.Count);
//            }
//        }
    }
}