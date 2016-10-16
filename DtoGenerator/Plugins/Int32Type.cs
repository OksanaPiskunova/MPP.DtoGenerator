using TypePluginInterface;

namespace Plugins
{
    public class Int32Type : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public Int32Type()
        {
            Type = "integer";
            Format = "int32";
            NetType = "System.Int32";
        }
    }
}