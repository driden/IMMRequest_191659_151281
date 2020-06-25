namespace IMMRequest.Domain.Tests.Fields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Fields;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateFieldTests
    {
        [TestMethod]
        public void AddToRangeTest()
        {
            var dateField = new DateField();
            var dateItem = new DateItem();
            dateField.AddToRange(dateItem);
            Assert.AreEqual(1, dateField.Range.Count());
        }

        [TestMethod]
        public void DateFieldTest()
        {
            var dateField = new DateField();

            Assert.AreEqual(default(int), dateField.Id);
            Assert.AreEqual(FieldType.Date, dateField.FieldType);
        }

        [TestMethod]
        public void DateFieldIsAdditionalField()
        {
            AdditionalField dateField = new DateField();

            Assert.AreEqual(default(int), dateField.Id);
            Assert.AreEqual(FieldType.Date, dateField.FieldType);
        }

        [TestMethod]
        public void AddToRangeTest1()
        {
            var dateField = new DateField();
            var other = new IntegerItem();
            dateField.AddToRange(other);
            Assert.AreEqual(0, dateField.Range.Count());
        }

        [TestMethod]
        public void NotRequiredTest()
        {
            var dateField = new DateField();
            Assert.IsFalse(dateField.IsRequired);
        }

        [TestMethod]
        public void IsRequiredTest()
        {
            var dateField = new DateField
            {
                IsRequired = true
            };
            Assert.IsTrue(dateField.IsRequired);
        }

        [TestMethod]
        public void TypeIdTest()
        {
            var number = 12;
            var dateField = new DateField
            {
                TypeId = number
            };
            Assert.AreEqual(number, dateField.TypeId);
        }

        [TestMethod]
        public void BadFormRangesThrowException()
        {
            var dateField = new DateField();
            var lowRange = new DateItem { Id = 1, Value = DateTime.Now };
            var midRange = new DateItem { Id = 2, Value = DateTime.Now };
            var highRange = new DateItem { Id = 2, Value = DateTime.Now };

            dateField.AddToRange(lowRange);
            dateField.AddToRange(midRange);
            dateField.AddToRange(highRange);

            Assert.ThrowsException<InvalidFieldRangeException>(() => dateField.ValidateRangeIsCorrect());
        }

        [TestMethod]
        public void LoweThanInitialRangeThrowsException()
        {
            var dateField = new DateField();
            var lowRange = new DateItem { Id = 1, Value = DateTime.Now.AddDays(-1) };
            var midRange = new DateItem { Id = 2, Value = DateTime.Now };

            dateField.AddToRange(lowRange);
            dateField.AddToRange(midRange);

            Assert.ThrowsException<InvalidFieldRangeException>(() =>
                dateField.ValidateRange(new List<DateTime> { DateTime.Now.AddDays(-2) }));
        }


        [TestMethod]
        public void HigherThanEndRangeThrowsException()
        {
            var dateField = new DateField();
            var lowRange = new DateItem { Id = 1, Value = DateTime.Now };
            var midRange = new DateItem { Id = 2, Value = DateTime.Now.AddDays(1) };

            dateField.AddToRange(lowRange);
            dateField.AddToRange(midRange);

            Assert.ThrowsException<InvalidFieldRangeException>(() => dateField.ValidateRange<DateTime>(new[] { DateTime.Now.AddDays(2) }));
        }
    }
}
