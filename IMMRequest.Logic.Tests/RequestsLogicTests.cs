namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Domain.Fields;
    using Domain.States;
    using Exceptions;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using Type = Domain.Type;

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
        public void CanGetTheRequestStatusWithAValidRequestId()
        {
            var request = NewRequest();
            request.FieldValues = GetSomeAdditionaFields();

            _requestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(request);
            var requestResponse = _requestsLogic.GetRequestStatus(1);

            Assert.AreEqual(request.Citizen.Email, requestResponse.CitizenEmail);
            Assert.AreEqual(request.Citizen.Name, requestResponse.CitizenName);
            Assert.AreEqual(request.Citizen.PhoneNumber, requestResponse.CitizenPhoneNumber);
            Assert.AreEqual(request.Details, requestResponse.Details);
            Assert.AreEqual(request.Status.Description, requestResponse.RequestState);
            Assert.AreEqual(1, requestResponse.RequestId);
            CollectionAssert.AreEqual(
            new List<FieldRequestModel>{
                new FieldRequestModel { Name = "num", Value = "4"},
                new FieldRequestModel { Name = "text", Value = "some text"},
                new FieldRequestModel { Name = "date", Value = DateTime.Today.ToString("G")}
            },
            requestResponse.Fields.ToList());
        }

        [TestMethod]
        public void CantGetARequestStatusOfANullRequest()
        {
            _requestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(() => null);

            Assert.ThrowsException<NoSuchRequestException>(() => this._requestsLogic.GetRequestStatus(1));
        }

        [TestMethod]
        public void CantGetARequestStatusWithAnInvalidRequestId()
        {
            Assert.ThrowsException<InvalidRequestIdException>(() => this._requestsLogic.GetRequestStatus(-1));
        }

        [TestMethod]
        public void CanGetAllRequests()
        {
            var request = NewRequest();
            _requestRepo.Setup(x => x.GetAll()).Returns(new[] { request });

            var allRequests = _requestsLogic.GetAllRequests().ToList();

            Assert.AreEqual(1, allRequests.Count);

            var first = allRequests.First();
            Assert.AreEqual(request.Status.Description, first.Status);
            Assert.AreEqual(request.Citizen.Email, first.RequestedBy);
            Assert.AreEqual(request.Id, first.RequestId);
            Assert.AreEqual(request.Details, first.Details);
        }

        [TestMethod]
        public void CantUpdateARequestToAnEmptyState()
        {
            Assert.ThrowsException<InvalidStateNameException>(() => _requestsLogic.UpdateRequestStatus(1, string.Empty));
        }

        [TestMethod]
        public void CantUpdateARequestToANullState()
        {
            Assert.ThrowsException<InvalidStateNameException>(() => _requestsLogic.UpdateRequestStatus(1, null));
        }

        [TestMethod]
        public void CantUpdateARequestToAWhiteSpaceState()
        {
            Assert.ThrowsException<InvalidStateNameException>(() => _requestsLogic.UpdateRequestStatus(1, "  "));
        }

        [TestMethod]
        public void CantUpdateARequestToANonExistentState()
        {
            Assert.ThrowsException<InvalidStateNameException>(() => _requestsLogic.UpdateRequestStatus(1, "state"));
        }

        [TestMethod]
        public void CantUpdateARequestWithAnInvalidId()
        {
            Assert.ThrowsException<InvalidRequestIdException>(() => _requestsLogic.UpdateRequestStatus(-1, "Accepted"));
        }

        [TestMethod]
        public void CantUpdateANonExistentRequest()
        {
            _requestRepo.Setup(x => x.Get(1)).Returns<Request>(null);

            Assert.ThrowsException<NoSuchRequestException>(() => _requestsLogic.UpdateRequestStatus(1, "Accepted"));
        }

        [TestMethod]
        public void CanUpdateARequestFromCreatedToInReview()
        {
            var req = NewRequest();
            _requestRepo.Setup(x => x.Get(1)).Returns(req);
            _requestRepo.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "InReview");

            Assert.AreEqual("InReview", req.Status.ToString());
            _requestRepo.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromInReviewToAccepted()
        {
            var req = NewRequest();
            req.Status = new InReviewState(req);
            _requestRepo.Setup(x => x.Get(1)).Returns(req);
            _requestRepo.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Accepted");

            Assert.AreEqual("Accepted", req.Status.ToString());
            _requestRepo.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromInReviewToDenied()
        {
            var req = NewRequest();
            req.Status = new InReviewState(req);
            _requestRepo.Setup(x => x.Get(1)).Returns(req);
            _requestRepo.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Denied");

            Assert.AreEqual("Denied", req.Status.ToString());
            _requestRepo.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromAcceptedToDone()
        {
            var req = NewRequest();
            req.Status = new AcceptedState(req);
            _requestRepo.Setup(x => x.Get(1)).Returns(req);
            _requestRepo.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Done");

            Assert.AreEqual("Done", req.Status.ToString());
            _requestRepo.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromDeniedToDone()
        {
            var req = NewRequest();
            req.Status = new DeniedState(req);
            _requestRepo.Setup(x => x.Get(1)).Returns(req);
            _requestRepo.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Done");

            Assert.AreEqual("Done", req.Status.ToString());
            _requestRepo.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        private void SetUpAddMocks()
        {
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(NewType()).Verifiable();
        }
    }
}
