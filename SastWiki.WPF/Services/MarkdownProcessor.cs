using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig;
using Markdig.Syntax;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Utils;

namespace SastWiki.WPF.Services
{
    public class MarkdownProcessor : IMarkdownProcessor
    {
        private MarkdownPipeline _pipeline;
        private string HTMLTemplate =
            "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><style>{0}</style></head><body class=\"markdown-body\" margin=\"20\">{1}</body></html>";

        public string CSSStyle { get; set; }

        public MarkdownProcessor(MarkdownCSSProvider cssProvider)
        {
            _pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            CSSStyle = cssProvider.CSS;
        }

        public void Output(string input, out string html, out IEnumerable<int> images)
        {
            var document = Markdown.Parse(input, _pipeline);

            // html output
            html = string.Format(HTMLTemplate, CSSStyle, document.ToHtml());

            // image IDs, Unfinished
            images = document
                .Descendants()
                .OfType<Markdig.Syntax.Inlines.LinkInline>()
                .Where(link => link.IsImage)
                .Select(link => link.Url?.Split('-')[1])
                .Where(id => id != null)
                .Select(id => int.Parse(id))
                .ToList();
        }
    }
}
