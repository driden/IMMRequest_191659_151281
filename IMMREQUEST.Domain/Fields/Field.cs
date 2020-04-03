using System;

namespace IMMRequest.Domain.Fields
{
    public abstract class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public Field()
        {
            
        }
    }
}
