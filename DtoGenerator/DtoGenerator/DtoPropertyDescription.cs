using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGenerator
{
    public sealed class DtoPropertyDescription
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Format { get; private set; }

        public DtoPropertyDescription(string name, string type, string format)
        {
            this.Name = name;
            this.Type = type;
            this.Format = format;
        }
    }
}
