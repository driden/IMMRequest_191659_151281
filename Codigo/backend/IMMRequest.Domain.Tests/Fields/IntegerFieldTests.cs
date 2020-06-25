namespace IMMRequest.Domain.Tests.Fields
{
    using System.Linq;
    using Domain.Fields;
    using Exceptions;
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

            Assert.AreEqual(default(int), integerField.Id);
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

        [TestMethod]
        public void BadFormRangesThrowException()
        {
            var integerField = new IntegerField();
            var lowRange = new IntegerItem { Id = 1, Value = 5 };
            var midRange = new IntegerItem { Id = 2, Value = 3 };
            var highRange = new IntegerItem { Id = 2, Value = 3 };

            integerField.AddToRange(lowRange);
            integerField.AddToRange(midRange);
            integerField.AddToRange(highRange);

            Assert.ThrowsException<InvalidFieldRangeException>(() => integerField.ValidateRangeIsCorrect());
        }

        [TestMethod]
        public void LoweThanInitialRangeThrowsException()
        {
            var integerField = new IntegerField();
            var lowRange = new IntegerItem { Id = 1, Value = 3 };
            var midRange = new IntegerItem { Id = 2, Value = 5 };

            integerField.AddToRange(lowRange);
            integerField.AddToRange(midRange);

            Assert.ThrowsException<InvalidFieldRangeException>(() => integerField.ValidateRange(new[] { 2 }));
        }


        [TestMethod]
        public void HigherThanEndRangeThrowsException()
        {
            var integerField = new IntegerField();
            var lowRange = new IntegerItem { Id = 1, Value = 3 };
            var midRange = new IntegerItem { Id = 2, Value = 5 };

            integerField.AddToRange(lowRange);
            integerField.AddToRange(midRange);

            Assert.ThrowsException<InvalidFieldRangeException>(() => integerField.ValidateRange(new[] { 6 }));
        }


    }
}
