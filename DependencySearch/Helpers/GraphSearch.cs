using DependencySearch.Models;

namespace DependencySearch.Helpers
{
    public class GraphSearch
    {
        public static void OrganizeTaskNodesByDependencies(List<Node> taskNodes, ICollection<List<Node>> groupedLists)
        {
            var currentlyProcessing = new List<Node>();
            GetTasksWithoutDependencies(taskNodes, currentlyProcessing);

            UpdateProcessedDependencies(taskNodes, currentlyProcessing);

            if (currentlyProcessing.Count > 0)
            {
                groupedLists.Add(currentlyProcessing);
            }

            if (taskNodes.Count > 0)
            {
                OrganizeTaskNodesByDependencies(taskNodes, groupedLists);
            }
        }

        private static void GetTasksWithoutDependencies(IList<Node> taskNodes, ICollection<Node> currentlyProcessing)
        {
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
        }

        private static void UpdateProcessedDependencies(List<Node> taskNodes, IReadOnlyCollection<Node> currentlyProcessing)
        {
            foreach (var taskNode in taskNodes)
            {
                for (var index = taskNode.Dependencies.Count - 1; index >= 0; index--)
                {
                    var dependency = taskNode.Dependencies[index];
                    if (currentlyProcessing.All(x => x.Name != dependency.Name)) {continue;}
                    
                    taskNode.Dependencies.RemoveAt(index);
                }
            }
        }
    }
}
