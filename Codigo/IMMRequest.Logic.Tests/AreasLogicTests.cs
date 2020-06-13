namespace IMMRequest.Logic.Tests
{
    using System.Collections.Generic;
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
        private Mock<IRepository<Area>> _areaRepositoryMock;

        [TestInitialize]
        public void SetUp()
        {
            _areaRepositoryMock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            _areasLogic = new AreasLogic(_areaRepositoryMock.Object);
        }

        [TestMethod]
        public void ItShouldUserTheAreaRepository()
        {
            _areaRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Area>()).Verifiable();
            _areasLogic.GetAll();
            _areaRepositoryMock.Verify(m => m.GetAll(), Times.Once());
        }

        [TestMethod]
        public void ItShouldListAreasAsModels()
        {
            _areaRepositoryMock
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
