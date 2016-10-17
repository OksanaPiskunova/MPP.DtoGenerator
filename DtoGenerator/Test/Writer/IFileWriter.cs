using System.Collections.Generic;

namespace Test.Writer
{
    internal interface IFileWriter
    {
        void Write(IDictionary<string, string> generatedCodeDictionary);
    }
}