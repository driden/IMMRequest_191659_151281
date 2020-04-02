using System;

namespace IMMREQUEST.Domain
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