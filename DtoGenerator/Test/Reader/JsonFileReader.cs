using System;
using System.IO;

namespace Test.Reader
{
    internal sealed class JsonFileReader : IFileReader
    {
        public string ReadToEnd(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            string jsonData;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    jsonData = sr.ReadToEnd();
                }
            }

            return jsonData;
        }
    }
}