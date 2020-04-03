using System;

namespace IMMRequest.Domain.Fields
{
    public class IntegerField : Field
    {
        public int Type { get; set; }
        public int[] Range { get; set; }

        public IntegerField()
        {

        }
    }
}
