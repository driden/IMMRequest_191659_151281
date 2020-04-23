using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain.States;
using IMMRequest.Domain.Exceptions;
using System;
using System.Text;

namespace IMMRequest.Domain.Tests
{
    [TestClass()]
    public class RequestTests
    {
        [TestMethod()]
        public void RequestTest()
        {
            var request = new Request();
            Assert.AreEqual(typeof(CreatedState), request.Status.GetType());
        }
        
        [TestMethod()]
        public void RequestStatusTest()
        {
            var request = new Request();
            request.Status = new AcceptedState(request);

            Assert.AreEqual(typeof(AcceptedState), request.Status.GetType());
        }
        
        [TestMethod()]
        public void RequestDetailsTest()
        {
            var detail = "this is a test";
            var request = new Request
            {
                Details = detail
            };
            Assert.AreSame(detail, request.Details);
        }

       
        [TestMethod()]
        public void RequestDetailsLessTest()
        {
            int length = 2000;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            var request = new Request
            {
                Details = str_build.ToString()
            };
            Assert.AreEqual(str_build.ToString(), request.Details);
        }

        // Test that the detail is more than 2000 characters
        [TestMethod()]
        [ExpectedException(typeof(InvalidDetailsException))]
        public void RequestDetailsMoreTest()
        {
            int length = 2001;

            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            _ = new Request
            {
                Details = str_build.ToString()
            };
        }

        [TestMethod()]
        public void RequestCitizenTest()
        {
            var request = new Request();
            var citizen = new Citizen();
            request.Citizen = citizen;

            Assert.AreSame(citizen, request.Citizen);
        }
        
        [TestMethod()]
        public void RequestTopicTest()
        {
            var request = new Request();
            var type = new Type();
            request.Type = type;

            Assert.AreSame(type, request.Type);
        }
    }

}
