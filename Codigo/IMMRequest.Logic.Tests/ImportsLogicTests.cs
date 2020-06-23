namespace IMMRequest.Logic.Tests
{
    using Core;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Files;
    using Models.Request;
    using Moq;

    [TestClass]
    public class ImportsLogicTests
    {
        private ImportsLogic _importsLogic;
        private Mock<IRequestsLogic> _requestsLogicMock;

        [TestInitialize]
        public void Setup()
        {
            _requestsLogicMock = new Mock<IRequestsLogic>(MockBehavior.Strict);
            _importsLogic = new ImportsLogic(_requestsLogicMock.Object);
        }

        [TestMethod]
        public void CanParseAJsonFile()
        {
            _requestsLogicMock.Setup(mock => mock.Add(It.IsAny<CreateRequestModel>())).Returns(1).Verifiable();
            _importsLogic.Import(new FileRequestModel
            {
                Content = "{\r\n  \"requests\": [\r\n {\r\n \"details\": \"some details\",\r\n  \"email\": \"someemail@citizen.com\",\r\n  \"name\": \"some citizens name\",\r\n  \"phone\": \"5555-555-555\",\r\n \"typeId\": 1,\r\n \"additionalFields\": [\r\n {\r\n \"name\": \"Nro de Movil\",\r\n \"values\": \"111111|222222\"\r\n },\r\n {\r\n \"name\": \"Fecha y hora\",\r\n \"values\": \"9/5/2020|10/6/2021\"\r\n }\r\n ]\r\n  }\r\n  ]\r\n}",
                FileType = "json"
            });
            _requestsLogicMock.Verify(mock => mock.Add(It.IsAny<CreateRequestModel>()), Times.Exactly(1));

        }


        [TestMethod]
        public void CanParseAnXMLFile()
        {
            _requestsLogicMock.Setup(mock => mock.Add(It.IsAny<CreateRequestModel>())).Returns(1).Verifiable();
            _importsLogic.Import(new FileRequestModel
            {
                Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<CreateRequestList>\r\n  <Requests>\r\n    <CreateRequestModel>\r\n      <AdditionalFields>\r\n        <FieldRequestModel>\r\n          <Name>Nro de Movil</Name>\r\n          <Values>111111</Values>\r\n        </FieldRequestModel>\r\n        <FieldRequestModel>\r\n          <Name>Fecha y hora</Name>\r\n          <Values>9/5/2020|10/6/2030</Values>\r\n        </FieldRequestModel>\r\n      </AdditionalFields>\r\n      <Details>some details</Details>\r\n      <Email>someemail@citizen.com</Email>\r\n      <Name>some citizens name</Name>\r\n      <Phone>5555-555-555</Phone>\r\n      <TypeId>1</TypeId>\r\n    </CreateRequestModel>\r\n    <CreateRequestModel>\r\n      <AdditionalFields>\r\n        <FieldRequestModel>\r\n          <Name>Nro de Movil</Name>\r\n          <Values>111111</Values>\r\n        </FieldRequestModel>\r\n        <FieldRequestModel>\r\n          <Name>Fecha y hora</Name>\r\n          <Values>9/5/2020</Values>\r\n        </FieldRequestModel>\r\n      </AdditionalFields>\r\n      <Details>some more details</Details>\r\n      <Email>someemail2@citizen.com</Email>\r\n      <Name>some citizens name</Name>\r\n      <Phone>22222-555-555</Phone>\r\n      <TypeId>1</TypeId>\r\n    </CreateRequestModel>\r\n  </Requests>\r\n</CreateRequestList>\r\n"
,
                FileType = "xml"
            });
            _requestsLogicMock.Verify(mock => mock.Add(It.IsAny<CreateRequestModel>()), Times.Exactly(2));

        }
    }
}
