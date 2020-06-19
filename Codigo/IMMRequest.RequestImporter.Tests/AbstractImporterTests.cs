namespace IMMRequest.RequestImporter.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AbstractImporterTests
    {
        [TestMethod]
        public void CanFindAllInstancesOfIRequestImportable()
        {
            var abstractImporter = new AbstractRequestImporter();
            Assert.IsNotNull(abstractImporter.GetInstance());
        }

        [TestMethod]
        public void CanParseAJsonFile()
        {
            var abstractImporter = new AbstractRequestImporter();
            Assert.IsNotNull(abstractImporter.ParseFile("files/requests.json"));
        }

    }
}
