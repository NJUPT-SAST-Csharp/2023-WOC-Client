using Markdig;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Utils;

namespace SastWiki.WPF.Services;

public class MarkdownProcessor : IMarkdownProcessor
{
    private readonly MarkdownPipeline _pipeline;
    private readonly string HTMLTemplate =
        "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><style>{0}</style></head><body class=\"markdown-body\" margin=\"20\">{1}</body></html>";

    public string CSSStyle { get; set; }

    public MarkdownProcessor(MarkdownCSSProvider cssProvider)
    {
        _pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        CSSStyle = cssProvider.CSS;
    }

    public void Output(string input, out string html, out IEnumerable<int> images)
    {
        Markdig.Syntax.MarkdownDocument document = Markdown.Parse(input, _pipeline);

        // html output
        html = string.Format(HTMLTemplate, CSSStyle, document.ToHtml());

        images = []; // No longer needed
    }
}
