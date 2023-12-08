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
            { "storebrand2", Enumerable.Range(1, 20).ToList() },
            { "storebrand3", Enumerable.Range(1, 20).ToList() },
            { "storebrand4", Enumerable.Range(1, 20).ToList() },
            { "java1", Enumerable.Range(21, 20).ToList() },

            { "storebrand5", Enumerable.Range(1, 20).ToList() },
                        { "java3", Enumerable.Range(21, 20).ToList() },

            { "storebrand6", Enumerable.Range(1, 20).ToList() },
            { "storebrand7", Enumerable.Range(1, 20).ToList() },
            { "storebrand8", Enumerable.Range(1, 20).ToList() },
            { "java2", Enumerable.Range(21, 20).ToList() },
            { "storebrand9", Enumerable.Range(1, 20).ToList() },
            { "storebrand10", Enumerable.Range(1, 20).ToList() },
            { "java5", Enumerable.Range(21, 20).ToList() },
            { "storebrand11", Enumerable.Range(1, 20).ToList() },
            { "storebrand12", Enumerable.Range(1, 20).ToList() },
            { "storebrand13", Enumerable.Range(1, 20).ToList() },
            { "storebrand14", Enumerable.Range(1, 20).ToList() },
            { "java6", Enumerable.Range(21, 20).ToList() },
            { "java7", Enumerable.Range(21, 20).ToList() },
            { "storebrand15", Enumerable.Range(1, 20).ToList() },
            { "obos", Enumerable.Range(1, 20).ToList() }
        };

        // Calculate the maximum possible combination of keys and values
        int maxCombination = test.Values.Max(list => list.Count) * test.Count;

        // Determine the shift value based on the maximum combination
        int shift = maxCombination >= 3 ? 3 : (maxCombination == 2 ? 2 : 1);

        var test3 = new Dictionary<string, List<int>>();

        // Populate the first list as is
        var firstList = test.Values.First();
        test3.Add(test.Keys.First(), firstList);

        for (int i = 1; i < test.Count; i++)
        {
            var currentKey = test.Keys.ElementAt(i);
            var currentList = test[currentKey];
            var newList = new List<int>();

            for (int j = 0; j < currentList.Count; j++)
            {
                int totalShift = GetTotalShift(test, i, j, shift);
                int shiftedIndex = (j + totalShift) % currentList.Count;
                newList.Add(currentList[shiftedIndex]);
            }

            test3.Add(currentKey, newList);
        }

        // Print the test3 dictionary
        Console.WriteLine(string.Join(Environment.NewLine, test3.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }

    static int GetTotalShift(Dictionary<string, List<int>> test, int currentIndex, int innerIndex, int shift)
    {
        int totalShift = 0;

        for (int k = 0; k < currentIndex; k++)
        {
            var previousList = test.Values.ElementAt(k);
            var currentValue = test.Values.ElementAt(currentIndex)[innerIndex];

            int previousIndex = previousList.IndexOf(currentValue);
            totalShift += shift;
        }

        return totalShift;
    }
}
