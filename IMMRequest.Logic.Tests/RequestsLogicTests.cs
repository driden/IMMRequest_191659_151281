using IMMRequest.Logic.Core;
using IMMRequest.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using Moq;
using IMMRequest.Logic.Exceptions;

namespace IMMRequest.Logic.Tests
{
    [TestClass]
    public class RequestsLogicTests : IMMRequestLogicTestBase
    {
        private RequestsLogic _requestsLogic;
        private Mock<IRepository<Request>> _requestRepo;
        private Mock<IRepository<Topic>> _topicRepo;
        private Mock<IRepository<User>> _userRepo;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepo = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _topicRepo = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            _userRepo = new Mock<IRepository<User>>(MockBehavior.Strict);
            _requestsLogic = new RequestsLogic(_requestRepo.Object, _topicRepo.Object);
        }

        [TestMethod]
        public void CanCreateANewRequest()
        {
            SetUpMocks();
            _requestsLogic.Add(CreateRequest);

            _requestRepo.Verify(mock => mock.Add(It.IsAny<Request>()));
        }

        [TestMethod]
        public void NewRequestShouldHaveAnExistingTopicAssociated()
        {
            SetUpMocks();
            _requestsLogic.Add(CreateRequest);

            _topicRepo.Verify(tr => tr.Get(-1), Times.Once());
            _requestRepo.Verify(rr => rr.Add(It.IsAny<Request>()), Times.Once());
        }

        [TestMethod]
        public void NewRequestShouldThrowAnExceptionIfTopicIdDoesNotExist()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _topicRepo.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Topic>(null)
                .Verifiable();
            Assert.ThrowsException<NoSuchTopicException>(() => { _requestsLogic.Add(CreateRequest); });
            _requestRepo.Verify(reqRepo => reqRepo.Add(It.IsAny<Request>()), Times.Never());
        }

        [TestMethod]
        public void NewRequestShouldAddDataForCitizen()
        {
            SetUpMocks();
            User request = null;
            _requestRepo.Setup(userRepo => userRepo.Add(It.IsAny<Request>()))
                .Callback<Request>((req) =>
                {
                    request = new Citizen
                    {
                        Email = req.Citizen.Email,
                        Name = req.Citizen.Name,
                        PhoneNumber = req.Citizen.PhoneNumber
                    };
                }).Verifiable();

            _requestsLogic.Add(CreateRequest);
            Assert.AreEqual(CreateRequest.Email, request.Email);
            Assert.AreEqual(CreateRequest.Name, request.Name);
            Assert.AreEqual(CreateRequest.Phone, request.PhoneNumber);
        }

        private void SetUpMocks()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _topicRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(NewTopic()).Verifiable();
        }
    }
}
