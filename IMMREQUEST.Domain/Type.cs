using IMMRequest.Domain.Fields;
using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.Domain
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<AdditionalField> AdditionalFields { get; set; }

        //void foo()
        // {
        //     additionalFields = new List<AdditionalField>();

        //     additionalFields.Add(new IntegerField { Value = 2,
        //         Range = new List<Item<int>> { new Item<int> { Value = 1}, new Item<int> { Value = 1 }, new Item<int> { Value = 1 } } });



        //     var first = additionalFields.First();
        //     if (first.FieldType == FieldType.Integer)
        //     {
        //        var valor = (first as IntegerField).Value;
        //     }
        // }

    }
}
