using TypePluginInterface;

namespace Plugins
{
    public class ByteType : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public ByteType()
        {
            Type = "string";
            Format = "byte";
            NetType = "System.Byte";
        }
    }
}