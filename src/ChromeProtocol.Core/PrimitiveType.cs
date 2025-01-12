namespace ChromeProtocol.Core;

/// <summary>
/// An interface that represents a wrapper over CDP primitive (single-value) type.
/// </summary>
public interface IPrimitiveType : IType
{
  /// <summary>
  /// A raw untyped value of the object.
  /// </summary>
  object? RawValue { get; }
}

/// <summary>
/// An interface that represents a typed wrapper over CDP primitive (single-value) type.
/// </summary>
public abstract record PrimitiveType<TValue>(TValue Value) : IPrimitiveType
{
  /// <inheritdoc />
  object? IPrimitiveType.RawValue => Value;
}
