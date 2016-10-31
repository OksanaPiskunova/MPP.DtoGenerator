using System;
using System.Collections.Generic;
using System.IO;
using DtoGenerator;
using DtoGenerator.Descriptions;
using DtoGenerator.Generator;
using Newtonsoft.Json;
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
            string jsonClassDescriptions;

            try
            {
                jsonClassDescriptions = fileReader.ReadToEnd(jsonFileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File {0} not found.", jsonFileName);
                throw new DtoGeneratorProgramException();
            }
            catch (IOException)
            {
                Console.WriteLine("Failed to read the file {0}", jsonFileName);
                throw new DtoGeneratorProgramException();
            }

            return jsonClassDescriptions;
        }

        private static DtoClassDescription[] ParseJson(string jsonClassDescriptions)
        {
            if (jsonClassDescriptions == null) throw new ArgumentNullException(nameof(jsonClassDescriptions));

            var jsonParser = new JsonParser();
            DtoClassDescription[] classDescriptions;

            try
            {
                classDescriptions = jsonParser.Parse(jsonClassDescriptions);
            }
            catch (JsonException)
            {
                Console.WriteLine("Failed to desealized data.");
                throw new DtoGeneratorProgramException();
            }

            return classDescriptions;
        }

        private static void ReadApplicationSettings()
        {
            var applicationSettingsReader = new ApplicationSettingsReader();

            _maxTaskCount = applicationSettingsReader.MaxTaskCount;
            _classesNamespace = applicationSettingsReader.ClassesNamespace;
            _pluginDirectoryPath = applicationSettingsReader.PluginsDirectory;

            if (_classesNamespace == string.Empty)
            {
                throw new BadArgumentValueException("Namespace can not be empty.");
            }

            if (_pluginDirectoryPath == string.Empty)
            {
                throw new BadArgumentValueException("The path to the folder with plug-ins can not be empty.");
            }
        }

        private static IDictionary<string, string> GenerateDtoClasses(DtoClassDescription[] dtoClassDescriptions)
        {
            IDictionary<string, string> generatedCodeItems;

            try
            {
                var dtoGenerator = new DtoGenerator.DtoGenerator(_maxTaskCount, _pluginDirectoryPath);
                generatedCodeItems = dtoGenerator.GenerateDtoClasses(dtoClassDescriptions, _classesNamespace);
            }
            catch (CodeGeneratorException e)
            {
                Console.WriteLine(e.Message);
                throw new DtoGeneratorProgramException();
            }
            catch (TypeTableException e)
            {
                Console.WriteLine(e.Message);
                throw new DtoGeneratorProgramException();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory '{0}' not found.", _pluginDirectoryPath);
                throw new DtoGeneratorProgramException();
            }

            return generatedCodeItems;
        }

        private static void WriteCodeToFile(IDictionary<string, string> generatedCodeItems, string directoryPath)
        {
            try
            {
                var codeWriter = new CodeWriter(directoryPath);
                codeWriter.Write(generatedCodeItems);
            }
            catch (IOException)
            {
                Console.WriteLine("Failed to write to the file.");
                throw new DtoGeneratorProgramException();
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong number of arguments.");
                Console.WriteLine("Usage: jsonFilePath OutputDirectoryPath");
                return;
            }

            var jsonFileName = args[0];
            var outputDirectoryPath = args[1];

            try
            {
                var jsonClassDescriptions = ReadJsonFromFile(jsonFileName);
                var dtoClassDescriptions = ParseJson(jsonClassDescriptions);

                ReadApplicationSettings();

                var generatedCodeItems = GenerateDtoClasses(dtoClassDescriptions);
                WriteCodeToFile(generatedCodeItems, outputDirectoryPath);
            }
            catch (BadArgumentValueException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DtoGeneratorProgramException)
            {
                // ignored
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown exception: {0}.", e.Message);
            }
            finally
            {
                Console.WriteLine("Press Enter to exit...");
                Console.Read();
            }
        }
    }
}
