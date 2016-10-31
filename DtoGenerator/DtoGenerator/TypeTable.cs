using System;
using System.Collections.Generic;
using System.Linq;
using TypePluginInterface;

namespace DtoGenerator
{
    internal sealed class TypeTable
    {
        private readonly Dictionary<string[], string> _typeTable;

        public int Count => _typeTable.Count;

        public TypeTable()
        {
            _typeTable = new Dictionary<string[], string>();
        }

        public void AddType(INetType typePlugin)
        {
            if (typePlugin == null) throw new ArgumentNullException(nameof(typePlugin));
            
            var type = typePlugin.Type;
            var format = typePlugin.Format;
            var netType = typePlugin.NetType;

            if (type == null) throw new ArgumentNullException(nameof(type));
            if (format == null) throw new ArgumentNullException(nameof(format));
            if (netType == null) throw new ArgumentNullException(nameof(netType));

            var key = new[] { type, format };
            try
            {
                _typeTable.Add(key, netType);
            }
            catch (ArgumentException)
            {
                throw new TypeTableException();
            }
        }

        public string GetNetType(string type, string format)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (format == null) throw new ArgumentNullException(nameof(format));

            var netType = string.Empty;
            var typeTableKeys = _typeTable.Keys;
            foreach (var key in typeTableKeys)
            {
                if (key.SequenceEqual(new[] { type, format }))
                {
                    netType = _typeTable[key];
                }
            }

            if (netType == string.Empty) throw new TypeNotFoundException();

            return netType;
        }
    }
}