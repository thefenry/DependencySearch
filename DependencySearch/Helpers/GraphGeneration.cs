using DependencySearch.Models;

namespace DependencySearch.Helpers
{
    public class GraphGeneration
    {
        public static List<Node> GenerateTasksNodesGraph(string[,] dependencies)
        {
            List<Node> mainNodes = new();

            for (var i = 0; i < dependencies.GetLength(0); i++)
            {
                var neighborDependency = dependencies[i, 0];
                var item = dependencies[i, 1];

                var matchingNode = mainNodes.FirstOrDefault(x => x.Name.Equals(item));
                UpdateMainNodeListWithDependentNode(mainNodes, neighborDependency);

                if (matchingNode != null)
                {
                    UpdateDependenciesOnNode(matchingNode, neighborDependency);
                }
                else
                {
                    AddItemToMainNodeListWithItsDependency(item, neighborDependency, mainNodes);
                }
            }

            return mainNodes;
        }

        private static void UpdateDependenciesOnNode(Node matchingNode, string neighborDependency)
        {
            if (matchingNode.Dependencies.Any(x => x.Name.Equals(neighborDependency)))
            {
                return;
            }

            matchingNode.Dependencies.Add(new Node(neighborDependency));
        }

        private static void AddItemToMainNodeListWithItsDependency(string item, string neighborDependency, ICollection<Node> mainNodes)
        {
            var newNode = new Node(item);
            newNode.Dependencies.Add(new Node(neighborDependency));
            mainNodes.Add(newNode);
        }

        private static void UpdateMainNodeListWithDependentNode(ICollection<Node> mainNodes, string neighborDependency)
        {
            var matchingNeighboringNode = mainNodes.FirstOrDefault(x => x.Name.EndsWith(neighborDependency));
            if (matchingNeighboringNode != null) { return; }

            matchingNeighboringNode = new Node(neighborDependency);
            mainNodes.Add(matchingNeighboringNode);
        }
    }
}
