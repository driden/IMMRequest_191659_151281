namespace IMMRequest.JsonRequestImporter.Tests
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
            IRequestsImportable importer = new JsonRequestImporter();
            Assert.ThrowsException<Exception>(() => importer.Import(string.Empty));
        }

        [TestMethod]
        public void FilePathNeedsToBeValid()
        {
            IRequestsImportable importer = new JsonRequestImporter();
            Assert.ThrowsException<Exception>(() => importer.Import("blah/blah.json"));
        }

        [TestMethod]
        public void ItCanParseRequests()
        {
            IRequestsImportable importer = new JsonRequestImporter();
            var list = importer.Import(@"files/requests.json");
            Assert.AreEqual(1,list.Requests.Count);
        }
    }
}
