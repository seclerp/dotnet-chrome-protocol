namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpInheritance
{
    public CsharpInheritance(CsharpTypeInfo type)
    {
        Type = type;
    }

    public CsharpTypeInfo Type { get; }

    public IReadOnlyCollection<CsharpArgument>? Arguments { get; set; }
}