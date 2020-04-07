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
        private AdditionalFieldRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<IMMRequestContext>().UseInMemoryDatabase("AdditionalFields");
            _context = new IMMRequestContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CanAddAnAdditionalFieldToTheDatabase()
        {
            _repo.Add(new IntegerField {IsRequired = false, Name = "Int Field", Value = 5});
            Assert.AreEqual(1, _context.IntegerFields.Count());

            AdditionalField additionalField = _context.IntegerFields.First();

            Assert.AreEqual("Int Field", additionalField.Name);
            Assert.AreEqual(5, ((IntegerField) additionalField).Value);
        }
    }
}
