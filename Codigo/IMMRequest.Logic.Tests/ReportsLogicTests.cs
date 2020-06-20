namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.States;
    using IMMRequest.Domain.Fields;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.State;
    using Models.Type;
    using Moq;
    using Type = Domain.Type;

    [TestClass]
    public class ReportsLogicTests: IMMRequestLogicTestBase
    {
        private ReportsLogic _reportsLogic;
        private Mock<IRepository<Request>> _requestRepositoryMock;

        [TestInitialize]
        public void SetUp()
        {
            _requestRepositoryMock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _reportsLogic = new ReportsLogic(
                _requestRepositoryMock.Object
            );
        }

        [TestMethod]
        public void CanGetReportsRequestByMail()
        {
            const string mail = "citizen@mail.com";
            var requests = NewListOfRequests(mail);
            var searchByMailModel = new SearchByMailModel
            {
                Mail = mail,
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today
            };

            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            
            IEnumerable<StateReportModel> allRequests = _reportsLogic.GetRequestByMail(
                searchByMailModel.Mail, searchByMailModel.StartDate, searchByMailModel.EndDate);

            Assert.AreEqual(2, allRequests.Count());
            Assert.AreEqual("Created", allRequests.First().StateName);
            Assert.IsTrue(allRequests.First().Ids.Any());
        }

        [TestMethod]
        public void InvalidReportNotExistenMail()
        {
            const string mail = "a@mail.com";
            var requests = NewListOfRequests("foo@mail.com");
            var searchByMailModel = new SearchByMailModel
            {
                Mail = mail,
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today
            };

            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            IEnumerable<StateReportModel> allRequests = _reportsLogic.GetRequestByMail(
                searchByMailModel.Mail, searchByMailModel.StartDate, searchByMailModel.EndDate);

            Assert.AreEqual(0, allRequests.Count());
        }

        [TestMethod]
        public void InvalidReportEmptyMail()
        {
            const string mail = "";
            var requests = NewListOfRequests("foo@mail.com");
            var searchByMailModel = new SearchByMailModel
            {
                Mail = mail,
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today
            };

            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidMailFormatException>(() => _reportsLogic.GetRequestByMail(
                searchByMailModel.Mail, searchByMailModel.StartDate, searchByMailModel.EndDate));
        }

        [TestMethod]
        public void InvalidReportWrongFormatMail()
        {
            const string mail = " ";
            var requests = NewListOfRequests("foo@mail.com");
            var searchByMailModel = new SearchByMailModel
            {
                Mail = mail,
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today
            };

            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidMailFormatException>(() => _reportsLogic.GetRequestByMail(
                searchByMailModel.Mail, searchByMailModel.StartDate, searchByMailModel.EndDate));
        }

        [TestMethod]
        public void InvalidReportWithNullMail()
        {
            const string mail = "foo@mail.com";
            var requests = NewListOfRequests(mail);
            var searchByMailModel = new SearchByMailModel
            {
                Mail = null,
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today.AddYears(1)
            };

            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidMailFormatException>(() => _reportsLogic.GetRequestByMail(
                searchByMailModel.Mail, searchByMailModel.StartDate, searchByMailModel.EndDate));
        }

        [TestMethod]
        public void InvalidReportWithWrongRangeDate()
        {
            const string mail = "foo@mail.com";
            var requests = NewListOfRequests(mail);
            var searchByMailModel = new SearchByMailModel
            {
                Mail = mail,
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today.AddYears(-2)
            };

            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidDateRageException>(() => _reportsLogic.GetRequestByMail(
                mail,
                DateTime.Today.AddYears(-1),
                DateTime.Today.AddYears(-2)));
        }


        [TestMethod]
        public void CanGetTypeReports()
        {
            var requests = NewListOfRequests("citizen@mail.com");
            var searchTypeModel = new SearchTypeModel
            {
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today
            };
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            var allTypes = _reportsLogic.GetMostUsedTypes(
                searchTypeModel.StartDate, searchTypeModel.EndDate);

            Assert.AreEqual(2, allTypes.Count());
        }

        [TestMethod]
        public void CanGetTypeAsReports()
        {
            var requests = NewListOfRequests("citizen@mail.com");
            var searchTypeModel = new SearchTypeModel
            {
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today
            };
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            var allTypes = _reportsLogic.GetMostUsedTypes(
                searchTypeModel.StartDate, searchTypeModel.EndDate);

            Assert.AreEqual("Taxi Acoso", allTypes.First().Name);
        }

        [TestMethod]
        public void InvalidDateRageTypesReport()
        {
            var requests = NewListOfRequests("citizen@mail.com");
            var searchTypeModel = new SearchTypeModel
            {
                StartDate = DateTime.Today.AddYears(-1),
                EndDate = DateTime.Today.AddYears(-2)
            };
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidDateRageException>(() => _reportsLogic.GetMostUsedTypes(
                searchTypeModel.StartDate, searchTypeModel.EndDate));
        }
        
        [TestMethod]
        public void StateReportName()
        {
            var newState = "test text";
            StateReportModel stateReportModel = new StateReportModel { StateName = newState };

            Assert.AreEqual(newState, stateReportModel.StateName);
        }
        
        [TestMethod]
        public void StateReportQuantitie()
        {
            var quantity = 5;
            StateReportModel stateReportModel = new StateReportModel { Quantity = quantity };

            Assert.AreEqual(quantity, stateReportModel.Quantity);
        }

        private IEnumerable<Request> NewListOfRequests(string mail)
        {
            var list = new List<Request>();
            for (var i = 1; i < 5; i++)
            {
                list.Add(new Request
                {
                    Citizen = new Citizen {Id = new Random().Next(0,30),Email = mail, Name = "Name"+i, PhoneNumber = "555-5555555"},
                    Details = "Request Details",
                    Type = NewType("Taxi Acoso"),
                    CreationDateTime = System.DateTime.Today,
                    Id = i
                });
            }

            list.Add(new Request
            {
                Citizen = new Citizen
                    {Id = new Random().Next(0, 30), Email = mail, Name = "Name", PhoneNumber = "555-5555555"},
                Details = "Request Details",
                Type = NewType("Contenedor roto"),
                Status = new DeniedState(),
                CreationDateTime = System.DateTime.Today,
                Id = 100
            });

            return list;
        }

        private Type NewType(string typeName)
        {
            DateField dateFieldFechaYHora = new DateField
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
                Name = typeName,
                AdditionalFields = new List<AdditionalField> { dateFieldFechaYHora }
            };
        }
    }
}
