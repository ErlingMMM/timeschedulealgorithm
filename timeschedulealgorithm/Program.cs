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

        // Split the original test dictionary into several lists
        var lists = test.Values.SelectMany(list => list.Select((value, index) => new { value, index }))
                              .GroupBy(x => x.index % test.Count)
                              .ToDictionary(g => g.Key.ToString(), g => g.Select(x => x.value).ToList());

        var previousKey = "";
        foreach (var entry in lists)
        {
            var newList = new List<int>();
            var seenNumbers = new HashSet<int>();

            foreach (var number in entry.Value)
            {
                if (test3.Any(e => e.Value.Contains(number) && e.Key != previousKey))
                {
                    var shiftedNumber = number;
                    while (seenNumbers.Contains(shiftedNumber))
                    {
                        shiftedNumber++;
                    }

                    newList.Add(shiftedNumber);
                    seenNumbers.Add(shiftedNumber);
                }
                else
                {
                    newList.Add(number);
                    seenNumbers.Add(number);
                }
            }

            test3.Add(entry.Key, newList);
            previousKey = entry.Key;
        }

        // Print the test3 dictionary
        Console.WriteLine(string.Join(Environment.NewLine, test3.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }
}
