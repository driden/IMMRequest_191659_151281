namespace IMMRequest.Logic.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Exceptions;
    using Exceptions.CreateTopic;
    using Exceptions.RemoveType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Models.Type;
    using Moq;
    using AdditionalFieldModel = Models.AdditionalFieldModel;

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

        #region Add Type
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
            var createRequest = new CreateTypeRequest { TopicId = 1, AdditionalFields = new List<AdditionalFieldModel>() };
            createRequest.AdditionalFields.Add(new AdditionalFieldModel { FieldType = "invalid", Name = "name" });

            Assert.ThrowsException<InvalidFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AnEmptyTypeAdditionalFieldShouldThrowExceptionEvenIfOneIsValid()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, AdditionalFields = new List<AdditionalFieldModel>() };
            createRequest.AdditionalFields.Add(new AdditionalFieldModel { FieldType = string.Empty, Name = "name" });

            Assert.ThrowsException<InvalidFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AnInvalidTypeAdditionalFieldShouldThrowExceptionEvenIfOneIsValid()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, AdditionalFields = new List<AdditionalFieldModel>() };
            createRequest.AdditionalFields.Add(new AdditionalFieldModel { Name = "foo", FieldType = "text" });
            createRequest.AdditionalFields.Add(new AdditionalFieldModel { Name = "bar", FieldType = "invalid" });

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
        public void ATopicWithAnExistingTypeNameShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "name" };
            _topicsMock
                .Setup(repo => repo.Get(1))
                .Returns(new Topic
                {
                    Name = "topic",
                    Types = new List<Type> { new Type { Name = "name" } }
                });

            Assert.ThrowsException<ExistingTypeNameException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void CantAddATypeWithMultipleAdditionalFieldsNameTheSame()
        {
            var createRequest = new CreateTypeRequest
            {
                TopicId = 1,
                AdditionalFields = new List<AdditionalFieldModel>
                {
                    new AdditionalFieldModel { Name = "foo", FieldType = "text"},
                    new AdditionalFieldModel { Name = "foo", FieldType = "int"}
                }
            };

            Assert.ThrowsException<InvalidAdditionalFieldForTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void AFieldWithNoNameSpecifiedShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new AdditionalFieldModel
                {
                    FieldType = "int",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "0" }, new FieldRequestModel { Value = "5" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidNameForAdditionalFieldException>(() => _typesLogic.Add(createRequest));
        }
        [TestMethod]
        public void AFieldWithAnInvalidIntRangeShouldThrowException()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newTopic" };
            createRequest.AdditionalFields.Add(
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
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
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
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
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
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
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
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
                new AdditionalFieldModel
                {
                    FieldType = "date",
                    Name = "additionalFieldName",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "blah" }, new FieldRequestModel { Value = string.Empty } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Assert.ThrowsException<InvalidFieldValueCastForFieldTypeException>(() => _typesLogic.Add(createRequest));
        }

        [TestMethod]
        public void CanAddANewTypeWithTextAdditionalField()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newType" };
            createRequest.AdditionalFields.Add(
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
                    FieldType = "text",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "blah" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            _typesMock.Setup(repo => repo.Add(It.IsAny<Type>())).Verifiable();
            _typesLogic.Add(createRequest);

            _typesMock.Verify(f => f.Add(It.IsAny<Type>()), Times.Once());
        }

        [TestMethod]
        public void CanAddANewTypeWithDateAdditionalField()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newType" };
            createRequest.AdditionalFields.Add(
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
                    FieldType = "int",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "5" }, new FieldRequestModel { Value = "6" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            _typesMock.Setup(repo => repo.Add(It.IsAny<Type>())).Verifiable();
            _typesLogic.Add(createRequest);

            _typesMock.Verify(f => f.Add(It.IsAny<Type>()), Times.Once());
        }

        [TestMethod]
        public void CanAddANewTypeWithIntegerAdditionalField()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newType" };
            createRequest.AdditionalFields.Add(
                new AdditionalFieldModel
                {
                    Name = "additionalFieldName",
                    FieldType = "date",
                    Range = new List<FieldRequestModel> { new FieldRequestModel { Value = "15/6/2020" }, new FieldRequestModel { Value = "16/6/2020" } }
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            _typesMock.Setup(repo => repo.Add(It.IsAny<Type>())).Verifiable();
            _typesLogic.Add(createRequest);

            _typesMock.Verify(f => f.Add(It.IsAny<Type>()), Times.Once());
        }

        [TestMethod]
        public void CanAddANewTypeWithAdditionalFieldsAndNoRange()
        {
            var createRequest = new CreateTypeRequest { TopicId = 1, Name = "newType" };
            createRequest.AdditionalFields.Add(
                new AdditionalFieldModel
                {
                    Name = "fieldName",
                    IsRequired = true,
                    FieldType = "text",
                    Range = new List<FieldRequestModel>()
                });

            _topicsMock.Setup(repo => repo.Get(1)).Returns(new Topic { Name = "name", Id = 1 });
            Type typeBeingAdded = null;
            _typesMock.Setup(repo => repo.Add(It.IsAny<Type>())).Callback<Type>(type => typeBeingAdded = type);

            _typesLogic.Add(createRequest);
            var additionalField = typeBeingAdded.AdditionalFields.First();

            Assert.AreEqual("fieldName", additionalField.Name);
            Assert.AreEqual(true, additionalField.IsRequired);
        }

        #endregion Add Type

        #region Remove Type

        [TestMethod]
        public void DeletingATypeWithNegativeIdThrowsException()
        {
            Assert.ThrowsException<InvalidIdException>(() => _typesLogic.Remove(-1));
        }

        [TestMethod]
        public void DeletingANonExistingTypeThrowsException()
        {
            _typesMock.Setup(m => m.Get(It.IsAny<int>())).Returns<Type>(null);
            Assert.ThrowsException<NoSuchTypeException>(() => _typesLogic.Remove(1));
        }

        [TestMethod]
        public void DeletingADisabledTypeThrowsException()
        {
            _typesMock
                .Setup(m => m.Get(It.IsAny<int>()))
                .Returns(() => new Type { IsActive = false });
            Assert.ThrowsException<NoSuchTypeException>(() => _typesLogic.Remove(1));
        }

        [TestMethod]
        public void CanDeleteAnExistingType()
        {
            _typesMock
                .Setup(m => m.Get(It.IsAny<int>()))
                .Returns(() => new Type { Id = 1, IsActive = true });

            _typesMock.Setup(m => m.Remove(It.IsAny<Type>())).Verifiable();
            _typesLogic.Remove(1);

            _typesMock.Verify(m => m.Remove(It.IsAny<Type>()), Times.Once());
        }
        #endregion

        #region GetAllTypes

        [TestMethod]
        public void GetAllShouldCallTheDatabase()
        {
            _typesMock.Setup(repo => repo.GetAll()).Returns<IList<TypeModel>>(null).Verifiable();
            _typesLogic.GetAll(1);
            _typesMock.Verify(mock => mock.GetAll(), Times.Once());
        }
        #endregion
    }
}
