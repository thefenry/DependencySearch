using DependencySearch.Models;

namespace DependencySearch.Helpers
{
    public class GraphGeneration
    {
        /// <summary>
        /// Returns a list of task nodes and its dependencies
        /// </summary>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        public static List<Node> GenerateTasksNodesGraph(string[,] dependencies)
        {
            List<Node> mainNodes = new();

            for (var i = 0; i < dependencies.GetLength(0); i++)
            {
                var neighborDependency = dependencies[i, 0];
                var item = dependencies[i, 1];

                var matchingNode = mainNodes.FirstOrDefault(x => x.ItemName.Equals(item));
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

        /// <summary>
        /// Updates the dependencies on a node if it does not exist
        /// </summary>
        /// <param name="matchingNode"></param>
        /// <param name="neighborDependency"></param>
        private static void UpdateDependenciesOnNode(Node matchingNode, string neighborDependency)
        {
            if (matchingNode.Dependencies.Any(x => x.ItemName.Equals(neighborDependency)))
            {
                return;
            }

            matchingNode.Dependencies.Add(new Node(neighborDependency));
        }

        /// <summary>
        /// Adds a task item as a node to the main node list with its dependency
        /// </summary>
        /// <param name="item"></param>
        /// <param name="neighborDependency"></param>
        /// <param name="mainNodes"></param>
        private static void AddItemToMainNodeListWithItsDependency(string item, string neighborDependency, ICollection<Node> mainNodes)
        {
            var newNode = new Node(item);
            newNode.Dependencies.Add(new Node(neighborDependency));
            mainNodes.Add(newNode);
        }

        /// <summary>
        /// Adds the dependency as a node if it has not been added previously. This is used to determine if the node will be a starting node. I.e socks
        /// </summary>
        /// <param name="mainNodes"></param>
        /// <param name="neighborDependency"></param>
        private static void UpdateMainNodeListWithDependentNode(ICollection<Node> mainNodes, string neighborDependency)
        {
            var matchingNeighboringNode = mainNodes.FirstOrDefault(x => x.ItemName.EndsWith(neighborDependency));
            if (matchingNeighboringNode != null) { return; }

            matchingNeighboringNode = new Node(neighborDependency);
            mainNodes.Add(matchingNeighboringNode);
        }
    }
}
