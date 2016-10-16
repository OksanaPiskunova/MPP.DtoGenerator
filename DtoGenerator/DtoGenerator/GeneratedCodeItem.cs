namespace DtoGenerator
{
    public sealed class GeneratedCodeItem
    {
        public string ClassName { get; private set; }
        public string Code { get; private set; }

        public GeneratedCodeItem(string className, string code)
        {
            ClassName = className;
            Code = code;
        }
    }
}