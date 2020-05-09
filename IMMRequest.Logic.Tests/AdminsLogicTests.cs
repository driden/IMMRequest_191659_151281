namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Exceptions.RemoveType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;

    [TestClass]
    public class AdminsLogicTests
    {
        private AdminsLogic _logic;
        private Mock<IRepository<Admin>> mockedRepo;

        [TestInitialize]
        public void SetUpClass()
        {
            mockedRepo = new Mock<IRepository<Admin>>(MockBehavior.Strict);
            _logic = new AdminsLogic(mockedRepo.Object);
        }

        [TestMethod]
        public void CantAddAdminWithoutName()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "pass",
                Name = string.Empty,
                PhoneNumber = "5555555"
            };

            Assert.ThrowsException<InvalidNameFormatException>(() => _logic.Add(request));
        }

        [TestMethod]
        public void CantAddAdminWithoutEmail()
        {
            var request = new CreateAdminRequest
            {
                Email = string.Empty,
                Password = "pass",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            Assert.ThrowsException<InvalidEmailException>(() => _logic.Add(request));
        }

        [TestMethod]
        public void CantAddAdminWithoutPhone()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "pass",
                Name = "a name",
                PhoneNumber = string.Empty
            };

            Assert.ThrowsException<InvalidPhoneNumberException>(() => _logic.Add(request));
        }

        [TestMethod]
        public void CantAddAdminWithoutPassword()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = string.Empty,
                Name = "a name",
                PhoneNumber = "5555555"
            };

            Assert.ThrowsException<InvalidPasswordException>(() => _logic.Add(request));
        }

        [TestMethod]
        public void CantAddARepeatedEmail()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(true);

            Assert.ThrowsException<InvalidEmailException>(() => _logic.Add(request));
        }

        [TestMethod]
        public void CanAddAnAdmin()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(false);
            mockedRepo.Setup(m => m.Add(It.IsAny<Admin>())).Verifiable();
            _logic.Add(request);

            mockedRepo.Verify(m => m.Add(It.IsAny<Admin>()), Times.Once());
        }

        [TestMethod]
        public void CantUpdateAnAdminWithANegativeId()
        {
            Assert.ThrowsException<InvalidIdException>(() => _logic.Update(-1, new CreateAdminRequest()));
        }

        [TestMethod]
        public void CantUpdateAnAdminWithAnEmailThatIsBeingUsed()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(true);

            mockedRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 2 });
            Assert.ThrowsException<InvalidEmailException>(() => _logic.Update(1, request));
        }

        [TestMethod]
        public void CanStillUseTheSameEmailWhenUpdating()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(true);

            mockedRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            mockedRepo
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });
            mockedRepo.Setup(m => m.Update(It.IsAny<Admin>())).Verifiable();

            _logic.Update(1, request);


            mockedRepo.Verify(m => m.Update(It.IsAny<Admin>()), Times.Once());
        }

        [TestMethod]
        public void CantUpdateIfAdminIsIsNotValid()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(true);

            mockedRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            mockedRepo
                .Setup(m => m.Get(1))
                .Returns<Admin>(null);

            Assert.ThrowsException<InvalidIdException>(() => _logic.Update(1, request));
        }

        [TestMethod]
        public void CanUpdateAnAdmin()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(true);

            mockedRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            mockedRepo
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });
            mockedRepo.Setup(m => m.Update(It.IsAny<Admin>())).Verifiable();

            _logic.Update(1, request);


            mockedRepo.Verify(m => m.Update(It.IsAny<Admin>()), Times.Once());
        }

        [TestMethod]
        public void AdminDataGetsUpdated()
        {
            var request = new CreateAdminRequest
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            mockedRepo
                .Setup(m => m.Exists(It.IsAny<Func<Admin, bool>>()))
                .Returns(true);

            mockedRepo
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            mockedRepo
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });

            Admin updatedAdmin = null;
            mockedRepo
                .Setup(m => m.Update(It.IsAny<Admin>()))
                .Callback<Admin>(s => updatedAdmin = s)
                .Verifiable();

            _logic.Update(1, request);

            Assert.AreEqual("some@mail.com", updatedAdmin.Email);
            Assert.AreEqual("password", updatedAdmin.Password);
            Assert.AreEqual("a name", updatedAdmin.Name);
            Assert.AreEqual("5555555", updatedAdmin.PhoneNumber);
        }
    }
}
