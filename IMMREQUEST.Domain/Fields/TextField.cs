using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class TextField : AdditionalField
    {
        public IEnumerable<TextItem> Range { get; set; } = new List<TextItem>();
        public string Value { get; set; } = string.Empty;

        public void AddToRange(string item)
        {
            (this.Range as IList<TextItem>).Add(new TextItem { Value = item });

        }
        public TextField()
        {
            this.FieldType = FieldType.Text;
        }

        public void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
