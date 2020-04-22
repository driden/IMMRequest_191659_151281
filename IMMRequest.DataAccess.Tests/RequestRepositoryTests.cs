using System.Collections.Generic;
using System.Linq;
using IMMRequest.DataAccess.Core.Repositories;
using IMMRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Type = IMMRequest.Domain.Type;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class RequestRepositoryTests : IMMRequestTestBase
    {
        private RequestRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _repository = new RequestRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanAddARequestToTheDatabase()
        {
            var request = NewRequest();

            _repository.Add(request);

            Assert.AreEqual(1, _context.Set<Request>().Count());
            Assert.AreEqual(request, _context.Set<Request>().First());
            Assert.AreEqual(1, _context.Set<Citizen>().Count());
            Assert.AreEqual(1, _context.Set<User>().Count());
        }

        [TestMethod]
        public void CanModifyARequestInTheDatabase()
        {
            var request = NewRequest();
            _context.Add(request);
            _context.SaveChanges();

            request.Details = "New Details";
            _repository.Update(request);

            Assert.AreEqual(1, _context.Set<Request>().Count());
            Assert.AreEqual("New Details", _context.Set<Request>().First().Details);
        }

        [TestMethod]
        public void CanGetARequestFromTheDatabase()
        {
            var request = NewRequest();
            _context.Add(request);
            _context.SaveChanges();


            Assert.AreEqual(request, _repository.Get(request.Id));
        }

        [TestMethod]
        public void CanGetAllRequestsFromTheDatabase()
        {
            var newRequest = NewRequest();
            newRequest.Citizen.Email = "new@email.com";
            _context.Set<Request>().AddRange(NewRequest(), newRequest);
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
        }

        [TestMethod]
        public void CanDeleteARequestFromTheDatabase()
        {
            var request = NewRequest();
            _context.Add(request);
            _context.SaveChanges();

            _repository.Remove(request);

            Assert.AreEqual(0, _context.Set<Request>().Count());
        }

        [TestMethod]
        public void CanGetARequestWithCorrespondingAreaTopicAndType()
        {
            var type = NewType();
            var topic = NewTopic();
            var area = NewArea();
            area.Topics = new List<Topic> { topic };
            topic.Types = new List<Type> { type };

            var request = new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Citizen Name" },
                Details = "new request details",
                Type = type,
            };

            _context.Add(request);
            _context.SaveChanges();

            var requestInDb = _repository.Get(request.Id);

            Assert.IsNotNull(requestInDb.Type);
        }

    }
}
