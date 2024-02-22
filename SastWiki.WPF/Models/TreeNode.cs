using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.Models
{
    public class TreeNode
    {
        public string NodeID { get; set; }
        public string ParentID { get; set; }
        public string NodeName { get; set; }
        public List<TreeNode> ChildNodes { get; set; }
        public NodeType Type { get; set; }

        public TreeNode()
        {
            ChildNodes = new List<TreeNode>();
        }
    }

    public enum NodeType
    {
        Category,
        Entry
    }
}
