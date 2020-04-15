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
        private Mock<IRequestRepository> _requestRepo;
        private Mock<IAreaRepository> _areaRepo;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepo = new Mock<IRequestRepository>(MockBehavior.Strict);
            _areaRepo = new Mock<IAreaRepository>(MockBehavior.Strict);
            _requestsLogic = new RequestsLogic(_requestRepo.Object, _areaRepo.Object);
        }

        [TestMethod]
        public void CanCreateANewRequest()
        {
            SetUpMocks();
            _requestsLogic.Add(CreateRequest);

            _requestRepo.Verify(mock => mock.Add(It.IsAny<Request>()));
        }

        private void SetUpMocks()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _areaRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(NewArea()).Verifiable();
        }

        [TestMethod]
        public void NewRequestShouldHaveAnExistingAreaAssociated()
        {
            var area = NewArea();
            _areaRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(area).Verifiable();
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();

            _requestsLogic.Add(CreateRequest);

            _areaRepo.Verify(ar => ar.Get(-1), Times.Once());
            _requestRepo.Verify(rr => rr.Add(It.IsAny<Request>()), Times.Once());
        }
    }
}
