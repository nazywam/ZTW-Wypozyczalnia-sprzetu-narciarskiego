using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using SkiRent.Authorization;

namespace SkiRent.Tests.Authorization
{
    [TestClass]
    public class AuthorizationHelperTest
    {
//        private Faker<EmployeeDTO> _employeeDTOFaker;
//
//        [TestInitialize]
//        public void Init()
//        {
//            _employeeDTOFaker = new Faker<EmployeeDTO>()
//                .RuleFor(x => x.ID, f => f.IndexFaker)
//                .RuleFor(x => x.Address, f => f.Address.StreetAddress(true))
//                .RuleFor(x => x.EmploymentDate, f => f.Date.Recent())
//                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
//                .RuleFor(x => x.LastName, f => f.Name.LastName())
//                .RuleFor(x => x.Login, f => "Login")
//                .RuleFor(x => x.Password, f => "P4SSw0rd")
//                .RuleFor(x => x.PermissionLevel, f => f.Random.Int(0, 1))
//                .RuleFor(x => x.PhoneNumber, f => f.Random.Int(100000000, 999999999))
//                .RuleFor(x => x.Salary, f => f.Random.Decimal())
//                .RuleFor(x => x.Orders, f => new List<OrderDTO>());
//        }
//
//        [TestMethod]
//        public void HashPassword_Should_Hash_Successfully()
//        {
//            var input = _employeeDTOFaker.Generate(1).FirstOrDefault();
//            var result = AuthorizationHelper.HashPassword(input.Login, input.Password);
//
//            //Change this string, if Hashing Algorithm was changed
//            result.ShouldBe("83C25C0E3AD21D5D2EBDD6E76AD3A3EB");
//        }
    }
}