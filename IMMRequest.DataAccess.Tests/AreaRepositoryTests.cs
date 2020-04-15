using System.Linq;
using IMMRequest.DataAccess.Core.Repositories;
using IMMRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class AreaRepositoryTests : IMMRequestTestBase
    {
        private AreaRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new AreaRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanAddAreaToDatabase()
        {
            _repository.Add(NewArea());

            Assert.AreEqual(1, _context.Set<Domain.Area>().Count());
            Assert.AreEqual(1, _context.Set<Domain.Topic>().Count());
            Assert.AreEqual(1, _context.Set<Domain.Type>().First().AdditionalFields.Count());
        }

        [TestMethod]
        public void CanGetAnAreaFromTheDatabase()
        {
            var area = NewArea();
            _context.Set<Area>().Add(area);
            _context.SaveChanges();

            Assert.AreEqual(area, _repository.Get(area.Id));
        }

        [TestMethod]
        public void CanModifyAnAreaInTheDatabase()
        {
            var area = NewArea();
            _context.Set<Area>().Add(area);
            _context.SaveChanges();
            area.Name = "NewName";

            _repository.Update(area);

            Assert.AreEqual("NewName", _context.Set<Area>().First().Name);
        }

        [TestMethod]
        public void CanDeleteAnAreaInTheDatabase()
        {
            var area = NewArea();
            _context.Set<Area>().Add(area);
            _context.SaveChanges();

            _repository.Remove(area);

            Assert.AreEqual(0, _context.Set<Area>().Count());
        }
    }
}
