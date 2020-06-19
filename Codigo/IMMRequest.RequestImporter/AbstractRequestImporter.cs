namespace IMMRequest.RequestImporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class AbstractRequestImporter
    {
        public IRequestsImportable GetInstance()
        {
            var first = ReadFirstInPluginsDirectory();
            var dllFile = new FileInfo(first);

            Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);
            var implementations = GetTypesInAssembly<IRequestsImportable>(myAssembly);
            IRequestsImportable importer = (IRequestsImportable)Activator.CreateInstance(implementations.First(), new object[] { });
            return importer;
        }

        public CreateRequestList ParseFile(string filePath)
        {
            IRequestsImportable instance = GetInstance();
            return instance.Import(filePath);
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

        private string ReadFirstInPluginsDirectory()
        {
            return Directory.EnumerateFiles(Path.Join(GetCurrentDirectory, "plugins"), "*.dll").FirstOrDefault();
        }

        private string GetCurrentDirectory => Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
    }
}
