using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGenerator
{
    public sealed class DtoClassDescription
    {
        public string ClassName { get; private set; }
        public List<DtoPropertyDescription> Properties { get; private set; }

        public DtoClassDescription(string className)
        {
            this.ClassName = className;

            this.Properties = new List<DtoPropertyDescription>();
        }
    }
}
