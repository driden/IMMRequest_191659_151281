namespace IMMRequest.XmlRequestImporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Logic.Models.Request;
    using RequestImporter;

    public class XmlRequestImporter : IRequestsImportable
    {
        public CreateRequestList Import(string fileContent)
        {
            if (string.IsNullOrEmpty(fileContent))
            {
                throw new Exception($"parameter {nameof(fileContent)} can't be null nor empty");
            }

            using (var textReader= new StringReader(fileContent))
            {
                XmlSerializer serializer = new XmlSerializer(
                    typeof(CreateRequestList),
                    new[]
                    {
                        typeof(List<FieldRequestModel>)
                    });
                var requests = (CreateRequestList)serializer.Deserialize(textReader);
                return requests;
            }
        }

    }
}
