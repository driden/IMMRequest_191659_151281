using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class TextField : Field
    {
        public string Type { get; set; }

        public IEnumerable<Item<string>> Range { get; set; }

        public TextField() : base()
        {

        }
    }
}
