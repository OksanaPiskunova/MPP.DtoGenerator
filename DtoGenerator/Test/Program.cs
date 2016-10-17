using System;
using System.Collections.Generic;
using DtoGenerator.Descriptions;
using Test.ApplicationSettings;
using Test.Parser;
using Test.Reader;
using Test.Writer;

namespace Test
{
    internal class Program
    {
        private static int _maxTaskCount;
        private static string _classesNamespace;
        private static string _pluginDirectoryPath;

        private static string ReadJsonFromFile(string jsonFileName)
        {
            var fileReader = new JsonFileReader();
            return fileReader.ReadToEnd(jsonFileName);
        }

        private static DtoClassDescription[] ParseJson(string jsonClassDescriptions)
        {
            var jsonParser = new JsonParser();
            return jsonParser.Parse(jsonClassDescriptions);
        }

        private static void ReadApplicationSettings()
        {
            var applicationSettingsReader = new ApplicationSettingsReader();

            _maxTaskCount = applicationSettingsReader.MaxTaskCount;
            _classesNamespace = applicationSettingsReader.ClassesNamespace;
            _pluginDirectoryPath = applicationSettingsReader.PluginsDirectory;
        }

        private static IDictionary<string, string> GenerateDtoClasses(DtoClassDescription[] dtoClassDescriptions)
        {
            var dtoGenerator = new DtoGenerator.DtoGenerator(_maxTaskCount, _pluginDirectoryPath);
            return dtoGenerator.GenerateDtoClasses(dtoClassDescriptions, _classesNamespace);
        }

        private static void WriteCodeToFile(IDictionary<string, string> generatedCodeList, string directoryPath)
        {
            var codeWriter = new CodeWriter(directoryPath);
            codeWriter.Write(generatedCodeList);
        }

        public static void Main(string[] args)
        {
            var jsonFileName = args[0];
            var outputDirectoryPath = args[1];
            
            var jsonClassDescriptions = ReadJsonFromFile(jsonFileName);
            var dtoClassDescriptions = ParseJson(jsonClassDescriptions);

            ReadApplicationSettings();

            var generatedCodeList = GenerateDtoClasses(dtoClassDescriptions);
            WriteCodeToFile(generatedCodeList, outputDirectoryPath);

            Console.WriteLine("Press Enter to exit...");
            Console.Read();
        }
    }
}
