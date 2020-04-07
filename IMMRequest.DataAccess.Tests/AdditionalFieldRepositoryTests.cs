using System;
using System.Collections.Generic;
using System.Linq;
using IMMRequest.DataAccess.Repositories;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class AdditionalFieldRepositoryTests
    {
        private IMMRequestContext _context;
        private AdditionalFieldRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<IMMRequestContext>().UseInMemoryDatabase("AdditionalFields");
            _context = new IMMRequestContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
            _repository = new AdditionalFieldRepository(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CanAddAnAdditionalFieldToTheDatabase()
        {
            _repository.Add(new IntegerField { IsRequired = false, Name = "Int Field", Value = 5 });
            Assert.AreEqual(1, _context.IntegerFields.Count());

            AdditionalField additionalField = _context.IntegerFields.First();

            Assert.AreEqual("Int Field", additionalField.Name);
            Assert.AreEqual(5, ((IntegerField)additionalField).Value);
        }

        [TestMethod]
        public void CannReadAdditionalFieldFromTheDatabase()
        {
            var dateField = new DateField
            {Id = 5, Name = "DateField", Range = new List<Item<DateTime>>(), Value = DateTime.Today };

            _context.DateFields.Add(dateField);
            _context.SaveChanges();

            var actualField = _repository.Get(5);

            Assert.AreEqual(dateField.Name, actualField.Name);
            Assert.AreEqual(dateField.Value, actualField.Value);
        }
    }
}
