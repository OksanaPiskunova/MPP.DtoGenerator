using TypePluginInterface;

namespace Plugins
{
    public class DoubleType : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public DoubleType()
        {
            Type = "number";
            Format = "double";
            NetType = "System.Double";
        }
    }
}