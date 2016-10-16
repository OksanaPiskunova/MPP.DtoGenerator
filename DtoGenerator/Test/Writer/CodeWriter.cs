using System.Collections.Generic;
using System.IO;
using System.Net;
using DtoGenerator;

namespace Test.Writer
{
    internal class CodeWriter : IFileWriter<GeneratedCodeItem>
    {
        private readonly string _directoryPath;

        public CodeWriter(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void Write(List<GeneratedCodeItem> generatedCodeList)
        {
            CreateDirectoryIfNotExist();

            foreach (var generatedCodeItem in generatedCodeList)
            {
                WriteClassCodeToFile(generatedCodeItem);
            }
        }

        private void WriteClassCodeToFile(GeneratedCodeItem generatedCodeItem)
        {
            var fileName = GetOutputFileName(generatedCodeItem.ClassName);
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(generatedCodeItem.Code);
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