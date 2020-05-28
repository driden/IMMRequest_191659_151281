using System.Collections.Generic;

namespace IMMRequest.Logic.Tests
{
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AreasLogicTests
    {
        private AreasLogic _areasLogic;
        private Mock<IRepository<Area>> _mockedRepo;

        [TestInitialize]
        public void SetUp()
        {
            _mockedRepo = new Mock<IRepository<Area>>(MockBehavior.Strict);
            _areasLogic = new AreasLogic(_mockedRepo.Object);
        }

        [TestMethod]
        public void ItShouldUserTheAreaRepository()
        {
            _mockedRepo.Setup(m => m.GetAll()).Returns(new List<Area>()).Verifiable();
            _areasLogic.GetAll();
            _mockedRepo.Verify(m => m.GetAll(), Times.Once());
        }

        [TestMethod]
        public void ItShouldListAreasAsModels()
        {
            _mockedRepo
                .Setup(m => m.GetAll())
                .Returns(new List<Area>
                {
                    new Area {Id = 5, Name = "Area5", Topics = new List<Topic>{new Topic { Id = 10} }}
                });

            var area = _areasLogic.GetAll().First();

            Assert.AreEqual("Area5", area.Name);
            Assert.AreEqual(5, area.Id);
            CollectionAssert.AreEqual(new List<int> {10},  area.Topics.ToList());
        }
    }
}
