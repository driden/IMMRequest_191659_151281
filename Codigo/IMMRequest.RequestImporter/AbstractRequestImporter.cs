namespace IMMRequest.RequestImporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class AbstractRequestImporter
    {
        private readonly Dictionary<string, string> _assemblyForType = new Dictionary<string, string>()
        {
            {"json", "IMMRequest.JsonRequestImporter.dll" },
            {"xml", "IMMRequest.XmlRequestImporter.dll" }
        };

        public IRequestsImportable GetInstance(string fileType)
        {
            var first = ReadFirstInPluginsDirectory(_assemblyForType[fileType.Trim().ToLower()]);
            var dllFile = new FileInfo(first);

            Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);
            var implementations = GetTypesInAssembly<IRequestsImportable>(myAssembly);
            IRequestsImportable importer = (IRequestsImportable)Activator.CreateInstance(implementations.First(), new object[] { });
            return importer;
        }

        public CreateRequestList ParseFile(string fileContent, string fileType)
        {
            IRequestsImportable instance = GetInstance(fileType);
            return instance.Import(fileContent);
        }

        private IEnumerable<Type> GetTypesInAssembly<T>(Assembly assembly)
        {
            List<Type> types = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                    types.Add(type);
            }
            return types;
        }

        private string ReadFirstInPluginsDirectory(string dllFile)
        {
            return Directory.EnumerateFiles(Path.Join(GetCurrentDirectory, "plugins"), dllFile).FirstOrDefault();
        }

        private string GetCurrentDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
