using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Xunit;

namespace ChromeProtocol.Core.Tests;

[JsonConverter(typeof(ObjectTypeConverter))]
internal record TestableObjectType(IReadOnlyDictionary<string, JsonNode?> Properties) : IObjectType;

public sealed class ObjectTypeConverterTests
{
  [Fact]
  public void Should_deserialize_using_attribute_properly()
  {
    // Arrange
    var expectedJson = "{ \"foo\": 1, \"bar\": 2 }";

    // Act
    var actual = JsonSerializer.Deserialize<TestableObjectType>(expectedJson) ?? new TestableObjectType(new Dictionary<string, JsonNode?>());

    // Assert
    Assert.Collection(
      actual.Properties,
      pair =>
      {
        Assert.Equal("foo", pair.Key);
        Assert.Equal(expected: 1, pair.Value?.GetValue<int>());
      },
      pair =>
      {
        Assert.Equal("bar", pair.Key);
        Assert.Equal(expected: 2, pair.Value?.GetValue<int>());
      });
  }

  [Fact]
  public void Should_serialize_using_attribute_properly()
  {
    var expected = new TestableObjectType(new Dictionary<string, JsonNode?>
    {
      { "foo", JsonValue.Create(1) },
      { "bar", JsonValue.Create(2) }
    });

    var actual = JsonSerializer.Serialize(expected);

    Assert.Equal("{\"foo\":1,\"bar\":2}", actual);
  }
}
