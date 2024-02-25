namespace SastWiki.Core.Models.Dto;

public record EntryDto
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? CategoryName { get; set; }
    public List<string> TagNames { get; set; } = [];

    public override string ToString() =>
        $"EntryDto {{ Id: {Id}, Title: {Title}, Content: {Content}, CategoryName: {CategoryName}, TagNames: {string.Join(",", TagNames.ToArray())} }}";
}
