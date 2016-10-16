namespace DtoGenerator.Descriptions
{
    public sealed class DtoPropertyDescription
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Format { get; private set; }

        public DtoPropertyDescription(string name, string type, string format)
        {
            Name = name;
            Type = type;
            Format = format;
        }
    }
}
