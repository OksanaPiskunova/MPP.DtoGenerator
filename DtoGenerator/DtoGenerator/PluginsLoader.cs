using System;
using System.IO;
using System.Reflection;
using TypePluginInterface;

namespace DtoGenerator
{
    internal class PluginsLoader
    {
        public TypeTable LoadTypesFromPlugins(string pluginsDirectoryPath)
        {
            if (pluginsDirectoryPath == null) throw new ArgumentNullException(nameof(pluginsDirectoryPath));
            if (!Directory.Exists(pluginsDirectoryPath)) throw new DirectoryNotFoundException(nameof(pluginsDirectoryPath));

            var typeTable = new TypeTable();

            foreach (var file in Directory.EnumerateFiles(Path.GetFullPath(pluginsDirectoryPath)))
            {
                try
                {
                    var assembly = Assembly.LoadFile(Path.GetFullPath(file));
                    var exportedTypes = assembly.GetExportedTypes();

                    var types = Array.FindAll(exportedTypes,
                        type => typeof(INetType).IsAssignableFrom(type) && !type.IsAbstract);
                    CreateInstances(typeTable, types);
                }
                catch
                {
                    // ignored
                }
            }

            if (typeTable.Count == 0) throw new TypeTableException("TypeTable is empty.");

            return typeTable;
        }

        private void CreateInstances(TypeTable typeTable, Type[] types)
        {
            foreach (var type in types)
            {
                var pluginInstance = (INetType)Activator.CreateInstance(type);
                typeTable.AddType(pluginInstance);
            }
        }
    }
}