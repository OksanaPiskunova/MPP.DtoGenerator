using DtoGenerator;

namespace Test.Parser
{
    public interface IParser<out T>
    {
        T[] Parse(string classDescriptions);
    }
}