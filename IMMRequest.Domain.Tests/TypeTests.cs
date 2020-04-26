namespace IMMRequest.Domain.Tests
{
    using System.Collections.Generic;
    using Fields;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeTests
    {
        [TestMethod]
        public void TypeTest()
        {
            var type = new Type();

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
    }
}
