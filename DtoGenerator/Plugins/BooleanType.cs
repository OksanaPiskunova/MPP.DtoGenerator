using TypePluginInterface;

namespace Plugins
{
    public class BooleanType : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public BooleanType()
        {
            Type = "boolean";
            Format = "";
            NetType = "System.Boolean";
        }
    }
}