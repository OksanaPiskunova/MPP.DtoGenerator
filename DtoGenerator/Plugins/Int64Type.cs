using TypePluginInterface;

namespace Plugins
{
    public class Int64Type : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public Int64Type()
        {
            Type = "integer";
            Format = "int64";
            NetType = "System.Int64";
        }
    }
}