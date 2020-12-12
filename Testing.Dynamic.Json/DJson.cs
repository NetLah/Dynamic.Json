using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Testing.Dynamic
{
    [JsonConverter(typeof(DJsonJsonConverter))]
    public abstract class DJson : DynamicObject
    {
        protected DJson() { }

        public abstract int CountMembers { get; }
        public virtual object this[int index] => null;
        public virtual object this[string index] => null;
        public virtual List<object> InnerList => null;
        public virtual Dictionary<string, object> InnerDict => null;

        public static DJson Parse(string json)
        {
            using var doc1 = JsonDocument.Parse(json);
            return Helper.Convert(doc1.RootElement) as DJson;
        }

        public T ToObject<T>()
        {
            var json = JsonSerializer.Serialize(this);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
