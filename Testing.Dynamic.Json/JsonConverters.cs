using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Testing.Dynamic
{
    public class DJsonJsonConverter : JsonConverter<DJson>
    {
        public override DJson Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            return Helper.Convert(jsonElement) as DJson;
        }

        public override void Write(Utf8JsonWriter writer, DJson value, JsonSerializerOptions options)
        {
            if (value is DJsonObject jsonObject)
            {
                var converter = (JsonConverter<DJsonObject>)options.GetConverter(typeof(DJsonObject));
                converter.Write(writer, jsonObject, options);
            }
            else if (value is DJsonArray jsonArray)
            {
                var converter = (JsonConverter<DJsonArray>)options.GetConverter(typeof(DJsonArray));
                converter.Write(writer, jsonArray, options);
            }
        }
    }

    public class DJsonObjectJsonConverter : JsonConverter<DJsonObject>
    {
        public override DJsonObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, DJsonObject value, JsonSerializerOptions options)
            => Serialize(writer, options, value.InnerDict);

        private static void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, Dictionary<string, object> value)
        {
            var converter = (JsonConverter<Dictionary<string, object>>)options.GetConverter(value.GetType());
            converter.Write(writer, value, options);
        }
    }

    public class DJsonArrayJsonConverter : JsonConverter<DJsonArray>
    {
        public override DJsonArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, DJsonArray value, JsonSerializerOptions options)
        {
            var converter = (JsonConverter<List<object>>)options.GetConverter(value.InnerList.GetType());
            converter.Write(writer, value.InnerList, options);
        }
    }
}
