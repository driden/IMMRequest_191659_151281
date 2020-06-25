namespace IMMRequest.DataAccess.Tests
{
    using System.Linq;
    using Core.Repositories;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserRepositoryTests : IMMRequestTestBase
    {
        private UserRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new UserRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanGetUsersFromTheDatabase()
        {
            User admin = new Admin { Email = "admin@admin.com", Name = "admin" };
            User citizen = new Citizen { Email = "citizen@citizen.com", Name = "citizen" };

            _context.Set<User>().AddRange(admin, citizen);
            _context.SaveChanges();

            Assert.AreEqual(admin, _context.Admins.First());
            Assert.AreEqual(citizen, _context.Citizens.First());
        }

        [TestMethod]
        public void CanGetAllUsersFromTheDatabase()
        {
            User admin = new Admin { Email = "admin@admin.com", Name = "admin" };
            User citizen = new Citizen { Email = "citizen@citizen.com", Name = "citizen" };

            _context.Set<User>().AddRange(admin, citizen);
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
        }

        [TestMethod]
        public void CanModifyAUserInTheDatabase()
        {
            User admin = new Admin { Email = "admin@admin.com", Name = "admin" };
            _context.Set<User>().Add(admin);
            _context.SaveChanges();

            admin.Name = "foo";
            _repository.Update(admin);

            Assert.AreEqual("foo", _context.Admins.First().Name);
        }

        [TestMethod]
        public void CanAddANewUserToTheDatabase()
        {
            User admin = new Admin { Email = "admin@admin.com", Name = "admin" };
            User citizen = new Citizen { Email = "citizen@citizen.com", Name = "citizen" };

            _repository.Add(admin);
            _repository.Add(citizen);

            Assert.AreEqual(2, _context.Set<User>().Count());
            Assert.AreEqual(1, _context.Admins.Count());
            Assert.AreEqual(1, _context.Set<Citizen>().Count());
        }

        [TestMethod]
        public void CanDeleteAnUserFromTheDatabase()
        {
            User admin = new Admin { Email = "admin@admin.com", Name = "admin" };

            _context.Set<User>().Add(admin);
            _context.SaveChanges();

            Assert.AreEqual(1, _context.Admins.Count());
            _repository.Remove(admin);

            Assert.AreEqual(0, _context.Admins.Count());
            Assert.AreEqual(0, _context.Set<User>().Count());
        }
    }
}