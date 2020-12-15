using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Testing.Dynamic
{
    [JsonConverter(typeof(DJsonArrayJsonConverter))]
    public class DJsonArray : DJson
    {
        private readonly List<object> _list = new();

        internal DJsonArray(IEnumerable<object> list = null) => _list = list?.ToList() ?? new();

        public override int CountMembers => _list.Count;

        public override List<object> InnerList => _list;

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = indexes.Length == 1 && indexes[0] is int index && index >= 0 && index < _list.Count ? _list[index] : null;
            return true;
        }

        public override object this[int index] => index >= 0 && index < _list.Count ? _list[index] : null;
    }
}
