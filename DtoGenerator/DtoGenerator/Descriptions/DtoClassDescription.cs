using System.Collections.Generic;

namespace DtoGenerator.Descriptions
{
    public sealed class DtoClassDescription
    {
        public string ClassName { get; private set; }
        public List<DtoPropertyDescription> Properties { get; private set; }

        public DtoClassDescription(string className)
        {
            ClassName = className;
            Properties = new List<DtoPropertyDescription>();
        }
    }
}
