namespace IMMRequest.DataAccess.Tests
{
    using System.Linq;
    using IMMRequest.DataAccess.Core.Repositories;
    using IMMRequest.Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CitizenRepositoryTest : IMMRequestTestBase
    {
        private CitizenRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new CitizenRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanAddCitizenToDatabase()
        {
            _repository.Add(NewCitizen());

            Assert.AreEqual(1, _context.Citizens.Count());
        }
        
        [TestMethod]
        public void CanModifyACitizenIntheDatabase()
        {
            var citizen = NewCitizen();
            _context.Citizens.Add(citizen);
            _context.SaveChanges();

            citizen.Name = "New Citizen";
            _repository.Update(citizen);
            
            Assert.AreEqual("New Citizen", _context.Citizens.First().Name);
            Assert.AreEqual(1, _context.Citizens.Count());
        }
        
        [TestMethod]
        public void CanGetACitizenFromtheDatabase()
        {
            var citizen = NewCitizen();
            _context.Citizens.Add(citizen);
            _context.SaveChanges();

            Assert.AreEqual(citizen, _repository.Get(citizen.Id));
        }
        
        [TestMethod]
        public void CanGetAllCitizensFromTheDatabase()
        {
            _context.Citizens.AddRange(NewCitizen(), NewCitizen());
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
        }
        
        [TestMethod]
        public void CanDeleteACitizenIntheDatabase()
        {
            var citizen = NewCitizen();
            _context.Citizens.Add(citizen);
            _context.SaveChanges();
            _repository.Remove(citizen);
            Assert.AreEqual(0, _context.Citizens.Count());
        }
        
        protected Citizen NewCitizen()
        {
            return new Citizen
            {
                Name = "TestTopic",
                Email = "citizen@mail.com",
                PhoneNumber = "+(598)899-980-878"
            };
        }
    }
}