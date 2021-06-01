# Testing.Dynamic.Json

Provides dynamicly access to property/member of Json object deserialized by System.Text.Json. This library is used for unit testing purpose only. It is not designated for production usage.

### Nuget package

[![NuGet](https://img.shields.io/nuget/v/Testing.Dynamic.Json.svg?style=flat-square&label=nuget&colorB=00b200)](https://www.nuget.org/packages/Testing.Dynamic.Json/)

### Build Status

[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2FNetLah%2FDynamic.Json%2Fbadge%3Fref%3Dmain&style=flat)](https://actions-badge.atrox.dev/NetLah/Dynamic.Json/goto?ref=main)

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
