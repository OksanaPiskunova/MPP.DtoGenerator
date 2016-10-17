using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DtoGenerator.Descriptions;
using DtoGenerator.Generator;

namespace DtoGenerator
{
    public sealed class DtoGenerator
    {
        private readonly TypeTable _typeTable;
        private volatile object _syncRoot = new object();

        public DtoGenerator(int maxThreadCount, string pluginsDirectory)
        {
            _typeTable = LoadPlugins(pluginsDirectory);
        }

        public IDictionary<string, string> GenerateDtoClasses(DtoClassDescription[] dtoClassDescriptions, string classesNamespace)
        {
            var codeGenerator = new CodeGenerator(_typeTable);
            var generatedCodeDictionary = new ConcurrentDictionary<string, string>();
            var countdown = new CountdownEvent(dtoClassDescriptions.Length);

            foreach (var classDescription in dtoClassDescriptions)
            {
                ThreadPool.QueueUserWorkItem(data =>
                {
                    var code = codeGenerator.GenerateCode(classDescription, classesNamespace);
                    generatedCodeDictionary[classDescription.ClassName] = code;

                    lock (_syncRoot)
                    {
                        countdown.Signal();
                    }
                });
            }

            countdown.Wait();

            return generatedCodeDictionary;
        }

        private TypeTable LoadPlugins(string pluginsDirectory)
        {
            var pluginsLoader = new PluginsLoader();
            return pluginsLoader.LoadTypesFromPlugins(pluginsDirectory);
        }
    }
}