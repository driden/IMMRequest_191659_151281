using System.Collections.Generic;

namespace IMMRequest.Logic.Tests
{
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain.Fields;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Topic;
    using Moq;

    [TestClass]
    public class TopicsLogicTests
    {
        private Mock<IRepository<Topic>> _mockedRepository;
        private TopicsLogic _topicsLogic;

        [TestInitialize]
        public void SetUp()
        {
            _mockedRepository = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            _topicsLogic = new TopicsLogic(_mockedRepository.Object);
        }

        [TestMethod]
        public void CanGetAllTheTopicsInTheDatabase()
        {
            _mockedRepository
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

            var actual = _topicsLogic.GetAllTopics(1).FirstOrDefault();

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.AreaId, actual.AreaId);
            Assert.AreEqual(expected.Id, actual.Id);
            CollectionAssert.AreEqual(expected.Types, actual.Types);
        }
    }
}
