namespace IMMRequest.Domain.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void AdminTest()
        {
            Admin admin = new Admin();
            Assert.IsNull(admin.Email);
            Assert.IsNull(admin.Password);
        }

        [TestMethod]
        public void EmptyPasswordTest()
        {
            Admin admin = new Admin
            {
                Email = "admin@admin.com"
            };
            Assert.IsNull(admin.Password);
        }

        [TestMethod]
        public void PasswordTest()
        {
            var password = "pass123";
            var admin = new Admin
            {
                Password = password
            };
            Assert.AreEqual(password, admin.Password);
        }

        [TestMethod]
        public void EmptyEmailTest()
        {
            var admin = new Admin
            {
                Password = "pass123"
            };
            Assert.IsNull(admin.Email);
        }

        [TestMethod]
        public void EmailTest()
        {
            var email = "admin@admin.com";
            var admin = new Admin
            {
                Email = email
            };
            Assert.AreEqual(email, admin.Email);
        }
    }
}
