using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DtoGenerator.Descriptions;
using DtoGenerator.Generator;

namespace DtoGenerator
{
    public sealed class DtoGenerator
    {
        private readonly int _maxThreadCount;
        private readonly TypeTable _typeTable;

        public DtoGenerator(int maxThreadCount, string pluginsDirectory)
        {
            if (maxThreadCount <= 0) throw new ArgumentOutOfRangeException(nameof(maxThreadCount));
            if (string.IsNullOrEmpty(pluginsDirectory))
                throw new ArgumentException("Value cannot be null or empty.", nameof(pluginsDirectory));

            _maxThreadCount = maxThreadCount;
            _typeTable = LoadPlugins(pluginsDirectory);
        }

        public IDictionary<string, string> GenerateDtoClasses(DtoClassDescription[] dtoClassDescriptions, string classesNamespace)
        {
            if (dtoClassDescriptions == null) throw new ArgumentNullException(nameof(dtoClassDescriptions));
            if (string.IsNullOrEmpty(classesNamespace))
                throw new ArgumentException("Value cannot be null or empty.", nameof(classesNamespace));

            var codeGenerator = new CodeGenerator(_typeTable);
            var generatedCodeDictionary = new ConcurrentDictionary<string, string>();
            var resetEvents = InitializeResetEvents(_maxThreadCount);

            foreach (var classDescription in dtoClassDescriptions)
            {
                var eventIndex = WaitHandle.WaitAny(resetEvents);
                resetEvents[eventIndex].Reset();

                ThreadPool.QueueUserWorkItem(data =>
                {
                    try
                    {
                        var code = codeGenerator.GenerateCode(classDescription, classesNamespace);
                        generatedCodeDictionary[classDescription.ClassName] = code;
                    }
                    catch (TypeNotFoundException)
                    {
                        // ignored
                    }

                    resetEvents[eventIndex].Set();
                });
            }

            WaitHandle.WaitAll(resetEvents);
            DisposeResetEvents(resetEvents);

            return generatedCodeDictionary;
        }

        private ManualResetEvent[] InitializeResetEvents(int eventCount)
        {
            var resetEvents = new ManualResetEvent[eventCount];

            for (var eventIndex = 0; eventIndex < eventCount; eventIndex++)
            {
                resetEvents[eventIndex] = new ManualResetEvent(true);
            }

            return resetEvents;
        }

        private void DisposeResetEvents(ManualResetEvent[] resetEvents)
        {
            foreach (var resetEvent in resetEvents)
            {
                resetEvent.Dispose();
            }
        }

        private TypeTable LoadPlugins(string pluginsDirectory)
        {
            var pluginsLoader = new PluginsLoader();
            return pluginsLoader.LoadTypesFromPlugins(pluginsDirectory);
        }
    }
}