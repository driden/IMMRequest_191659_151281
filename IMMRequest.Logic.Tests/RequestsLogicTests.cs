using IMMRequest.Logic.Core;
using IMMRequest.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using Moq;
using IMMRequest.Logic.Exceptions;
using System.Collections.Generic;
using System.Linq;
using IMMRequest.Domain.Fields;
using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Logic.Tests
{
    [TestClass]
    public class RequestsLogicTests : IMMRequestLogicTestBase
    {
        private RequestsLogic _requestsLogic;
        private Mock<IRepository<Request>> _requestRepo;
        private Mock<IRepository<Domain.Type>> _typeRepo;
        private Mock<IRepository<User>> _userRepo;
        private Mock<IAreaQueries> _areaQueries;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepo = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _typeRepo = new Mock<IRepository<Domain.Type>>(MockBehavior.Strict);
            _userRepo = new Mock<IRepository<User>>(MockBehavior.Strict);
            _areaQueries = new Mock<IAreaQueries>(MockBehavior.Strict);
            _requestsLogic = new RequestsLogic(
                _requestRepo.Object,
                _typeRepo.Object,
                _areaQueries.Object
                );
        }

        #region Add Request Tests
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
        public void SendingAFieldIfNoFieldIsDefinedShouldThrowException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField>();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "text", Value = "some text"}
                }
            };

            _typeRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);
            _ = Assert.ThrowsException<InvalidAdditionalFieldForTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingAnUnknownFieldNameThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new IntegerField { Name = "number" } };

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "text", Value = "some text"}
                }
            };

            Assert.ThrowsException<NoSuchAdditionalFieldException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingANonCorrespondingFieldIntegerTypeThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new IntegerField { Name = "number" } };

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "number", Value = "some text"}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingANonCorrespondingFieldDateTypeThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new DateField { Name = "date" } };

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "date", Value = "some text"}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingANonCorrespondingFieldTextTypeThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new TextField { Name = "text" } };

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "text", Value = ""}
                }
            };

            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void UsingAnEmptySpaceInsteadOfTextThrowsException()
        {
            var typeInDatabase = NewType();
            typeInDatabase.AdditionalFields = new List<AdditionalField> { new TextField { Name = "text" } };

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = new CreateRequest
            {
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "text", Value = "  "}
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

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            Assert.ThrowsException<LessAdditionalFieldsThanRequiredException>(() => _requestsLogic.Add(CreateRequest));
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

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequest;
            var requestFields = new List<FieldRequest> { new FieldRequest { Name = "number", Value = "-1" } };
            requestFields.Add(new FieldRequest { Name = "text", Value = "some text" });


            Assert.ThrowsException<LessAdditionalFieldsThanRequiredException>(() => _requestsLogic.Add(CreateRequest));
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

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequest;
            var requestFields = new List<FieldRequest> { new FieldRequest { Name = "number", Value = "-1" } };
            requestFields.Add(new FieldRequest { Name = "text", Value = "some text" });
            request.AdditionalFields = requestFields;

            _requestRepo.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
            _requestsLogic.Add(request);
            _requestRepo.Verify(reqRepo => reqRepo.Add(It.IsAny<Request>()), Times.Once());
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

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequest;
            request.AdditionalFields = new List<FieldRequest> { new FieldRequest { Name = "number", Value = "-1" } };

            _requestRepo.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
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

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequest;
            request.AdditionalFields = new List<FieldRequest> { new FieldRequest { Name = "text", Value = "-1" } };

            _requestRepo.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
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

            _typeRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(typeInDatabase).Verifiable();

            var request = CreateRequest;
            request.AdditionalFields = new List<FieldRequest> { new FieldRequest { Name = "date", Value = "01/04/1994" } };

            _requestRepo.Setup(mock => mock.Add(It.IsAny<Request>())).Verifiable();
            Assert.ThrowsException<InvalidFieldRangeException>(() => _requestsLogic.Add(request));
        }

        [TestMethod]
        public void NewRequestShouldContainAdditionalTextFields()
        {
            IList<RequestField> listOfFields = null;
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>()))
                .Callback<Request>(req => { listOfFields = req.FieldValues; })
                .Verifiable();

            var request = new CreateRequest
            {
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "text", Value = "some text"}
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

            _typeRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);

            _requestsLogic.Add(request);

            TextRequestField textRequestField = ((TextRequestField)listOfFields[0]);
            Assert.AreEqual(textRequestField.Name, "text");
            Assert.AreEqual(textRequestField.Value, "some text");
        }

        [TestMethod]
        public void NewRequestShouldContainAdditionalIntegerFields()
        {
            IList<RequestField> listOfFields = null;
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>()))
                .Callback<Request>(req => { listOfFields = req.FieldValues; })
                .Verifiable();

            var request = new CreateRequest
            {
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "num", Value = "5"}
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

            _typeRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);

            _requestsLogic.Add(request);

            IntRequestField intRequestField = (IntRequestField)listOfFields[0];
            Assert.AreEqual(intRequestField.Name, "num");
            Assert.AreEqual(intRequestField.Value, 5);
        }

        [TestMethod]
        public void NewRequestShouldContainAdditionalDateFields()
        {
            IList<RequestField> listOfFields = null;
            _requestRepo.Setup(x => x.Add(It.IsAny<Request>()))
                .Callback<Request>(req => { listOfFields = req.FieldValues; })
                .Verifiable();

            var request = new CreateRequest
            {
                Phone = "5555555",
                Email = "mail@mail.com",
                Details = "some details",
                AdditionalFields = new List<FieldRequest>
                {
                   new FieldRequest { Name = "date", Value = "05/11/1981"}
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

            _typeRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(typeInDatabase);

            _requestsLogic.Add(request);

            DateRequestField dateRequestField = (DateRequestField)listOfFields[0];
            Assert.AreEqual(dateRequestField.Name, "date");
            Assert.AreEqual(dateRequestField.Value, DateTime.Parse("05/11/1981"));

        }
#endregion Add Request Tests

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
