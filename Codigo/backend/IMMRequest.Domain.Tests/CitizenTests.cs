namespace IMMRequest.Domain.Tests
{
    using System;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CitizenTests
    {
        [TestMethod]
        public void CitizenTest()
        {
            var citizen = new Citizen();
            Assert.IsNull(citizen.Name);
            Assert.IsNull(citizen.Email);
            Assert.IsNull(citizen.PhoneNumber);
        }

        [TestMethod]
        public void CitizenNameTest()
        {
            var name = "Name Citizen";
            var citizen = new Citizen
            {
                Name = name
            };
            Assert.AreEqual(name, citizen.Name);
        }

        [TestMethod]
        public void CitizenValidEmailTest()
        {
            var email = "citizen@citizen.com";
            var citizen = new Citizen
            {
                Email = email
            };
            Assert.AreEqual(email, citizen.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEmailException))]
        public void CitizenInvalidEmailTest()
        {
            var email = "citizen@.com";
            var citizen = new Citizen
            {
                Email = email
            };
            Assert.AreEqual(email, citizen.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPhoneNumberException))]
        public void CitizenInvalidPhoneTest()
        {
            var phone = "+(598)899-9809-";
            var citizen = new Citizen
            {
                PhoneNumber = phone
            };
            Assert.AreEqual(phone, citizen.PhoneNumber);
        }

        [TestMethod]
        public void CitizenValidPhoneTest()
        {
            var phone = "+(598)899-980-878";
            var citizen = new Citizen
            {
                PhoneNumber = phone
            };
            Assert.AreEqual(phone, citizen.PhoneNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameFormatException))]
        public void CitizenEmptyNameTest()
        {
            var name = "";
            var citizen = new Citizen
            {
                Name = name
            };
            Assert.AreEqual(name, citizen.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameFormatException))]
        public void CitizenLessThanThreeCharNameTest()
        {
            var name = "f0";
            var citizen = new Citizen
            {
                Name = name
            };
            Assert.AreEqual(name, citizen.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameFormatException))]
        public void CitizenMoreNameTest()
        {
            // More than 50 characters
            Random random = new Random();
            var name = random.Next(51).ToString();
            var citizen = new Citizen
            {
                Name = name
            };
            Assert.AreEqual(name, citizen.Name);
        }
    }
}