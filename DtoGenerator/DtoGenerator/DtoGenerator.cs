using System.Collections.Generic;
using DtoGenerator.Descriptions;
using DtoGenerator.Generator;

namespace DtoGenerator
{
    public sealed class DtoGenerator
    {
        private TypeTable _typeTable;

        public DtoGenerator(int maxThreadCount, string pluginsDirectory)
        {
            _typeTable = LoadPlugins(pluginsDirectory);
        }

        public List<GeneratedCodeItem> GenerateDtoClasses(DtoClassDescription[] dtoClassDescriptions, string classesNamespace)
        {
            var codeGenerator = new CodeGenerator(_typeTable);
            var generatedCodeList = new List<GeneratedCodeItem>();

            foreach (var classDescription in dtoClassDescriptions)
            {
                var code = codeGenerator.GenerateCode(classDescription, classesNamespace);
                var generatedCodeItem = new GeneratedCodeItem(classDescription.ClassName, code);
                generatedCodeList.Add(generatedCodeItem);
            }

            return generatedCodeList;
        }

        private TypeTable LoadPlugins(string pluginsDirectory)
        {
            var pluginsLoader = new PluginsLoader();
            return pluginsLoader.LoadTypesFromPlugins(pluginsDirectory);
        }
    }
}