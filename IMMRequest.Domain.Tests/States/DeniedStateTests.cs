namespace IMMRequest.Domain.States.Tests
{
    using System;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DeniedStateTests
    {
        [TestMethod]
        public void DeniedStateTest()
        {
            DeniedState denied = new DeniedState();

            Assert.AreEqual(default(int), denied.Id);
            Assert.AreEqual(default(int), denied.RequestId);
            Assert.IsNull(denied.Request);
        }

        [TestMethod]
        public void DeniedStateTest1()
        {
            Request request = new Request();
            Assert.IsNotNull(request.Status);
            DeniedState denied = new DeniedState(request);
            Assert.AreEqual(denied.Request, request);
        }

        [TestMethod]
        public void AcceptedTest()
        {
            Request request = new Request();
            DeniedState State = new DeniedState(request);
            try
            {
                State.Accepted();
                Assert.AreEqual(State.GetType(), typeof(DeniedState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod]
        public void CreatedTest()
        {
            Request request = new Request();
            DeniedState State = new DeniedState(request);
            try
            {
                State.Created();
                Assert.AreEqual(State.GetType(), typeof(DeniedState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod]
        public void DeniedTest()
        {
            Request request = new Request();
            DeniedState State = new DeniedState(request);
            try
            {
                State.Denied();
                Assert.AreEqual(State.GetType(), typeof(DeniedState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod]
        public void DoneTest()
        {
            Request request = new Request();
            request.Status = new DeniedState(request);
            try
            {
                request.Status.Done();
                Assert.IsNotNull(request.Status);
                Assert.AreEqual(request.Status.GetType(), typeof(DoneState));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void InReviewTest()
        {
            Request request = new Request();
            request.Status = new DeniedState(request);
            try
            {
                request.Status.InReview();
                Assert.IsNotNull(request.Status);
                Assert.AreEqual(request.Status.GetType(), typeof(InReviewState));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
