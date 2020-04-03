using System;

namespace IMMRequest.Domain
{
    public abstract class Field
    {
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public Field()
        {
            
        }
    }
}