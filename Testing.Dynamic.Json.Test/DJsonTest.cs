using System;
using System.Text.Json;
using Xunit;

namespace Testing.Dynamic.Test
{
    public class DJsonTest
    {
        private const string json = " [ { \"name\": \"John\" }, [ \"425-000-1212\", 15 ], { \"grades\": [ 90, 80, 100, 75 ] } ]";

        [Fact]
        public void TestParse()
        {
            var dynJson = DJson.Parse(json);

            AssertDynamic(dynJson);

            AssertDynamicJson(dynJson);
        }

        [Fact]
        public void TestDeserialize()
        {
            var dynJson = JsonSerializer.Deserialize<DJson>(json);

            AssertDynamic(dynJson);

            AssertDynamicJson(dynJson);
        }

        private static void AssertDynamicJson(DJson dynJson)
        {
            Assert.Equal(3, dynJson.CountMembers);
            Assert.Null(dynJson[3]);
            var gradeObj = (DJson)dynJson[2];
            Assert.NotNull(gradeObj);
            Assert.Null(gradeObj["Dontknow"]);

            var grades = (DJson)gradeObj["grades"];
            Assert.NotNull(grades);

            var obj90L = grades[0];
            Assert.NotEqual(90, obj90L);
            Assert.Equal(90, (int)(long)obj90L);
            Assert.Equal(90, Convert.ToInt32(obj90L));
            Assert.Equal(90, Convert.ToInt64(obj90L));

            Assert.NotStrictEqual(90, grades[0]);
            Assert.NotStrictEqual(80, grades[1]);
            Assert.NotStrictEqual(100, grades[2]);
            Assert.NotStrictEqual(75, grades[3]);

            Assert.Equal("[{\"name\":\"John\"},[\"425-000-1212\",15],{\"grades\":[90,80,100,75]}]", JsonSerializer.Serialize(dynJson));
            Assert.Equal("{\"name\":\"John\"}", JsonSerializer.Serialize(dynJson[0]));
            Assert.Equal("[\"425-000-1212\",15]", JsonSerializer.Serialize(dynJson[1]));
            Assert.Equal("{\"grades\":[90,80,100,75]}", JsonSerializer.Serialize(dynJson[2]));
            Assert.Equal("null", JsonSerializer.Serialize(dynJson[3]));
        }

        private static void AssertDynamic(DJson dynJson)
        {
            dynamic dyn = dynJson;

            Assert.Equal(3, dyn.CountMembers);
            Assert.Null(dyn[3]);
            var gradeObj = dyn[2];
            Assert.NotNull(gradeObj);
            Assert.Null(gradeObj.Dontknow);

            var grades = gradeObj.grades;
            Assert.NotNull(grades);
            Assert.Equal(90, grades[0]);
            Assert.Equal(80, grades[1]);
            Assert.Equal(100, grades[2]);
            Assert.Equal(75, grades[3]);

            Assert.Equal("[{\"name\":\"John\"},[\"425-000-1212\",15],{\"grades\":[90,80,100,75]}]", JsonSerializer.Serialize(dyn));
            Assert.Equal("{\"name\":\"John\"}", JsonSerializer.Serialize(dyn[0]));
            Assert.Equal("[\"425-000-1212\",15]", JsonSerializer.Serialize(dyn[1]));
            Assert.Equal("{\"grades\":[90,80,100,75]}", JsonSerializer.Serialize(dyn[2]));
            Assert.Equal("null", JsonSerializer.Serialize((object)dyn[3]));
        }
    }
}
