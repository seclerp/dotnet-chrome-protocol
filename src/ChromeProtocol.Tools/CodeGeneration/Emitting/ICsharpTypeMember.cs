namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public interface ICsharpTypeMember : IEmittable
{
  ICollection<CsharpAttributeDecl> Attributes { get; set; }
  CsharpComment? Comment { get; set; }
}
