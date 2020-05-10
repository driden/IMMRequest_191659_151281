using System.Linq;
using IMMRequest.DataAccess.Core.Repositories;
using IMMRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
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
