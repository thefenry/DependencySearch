namespace DependencySearch.Models
{
    public class Node
    {
        public Node(string itemName)
        {
            ItemName = itemName;
            Dependencies = new List<Node>();
        }

        public string ItemName { get; set; }

        public List<Node> Dependencies { get; set; }
    }
}
