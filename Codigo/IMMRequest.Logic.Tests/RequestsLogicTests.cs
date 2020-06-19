namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Exceptions.AdditionalField;
    using Core.Exceptions.Request;
    using Core.Exceptions.Type;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Domain.Fields;
    using Domain.States;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Error;
    using Models.Request;
    using Moq;
    using Type = Domain.Type;

    [TestClass]
    public class RequestsLogicTests : IMMRequestLogicTestBase
    {
        private RequestsLogic _requestsLogic;
        private Mock<IRepository<Request>> _requestRepositoryMock;
        private Mock<IRepository<Type>> _typeRepositoryMock;
        private Mock<IRepository<User>> _userRepositoryMock;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepositoryMock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _typeRepositoryMock = new Mock<IRepository<Type>>(MockBehavior.Strict);
            _userRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            _requestsLogic = new RequestsLogic(
                _requestRepositoryMock.Object,
                _typeRepositoryMock.Object
                );
        }

        #region Add Request Tests
        [TestMethod]
        public void CanCreateANewRequest()
        {
            SetUpAddMocks();
            _requestsLogic.Add(base.CreateRequestModel);

            _requestRepositoryMock.Verify(mock => mock.Add(It.IsAny<Request>()));
        }

        [TestMethod]
        public void NewRequestShouldHaveAnExistingTypeAssociated()
        {
            SetUpAddMocks();
            _requestsLogic.Add(CreateRequestModel);

            _typeRepositoryMock.Verify(tr => tr.Get(-1), Times.Once());
            _requestRepositoryMock.Verify(rr => rr.Add(It.IsAny<Request>()), Times.Once());
        }

        [TestMethod]
        public void NewRequestShouldThrowAnExceptionIfTypeIdDoesNotExist()
        {
            _requestRepositoryMock.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<Topic>(null)
                .Verifiable();
            Assert.ThrowsException<NoSuchTypeException>(() => { _requestsLogic.Add(CreateRequestModel); });
            _requestRepositoryMock.Verify(reqRepo => reqRepo.Add(It.IsAny<Request>()), Times.Never());
        }

        [TestMethod]
        public void NewRequestShouldAddDataForCitizen()
        {
            SetUpAddMocks();
            User request = null;
            _requestRepositoryMock.Setup(userRepo => userRepo.Add(It.IsAny<Request>()))
                .Callback<Request>(req =>
                {
                    request = new Citizen
                    {
                        Email = req.Citizen.Email,
                        Name = req.Citizen.Name,
                        PhoneNumber = req.Citizen.PhoneNumber
                    };
                }).Verifiable();

            _requestsLogic.Add(CreateRequestModel);
            Assert.AreEqual(-1, CreateRequestModel.TypeId);
            Assert.AreEqual(CreateRequestModel.Email, request.Email);
            Assert.AreEqual(CreateRequestModel.Name, request.Name);
            Assert.AreEqual(CreateRequestModel.Phone, request.PhoneNumber);
        }

        [TestMethod]
        public void SendingAFieldIfNoFieldIsDefinedShouldThrowException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>();

            var request = new CreateRequestModel
            {
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "text", Value = "some text"}
                }
            };

            _typeRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);
            _ = Assert.ThrowsException<InvalidAdditionalFieldForTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingAnUnknownFieldNameThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new IntegerField { Name = "number" } };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequestModel
            {
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "text", Value = "some text"}
                }
            };

            Assert.ThrowsException<NoSuchAdditionalFieldException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingANonCorrespondingFieldIntegerTypeThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new IntegerField { Name = "number" } };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequestModel
            {
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "number", Value = "some text"}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingANonCorrespondingFieldDateTypeThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new DateField { Name = "date" } };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequestModel
            {
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "date", Value = "some text"}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingANonCorrespondingFieldTextTypeThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new TextField { Name = "text" } };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequestModel
            {
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "text", Value = ""}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingAnEmptySpaceInsteadOfTextThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new TextField { Name = "text" } };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequestModel
            {
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "text", Value = "  "}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void ProvidingLessThanExpectedRequiredFieldsThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new TextField { Name = "text", IsRequired = true }
            };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            Assert.ThrowsException<LessAdditionalFieldsThanRequiredException>(() => _requestsLogic.Add(CreateRequestModel));
        }

        [TestMethod]
        public void ProvidingOnlySomeOfTheExpectedRequiredFieldsThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new TextField { Name = "text", IsRequired = true },
                new IntegerField { Name = "number", IsRequired = true },
                new DateField { Name = "date", IsRequired = false }
            };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequestModel;
            var requestFields = new List<FieldRequestModel> { new FieldRequestModel { Name = "number", Value = "-1" } };
            requestFields.Add(new FieldRequestModel { Name = "text", Value = "some text" });


            Assert.ThrowsException<LessAdditionalFieldsThanRequiredException>(() => _requestsLogic.Add(CreateRequestModel));
        }

        [TestMethod]
        public void ProvidingAllTheRequiredFieldsShouldAllowToAddRequest()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new TextField { Name = "text", IsRequired = true },
                new IntegerField { Name = "number", IsRequired = true },
                new DateField { Name = "date", IsRequired = false }
            };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequestModel;
            var requestFields = new List<FieldRequestModel>
            {
                new FieldRequestModel {Name = "number", Value = "-1"},
                new FieldRequestModel {Name = "text", Value = "some text"}
            };

            request.AdditionalFields = requestFields;

            _requestRepositoryMock.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
            _requestsLogic.Add(request);
            _requestRepositoryMock.Verify(reqRepo => reqRepo.Add(It.IsAny<Request>()), Times.Once());
        }

        [TestMethod]
        public void ProvidingAnIntegerFieldOutOfRangeShouldThrowException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new IntegerField
                {
                    Name = "number",
                    IsRequired = true,
                    Range = new List<IntegerItem>
                    {
                        new IntegerItem { Value = 0},
                        new IntegerItem { Value = 2}
                    }
                }
            };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequestModel;
            request.AdditionalFields = new List<FieldRequestModel> { new FieldRequestModel { Name = "number", Value = "-1" } };

            _requestRepositoryMock.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
            Assert.ThrowsException<InvalidFieldRangeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void ProvidingATextFieldOutOfRangeShouldThrowException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new TextField
                {
                    Name = "text",
                    IsRequired = true,
                    Range = new List<TextItem>
                    {
                        new TextItem { Value = "value"},
                    }
                }
            };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequestModel;
            request.AdditionalFields = new List<FieldRequestModel> { new FieldRequestModel { Name = "text", Value = "-1" } };

            _requestRepositoryMock.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
            Assert.ThrowsException<InvalidFieldRangeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void ProvidingADateFieldOutOfRangeShouldThrowException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new DateField
                {
                    Name = "date",
                    IsRequired = true,
                    Range = new List<DateItem>
                    {
                        new DateItem { Value = DateTime.Parse("01/05/1994")},
                        new DateItem { Value = DateTime.Parse("05/05/1994")},
                    }
                }
            };

            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequestModel;
            request.AdditionalFields = new List<FieldRequestModel> { new FieldRequestModel { Name = "date", Value = "01/04/1994" } };

            _requestRepositoryMock.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
            Assert.ThrowsException<InvalidFieldRangeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void NewRequestShouldContainAdditionalTextFields()
        {
            IList<RequestField> listOfFields = null;
            _requestRepositoryMock.Setup(x => x.Add(It.IsAny<Request>()))
                .Callback<Request>(req => { listOfFields = req.FieldValues; })
                .Verifiable();

            var request = new CreateRequestModel
            {
                Name = "Name Request",
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "text", Value = "some text"}
                }
            };

            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new TextField
                {
                    Name = "text",
                    IsRequired = true,
                }
            };

            _typeRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);

            _requestsLogic.Add(request);

            TextRequestField textRequestField = ((TextRequestField)listOfFields[0]);
            Assert.AreEqual(textRequestField.Name, "text");
            Assert.AreEqual(textRequestField.Value, "some text");
        }

        [TestMethod]
        public void NewRequestShouldContainAdditionalIntegerFields()
        {
            IList<RequestField> listOfFields = null;
            _requestRepositoryMock.Setup(x => x.Add(It.IsAny<Request>()))
                .Callback<Request>(req => { listOfFields = req.FieldValues; })
                .Verifiable();

            var request = new CreateRequestModel
            {
                Name = "Name Request",
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "num", Value = "5"}
                }
            };

            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new IntegerField
                {
                    Name = "num",
                    IsRequired = true,
                }
            };

            _typeRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);

            _requestsLogic.Add(request);

            IntRequestField intRequestField = (IntRequestField)listOfFields[0];
            Assert.AreEqual(intRequestField.Name, "num");
            Assert.AreEqual(intRequestField.Value, 5);
        }

        [TestMethod]
        public void NewRequestShouldContainAdditionalDateFields()
        {
            IList<RequestField> listOfFields = null;
            _requestRepositoryMock.Setup(x => x.Add(It.IsAny<Request>()))
                .Callback<Request>(req => { listOfFields = req.FieldValues; })
                .Verifiable();

            var request = new CreateRequestModel
            {
                Name = "Name Request",
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "date", Value = "05/11/1981"}
                }
            };

            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>
            {
                new DateField
                {
                    Name = "date",
                    IsRequired = true,
                }
            };

            _typeRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);

            _requestsLogic.Add(request);

            DateRequestField dateRequestField = (DateRequestField)listOfFields[0];
            Assert.AreEqual(dateRequestField.Name, "date");
            Assert.AreEqual(dateRequestField.Value, DateTime.Parse("05/11/1981"));

        }

        [TestMethod]
        public void NewRequestTypeIdShouldBeProvidedByUser()
        {
            var request = new CreateRequestModel
            {
                Name = "Name Request",
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequestModel>
                {
                   new FieldRequestModel { Name = "date", Value = "05/11/1981"}
                }
            };

            Assert.AreEqual(-1, request.TypeId);
        }
        #endregion Add Request Tests

        [TestMethod]
        public void CanGetTheRequestStatusWithAValidRequestId()
        {
            var request = NewRequest();
            request.FieldValues = GetSomeAdditionalFields();

            _requestRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(request);
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
            _requestRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(() => null);

            Assert.ThrowsException<NoSuchRequestException>(() => _requestsLogic.GetRequestStatus(1));
        }

        [TestMethod]
        public void CantGetARequestStatusWithAnInvalidRequestId()
        {
            Assert.ThrowsException<InvalidRequestIdException>(() => _requestsLogic.GetRequestStatus(-1));
        }

        [TestMethod]
        public void CanGetAllRequests()
        {
            var request = NewRequest();
            _requestRepositoryMock.Setup(x => x.GetAll()).Returns(new[] { request });

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
            _requestRepositoryMock.Setup(x => x.Get(1)).Returns<Request>(null);

            Assert.ThrowsException<NoSuchRequestException>(() => _requestsLogic.UpdateRequestStatus(1, "Accepted"));
        }

        [TestMethod]
        public void CanUpdateARequestFromCreatedToInReview()
        {
            var req = NewRequest();
            _requestRepositoryMock.Setup(x => x.Get(1)).Returns(req);
            _requestRepositoryMock.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "InReview");

            Assert.AreEqual("InReview", req.Status.ToString());
            _requestRepositoryMock.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromInReviewToAccepted()
        {
            var req = NewRequest();
            req.Status = new InReviewState(req);
            _requestRepositoryMock.Setup(x => x.Get(1)).Returns(req);
            _requestRepositoryMock.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Accepted");

            Assert.AreEqual("Accepted", req.Status.ToString());
            _requestRepositoryMock.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromInReviewToDenied()
        {
            var req = NewRequest();
            req.Status = new InReviewState(req);
            _requestRepositoryMock.Setup(x => x.Get(1)).Returns(req);
            _requestRepositoryMock.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Denied");

            Assert.AreEqual("Denied", req.Status.ToString());
            _requestRepositoryMock.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromAcceptedToDone()
        {
            var req = NewRequest();
            req.Status = new AcceptedState(req);
            _requestRepositoryMock.Setup(x => x.Get(1)).Returns(req);
            _requestRepositoryMock.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Done");

            Assert.AreEqual("Done", req.Status.ToString());
            _requestRepositoryMock.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanUpdateARequestFromDeniedToDone()
        {
            var req = NewRequest();
            req.Status = new DeniedState(req);
            _requestRepositoryMock.Setup(x => x.Get(1)).Returns(req);
            _requestRepositoryMock.Setup(x => x.Update(req)).Verifiable();

            _requestsLogic.UpdateRequestStatus(1, "Done");

            Assert.AreEqual("Done", req.Status.ToString());
            _requestRepositoryMock.Verify(repo => repo.Update(req), Times.Exactly(1));
        }

        [TestMethod]
        public void CanSendAnErrorMessageToTheUser()
        {
            var error = new ErrorModel("msg");
            Assert.AreEqual("msg", error.Error);
        }

        private void SetUpAddMocks()
        {
            _requestRepositoryMock.Setup(x => x.Add(It.IsAny<Request>())).Verifiable();
            _typeRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(NewType()).Verifiable();
        }
    }
}
