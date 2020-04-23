using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.Domain.Tests
{
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void AreaTest()
        {
            var area = new Area();

            Assert.IsNotNull(area.Topics);
        }

        [TestMethod()]
        public void NameAreaTest()
        {
            string name = "Name Area";
            var area = new Area
            {
                Name = name
            };

            Assert.AreEqual(name, area.Name);
        }
    }
}
