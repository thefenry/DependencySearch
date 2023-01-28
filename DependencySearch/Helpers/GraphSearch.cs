using DependencySearch.Models;

namespace DependencySearch.Helpers
{
    public class GraphSearch
    {
        public static void SearchNodes(List<Node> taskNodes, ICollection<List<Node>> groupedLists)
        {
            var currentlyProcessing = new List<Node>();
            for (var index = taskNodes.Count - 1; index >= 0; index--)
            {
                var taskNode = taskNodes[index];
                if (taskNode.Dependencies.Count > 0)
                {
                    continue;
                }

                currentlyProcessing.Add(taskNode);
                taskNodes.RemoveAt(index);
            }

            foreach (var taskNode in taskNodes)
            {
                for (var index = taskNode.Dependencies.Count - 1; index >= 0; index--)
                {
                    var dependency = taskNode.Dependencies[index];
                    if (currentlyProcessing.Any(x => x.Name == dependency.Name))
                    {
                        taskNode.Dependencies.RemoveAt(index);
                    }
                }
            }

            if (currentlyProcessing.Count > 0)
            {
                groupedLists.Add(currentlyProcessing);
            }

            if (taskNodes.Count > 0)
            {
                SearchNodes(taskNodes, groupedLists);
            }
        }
    }
}
