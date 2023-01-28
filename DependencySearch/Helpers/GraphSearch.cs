using DependencySearch.Models;

namespace DependencySearch.Helpers
{
    public class GraphSearch
    {
        /// <summary>
        /// Organizes the list of nodes into a grouped list based on their dependencies
        /// so we can use it later for processing
        /// </summary>
        /// <param name="taskNodes"></param>
        /// <param name="groupedLists"></param>
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
        
        /// <summary>
        /// Adds nodes without dependencies to the currently being done tasks. 
        /// </summary>
        /// <param name="taskNodes"></param>
        /// <param name="currentlyProcessing"></param>
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

        /// <summary>
        /// If the processed Items exist as a dependency to other nodes, we can remove it.
        /// This is equivalent as marking it as done on the tasks
        /// </summary>
        /// <param name="taskNodes"></param>
        /// <param name="currentlyProcessing"></param>
        private static void UpdateProcessedDependencies(List<Node> taskNodes, IReadOnlyCollection<Node> currentlyProcessing)
        {
            foreach (var taskNode in taskNodes)
            {
                for (var index = taskNode.Dependencies.Count - 1; index >= 0; index--)
                {
                    var dependency = taskNode.Dependencies[index];
                    if (currentlyProcessing.All(x => x.ItemName != dependency.ItemName)) {continue;}
                    
                    taskNode.Dependencies.RemoveAt(index);
                }
            }
        }
    }
}
