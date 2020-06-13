namespace IMMRequest.DataAccess.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Core.Repositories;
    using Domain;

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

            Assert.AreEqual(1, _context.Requests.Count());
            Assert.AreEqual(request, _context.Requests.First());
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

            Assert.AreEqual(1, _context.Requests.Count());
            Assert.AreEqual("New Details", _context.Requests.First().Details);
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
            _context.Requests.AddRange(NewRequest(), newRequest);
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

            Assert.AreEqual(0, _context.Requests.Count());
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

        [TestMethod]
        public void CanSetAUsersRequestToAnAlreadyExistingUser()
        {
            var type = NewType();
            var topic = NewTopic();
            var area = NewArea();
            area.Topics = new List<Topic> { topic };
            topic.Types = new List<Type> { type };

            var citizen = new Citizen { Email = "citizen@mail.com", Name = "Citizen Name" };
            _context.Citizens.Add(citizen);
            _context.SaveChanges();

            var request = new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Citizen Name" },
                Details = "new request details",
                Type = type,
            };

            _repository.Add(request);

            var requestInDb = _repository.Get(request.Id);

            Assert.AreSame(citizen, requestInDb.Citizen);
        }

        [TestMethod]
        public void CanGetTheFirstRequestThatHasAFulfillsAPredicate()
        {
            var type = NewType();
            var topic = NewTopic();
            var area = NewArea();
            area.Topics = new List<Topic> { topic };
            topic.Types = new List<Type> { type };

            _context.Add(new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Citizen Name" },
                Details = "request details",
                Type = type,
            });

            _context.Add(new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Citizen Name" },
                Details = "new request details",
                Type = type,
            });
            _context.SaveChanges();

            Assert.AreEqual("new request details", _repository.FirstOrDefault(r => r.Id == 2).Details);
        }

        [TestMethod]
        public void CanCheckARequestExists()
        {
            var type = NewType();
            var topic = NewTopic();
            var area = NewArea();
            area.Topics = new List<Topic> { topic };
            topic.Types = new List<Type> { type };

            _context.Add(new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Citizen Name" },
                Details = "request details",
                Type = type,
            });

            _context.SaveChanges();

            Assert.IsTrue(_repository.Exists(r => r.Id == 1));
        }

    }
}
