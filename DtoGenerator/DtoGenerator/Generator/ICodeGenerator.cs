using DtoGenerator.Descriptions;

namespace DtoGenerator.Generator
{
    internal interface ICodeGenerator
    {
        string GenerateCode(DtoClassDescription classDescription, string classNamespace);
    }
}