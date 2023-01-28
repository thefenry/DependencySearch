﻿using DependencySearch.Helpers;
using DependencySearch.Models;
using MethodTimer;

namespace DependencySearch
{
    //Expected Outcome
    //left sock, right sock, t-shirt
    //dress shirt 
    //pants, tie
    //belt
    //suit jacket
    //left shoe, right shoe, sun glasses
    //overcoat

    internal class Program
    {
        static void Main()
        {
            // dependency    //item
            var input = new[,] {
                {"t-shirt", "dress shirt"},
                {"dress shirt", "pants"},
                {"dress shirt", "suit jacket"},
                {"tie", "suit jacket"},
                {"pants", "suit jacket"},
                {"belt", "suit jacket"},
                {"suit jacket", "overcoat"},
                {"dress shirt", "tie"},
                {"suit jacket", "sun glasses"},
                {"sun glasses", "overcoat"},
                {"left sock", "pants"},
                {"pants", "belt"},
                {"suit jacket", "left shoe"},
                {"suit jacket", "right shoe"},
                {"left shoe", "overcoat"},
                {"right sock", "pants"},
                {"right shoe", "overcoat"},
                {"t-shirt", "suit jacket"}};


            ListOrderOfItemsToWear(input);

            Console.ReadLine();
        }
        
        [Time]
        private static void ListOrderOfItemsToWear(string[,] input)
        {
            var tasksNodeGraph = GraphGeneration.GenerateTasksNodesGraph(input);

            List<List<Node>> groupedTasksDependency = new();
            GraphSearch.OrganizeTaskNodesByDependencies(tasksNodeGraph, groupedTasksDependency);

            PrintOrderedDependencies(groupedTasksDependency);
        }

        private static void PrintOrderedDependencies(List<List<Node>> groupedTasksDependency)
        {
            foreach (var groupedList in groupedTasksDependency)
            {
                Console.WriteLine(string.Join(',', groupedList.Select(x => x.Name).OrderBy(x => x)));
            }
        }
    }
}