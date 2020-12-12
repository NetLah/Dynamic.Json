# Testing.Dynamic.Json

Provides dynamicly access to property/member of Json object deserialized by System.Text.Json. This library is used for unit testing purpose only. It is not designated for production usage.

### Getting started
To support testing with dynamic json, first install the NuGet package:

```
Install-Package Testing.Dynamic.Json
```
### Example dynamic json

- Newtonsoft Json support
```
const string json = "[ 123, \"abc\", { \"iss\": \"Sso Issuer\", \"iat\": 1607746395 }]";

var jToken = Newtonsoft.Json.Linq.JToken.Parse(json);
Assert.Equal(3, (jToken as JArray).Count);
Assert.Equal(123, jToken[0]);
Assert.Equal(123L, jToken[0]);
Assert.Equal("abc", jToken[1]);
var jwtPayload = jToken[2];
Assert.Equal("Sso Issuer", jwtPayload["iss"]);
Assert.Equal(1607746395, jwtPayload["iat"]);
```

- Testing.Dynamic.Json support
```
var djson = System.Text.Json.JsonSerializer.Deserialize<DJson>(json);
Assert.Equal(3, djson.CountMembers);
Assert.Equal(123, (int)(long)djson[0]);
Assert.Equal(123L, djson[0]);
Assert.Equal("abc", djson[1]);
var jwtPayload = (DJson)djson[2];
Assert.Equal("Sso Issuer", jwtPayload["iss"]);
Assert.Equal(1607746395L, jwtPayload["iat"]);
```
