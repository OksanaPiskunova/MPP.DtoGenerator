using TypePluginInterface;

namespace Plugins
{
    public class DateTimeType : INetType
    {
        public string Type { get; private set; }
        public string Format { get; private set; }
        public string NetType { get; private set; }

        public DateTimeType()
        {
            Type = "string";
            Format = "date";
            NetType = "System.DateTime";
        }
    }
}