namespace SastWiki.WPF.Contracts;

public interface IMarkdownProcessor
{
    public string CSSStyle { get; set; }

    /// <summary>
    /// 生成markdown对应的HTML与图片ID列表
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public void Output(string input, out string markdown, out IEnumerable<int> images);
}
