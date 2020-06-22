namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Fields;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Topic;
    using Moq;
    using Type = Domain.Type;

    [TestClass]
    public class TopicsLogicTests
    {
        private Mock<IRepository<Topic>> _topicRepositoryMock;
        private TopicsLogic _topicsLogic;

        [TestInitialize]
        public void SetUp()
        {
            _topicRepositoryMock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            _topicsLogic = new TopicsLogic(_topicRepositoryMock.Object);
        }

        [TestMethod]
        public void CanGetAllTheTopicsInTheDatabase()
        {
            _topicRepositoryMock
                .Setup(m => m.GetAll())
                .Returns(
                    new List<Topic>
                {
                    new Topic
                    {
                        AreaId = 1,
                        Id = 1,
                        Name = "Topic",
                        Types = new List<Type>
                        {
                            new Type
                            {
                                AdditionalFields = new List<AdditionalField>(),
                                Id = 1,
                                Name = "Type",
                                TopicId =  1
                            }
                        }
                    }
                });

            var expected =
                new TopicModel
                {
                    AreaId = 1,
                    Id = 1,
                    Name = "Topic",
                    Types = new List<int> { 1 }
                };

            var actual = _topicsLogic.GetAll(1).FirstOrDefault();

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.AreaId, actual.AreaId);
            Assert.AreEqual(expected.Id, actual.Id);
            CollectionAssert.AreEqual(expected.Types, actual.Types);
        }


        [TestMethod]
        public void CantCreateNewTopic()
        {
            var newTopic = new TopicModel
            {
                Name = "Contenedor",
                AreaId = 1
            };

            _topicRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Topic, bool>>>()))
                .Returns(false);
            _topicRepositoryMock.Setup(m => m.Add(It.IsAny<Topic>())).Verifiable();
            _topicsLogic.Add(newTopic);

            _topicRepositoryMock.Verify(m => m.Add(It.IsAny<Topic>()), Times.Once());
        }

    }
}
