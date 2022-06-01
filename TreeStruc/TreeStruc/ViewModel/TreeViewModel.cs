using System.Collections.Generic;
using TreeStruc.Models;

namespace TreeStruc.ViewModel
{
    public class TreeViewModel
    {
        public List<Node> Nodes { get; set; }
        public bool IsFirstElement { get; set; }
    }
}
