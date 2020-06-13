namespace IMMRequest.DataAccess.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Core.Repositories;
    using Domain;

    [TestClass]
    public class AdminRepositoryTests : IMMRequestTestBase
    {
        private AdminRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new AdminRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanAddAdminToDatabase()
        {
            _repository.Add(NewAdmin());

            Assert.AreEqual(1, _context.Set<Admin>().Count());
        }
    }
}
