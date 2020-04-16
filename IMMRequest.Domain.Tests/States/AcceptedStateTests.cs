using IMMRequest.Domain.States;
using IMMRequest.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IMMRequest.Domain.States.Tests
{
    [TestClass()]
    public class AcceptedStateTests
    {
        [TestMethod()]
        public void AcceptedStateTest()
        {
            AcceptedState accepted = new AcceptedState();
            Assert.IsNull(accepted.Request);
        }

        [TestMethod()]
        public void AcceptedStateTest1()
        {
            Request request = new Request();
            AcceptedState accepted = new AcceptedState(request);
            Assert.AreEqual(accepted.Request, request);
        }

        [TestMethod()]
        public void AcceptedTest()
        {
            AcceptedState State = new AcceptedState(new Request());
            try
            {
                State.Accepted();
                Assert.AreEqual(State.GetType(), typeof(AcceptedState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod()]
        public void CreatedTest()
        {
            AcceptedState State = new AcceptedState(new Request());
            try
            {
                State.Created();
                Assert.AreEqual(State.GetType(), typeof(AcceptedState));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidStateException);
            }
        }

        [TestMethod()]
        public void DeniedTest()
        {
            AcceptedState State = new AcceptedState(new Request());
            try
            {
                State.Denied();
                Assert.AreEqual(State.GetType(), typeof(AcceptedState));
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
            request.Status = new AcceptedState(request);
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

        [TestMethod()]
        public void InReviewTest()
        {
            Request request = new Request();
            request.Status = new AcceptedState(request);
            try
            {
                request.Status.InReview();
                Assert.AreEqual(request.Status.GetType(), typeof(InReviewState));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}