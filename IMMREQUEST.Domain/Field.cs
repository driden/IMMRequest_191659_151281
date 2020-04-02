using System;

namespace IMMREQUEST.Domain
{
    public abstract class Field
    {
        public String Name { get; set; }
        public Boolean Required { get; set; }

        public Field()
        {
            
        }
    }
}