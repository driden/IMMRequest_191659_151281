namespace IMMRequest.XmlRequestImporter.Tests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RequestImporter;

    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void NeedContentToDeserialize()
        {
            IRequestsImportable importer = new XmlRequestImporter();
            Assert.ThrowsException<Exception>(() => importer.Import(""));
        }

        [TestMethod]
        public void ItCanParseRequests()
        {
            IRequestsImportable importer = new XmlRequestImporter();
            var content = File.ReadAllText("files/requests.xml");
            var list = importer.Import(content);
            Assert.AreEqual(2, list.Requests.Count);
        }

    }
}
