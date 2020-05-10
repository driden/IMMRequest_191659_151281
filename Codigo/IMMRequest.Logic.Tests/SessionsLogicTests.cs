namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;

    [TestClass]
    public class SessionsLogicTests
    {
        private Mock<IRepository<Admin>> mockedAdminRepo;
        private SessionLogic _sessionLogic;

        [TestInitialize]
        public void SetUp()
        {
            mockedAdminRepo = new Mock<IRepository<Admin>>(MockBehavior.Strict);
            _sessionLogic = new SessionLogic(mockedAdminRepo.Object);
        }

        [TestMethod]
        public void CanSendLoginInformationToLoginMethod()
        {
            var loginInfo = new ModelAdminLogin { Email = "email@mail.com", Password = "password" };
            mockedAdminRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });

            Admin updatedAdmin = null;
            mockedAdminRepo
                .Setup(m => m.Update(It.IsAny<Admin>()))
                .Callback<Admin>(admin => updatedAdmin = admin);

            _sessionLogic.Login(loginInfo);
            Assert.AreEqual(1, updatedAdmin.Id);

        }

        [TestMethod]
        public void NoAdministratorWithCredentialsThrowsException()
        {
            var loginInfo = new ModelAdminLogin { Email = "email@mail.com", Password = "password" };
            mockedAdminRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns<Admin>(null);

            Assert.ThrowsException<NoSuchAdministrator>(() => _sessionLogic.Login(loginInfo));
        }
    }
}
