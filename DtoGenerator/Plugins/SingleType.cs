using TypePluginInterface;

namespace Plugins
{
    public class SingleType : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public SingleType()
        {
            Type = "number";
            Format = "float";
            NetType = "System.Single";
        }
    }
}