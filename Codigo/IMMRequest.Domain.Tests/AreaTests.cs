namespace IMMRequest.Domain.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AreaTests
    {
        [TestMethod]
        public void AreaTest()
        {
            var area = new Area();

            Assert.IsNotNull(area.Topics);
        }

        [TestMethod]
        public void NameAreaTest()
        {
            string name = "Name Area";
            var area = new Area
            {
                Name = name
            };

            Assert.AreEqual(name, area.Name);
        }

        [TestMethod]
        public void AreaHasId()
        {
            var area = new Area {Id = 1};
            Assert.AreEqual(1, area.Id);
        }
    }
}
