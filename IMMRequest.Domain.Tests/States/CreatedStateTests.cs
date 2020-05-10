namespace IMMRequest.Domain.States.Tests
{
    using System;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CreatedStateTests
    {
        [TestMethod]
        public void CreatedStateTest()
        {
            State created = new CreatedState();

            Assert.AreEqual(default(int), created.Id);
            Assert.AreEqual(default(int), created.RequestId);
            Assert.IsNull(created.Request);
            Assert.AreEqual("Created", created.ToString());
        }

        [TestMethod]
        public void CreatedStateTest1()
        {
            Request request = new Request();
            Assert.IsNotNull(request.Status);
            CreatedState created = new CreatedState(request);
            Assert.AreEqual(created.Request, request);
        }

        [TestMethod]
        public void AcceptedTest()
        {
            Request request = new Request();
            CreatedState State = new CreatedState(request);
            try
            {
                State.Accepted();
                Assert.AreEqual(State.GetType(), typeof(CreatedState));
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
            CreatedState State = new CreatedState(request);
            try
            {
                State.Created();
                Assert.AreEqual(State.GetType(), typeof(CreatedState));
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
            CreatedState State = new CreatedState(request);
            try
            {
                State.Denied();
                Assert.AreEqual(State.GetType(), typeof(CreatedState));
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
            CreatedState State = new CreatedState(request);
            try
            {
                State.Done();
                Assert.AreEqual(State.GetType(), typeof(CreatedState));
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
            request.Status = new CreatedState(request);
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
