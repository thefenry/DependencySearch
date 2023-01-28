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
                var matchingNeighboringNode = mainNodes.FirstOrDefault(x => x.Name.EndsWith(neighborDependency));
                if (matchingNeighboringNode == null)
                {
                    matchingNeighboringNode = new Node(neighborDependency);
                    mainNodes.Add(matchingNeighboringNode);
                }

                if (matchingNode != null)
                {
                    if (matchingNode.Dependencies.Any(x => x.Name.Equals(neighborDependency))) { continue; }

                    matchingNode.Dependencies.Add(new Node(neighborDependency));
                }
                else
                {
                    var newNode = new Node(item);
                    newNode.Dependencies.Add(new Node(neighborDependency));
                    mainNodes.Add(newNode);
                }

            }

            return mainNodes;
        }
    }
}
