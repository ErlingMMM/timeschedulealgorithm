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
            { "storebrand5", Enumerable.Range(1, 20).ToList() },
            { "storebrand6", Enumerable.Range(1, 20).ToList() },
            { "storebrand7", Enumerable.Range(1, 20).ToList() },
            { "storebrand8", Enumerable.Range(1, 20).ToList() },
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
                int shiftedIndex = GetShiftedIndex(test, i, j, shift);
                newList.Add(currentList[shiftedIndex]);
            }

            test3.Add(currentKey, newList);
        }

        // Print the test3 dictionary
        Console.WriteLine(string.Join(Environment.NewLine, test3.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }

    static int GetShiftedIndex(Dictionary<string, List<int>> test, int currentIndex, int innerIndex, int shift)
    {
        int shiftedIndex = innerIndex;

        for (int k = 0; k < currentIndex; k++)
        {
            var previousList = test.Values.ElementAt(k);
            var currentValue = test.Values.ElementAt(currentIndex)[innerIndex];

            int previousIndex = previousList.IndexOf(currentValue);
            shiftedIndex += shift;

            if (shiftedIndex >= previousList.Count)
            {
                shiftedIndex -= previousList.Count;
            }
        }

        return shiftedIndex;
    }
}
