namespace IMMRequest.RequestImporter.Tests
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AbstractImporterTests
    {
        [TestMethod]
        public void CanFindAllInstancesOfIRequestImportable()
        {
            var abstractImporter = new AbstractRequestImporter();
            Assert.IsNotNull(abstractImporter.GetInstance("json"));
        }

        [TestMethod]
        public void CanParseAJsonFile()
        {
            var abstractImporter = new AbstractRequestImporter();
            var fileContent = File.ReadAllText("requests.xml");
            Assert.IsNotNull(abstractImporter.ParseFile(fileContent, "xml"));
        }

    }
}
