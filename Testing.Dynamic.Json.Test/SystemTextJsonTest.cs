using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Testing.Dynamic.Test
{
    public class SystemTextJsonTest
    {
        [Fact]
        public void JsonIgnoreDefault_TestDefault()
        {
            var obj = new DtoIgnoreWriteDefault();

            var json = JsonSerializer.Serialize<DtoIgnoreWriteDefault>(obj);

            Assert.Equal("{\"Id\":null}", json);
        }

        [Fact]
        public void JsonIgnoreDefault_TestValue()
        {
            var obj = new DtoIgnoreWriteDefault { Id = null, Value = 1 };

            var json = JsonSerializer.Serialize<DtoIgnoreWriteDefault>(obj);

            Assert.Equal("{\"Id\":null,\"Value\":1}", json);
        }

        [Fact]
        public void JsonIgnoreNull_TestNull()
        {
            var obj = new DtoIgnoreWriteNull();

            var json = JsonSerializer.Serialize<DtoIgnoreWriteNull>(obj);

            Assert.Equal("{\"Id\":null}", json);
        }

        [Fact]
        public void JsonIgnoreNull_TestEmpty()
        {
            var obj = new DtoIgnoreWriteNull { Id = null, Name = "" };

            var json = JsonSerializer.Serialize<DtoIgnoreWriteNull>(obj);

            Assert.Equal("{\"Id\":null,\"Name\":\"\"}", json);
        }

        private class DtoIgnoreWriteDefault
        {
            public string Id { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public int Value { get; set; }
        }

        private class DtoIgnoreWriteNull
        {
            public string Id { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string Name { get; set; }
        }
    }
}
