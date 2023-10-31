namespace ChromeProtocol.Core;

public interface IType
{
}

public record ExampleValue(string Value) : PrimitiveType<string>(Value);
