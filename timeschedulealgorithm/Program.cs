using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var DUMMY_DATA = new Dictionary<string, List<int>>
        {
            { "fhi", Enumerable.Range(1, 20).ToList() },
            { "dnb", Enumerable.Range(1, 20).ToList() },
            { "storebrand", Enumerable.Range(1, 22).ToList() },
            { "storebrand2", Enumerable.Range(1, 23).ToList() },
            { "storebrand3", Enumerable.Range(1, 25).ToList() },
            { "storebrand4", Enumerable.Range(1, 20).ToList() },
            { "java1", Enumerable.Range(21, 20).ToList() },

            { "storebrand5", Enumerable.Range(1, 20).ToList() },
                        { "java3", Enumerable.Range(21, 20).ToList() },
{ "dnb2", new List<int> { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 25,30 } },

            { "storebrand6", Enumerable.Range(1, 20).ToList() },
            { "storebrand7", Enumerable.Range(1, 20).ToList() },
            { "storebrand8", Enumerable.Range(3, 21).ToList() },
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
           // { "storebrand15", Enumerable.Range(1, 20).ToList() },   to many keys
           // { "obos2", Enumerable.Range(1, 20).ToList() },
           // { "obos", Enumerable.Range(1, 20).ToList() }
        };

        // Calculate the maximum possible combination of keys and values
        int maxCombination = DUMMY_DATA.Values.Max(list => list.Count) * DUMMY_DATA.Count;

        // Determine the shift value based on the maximum combination
        int shift = maxCombination >= 3 ? 3 : (maxCombination == 2 ? 2 : 1);

        var sortedList = new Dictionary<string, List<int>>();

        // Populate the first list as is
        var firstList = DUMMY_DATA.Values.First();
        sortedList.Add(DUMMY_DATA.Keys.First(), firstList);

        for (int i = 1; i < DUMMY_DATA.Count; i++)
        {
            var currentKey = DUMMY_DATA.Keys.ElementAt(i);
            var currentList = DUMMY_DATA[currentKey];
            var newList = new List<int>();

            for (int j = 0; j < currentList.Count; j++)
            {
                int totalShift = GetTotalShift(DUMMY_DATA, i, j, shift);
                int shiftedIndex = (j + totalShift) % currentList.Count;
                newList.Add(currentList[shiftedIndex]);
            }

            sortedList.Add(currentKey, newList);
        }

        Console.WriteLine(string.Join(Environment.NewLine, sortedList.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }

    static int GetTotalShift(Dictionary<string, List<int>> unsortedList, int currentIndex, int innerIndex, int shift)
    {
        int totalShift = 0;

        for (int k = 0; k < currentIndex; k++)
        {
            var previousList = unsortedList.Values.ElementAt(k);
            var currentValue = unsortedList.Values.ElementAt(currentIndex)[innerIndex];

            int previousIndex = previousList.IndexOf(currentValue);
            totalShift += shift;
        }

        return totalShift;
    }
}
