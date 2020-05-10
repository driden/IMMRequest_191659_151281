using System;

namespace IMMRequest.Logic.Models
{
    public class FieldRequestModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            return obj is FieldRequestModel frm && frm.Name == Name && frm.Value == Value;
        }
    }
}
