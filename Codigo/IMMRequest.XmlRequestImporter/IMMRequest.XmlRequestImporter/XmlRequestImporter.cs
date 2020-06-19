namespace IMMRequest.XmlRequestImporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;
    using Logic.Models.Request;
    using Logic.Models.Type;
    using RequestImporter;

    public class XmlRequestImporter : IRequestsImportable
    {
        public List<CreateRequest> Import(string filePath)
        {
            var fullPath = Path.Join(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), filePath);
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception($"parameter {nameof(filePath)} can't be null nor empty");
            }

            if (!File.Exists(filePath))
            {
                throw new Exception($"parameter {nameof(filePath)} needs to be a valid json file");
            }


            using (var fileStream = File.Open(fullPath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(
                    typeof(List<CreateRequest>),
                    new[]
                    {
                        typeof(List<AdditionalFieldModel>),
                        typeof(List<string>)
                    });
                var requests = (List<CreateRequest>)serializer.Deserialize(fileStream);

                return requests;
            }
        }

    }
}
