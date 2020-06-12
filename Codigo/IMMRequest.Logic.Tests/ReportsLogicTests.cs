namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Fields;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Type = Domain.Type;

    [TestClass]
    public class ReportsLogicTests
    {
        private ReportsLogic _reportsLogic;
        private Mock<IRepository<Request>> _mockedRequestRepo;
        private Mock<IRepository<Type>> _typeRepo;
        private Mock<IRepository<User>> _userRepo;
        private Mock<IAreaQueries> _areaQueries;

        [TestInitialize]
        public void SetUp()
        {
            _mockedRequestRepo = new Mock<IRepository<Request>>(MockBehavior.Strict);
            
            _reportsLogic = new ReportsLogic(_mockedRequestRepo.Object);
        }

        [TestMethod]
        public void CanGetReportsRequestByMail()
        {
            const string mail = "citizen@mail.com";
            var request = NewRequest(mail);
            
            _mockedRequestRepo
                .Setup(r => r.GetAllByCondition(It.IsAny<Func<Request, bool>>()))
                .Returns(new [] { request});

            var allRequests = _reportsLogic.GetRequestByMail(mail).ToList();

            Assert.AreEqual(1, allRequests.Count);

            var first = allRequests.First();
            Assert.AreEqual(mail, first.Citizen.Email);
        }

        private Request NewRequest(string mail)
        {
            return new Request
            {
                Citizen = new Citizen { Email = mail, Name = "Name", PhoneNumber = "555-5555555" },
                Details = "Request Details",
                Type = NewType(),
                Id = 1
            };
        }
        
        private Type NewType()
        {
            var dateField = new DateField
            {
                Name = "TestAdditionalDateField",
                Range = new List<DateItem>
                {
                    new DateItem { Value = DateTime.Today.AddDays(-1) },
                    new DateItem { Value = DateTime.Today.AddDays(1) },
                }
            };

            return new Type
            {
                Name = "TestType",
                AdditionalFields = new List<AdditionalField> { dateField }
            };
        }
    }
}
