namespace IMMRequest.XmlRequestImporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;
    using Logic.Models.Request;
    using RequestImporter;

    public class XmlRequestImporter : IRequestsImportable
    {
        public CreateRequestList Import(string filePath)
        {
            var fullPath = Path.Join(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), filePath);
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception($"parameter {nameof(filePath)} can't be null nor empty");
            }

            if (!File.Exists(filePath))
            {
                throw new Exception($"parameter {nameof(filePath)} needs to be a valid xml file");
            }


            using (var fileStream = File.Open(fullPath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(
                    typeof(CreateRequestList),
                    new[]
                    {
                        typeof(List<FieldRequestModel>)
                    });
                var requests = (CreateRequestList)serializer.Deserialize(fileStream);
                return requests;
            }
        }

    }
}
