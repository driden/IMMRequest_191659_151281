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
        public void ItCanParseRequests()
        {
            IRequestsImportable importer = new JsonRequestImporter();
            var list = importer.Import(@"requests.json");
            Assert.AreEqual(1,list.Requests.Count);
        }
    }
}
