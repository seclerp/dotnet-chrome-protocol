using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpArgumentListBuilder : CsharpCodeBuilder<List<CsharpArgument>>
{
    public CsharpArgumentListBuilder Argument(string name)
    {
        Node.Add(new CsharpArgument { Name = name });

        return this;
    }
}