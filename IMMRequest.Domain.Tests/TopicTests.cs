using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain.Tests
{
    [TestClass()]
    public class TopicTests
    {
        [TestMethod()]
        public void TopicTest()
        {
            var topic = new Topic();
            Assert.IsNull(topic.Name);
            Assert.IsNull(topic.Types);
        }

        [TestMethod()]
        public void TopicNameTest()
        {
            var name = "Topic name";
            var topic = new Topic
            {
                Name = name
            };
            Assert.AreEqual(name, topic.Name);
        }

        [TestMethod()]
        public void TopicTypesTest()
        {
            var types = new List<Type>();
            var topic = new Topic
            {
                Types = types
            };
            Assert.AreEqual(types, topic.Types);
        }
    }

}
