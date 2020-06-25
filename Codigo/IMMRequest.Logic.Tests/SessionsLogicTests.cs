namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Exceptions.Account;
    using DataAccess.Interfaces;
    using Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Admin;
    using Moq;

    [TestClass]
    public class SessionsLogicTests
    {
        private Mock<IRepository<Admin>> _adminRepositoryMock;
        private SessionLogic _sessionLogic;

        [TestInitialize]
        public void SetUp()
        {
            _adminRepositoryMock = new Mock<IRepository<Admin>>(MockBehavior.Strict);
            _sessionLogic = new SessionLogic(_adminRepositoryMock.Object);
        }

        [TestMethod]
        public void CanSendLoginInformationToLoginMethod()
        {
            var loginInfo = new AdminLoginModel { Email = "email@mail.com", Password = "password" };
            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });

            Admin updatedAdmin = null;
            _adminRepositoryMock
                .Setup(m => m.Update(It.IsAny<Admin>()))
                .Callback<Admin>(admin => updatedAdmin = admin);

            _sessionLogic.Login(loginInfo);
            Assert.AreEqual(1, updatedAdmin.Id);

        }

        [TestMethod]
        public void NoAdministratorWithCredentialsThrowsException()
        {
            var loginInfo = new AdminLoginModel { Email = "email@mail.com", Password = "password" };
            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns<Admin>(null);

            Assert.ThrowsException<NoSuchAdministrator>(() => _sessionLogic.Login(loginInfo));
        }
    }
}
