namespace IMMRequest.Logic.Tests
{
    using System.Collections.Generic;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Exceptions;
    using Exceptions.CreateTopic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;

    [TestClass]
    public class TypesLogicTests
    {
        private Mock<IRepository<Topic>> _topicsMock;
        private Mock<IRepository<Type>> _typesMock;
        private TypesLogic _typesLogic;

        [TestInitialize]
        public void SetUp()
        {
            _topicsMock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            _typesMock = new Mock<IRepository<Type>>(MockBehavior.Strict);
            _typesLogic = new TypesLogic(_topicsMock.Object, _typesMock.Object);
        }

        [TestMethod]
        public void CantCreateTypeWithNegativeTopicId()
        {
            Assert.ThrowsException<InvalidTopicIdException>(() => _typesLogic.Add(new CreateTypeRequest { TopicId = -1 }));
        }

        [TestMethod]
        public void CantCreateTypeWithANonExistingTopic()
        {
            _topicsMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns<Topic>(null);
            Assert.ThrowsException<NoSuchTopicException>(() => _typesLogic.Add(new CreateTypeRequest { TopicId = 1 }));
        }

        [TestMethod]
        public void AnInvalidTypeAdditionalFieldShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, AdditionalFields = new List<NewTypeAdditionalField>() };
            createRequest.AdditionalFields.Add(new NewTypeAdditionalField { FieldType = "invalid" });

            Assert.ThrowsException<InvalidFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AnEmptyTypeAdditionalFieldShouldThrowExceptionEvenIfOneIsValid()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, AdditionalFields = new List<NewTypeAdditionalField>() };
            createRequest.AdditionalFields.Add(new NewTypeAdditionalField { FieldType = string.Empty });

            Assert.ThrowsException<InvalidFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AnInvalidTypeAdditionalFieldShouldThrowExceptionEvenIfOneIsValid()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, AdditionalFields = new List<NewTypeAdditionalField>() };
            createRequest.AdditionalFields.Add(new NewTypeAdditionalField { FieldType = "text" });
            createRequest.AdditionalFields.Add(new NewTypeAdditionalField { FieldType = "invalid" });

            Assert.ThrowsException<InvalidFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void ATopicWithAnEmptyNameShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = string.Empty };
            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name" });

            Assert.ThrowsException<EmptyTypeNameException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void ATopicWithAnExistingNameShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "name" };
            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name" });

            Assert.ThrowsException<ExistingTypeNameException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AFieldWithAnInvalidIntRangeShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new NewTypeAdditionalField
                {
                    FieldType = "int",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "6" }, new FieldRequestModel { Value = "5" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidFieldRangeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AFieldWithAnInvalidDateRangeShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new NewTypeAdditionalField
                {
                    FieldType = "date",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "09/05/2020" }, new FieldRequestModel { Value = "08/05/2020" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidFieldRangeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AFieldWithAnInvalidTextRangeShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new NewTypeAdditionalField
                {
                    FieldType = "text",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "blah" }, new FieldRequestModel { Value = string.Empty } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidFieldRangeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AnAdditionalFieldWithWrongDataTypeShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new NewTypeAdditionalField
                {
                    FieldType = "int",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "blah" }, new FieldRequestModel { Value = string.Empty } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AnAdditionalFieldWithWrongDateTypeShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new NewTypeAdditionalField
                {
                    FieldType = "date",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "blah" }, new FieldRequestModel { Value = string.Empty } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void CanAddANewType()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newType" };
            createRequest.AdditionalFields.Add(
                new NewTypeAdditionalField
                {
                    FieldType = "text",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "blah" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            _typesMock.Setup(repo => repo.Add(It.IsAny<Type>())).Verifiable();
            _typesLogic.Add(createRequest);

            _typesMock.Verify(f => f.Add(It.IsAny<Type>()), Times.Once());
        }
    }
}
