using System;
using DtoGenerator.Descriptions;
using Newtonsoft.Json;

namespace Test.Parser
{
    internal sealed class JsonParser
    {
        public DtoClassDescription[] Parse(string classDescriptions)
        {
            if (classDescriptions == null) throw new ArgumentNullException(nameof(classDescriptions));

            return JsonConvert.DeserializeObject<DtoClassDescription[]>(classDescriptions);
        }
    }
}
