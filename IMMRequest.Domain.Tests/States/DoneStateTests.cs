namespace IMMRequest.Domain.States.Tests
{
    using System;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DoneStateTests
    {
        [TestMethod]
        public void DoneStateTest()
        {
            State done = new DoneState();

            Assert.AreEqual(default(int), done.Id);
            Assert.AreEqual(default(int), done.RequestId);
            Assert.IsNull(done.Request);
        }

        [TestMethod]
        public void DoneStateTest1()
        {
            Request request = new Request();
            Assert.IsNotNull(request.Status);
            DoneState done = new DoneState(request);
            Assert.AreEqual(done.Request, request);
        }

        [TestMethod]
        public void AcceptedTest()
        {
            Request request = new Request();
            request.Status = new DoneState(request);
            try
            {
                request.Status.Accepted();
                Assert.IsNotNull(request.Status);
                Assert.AreEqual(request.Status.GetType(), typeof(AcceptedState));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CreatedTest()
        {
            Request request = new Request();
            DoneState State = new DoneState(request);
            try
            {
                State.Created();
                Assert.AreEqual(State.GetType(), typeof(DoneState));
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
            request.Status = new DoneState(request);
            try
            {
                request.Status.Denied();
                Assert.IsNotNull(request.Status);
                Assert.AreEqual(request.Status.GetType(), typeof(DeniedState));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void DoneTest()
        {
            Request request = new Request();
            DoneState State = new DoneState(request);
            try
            {
                State.Done();
                Assert.AreEqual(State.GetType(), typeof(DoneState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod]
        public void InReviewTest()
        {
            Request request = new Request();
            request.Status = new DoneState(request);
            try
            {
                request.Status.InReview();
                Assert.AreEqual(request.Status.GetType(), typeof(DoneState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }
    }
}
