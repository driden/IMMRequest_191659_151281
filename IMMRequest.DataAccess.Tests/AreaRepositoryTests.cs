using System.Linq;
using IMMRequest.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class AreaRepositoryTests: IMMRequestTestBase
    {
        private AreaRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<IMMRequestContext>().UseInMemoryDatabase("Types");
            _context = new IMMRequestContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
            _repository = new AreaRepository(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CanAddAreaToDatabase()
        {
            _repository.Add(NewArea());

            Assert.AreEqual(1, _context.Set<Domain.Area>().Count());
            Assert.AreEqual(1, _context.Set<Domain.Topic>().Count());
            Assert.AreEqual(1, _context.Set<Domain.Type>().First().AdditionalFields.Count());
        }
    }
}
