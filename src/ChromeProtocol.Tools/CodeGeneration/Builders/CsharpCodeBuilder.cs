namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public abstract class CsharpCodeBuilder<TNode>
  where TNode : new()
{
  public TNode Node { get; private set; } = new TNode();

  public virtual TNode Build() => Node;
}
