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
using SkiRent.ViewModels.Item;

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class ItemServiceTest
    {
        private Faker<Item> _itemFaker;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var catHelper = new Category() { ID = 1, Items = new List<Item>(), Name = "Nazwa", PricePerDay = decimal.One};
        _itemFaker = new Faker<Item>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.Category, f => catHelper) 
                .RuleFor(x => x.CategoryID, (f, x) => x.Category.ID)
                .RuleFor(x => x.Manufacturer, f => f.Company.CompanyName())
                .RuleFor(x => x.ModelName, f => f.Commerce.ProductName())
                .RuleFor(x => x.Size, f => f.Commerce.ProductAdjective())
                .RuleFor(x => x.Avaiable, f => f.Random.String2(1, "YN"))
                .RuleFor(x => x.Purchase_date, f => f.Date.Recent(1000))
                .RuleFor(x => x.Barcode, f => f.Random.Int(10,12).ToString())
                .RuleFor(x => x.Rented_Items, f => new List<RentedItem>());
        _mapper = MapperService.GetMapperInstance();
        }

        [TestMethod]
        public void GetAll_Should_Get_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _itemFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Item>>()
                    .SetupData(input);

                var service = kernel.Get<ItemService>();
                var result = service.GetAll();

                result.Count.ShouldBe(input.Count);
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].ID.ShouldBe(input[i].ID);
                    result[i].Avaiable.ShouldBe(input[i].Avaiable);
                    result[i].CategoryID.ShouldBe(input[i].CategoryID.ToString());
                    result[i].Category.Name.ShouldBe(input[i].Category.Name);
                    result[i].Manufacturer.ShouldBe(input[i].Manufacturer);
                    result[i].ModelName.ShouldBe(input[i].ModelName);
                    result[i].Size.ShouldBe(input[i].Size);
                    result[i].Purchase_date.ShouldBe(input[i].Purchase_date);
                    result[i].Barcode.ShouldBe(input[i].Barcode);
                }
            }
        }

        [TestMethod]
        public void Get_Should_Get_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _itemFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Item>>()
                    .SetupData(data);

                var input = data.SingleOrDefault(x => x.ID == 1);

                var service = kernel.Get<ItemService>();
                var result = service.Get(1);

                result.ID.ShouldBe(input.ID);
                result.Avaiable.ShouldBe(input.Avaiable);
                result.CategoryID.ShouldBe(input.CategoryID.ToString());
                result.Category.Name.ShouldBe(input.Category.Name);
                result.Manufacturer.ShouldBe(input.Manufacturer);
                result.ModelName.ShouldBe(input.ModelName);
                result.Size.ShouldBe(input.Size);
                result.Purchase_date.ShouldBe(input.Purchase_date);
                result.Rented_Items.Count.ShouldBe(input.Rented_Items.Count);
                result.Barcode.ShouldBe(input.Barcode);
            }
        }

        [TestMethod]
        public void Add_Should_Add_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _itemFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Item>>()
                    .SetupData(data);

                var input = _itemFaker.Generate(1).FirstOrDefault();

                var service = kernel.Get<ItemService>();

//                service.Add(_mapper.Map<ItemDetailViewModel>(input));
//                Action act = () => { };
//                Assert.ThrowsException<FileLoadException>(act);
                

//                data.Count.ShouldBe(6);
//                var result = data.LastOrDefault();
//
//                result.ID.ShouldBe(input.ID);
//                result.Avaiable.ShouldBe(input.Avaiable);
//                result.CategoryID.ShouldBe(input.CategoryID);
//                result.Category.Name.ShouldBe(input.Category.Name);
//                result.Manufacturer.ShouldBe(input.Manufacturer);
//                result.ModelName.ShouldBe(input.ModelName);
//                result.Size.ShouldBe(input.Size);
//                result.Purchase_date.ShouldBe(input.Purchase_date);
//                result.Barcode.ShouldBe(input.Barcode);
//                result.Rented_Items.Count.ShouldBe(input.Rented_Items.Count);
            }
        }

        [TestMethod]
        public void Delete_Should_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _itemFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Item>>()
                    .SetupData(data);

                var service = kernel.Get<ItemService>();
                var mapper = MapperService.GetMapperInstance();
                var input = service.Get(1);
                Action act = () => { service.Delete(mapper.Map<ItemDetailViewModel>(input)); };
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

                var data = _itemFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Item>>()
                    .SetupData(data);

                var service = kernel.Get<ItemService>();
                var mapper = MapperService.GetMapperInstance();
                var input = _itemFaker.Generate(6).LastOrDefault();
                service.Delete(_mapper.Map<ItemDetailViewModel>(input));

                data.Count.ShouldBe(5);
            }
        }

        [TestMethod]
        public void Update_Should_Update_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _itemFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Item>>()
                    .SetupData(data);

                var service = kernel.Get<ItemService>();
                var inputID = service.Get(1).ID;
                var input = _itemFaker.Generate(1).FirstOrDefault();
                input.ID = inputID;
//
//                Action act = () => { service.Update(_mapper.Map<ItemDetailViewModel>(input)); };
//                Assert.ThrowsException<FileLoadException>(act);
//                var result = data[1];
//
//                data.Count.ShouldBe(5);
//                result.ID.ShouldBe(input.ID);
//                result.Avaiable.ShouldBe(input.Avaiable);
//                result.CategoryID.ShouldBe(input.CategoryID);
//                result.Category.Name.ShouldBe(input.Category.Name);
//                result.Manufacturer.ShouldBe(input.Manufacturer);
//                result.ModelName.ShouldBe(input.ModelName);
//                result.Size.ShouldBe(input.Size);
//                result.Purchase_date.ShouldBe(input.Purchase_date);
//                result.Rented_Items.Count.ShouldBe(input.Rented_Items.Count);
//                result.Barcode.ShouldBe(input.Barcode);
            }
        }
    }
}