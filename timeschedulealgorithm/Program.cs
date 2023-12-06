using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var test = new Dictionary<string, List<int>>
        {
            { "fhi", Enumerable.Range(1, 20).ToList() },
            { "dnb", Enumerable.Range(1, 20).ToList() },
            { "storebrand", Enumerable.Range(1, 20).ToList() },
            { "obos", Enumerable.Range(1, 20).ToList() }
        };

        var test3 = new Dictionary<string, List<int>>();

        // Populate the first list as is
        var firstList = test.Values.First();
        test3.Add(test.Keys.First(), firstList);

        for (int i = 1; i < test.Count; i++)
        {
            var previousList = test3[test.Keys.ElementAt(i - 1)];
            var currentList = test[test.Keys.ElementAt(i)];
            var newList = new List<int>();

            for (int j = 0; j < currentList.Count; j++)
            {
                int shiftedIndex = previousList.IndexOf(currentList[j]) + 2;
                if (shiftedIndex >= currentList.Count)
                {
                    shiftedIndex -= currentList.Count;
                }

                newList.Add(currentList[shiftedIndex]);
            }

            test3.Add(test.Keys.ElementAt(i), newList);
        }

        // Print the test3 dictionary
        Console.WriteLine(string.Join(Environment.NewLine, test3.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }
}
