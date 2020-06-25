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
            var content =
                "{\r\n  \"requests\": [\r\n    {\r\n      \"details\": \"some details\",\r\n      \"email\": \"someemail@citizen.com\",\r\n      \"name\": \"some citizens name\",\r\n      \"phone\": \"5555-555-555\",\r\n      \"typeId\": 1,\r\n      \"additionalFields\": [\r\n        {\r\n          \"name\": \"Nro de Movil\",\r\n          \"values\": \"111111|222222\"\r\n        },\r\n        {\r\n          \"name\": \"Fecha y hora\",\r\n          \"values\": \"9/5/2020|10/6/2021\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}\r\n";
            var list = importer.Import(content);
            Assert.AreEqual(1,list.Requests.Count);
        }
    }
}
