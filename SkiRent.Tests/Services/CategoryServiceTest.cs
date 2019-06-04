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

namespace SkiRent.Tests.Services
{
    [TestClass]
    public class CategoryServiceTest
    {
        private IMapper _mapper;

        private Faker<Category> _categoryFaker;

        [TestInitialize]
        public void Init()
        {
            _categoryFaker = new Faker<Category>()
                .RuleFor(x => x.ID, f => f.IndexFaker)
                .RuleFor(x => x.Name, f => f.Commerce.ProductAdjective())
                .RuleFor(x => x.PricePerDay, f => f.Random.Decimal())
                .RuleFor(x => x.ParentCategory, f => null)
                .RuleFor(x => x.SubCategories, f => new List<Category>())
                .RuleFor(x => x.Items, f => new HashSet<Item>());
            _mapper = MapperService.GetMapperInstance();
        }


        [TestMethod]
        public void GetAll_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _categoryFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Category>>()
                    .SetupData(input);

                var service = kernel.Get<CategoryService>();
                var result = service.GetAll();

                result.Count.ShouldBe(input.Count);
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].ID.ShouldBe(input[i].ID);
                    result[i].Name.ShouldBe(input[i].Name);
                    result[i].PricePerDay.ShouldBe(input[i].PricePerDay);
                    result[i].ParentCategory.ShouldBeSameAs(input[i].ParentCategory);
                }
            }
        }

        [TestMethod]
        public void Get_Should_GetAll_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _categoryFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Category>>()
                    .SetupData(input);

                var service = kernel.Get<CategoryService>();
                var result = service.Get(1);

                result.ID.ShouldBe(input[1].ID);
//                result.ParentCategory.ShouldBeSameAs(input[1].ParentCategory);
                result.Name.ShouldBe(input[1].Name);
                result.PricePerDay.ShouldBe(input[1].PricePerDay);
//                result.ItemList.Count.ShouldBe(input[1].Items.Count);
                result.SubCategories.Count.ShouldBe(input[1].SubCategories.Count);
            }
        }

        [TestMethod]
        public void Add_Should_Add_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var input = _categoryFaker.Generate(1).FirstOrDefault();
                var data = _categoryFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Category>>()
                    .SetupData(data);

                var service = kernel.Get<CategoryService>();

                Action act = () => { service.Add(_mapper.Map<CategoryDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);

                data.Count.ShouldBe(6);
                var result = data.LastOrDefault();

                result.ID.ShouldBe(input.ID);
                result.ParentCategory.ShouldBe(input.ParentCategory);
                result.Name.ShouldBe(input.Name);
                result.PricePerDay.ShouldBe(input.PricePerDay);
                result.Items.Count.ShouldBe(input.Items.Count);
                result.SubCategories.Count.ShouldBe(input.SubCategories.Count);
            }
        }

        [TestMethod]
        public void Delete_Should_Delete_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _categoryFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Category>>()
                    .SetupData(data);

                var service = kernel.Get<CategoryService>();
                var input = _mapper.Map<CategoryDetailViewModel>(service.Get(1));
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

                var data = _categoryFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Category>>()
                    .SetupData(data);

                var service = kernel.Get<CategoryService>();
                var input = _categoryFaker.Generate(1).LastOrDefault();
                service.Delete(_mapper.Map<CategoryDetailViewModel>(input));

                data.Count.ShouldBe(5);
            }
        }

        [TestMethod]
        public void Update_Should_Update_Properly()
        {
            using (var kernel = new MoqMockingKernel())
            {
                kernel.Load(new EntityFrameworkTestingMoqModule());

                var data = _categoryFaker.Generate(5).ToList();

                kernel.GetMock<DbSet<Category>>()
                    .SetupData(data);

                var service = kernel.Get<CategoryService>();
                var inputID = service.Get(1).ID;
                var input = _categoryFaker.Generate(1).LastOrDefault();
                input.ID = inputID;

                Action act = () => { service.Update(_mapper.Map<CategoryDetailViewModel>(input)); };
                Assert.ThrowsException<FileLoadException>(act);
                var result = data[1];

                data.Count.ShouldBe(5);
                result.ID.ShouldBe(input.ID);
                result.Items.Count.ShouldBe(input.Items.Count);
                result.Name.ShouldBe(input.Name);
                result.PricePerDay.ShouldBe(input.PricePerDay);
            }
        }
    }
}