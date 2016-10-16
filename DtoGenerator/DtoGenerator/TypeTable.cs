using System.Collections.Generic;
using System.Linq;
using TypePluginInterface;

namespace DtoGenerator
{
    internal sealed class TypeTable
    {
        private Dictionary<string[], string> typeTable;

        public TypeTable()
        {
            typeTable = new Dictionary<string[], string>();
        }

        public void AddType(INetType typePlugin)
        {
            var type = typePlugin.Type;
            var format = typePlugin.Format;
            var netType = typePlugin.NetType;

            var key = new[] {type, format};
            typeTable.Add(key, netType);
        }

        public string GetNetType(string type, string format)
        {
            var netType = string.Empty;
            var typeTableKeys = typeTable.Keys;
            foreach (var key in typeTableKeys)
            {
                if (Enumerable.SequenceEqual(key, new[] { type, format }))
                {
                    return typeTable[key];
                }
            }

            return netType;
        }
    }
}