using DtoGenerator;
using Newtonsoft.Json;

namespace Test.Parser
{
    internal sealed class JsonParser
    {
        public DtoClassDescription[] Parse(string classDescriptions)
        {
            return JsonConvert.DeserializeObject<DtoClassDescription[]>(classDescriptions);
        }
    }
}
