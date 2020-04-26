namespace IMMRequest.DataAccess.Tests
{
    using System.Linq;
    using Core.Repositories;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.AreEqual(1, _context.Set<Area>().Count());
            Assert.AreEqual(1, _context.Set<Topic>().Count());
            Assert.AreEqual(1, _context.Set<Type>().First().AdditionalFields.Count());
        }
        
        [TestMethod]
        public void ATrackedAreaHasAnId()
        {
            var area = NewArea();
            Assert.AreNotEqual(1, area.Id);

            _repository.Add(area);

            Assert.AreEqual(1, area.Id);
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
        public void CanGetAllAreasFromTheDatabase()
        {
            _context.Set<Area>().AddRange(NewArea(), NewArea());
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
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

        [TestMethod]
        public void CanFindTheAreaThatOwnsATypeById()
        {
            var area = NewArea();
            _context.Set<Area>().Add(area);
            _context.SaveChanges();

            var areaFromType = _repository.FindWithTypeId(_context.Set<Type>().First().Id);
            Assert.AreEqual(areaFromType.Id,area.Id);
        }
    }
}
