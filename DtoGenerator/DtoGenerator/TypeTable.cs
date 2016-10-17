using System.Collections.Generic;
using System.Linq;
using TypePluginInterface;

namespace DtoGenerator
{
    internal sealed class TypeTable
    {
        private Dictionary<string[], string> _typeTable;

        public TypeTable()
        {
            _typeTable = new Dictionary<string[], string>();
        }

        public void AddType(INetType typePlugin)
        {
            var type = typePlugin.Type;
            var format = typePlugin.Format;
            var netType = typePlugin.NetType;

            var key = new[] {type, format};
            _typeTable.Add(key, netType);
        }

        public string GetNetType(string type, string format)
        {
            var netType = string.Empty;
            var typeTableKeys = _typeTable.Keys;
            foreach (var key in typeTableKeys)
            {
                if (key.SequenceEqual(new[] { type, format }))
                {
                    return _typeTable[key];
                }
            }

            return netType;
        }
    }
}