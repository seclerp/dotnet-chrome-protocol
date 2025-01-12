using ChromeProtocol.Tools.CodeGeneration.Pipeline;
using Json.Schema;

namespace ChromeProtocol.Tools.Logging;

public interface IErrorFormatter
{
  string Format(string fileName, EvaluationResults error);

  string Format(CodeGenerationError error);
}
