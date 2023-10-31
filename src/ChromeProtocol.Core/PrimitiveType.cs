namespace ChromeProtocol.Core;

public interface IPrimitiveType : IType
{
  object? RawValue { get; }
}

public abstract record PrimitiveType<TValue>(TValue Value) : IPrimitiveType
{
  object? IPrimitiveType.RawValue => Value;
}
