namespace ChromeProtocol.Core;

/// <summary>
/// An interface that represents CDP domain command without typed response.
/// </summary>
public interface ICommand;

/// <summary>
/// An interface that represents CDP domain command with a typed response.
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface ICommand<TResponse> : ICommand
  where TResponse : IType;
