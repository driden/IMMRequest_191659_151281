using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.Logic.Tests
{
    [TestClass]
    public class RequestsLogicTests
    {
        [TestMethod]
        public void CanCreateANewRequest()
        {
            var _requestsLogic = new RequestsLogic();
            _requestsLogic.Add(new CreateRequest());

            var _repository = new IRequestRepository();

            Assert.AreEqual(1, _repository.GetAll().Count());
        }
    }
}
