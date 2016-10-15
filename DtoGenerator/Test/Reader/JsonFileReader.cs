using System.IO;

namespace Test.Reader
{
    internal sealed class JsonFileReader : IFileReader
    {
        public string ReadToEnd(string fileName)
        {
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