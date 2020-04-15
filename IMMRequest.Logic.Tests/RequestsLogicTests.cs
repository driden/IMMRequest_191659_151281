using IMMRequest.Logic.Core;
using IMMRequest.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using Moq;

namespace IMMRequest.Logic.Tests
{
    [TestClass]
    public class RequestsLogicTests : IMMRequestLogicTestBase
    {
        private RequestsLogic _requestsLogic;
        private Mock<IRepository<Request>> _requestRepo;
        private Mock<IRepository<Topic>> _topicRepo;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepo = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _topicRepo = new Mock<IRepository<Topic>>(MockBehavior.Strict);
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
        private void SetUpMocks()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _topicRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(NewTopic()).Verifiable();
        }

        //Tests para hacer
        // Que los gets de topic y type devuelvan algo
        // hacerlo reventar si no existen
        // que no devuelvan fruta? el tema aca es si quiero dejar solo el topicId en el request no dtendria drama
        // Citizen ya registrado o no? se agrega siempre creo, y si repite el mail?
    }
}
