using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IMMRequest.Domain.Fields.Tests
{
    [TestClass()]
    public class DateFieldTests
    {
        [TestMethod()]
        public void AddToRangeTest()
        {
            var dateField = new DateField();
            var dateItem = new DateItem();
            dateField.AddToRange(dateItem);
            Assert.AreEqual(1, dateField.Range.Count());
        }

        [TestMethod()]
        public void DateFieldTest()
        {
            var dateField = new DateField();
            Assert.AreEqual(FieldType.Date, dateField.FieldType);
        }

        [TestMethod()]
        public void ValidateRangeTest()
        {
            // TODO Definir validacion
            try
            {
                var dateField = new DateField();
                dateField.ValidateRange();
                
            } catch (Exception ex)
            {

            }
        
        }

        [TestMethod()]
        public void AddToRangeTest1()
        {
            var dateField = new DateField();
            var other = new IntegerItem();
            dateField.AddToRange(other);
            Assert.AreEqual(0, dateField.Range.Count());
        }

        [TestMethod()]
        public void NotRequiredTest()
        {
            var dateField = new DateField();
            Assert.IsFalse(dateField.IsRequired);
        }

        [TestMethod()]
        public void IsRequiredTest()
        {
            var dateField = new DateField
            {
                IsRequired = true
            };
            Assert.IsTrue(dateField.IsRequired);
        }

        [TestMethod()]
        public void TypeIdTest()
        {
            var number = 12;
            var dateField = new DateField
            {
                TypeId = number
            };
            Assert.AreEqual(number, dateField.TypeId);
        }
    }
}