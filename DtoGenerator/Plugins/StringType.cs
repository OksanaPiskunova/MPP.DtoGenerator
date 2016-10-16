using TypePluginInterface;

namespace Plugins
{
    public class StringType : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public StringType()
        {
            Type = "string";
            Format = "string";
            NetType = "System.String";
        }
    }
}