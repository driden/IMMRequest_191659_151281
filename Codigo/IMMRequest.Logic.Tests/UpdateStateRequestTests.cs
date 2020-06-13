namespace IMMRequest.Logic.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Request;

    [TestClass]
    public class UpdateStateRequestTests
    {
        [TestMethod]
        public void NewStateTest()
        {
            var newState = "test text";
            UpdateStateModel updateTest = new UpdateStateModel {NewState = newState};

            Assert.AreEqual(newState, updateTest.NewState);
        }
    }
}
