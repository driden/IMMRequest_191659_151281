namespace IMMRequest.Domain.States.Tests
{
    using System;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InReviewStateTests
    {
        [TestMethod]
        public void InReviewStateTest()
        {
            State inReview = new InReviewState();

            Assert.AreEqual(default(int), inReview.Id);
            Assert.AreEqual(default(int), inReview.RequestId);
            Assert.IsNull(inReview.Request);
        }

        [TestMethod]
        public void InReviewStateTest1()
        {
            Request request = new Request();
            Assert.IsNotNull(request.Status);
            InReviewState inReview = new InReviewState(request);
            Assert.AreEqual(inReview.Request, request);
        }

        [TestMethod]
        public void AcceptedTest()
        {
            Request request = new Request();
            request.Status = new InReviewState(request);
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
            request.Status = new InReviewState(request);
            try
            {
                request.Status.Created();
                Assert.AreEqual(request.Status.GetType(), typeof(CreatedState));
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
            request.Status = new InReviewState(request);
            try
            {
                request.Status.Denied();
                Assert.AreEqual(request.Status.GetType(), typeof(DeniedState));
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
            InReviewState State = new InReviewState(request);
            try
            {
                State.Done();
                Assert.AreEqual(State.GetType(), typeof(InReviewState));
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
            InReviewState State = new InReviewState(request);
            try
            {
                State.InReview();
                Assert.AreEqual(State.GetType(), typeof(InReviewState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }
    }
}
