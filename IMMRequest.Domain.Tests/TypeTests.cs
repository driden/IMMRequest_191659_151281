using System;

namespace IMMRequest.Domain.Tests
{
    using System.Collections.Generic;
    using Domain.Fields;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeTests
    {
        [TestMethod]
        public void TypeTest()
        {
            var type = new Type();

            Assert.AreEqual(default(int), type.Id);
            Assert.IsNull(type.Name);
            Assert.IsNull(type.AdditionalFields);
        }

        [TestMethod]
        public void TypeNameTest()
        {
            var name = "Type name";
            var type = new Type
            {
                Name = name
            };

            Assert.AreSame(name, type.Name);
        }

        [TestMethod]
        public void TypeAdditionalFieldsTest()
        {
            var additionalFields = new List<AdditionalField>();
            var type = new Type
            {
                AdditionalFields = additionalFields
            };

            Assert.AreSame(additionalFields, type.AdditionalFields);
        }

        [TestMethod]
        public void TopicIdTest()
        {
            var topic = new Random().Next(5, 50);
            var type = new Type
            {
                TopicId = topic
            };

            Assert.AreEqual(topic, type.TopicId);
        }

        [TestMethod]
        public void IdTest()
        {
            var id = new Random().Next(5, 50);
            var type = new Type
            {
                Id = id
            };

            Assert.AreEqual(id, type.Id);
        }
    }
}
