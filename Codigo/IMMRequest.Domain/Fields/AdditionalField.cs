namespace IMMRequest.Domain.Fields
{
    using System.Collections.Generic;

    public enum FieldType { Integer, Text, Date, Boolean }
    public abstract class AdditionalField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldType FieldType { get; set; }
        public bool IsRequired { get; set; }
        public int TypeId { get; set; }

        public abstract void ValidateRange<T>(IEnumerable<T> values);
        public abstract void ValidateRangeIsCorrect();
        public abstract void AddToRange(IItem item);
        public abstract IEnumerable<string> GetRangeAsText();
        public abstract string GetTypeName();
        public static FieldType MapStringToFieldType(string fieldTypeStr)
        {
            switch (fieldTypeStr)
            {
                case "int": return FieldType.Integer;
                case "text": return FieldType.Text;
                case "date": return FieldType.Date;
                default: return FieldType.Boolean;
            }
        }
    }
}
