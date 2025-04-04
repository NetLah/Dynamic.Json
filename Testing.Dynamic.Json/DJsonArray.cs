using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Testing.Dynamic
{
    [JsonConverter(typeof(DJsonArrayJsonConverter))]
    public class DJsonArray : DJson
    {
        private readonly List<object> _list = [];

        internal DJsonArray(IEnumerable<object> list = null) => _list = list?.ToList() ?? [];

        public override int CountMembers => _list.Count;

        public override List<object> InnerList => _list;

        public override object this[int index] => index >= 0 && index < _list.Count ? _list[index] : null;
    }
}
