using System;
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

        public List<GeneratedCodeItem> GenerateDtoClasses(DtoClassDescription[] dtoClassDescriptions, string classesNamespace)
        {
            var codeGenerator = new CodeGenerator(_typeTable);
            var generatedCodeList = new List<GeneratedCodeItem>();
            var countdown = new CountdownEvent(dtoClassDescriptions.Length);

            foreach (var classDescription in dtoClassDescriptions)
            {
                ThreadPool.QueueUserWorkItem(data =>
                {
                    var code = codeGenerator.GenerateCode(classDescription, classesNamespace);
                    var generatedCodeItem = new GeneratedCodeItem(classDescription.ClassName, code);
                    generatedCodeList.Add(generatedCodeItem);

                    lock (_syncRoot)
                    {
                        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                        countdown.Signal();
                    }
                });
            }

            countdown.Wait();

            return generatedCodeList;
        }

        private TypeTable LoadPlugins(string pluginsDirectory)
        {
            var pluginsLoader = new PluginsLoader();
            return pluginsLoader.LoadTypesFromPlugins(pluginsDirectory);
        }
    }
}