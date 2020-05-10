using System;
using IMMRequest.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.Logic.Tests
{
    [TestClass]
    public class UpdateStateRequestTests
    {
        private UpdateStateRequest updateStateRequest;

        [TestMethod]
        public void NewStateTest()
        {
            var newState = "test text";
            UpdateStateRequest updateTest = new UpdateStateRequest();
            updateTest.NewState = newState;

            Assert.AreEqual(newState, updateTest.NewState);
        }
    }
}
