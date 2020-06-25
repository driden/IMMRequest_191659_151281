namespace IMMRequest.Domain.Tests
{
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserTest()
        {
            var user = new User();

            Assert.AreEqual(default(int), user.Id);
            Assert.AreEqual(null, user.Name);
            Assert.AreEqual(null, user.Email);
            Assert.AreEqual(null, user.PhoneNumber);
        }

        [TestMethod]
        public void CantAddAUserWithSpacesInEmail()
        {
            var user = new User();

            Assert.ThrowsException<InvalidEmailException>(() => user.Email = "mail @yahoo.com");
        }

    }
}
