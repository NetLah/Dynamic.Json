using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json.Serialization;

namespace Testing.Dynamic
{
    [JsonConverter(typeof(DJsonObjectJsonConverter))]
    public class DJsonObject : DJson
    {
        private readonly Dictionary<string, object> _dict = new();
        private readonly Dictionary<string, object> _dict2 = new(StringComparer.OrdinalIgnoreCase);

        internal DJsonObject() { }

        public override int CountMembers => _dict.Count;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.IgnoreCase && _dict2.TryGetValue(binder.Name, out var value1))
            {
                result = value1;
            }
            else if (!binder.IgnoreCase && _dict.TryGetValue(binder.Name, out var value2))
            {
                result = value2;
            }
            else
            {
                result = null;
            }
            return true;
        }

        public override object this[string index] => !string.IsNullOrEmpty(index) && _dict2.TryGetValue(index, out var value) ? value : null;

        public override Dictionary<string, object> InnerDict => _dict;

        internal void Add(string key, object value)
        {
            _dict.Add(key, value);
            _dict2.Add(key, value);
        }
    }
}
