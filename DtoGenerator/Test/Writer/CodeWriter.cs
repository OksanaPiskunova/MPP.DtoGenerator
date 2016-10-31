using System;
using System.Collections.Generic;
using System.IO;

namespace Test.Writer
{
    internal class CodeWriter : IFileWriter
    {
        private readonly string _directoryPath;

        public CodeWriter(string directoryPath)
        {
            if (directoryPath == null) throw new ArgumentNullException(nameof(directoryPath));

            _directoryPath = directoryPath;
        }

        public void Write(IDictionary<string, string> generatedCodeDictionary)
        {
            if (generatedCodeDictionary == null) throw new ArgumentNullException(nameof(generatedCodeDictionary));

            CreateDirectoryIfNotExist();

            foreach (var generatedCodeItem in generatedCodeDictionary)
            {
                WriteClassCodeToFile(generatedCodeItem);
            }
        }

        private void WriteClassCodeToFile(KeyValuePair<string, string> generatedCodeItem)
        {
            var className = generatedCodeItem.Key;
            var code = generatedCodeItem.Value;

            var fileName = GetOutputFileName(className);
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(code);
                }
            }
        }

        private string GetOutputFileName(string className)
        {
            const string csExtension = ".cs";
            return Path.Combine(_directoryPath, className + csExtension);
        }

        private void CreateDirectoryIfNotExist()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }
    }
}