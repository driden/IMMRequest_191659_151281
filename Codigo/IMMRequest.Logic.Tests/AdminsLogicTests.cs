namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Exceptions.RemoveType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Admin;
    using Moq;

    [TestClass]
    public class AdminsLogicTests
    {
        private AdminsLogic _adminsLogic;
        private Mock<IRepository<Admin>> _adminRepositoryMock;

        [TestInitialize]
        public void SetUpClass()
        {
            _adminRepositoryMock = new Mock<IRepository<Admin>>(MockBehavior.Strict);
            _adminsLogic = new AdminsLogic(_adminRepositoryMock.Object);
        }

        [TestMethod]
        public void CantAddAdminWithoutName()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "pass",
                Name = string.Empty,
                PhoneNumber = "5555555"
            };

            Assert.ThrowsException<InvalidNameFormatException>(() => _adminsLogic.Add(request));
        }

        [TestMethod]
        public void CantAddAdminWithoutEmail()
        {
            var request = new AdminModel
            {
                Email = string.Empty,
                Password = "pass",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            Assert.ThrowsException<InvalidEmailException>(() => _adminsLogic.Add(request));
        }

        [TestMethod]
        public void CantAddAdminWithoutPhone()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "pass",
                Name = "a name",
                PhoneNumber = string.Empty
            };

            Assert.ThrowsException<InvalidPhoneNumberException>(() => _adminsLogic.Add(request));
        }

        [TestMethod]
        public void CantAddAdminWithoutPassword()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = string.Empty,
                Name = "a name",
                PhoneNumber = "5555555"
            };

            Assert.ThrowsException<InvalidPasswordException>(() => _adminsLogic.Add(request));
        }

        [TestMethod]
        public void CantAddARepeatedEmail()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(true);

            Assert.ThrowsException<InvalidEmailException>(() => _adminsLogic.Add(request));
        }

        [TestMethod]
        public void CanAddAnAdmin()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(false);
            _adminRepositoryMock.Setup(m => m.Add(It.IsAny<Admin>())).Verifiable();
            _adminsLogic.Add(request);

            _adminRepositoryMock.Verify(m => m.Add(It.IsAny<Admin>()), Times.Once());
        }

        [TestMethod]
        public void CantUpdateAnAdminWithANegativeId()
        {
            Assert.ThrowsException<InvalidIdException>(() => _adminsLogic.Update(-1, new AdminModel()));
        }

        [TestMethod]
        public void CantUpdateAnAdminWithAnEmailThatIsBeingUsed()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(true);

            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 2 });
            Assert.ThrowsException<InvalidEmailException>(() => _adminsLogic.Update(1, request));
        }

        [TestMethod]
        public void CanStillUseTheSameEmailWhenUpdating()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(true);

            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock.Setup(m => m.Update(It.IsAny<Admin>())).Verifiable();

            _adminsLogic.Update(1, request);


            _adminRepositoryMock.Verify(m => m.Update(It.IsAny<Admin>()), Times.Once());
        }

        [TestMethod]
        public void CantUpdateIfAdminIsIsNotValid()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(true);

            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock
                .Setup(m => m.Get(1))
                .Returns<Admin>(null);

            Assert.ThrowsException<InvalidIdException>(() => _adminsLogic.Update(1, request));
        }

        [TestMethod]
        public void CanUpdateAnAdmin()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(true);

            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock.Setup(m => m.Update(It.IsAny<Admin>())).Verifiable();

            _adminsLogic.Update(1, request);


            _adminRepositoryMock.Verify(m => m.Update(It.IsAny<Admin>()), Times.Once());
        }

        [TestMethod]
        public void AdminDataGetsUpdated()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(true);

            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });

            Admin updatedAdmin = null;
            _adminRepositoryMock
                .Setup(m => m.Update(It.IsAny<Admin>()))
                .Callback<Admin>(s => updatedAdmin = s)
                .Verifiable();

            _adminsLogic.Update(1, request);

            Assert.AreEqual("some@mail.com", updatedAdmin.Email);
            Assert.AreEqual("password", updatedAdmin.Password);
            Assert.AreEqual("a name", updatedAdmin.Name);
            Assert.AreEqual("5555555", updatedAdmin.PhoneNumber);
        }

        [TestMethod]
        public void AdminUsesANewEmail()
        {
            var request = new AdminModel
            {
                Email = "some@mail.com",
                Password = "password",
                Name = "a name",
                PhoneNumber = "5555555"
            };

            _adminRepositoryMock
                .Setup(m => m.Exists(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(false);

            _adminRepositoryMock
                .Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Admin, bool>>>()))
                .Returns(new Admin { Id = 1 });
            _adminRepositoryMock
                .Setup(m => m.Get(1))
                .Returns(new Admin { Id = 1 });

            Admin updatedAdmin = null;
            _adminRepositoryMock
                .Setup(m => m.Update(It.IsAny<Admin>()))
                .Callback<Admin>(s => updatedAdmin = s)
                .Verifiable();

            _adminsLogic.Update(1, request);

            Assert.AreEqual("some@mail.com", updatedAdmin.Email);
            Assert.AreEqual("password", updatedAdmin.Password);
            Assert.AreEqual("a name", updatedAdmin.Name);
            Assert.AreEqual("5555555", updatedAdmin.PhoneNumber);
        }

        [TestMethod]
        public void CantRemoveAnAdminUsingANegativeId()
        {
            Assert.ThrowsException<InvalidIdException>(() => _adminsLogic.Remove(-1));
        }

        [TestMethod]
        public void CantRemoveAnAdminThatDoesNotExist()
        {
            _adminRepositoryMock.Setup(m => m.Get(10)).Returns<Admin>(null);
            Assert.ThrowsException<InvalidIdException>(() => _adminsLogic.Remove(10));
        }

        [TestMethod]
        public void CanDeleteAnAdmin()
        {
            var adminToDelete = new Admin { Id = 10 };
            Admin deletedAdmin = null;
            _adminRepositoryMock.Setup(m => m.Get(10)).Returns(adminToDelete);
            _adminRepositoryMock.Setup(m => m.Remove(adminToDelete)).Callback<Admin>(cb => deletedAdmin = cb).Verifiable();

            _adminsLogic.Remove(10);

            _adminRepositoryMock.Verify();
            Assert.AreEqual(10, deletedAdmin.Id);
        }

        [TestMethod]
        public void CanGetAll()
        {
            _adminRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Admin> { new Admin { Id = 1 } });
            Assert.AreEqual(1, _adminsLogic.GetAll().First().Id);
        }
    }
}
