namespace IMMRequest.XmlRequestImporter.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RequestImporter;

    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void NeedToProvideANonEmptyString()
        {
            IRequestsImportable importer = new XmlRequestImporter();
            Assert.ThrowsException<Exception>(() => importer.Import(string.Empty));
        }

        [TestMethod]
        public void FilePathNeedsToBeValid()
        {
            IRequestsImportable importer = new XmlRequestImporter();
            Assert.ThrowsException<Exception>(() => importer.Import("blah/blah.json"));
        }

        [TestMethod]
        public void ItCanParseRequests()
        {
            IRequestsImportable importer = new XmlRequestImporter();
            var list = importer.Import(@"files/requests.xml");
            Assert.AreEqual(2, list.Requests.Count);
        }

    }
}
