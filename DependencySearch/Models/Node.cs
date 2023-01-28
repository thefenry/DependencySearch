namespace DependencySearch.Models
{
    public class Node
    {
        public Node(string name)
        {
            Name = name;
            Dependencies = new List<Node>();
        }

        public string Name { get; set; }

        public List<Node> Dependencies { get; set; }
    }
}
