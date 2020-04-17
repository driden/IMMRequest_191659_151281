using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
