using IMMRequest.Logic.Core;
using IMMRequest.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using Moq;
using IMMRequest.Logic.Exceptions;
using System.Collections.Generic;

namespace IMMRequest.Logic.Tests
{
    [TestClass]
    public class RequestsLogicTests : IMMRequestLogicTestBase
    {
        private RequestsLogic _requestsLogic;
        private Mock<IRepository<Request>> _requestRepo;
        private Mock<IRepository<Type>> _typeRepo;
        private Mock<IRepository<User>> _userRepo;
        private Mock<IAreaQueries> _areaQueries;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepo = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _typeRepo = new Mock<IRepository<Type>>(MockBehavior.Strict);
            _userRepo = new Mock<IRepository<User>>(MockBehavior.Strict);
            _areaQueries = new Mock<IAreaQueries>(MockBehavior.Strict);
            _requestsLogic = new RequestsLogic(
                _requestRepo.Object,
                _typeRepo.Object,
                _areaQueries.Object
                );
        }

        [TestMethod]
        public void CanCreateANewRequest()
        {
            SetUpAddMocks();
            _requestsLogic.Add(CreateRequest);

            _requestRepo.Verify(mock => mock.Add(It.IsAny<Request>()));
        }

        [TestMethod]
        public void NewRequestShouldHaveAnExistingTypeAssociated()
        {
            SetUpAddMocks();
            _requestsLogic.Add(CreateRequest);

            _typeRepo.Verify(tr => tr.Get(-1), Times.Once());
            _requestRepo.Verify(rr => rr.Add(It.IsAny<Request>()), Times.Once());
        }

        [TestMethod]
        public void NewRequestShouldThrowAnExceptionIfTypeIdDoesNotExist()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _typeRepo.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Topic>(null)
                .Verifiable();
            Assert.ThrowsException<NoSuchTopicException>(() => { _requestsLogic.Add(CreateRequest); });
            _requestRepo.Verify(reqRepo => reqRepo.Add(It.IsAny<Request>()), Times.Never());
        }

        [TestMethod]
        public void NewRequestShouldAddDataForCitizen()
        {
            SetUpAddMocks();
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

        [TestMethod]
        public void NewRequestShouldContainAdditionalFields()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "date", Value = "01-12-1998"},
                   new FieldRequest { Name = "numero", Value = "52"},
                   new FieldRequest { Name = "text", Value = "some text"}
                }
            };

            _requestsLogic.Add(request);
        }


        //[TestMethod]
        //public void CantGetARequestStatusWithANegativeId()
        //{
        //    Assert.ThrowsException<InvalidGetRequestStatusException>(() => _requestsLogic.GetRequestStatus(-1));
        //}

        //[TestMethod]
        //public void CanGetTheRequestStatusWithAValidRequestId()
        //{
        //    var request = NewRequest();
        //    _requestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(request);

        //    var requestResponse = this._requestsLogic.GetRequestStatus(1);

        //    Assert.AreEqual(request.Citizen.Email, requestResponse.CitizenEmail);
        //    Assert.AreEqual(request.Citizen.Name, requestResponse.CitizenName);
        //    Assert.AreEqual(request.Citizen.PhoneNumber, requestResponse.CitizenPhoneNumber);
        //    Assert.AreEqual(request.Details, requestResponse.Details);
        //    Assert.AreEqual(request.Status.Description, requestResponse.RequestState);
        //}

        //[TestMethod]
        //public void CantGetARequestStatusWithAnInvalidRequestId()
        //{
        //    _requestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(() => null);

        //    var request = NewRequest();
        //    Assert.ThrowsException<NoSuchRequestException>(() => this._requestsLogic.GetRequestStatus(1));
        //}

        private void SetUpAddMocks()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(NewType()).Verifiable();
        }
    }
}
