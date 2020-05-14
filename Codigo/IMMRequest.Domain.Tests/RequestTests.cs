namespace IMMRequest.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Domain.Fields;
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using States;
    using Type = Domain.Type;

    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void RequestTest()
        {
            var request = new Request();
            Assert.AreEqual(typeof(CreatedState), request.Status.GetType());
        }
        
        [TestMethod]
        public void RequestStatusTest()
        {
            var request = new Request();
            request.Status = new AcceptedState(request);

            Assert.AreEqual(typeof(AcceptedState), request.Status.GetType());
        }
        
        [TestMethod]
        public void RequestDetailsTest()
        {
            var detail = "this is a test";
            var request = new Request
            {
                Details = detail
            };
            Assert.AreSame(detail, request.Details);
        }

       
        [TestMethod]
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
        [TestMethod]
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

        [TestMethod]
        public void RequestCitizenTest()
        {
            var request = new Request();
            var citizen = new Citizen();
            request.Citizen = citizen;

            Assert.AreSame(citizen, request.Citizen);
        }
        
        [TestMethod]
        public void RequestTopicTest()
        {
            var request = new Request();
            var type = new Type();
            request.Type = type;

            Assert.AreSame(type, request.Type);
        }

        [TestMethod]
        public void DateItem()
        {
            var dateItem = new DateItem
            {
                Id = 1, DateFieldId = 1
            };
            Assert.AreEqual(1, dateItem.Id);
            Assert.AreEqual(1, dateItem.DateFieldId);
        }

        [TestMethod]
        public void TextItem()
        {
            var textItem = new TextItem
            {
                Id = 1, TextFieldId = 1
            };

            Assert.AreEqual(1, textItem.Id);
            Assert.AreEqual(1, textItem.TextFieldId);
        }

        [TestMethod]
        public void IntItem()
        {
            var intItem = new IntegerItem
            {
                Id = 1, IntegerFieldId = 1
            };

            Assert.AreEqual(1, intItem.Id);
            Assert.AreEqual(1, intItem.IntegerFieldId);
        }

        [TestMethod]
        public void RequestHasRequestFields()
        {
            var request = new Request
            {
                Id = 1, FieldValues = new List<RequestField> {new TextRequestField {Id = 1, requestId = 1}}
            };

            Assert.AreEqual(1, request.Id);
            Assert.AreEqual(1, request.FieldValues.First().Id);
            Assert.AreEqual(1, request.FieldValues.First().requestId);
        }
    }

}
