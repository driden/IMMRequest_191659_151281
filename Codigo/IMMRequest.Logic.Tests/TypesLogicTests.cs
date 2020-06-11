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
    using Exceptions;
    using Exceptions.CreateTopic;
    using Exceptions.RemoveType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Models.Type;
    using Moq;
    using AdditionalFieldModel = Models.AdditionalFieldModel;
    using Type = Domain.Type;

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

        [TestMethod]
        public void GetAllShouldListOnlyActiveTypes()
        {
            _typesMock.Setup(repo => repo.GetAll())
                .Returns(new List<Type>
                {
                    new Type { Name = "Active", IsActive = true, TopicId = 1},
                    new Type { Name = "Inactive", IsActive = false, TopicId = 1}
                })
                .Verifiable();

            var availableTypes = _typesLogic.GetAll(1).ToArray();
            Assert.AreEqual(1, availableTypes.Length);
            Assert.AreEqual("Active", availableTypes.FirstOrDefault()?.Name);

            _typesMock.Verify(mock => mock.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetAllShouldGetAllTheAdditionalFields()
        {
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>{
                    new IntegerField
                        {
                            Id = 1,
                            IsRequired = true,
                            Name = "Int Field",
                            Range = new List<IntegerItem>
                            {
                                new IntegerItem {Id = 1, IntegerFieldId = 1, Value = 1}
                            }
                        },
                    new DateField
                        {
                            Id = 2,
                            IsRequired = true,
                            Name = "Date Field",
                            Range = new List<DateItem>
                            {
                                new DateItem {Id = 2, DateFieldId = 2, Value = DateTime.Now}
                            }
                        },
                    new TextField
                        {
                            Id = 3,
                            IsRequired = true,
                            Name = "Text Field",
                            Range = new List<TextItem>
                            {
                                new TextItem {Id = 3, TextFieldId = 3, Value = "this is some text"}
                            }

                        }
                    },
                IsActive = true,
                TopicId = 1
            };

            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb }).Verifiable();

            var allTypes = _typesLogic.GetAll(1).ToArray();

            Assert.AreEqual(3, allTypes.First().AdditionalFields.Count);
        }

        [TestMethod]
        public void AnAdditionalIntegerFieldShouldHaveACorrespondingTypeName()
        {
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>{
                    new IntegerField
                        {
                            Id = 1,
                            IsRequired = true,
                            Name = "Int Field",
                            Range = new List<IntegerItem>
                            {
                                new IntegerItem {Id = 1, IntegerFieldId = 1, Value = 1}
                            }
                        }
                },
                IsActive = true,
                TopicId = 1
            };
            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb }).Verifiable();

            var type = _typesLogic.GetAll(1).FirstOrDefault();
            Assert.AreEqual("integer", type?.AdditionalFields[0].FieldType);

            _typesMock.Verify(mock => mock.GetAll(), Times.Once());
        }

        [TestMethod]
        public void AnAdditionalTextFieldShouldHaveACorrespondingTypeName()
        {
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>{
                    new TextField
                        {
                            Id = 3,
                            IsRequired = true,
                            Name = "Text Field",
                            Range = new List<TextItem>
                            {
                                new TextItem {Id = 3, TextFieldId = 3, Value = "this is some text"}
                            }
                        }
                },
                IsActive = true,
                TopicId = 1
            };

            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb }).Verifiable();

            var type = _typesLogic.GetAll(1).FirstOrDefault();
            Assert.AreEqual("text", type?.AdditionalFields[0].FieldType);

            _typesMock.Verify(mock => mock.GetAll(), Times.Once());
        }

        [TestMethod]
        public void AnAdditionalDateFieldShouldHaveACorrespondingTypeName()
        {
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>{
                    new DateField
                        {
                            Id = 2,
                            IsRequired = true,
                            Name = "Date Field",
                            Range = new List<DateItem>
                            {
                                new DateItem {Id = 2, DateFieldId = 2, Value = DateTime.Now}
                            }
                        },
                },
                IsActive = true,
                TopicId = 1
            };

            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb }).Verifiable();

            var type = _typesLogic.GetAll(1).FirstOrDefault();
            Assert.AreEqual("date", type?.AdditionalFields[0].FieldType);

            _typesMock.Verify(mock => mock.GetAll(), Times.Once());
        }

        [TestMethod]
        public void AnAdditionalIntegerFieldShouldHaveACorrespondingRangeValue()
        {
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>{
                    new IntegerField
                        {
                            Id = 1,
                            IsRequired = true,
                            Name = "Int Field",
                            Range = new List<IntegerItem>
                            {
                                new IntegerItem {Id = 1, IntegerFieldId = 1, Value = 1}
                            }
                        }
                },
                IsActive = true,
                TopicId = 1
            };
            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb });

            var type = _typesLogic.GetAll(1).FirstOrDefault();
            var range = type?.AdditionalFields[0].Range.ToArray();

            Assert.AreEqual("1", range?.First());
        }

        [TestMethod]
        public void AnAdditionalDateFieldShouldHaveACorrespondingRangeValue()
        {
            var now = DateTime.Now;
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>{
                    new DateField
                        {
                            Id = 2,
                            IsRequired = true,
                            Name = "Date Field",
                            Range = new List<DateItem>
                            {
                                new DateItem {Id = 2, DateFieldId = 2, Value = now }
                            }
                        },
                },
                IsActive = true,
                TopicId = 1
            };
            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb });

            var type = _typesLogic.GetAll(1).FirstOrDefault();
            var range = type?.AdditionalFields[0].Range.ToArray();

            Assert.AreEqual(now.ToString("dd-MM-yyyy"), range?.First());
        }

        [TestMethod]
        public void AnAdditionalTextFieldShouldHaveACorrespondingRangeValue()
        {
            var typeInDb = new Type
            {
                Name = "Type",
                Id = 2,
                AdditionalFields = new List<AdditionalField>
                {
                    new TextField
                    { Id = 3,
                        IsRequired = true,
                        Name = "Text Field",
                        Range = new List<TextItem>
                        {
                            new TextItem {Id = 3, TextFieldId = 3, Value = "this is some text"}
                        }

                    }
                },
                IsActive = true,
                TopicId = 1
            };
            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type> { typeInDb });

            var type = _typesLogic.GetAll(1).FirstOrDefault();
            var range = type?.AdditionalFields[0].Range.ToArray();

            Assert.AreEqual("this is some text", range?.First());
        }

        [TestMethod]
        public void GetAllShouldGetTypesFromAGivenTopic()
        {
            _typesMock.Setup(repo => repo.GetAll()).Returns(new List<Type>
            {
                new Type
                {
                    Name = "Type",
                    Id = 2,
                    IsActive = true,
                    TopicId = 1
                },
                new Type
                {
                    Name = "Type2",
                    Id = 3,
                    IsActive = true,
                    TopicId = 2
                }
            });

            var types =_typesLogic.GetAll(2).ToArray();

            Assert.AreEqual(1, types.Length);
            Assert.AreEqual(3, types.First().Id);
        }
        #endregion
    }
}
