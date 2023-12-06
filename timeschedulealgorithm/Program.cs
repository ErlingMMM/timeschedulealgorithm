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
            var currentKey = test.Keys.ElementAt(i);
            var currentList = test[currentKey];
            var newList = new List<int>();

            for (int j = 0; j < currentList.Count; j++)
            {
                int shiftedIndex = GetShiftedIndex(test, i, j);
                newList.Add(currentList[shiftedIndex]);
            }

            test3.Add(currentKey, newList);
        }

        // Print the test3 dictionary
        Console.WriteLine(string.Join(Environment.NewLine, test3.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }

    static int GetShiftedIndex(Dictionary<string, List<int>> test, int currentIndex, int innerIndex)
    {
        int shiftedIndex = innerIndex;

        for (int k = 0; k < currentIndex; k++)
        {
            var previousList = test.Values.ElementAt(k);
            var currentValue = test.Values.ElementAt(currentIndex)[innerIndex];

            int previousIndex = previousList.IndexOf(currentValue);
            shiftedIndex += 2;

            if (shiftedIndex >= previousList.Count)
            {
                shiftedIndex -= previousList.Count;
            }
        }

        return shiftedIndex;
    }
}
