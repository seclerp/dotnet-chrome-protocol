namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public class CsharpMethodParameter
{
  public CsharpTypeInfo Type { get; set; }
  public string Name { get; set; }
  public string? DefaultValue { get; set; }
  public bool IsExtensionMethodTarget { get; set; }
  public ICollection<CsharpAttributeDecl> Attributes { get; set; } = new LinkedList<CsharpAttributeDecl>();
}
