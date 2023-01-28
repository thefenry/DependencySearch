using System.Reflection;

namespace DependencySearch.Helpers
{
    public static class MethodTimeLogger
    {
        public static void Log(MethodBase methodBase, TimeSpan elapsed, string message)
        {
            Console.WriteLine($"{methodBase.Name} {elapsed}");
        }
    }
}
