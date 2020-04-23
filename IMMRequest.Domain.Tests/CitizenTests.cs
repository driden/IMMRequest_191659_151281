using IMMRequest.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.Domain.Tests
{
    [TestClass()]
    public class CitizenTests
    {
        [TestMethod()]
        public void CitizenTest()
        {
            var citizen = new Citizen();
            Assert.IsNull(citizen.Name);
            Assert.IsNull(citizen.Email);
            Assert.IsNull(citizen.PhoneNumber);
        }

        [TestMethod()]
        public void CitizenNameTest()
        {
            var name = "Name Citizen";
            var citizen = new Citizen
            {
                Name = name
            };
            Assert.AreEqual(name, citizen.Name);
        }

        [TestMethod()]
        public void CitizenValidEmailTest()
        {
            var email = "citizen@citizen.com";
            var citizen = new Citizen
            {
                Email = email
            };
            Assert.AreEqual(email, citizen.Email);
        }

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
        public void CitizenValidPhoneTest()
        {
            var phone = "+(598)899-980-878";
            var citizen = new Citizen
            {
                PhoneNumber = phone
            };
            Assert.AreEqual(phone, citizen.PhoneNumber);
        }
    }
}
