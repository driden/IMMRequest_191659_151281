using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IMMRequest.Domain.Fields.Tests
{
    [TestClass()]
    public class TextFieldTests
    {
        [TestMethod()]
        public void AddToRangeTest()
        {
            var textField = new TextField();
            var textItem = new TextItem();
            textField.AddToRange(textItem);
            Assert.AreEqual(1, textField.Range.Count());
        }

        [TestMethod()]
        public void TextFieldTest()
        {
            var textField = new TextField();
            Assert.AreEqual(FieldType.Text, textField.FieldType);
        }

        [TestMethod()]
        public void ValidateRangeTest()
        {
            // TODO Definir validacion
            try
            {
                var dateField = new DateField();
                dateField.ValidateRange();

            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod()]
        public void AddToRangeTest1()
        {
            var textField = new TextField();
            var other = new IntegerItem();
            textField.AddToRange(other);
            Assert.AreEqual(0, textField.Range.Count());
        }

        [TestMethod()]
        public void IsRequiredTest()
        {
            var textField = new TextField
            {
                IsRequired = true
            };
            Assert.IsTrue(textField.IsRequired);
        }

        [TestMethod()]
        public void TypeIdTest()
        {
            var number = 12;
            var textField = new TextField
            {
                TypeId = number
            };
            Assert.AreEqual(number, textField.TypeId);
        }
    }
}
