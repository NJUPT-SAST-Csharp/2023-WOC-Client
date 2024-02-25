namespace SastWiki.WPF.Models;

public class TreeNode
{
    public string NodeID { get; set; }
    public string ParentID { get; set; }
    public string NodeName { get; set; }
    public List<TreeNode> ChildNodes { get; set; }
    public NodeType Type { get; set; }

    public TreeNode() => ChildNodes = [];
}

public enum NodeType
{
    Category,
    Entry
}
