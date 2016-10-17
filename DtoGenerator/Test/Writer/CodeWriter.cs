using System.Collections.Generic;
using System.IO;

namespace Test.Writer
{
    internal class CodeWriter : IFileWriter
    {
        private readonly string _directoryPath;

        public CodeWriter(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void Write(IDictionary<string, string> generatedCodeDictionary)
        {
            CreateDirectoryIfNotExist();

            foreach (var generatedCodeItem in generatedCodeDictionary)
            {
                WriteClassCodeToFile(generatedCodeItem);
            }
        }

        private void WriteClassCodeToFile(KeyValuePair<string, string> generatedCodeItem)
        {
            var fileName = GetOutputFileName(generatedCodeItem.Key);
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(generatedCodeItem.Value);
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