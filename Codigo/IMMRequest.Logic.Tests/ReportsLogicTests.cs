namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.States;
    using IMMRequest.Logic.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    
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
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            var allRequests = _reportsLogic.GetRequestByMail(
                mail, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today);

            Assert.AreEqual(2, allRequests.Count());
        }

        [TestMethod]
        public void InvalidReportNotExistenMail()
        {
            const string mail = "a@mail.com";
            var requests = NewListOfRequests("foo@mail.com");
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            var allRequests = _reportsLogic.GetRequestByMail(
                mail, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today);

            Assert.AreEqual(0, allRequests.Count());
        }
        
        [TestMethod]
        public void InvalidReportEmptyMail()
        {
            const string mail = "";
            var requests = NewListOfRequests("foo@mail.com");
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidMailFormatException>(() => _reportsLogic.GetRequestByMail(
                mail, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today.AddYears(1)));
        }

        [TestMethod]
        public void InvalidReportWrongFormatMail()
        {
            const string mail = " ";
            var requests = NewListOfRequests("foo@mail.com");
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidMailFormatException>(() => _reportsLogic.GetRequestByMail(
                mail, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today));
        }

        [TestMethod]
        public void InvalidReportWithNullMail()
        {
            const string mail = "foo@mail.com";
            var requests = NewListOfRequests(mail);
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidMailFormatException>(() => _reportsLogic.GetRequestByMail(
                null, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today.AddYears(1)));
        }

        [TestMethod]
        public void InvalidReportWithWrongRangeDate()
        {
            const string mail = "foo@mail.com";
            var requests = NewListOfRequests(mail);
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidDateRageException>(() => _reportsLogic.GetRequestByMail(
                mail, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today.AddYears(-2)));
        }

        [TestMethod]
        public void InvalidReportWithWrongEndDate()
        {
            const string mail = "foo@mail.com";
            var requests = NewListOfRequests(mail);
            
            _requestRepositoryMock
                .Setup(r => r.GetAll())
                .Returns(requests);

            Assert.ThrowsException<InvalidDateRageException>(() => _reportsLogic.GetRequestByMail(
                mail, 
                DateTime.Today.AddYears(-1), 
                DateTime.Today.AddYears(1)));
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
                    Type = NewType(),
                    CreationDateTime = System.DateTime.Today,
                    Id = i
                });
            }

            list.Add(new Request
            {
                Citizen = new Citizen
                    {Id = new Random().Next(0, 30), Email = mail, Name = "Name", PhoneNumber = "555-5555555"},
                Details = "Request Details",
                Type = NewType(),
                Status = new DeniedState(),
                CreationDateTime = System.DateTime.Today,
                Id = 100
            });

            return list;
        }
    }
}