using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Xunit;

namespace ChromeProtocol.Core.Tests;

[JsonConverter(typeof(ArrayTypeConverter))]
public record TestableArrayType(IReadOnlyCollection<JsonNode> Items) : IArrayType;

public sealed class ArrayTypeConverterTests
{
  [Fact]
  public void Should_deserialize_using_attribute_properly()
  {
    // Arrange & Act
    var actual = JsonSerializer.Deserialize<TestableArrayType>("[1,2,3]") ?? new TestableArrayType(Array.Empty<JsonNode>());

    // Assert
    Assert.Collection(
      actual.Items,
      jsonNode => Assert.Equal(expected: 1, jsonNode.GetValue<int>()),
      jsonNode => Assert.Equal(expected: 2, jsonNode.GetValue<int>()),
      jsonNode => Assert.Equal(expected: 3, jsonNode.GetValue<int>()));
  }

  [Fact]
  public void Should_serialize_using_attribute_properly()
  {
    // Arrange
    var expected = new TestableArrayType(new JsonNode[]
    {
      JsonValue.Create(1),
      JsonValue.Create(2),
      JsonValue.Create(3),
    });

    // Act
    var actual = JsonSerializer.Serialize(expected);

    // Assert
    Assert.Equal("[1,2,3]", actual);
  }
}
