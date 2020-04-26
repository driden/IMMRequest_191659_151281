namespace IMMRequest.Domain.Fields.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IntegerFieldTests
    {
        [TestMethod]
        public void AddToRangeTest()
        {
            var integerField = new IntegerField();
            var integerItem = new IntegerItem();
            integerField.AddToRange(integerItem);
            Assert.AreEqual(1, integerField.Range.Count());
        }

        [TestMethod]
        public void IntegerFieldTest()
        {
            var integerField = new IntegerField();
            Assert.AreEqual(FieldType.Integer, integerField.FieldType);
        }

        [TestMethod]
        public void AddToRangeTest1()
        {
            var integerField = new IntegerField();
            var other = new TextItem();
            integerField.AddToRange(other);
            Assert.AreEqual(0, integerField.Range.Count());
        }

        [TestMethod]
        public void IsRequiredTest()
        {
            var integerField = new IntegerField
            {
                IsRequired = true
            };
            Assert.IsTrue(integerField.IsRequired);
        }

        [TestMethod]
        public void TypeIdTest()
        {
            var number = 12;
            var integerField = new IntegerField
            {
                TypeId = number
            };
            Assert.AreEqual(number, integerField.TypeId);
        }
    }
}
