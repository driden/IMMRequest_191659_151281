namespace IMMRequest.Domain.Tests.Fields
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Fields;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BooleanFieldTests
    {
        [TestMethod]
        public void BooleanFieldHasBoolType()
        {
            var boolean = new BooleanField();
            Assert.AreEqual(FieldType.Boolean, boolean.FieldType);
        }

        [TestMethod]
        public void BooleanFieldHasTrueAndFalseAsRange()
        {
            var boolean = new BooleanField();
            CollectionAssert.AreEquivalent(new List<bool> { true, false }, boolean.Range.ToList());
            Assert.AreEqual(2, boolean.Range.Count);
        }

        [TestMethod]
        public void BooleanFieldHasAFixedRange()
        {
            var boolean = new BooleanField();
            boolean.AddToRange(new BooleanItem { Value = false });
            Assert.AreEqual(2, boolean.Range.Count);
        }

        [TestMethod]
        public void ValidateRangeThrowsExceptionIfNull()
        {
            var boolean = new BooleanField();
            Assert.ThrowsException<InvalidFieldRangeException>(() => boolean.ValidateRange(null));
       }
    }
}
