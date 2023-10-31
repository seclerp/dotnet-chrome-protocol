using System.Text;
using System.Web;
using ChromeProtocol.Tools.CodeGeneration.Emitting;

namespace ChromeProtocol.Tools.CodeGeneration.Builders;

public class CsharpXmlCommentBuilder : CsharpCodeBuilder<CsharpXmlComment>
{
  private readonly StringBuilder _stringBuilder = new();

  public CsharpXmlCommentBuilder Summary(string summary)
  {
    XmlTag("summary", summary);
    return this;
  }

  public CsharpXmlCommentBuilder Param(string name, string description)
  {
    XmlTag("param", description, ("name", name));
    return this;
  }

  public override CsharpXmlComment Build()
  {
    Node.Text = _stringBuilder.ToString();
    return base.Build();
  }

  private void XmlTag(string tagName,string tagBody, params (string, string)[] attributes)
  {
    _stringBuilder.Append($"<{tagName}");
    if (attributes.Length > 0)
    {
      foreach (var (name, value) in attributes)
      {
        _stringBuilder.Append($" {name}=\"{value}\"");
      }
    }

    var lines = tagBody
      .Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
      .Select(HttpUtility.HtmlEncode)
      .ToList();

    if (lines.Count == 1)
    {
      _stringBuilder.Append('>');
      _stringBuilder.Append(lines[0]);
    }
    else
    {
      _stringBuilder.AppendLine(">");
      foreach (var line in lines)
        _stringBuilder.AppendLine($"{line}<br/>");
    }

    _stringBuilder.AppendLine($"</{tagName}>");
  }
}
