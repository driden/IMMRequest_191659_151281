using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.Domain.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void UserTest()
        {
            var user = new User();

            Assert.AreEqual(null, user.Name);
            Assert.AreEqual(null, user.Email);
            Assert.AreEqual(null, user.PhoneNumber);
        }
    }
}
