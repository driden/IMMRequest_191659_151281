namespace IMMRequest.Logic.Models.Request
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("request")]
    public class CreateRequest
    {
        public string Details { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int TypeId { get; set; } = -1;

        [XmlArray("additionalFields")]
        [XmlArrayItem(ElementName = "field")]
        public IEnumerable<FieldRequestModel> AdditionalFields { get; set; } = new List<FieldRequestModel>();
    }
}
