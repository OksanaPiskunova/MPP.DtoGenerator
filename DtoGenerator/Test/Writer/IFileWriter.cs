using System.Collections.Generic;

namespace Test.Writer
{
    internal interface IFileWriter<T>
    {
        void Write(List<T> generatedCodeList);
    }
}