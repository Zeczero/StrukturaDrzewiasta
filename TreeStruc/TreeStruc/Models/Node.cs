using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TreeStruc.Models
{
    // NODE DATABASE MODEL
    public class Node
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }
        [DisplayName("Parent")]
        public int? ParentID { get; set; } // nullable
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
    }
}
