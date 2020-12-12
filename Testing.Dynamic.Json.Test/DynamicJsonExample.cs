using Newtonsoft.Json.Linq;
using Xunit;

namespace Testing.Dynamic.Test
{
    public class DynamicJsonExample
    {
        [Fact]
        public void NewtonsoftSupport()
        {
            const string json = "[ 123, \"abc\", { \"iss\": \"Sso Issuer\", \"iat\": 1607746395 }]";

            var jToken = Newtonsoft.Json.Linq.JToken.Parse(json);
            Assert.Equal(3, (jToken as JArray).Count);
            Assert.Equal(123, jToken[0]);
            Assert.Equal(123L, jToken[0]);
            Assert.Equal("abc", jToken[1]);
            var jwtPayload = jToken[2];
            Assert.Equal("Sso Issuer", jwtPayload["iss"]);
            Assert.Equal(1607746395, jwtPayload["iat"]);
        }

        [Fact]
        public void DJsonSupport()
        {
            const string json = "[ 123, \"abc\", { \"iss\": \"Sso Issuer\", \"iat\": 1607746395 }]";

            var djson = System.Text.Json.JsonSerializer.Deserialize<DJson>(json);
            Assert.Equal(3, djson.CountMembers);
            Assert.Equal(123, (int)(long)djson[0]);
            Assert.Equal(123L, djson[0]);
            Assert.Equal("abc", djson[1]);
            var jwtPayload = (DJson)djson[2];
            Assert.Equal("Sso Issuer", jwtPayload["iss"]);
            Assert.Equal(1607746395L, jwtPayload["iat"]);
        }
    }
}
