using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain.States;
using System;
using System.Collections.Generic;
using System.Text;
using IMMRequest.Domain.Exceptions;

namespace IMMRequest.Domain.States.Tests
{
    [TestClass()]
    public class InReviewStateTests
    {
        [TestMethod()]
        public void InReviewStateTest()
        {
            InReviewState done = new InReviewState();
            Assert.IsNull(done.Request);
        }

        [TestMethod()]
        public void InReviewStateTest1()
        {
            Request request = new Request();
            Assert.IsNotNull(request.Status);
            InReviewState inReview = new InReviewState(request);
            Assert.AreEqual(inReview.Request, request);
        }

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
        public void DoneTest()
        {
            Request request = new Request();
            InReviewState State = new InReviewState(request);
            try
            {
                State.Created();
                Assert.AreEqual(State.GetType(), typeof(InReviewState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod()]
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
