using System.Linq;
using System.Text.Json;

namespace Testing.Dynamic
{
    internal static class Helper
    {
        public static object Convert(JsonElement jsonElement) => jsonElement.ValueKind switch
        {
            JsonValueKind.Undefined => null,
            JsonValueKind.Object => ConvertObject(jsonElement),
            JsonValueKind.Array => ConvertArray(jsonElement),
            JsonValueKind.String => jsonElement.GetString(),
            JsonValueKind.Number => jsonElement.GetInt64(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => null,
        };

        public static DJsonObject ConvertObject(JsonElement jsonElement)
        {
            var obj = new DJsonObject();
            foreach (var item in jsonElement.EnumerateObject())
            {
                obj.Add(item.Name, Convert(item.Value));
            }
            return obj;
        }

        public static DJsonArray ConvertArray(JsonElement jsonElement)
            => new(jsonElement.EnumerateArray().Select(e => Convert(e)));
    }
}
