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

        var test2 = new Dictionary<string, List<int>>();

        foreach (var entry in test)
        {
            var newList = new List<int>();
            var seenNumbers = new HashSet<int>();

            foreach (var number in entry.Value)
            {
                if (test2.Any(e => e.Value.Contains(number)))
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

            test2.Add(entry.Key, newList);
        }

        // Shift the "dnb" list in test2 by two positions
        var dnbList = test2["dnb"];
        test2["dnb"] = dnbList.Skip(2).Concat(dnbList.Take(2)).ToList();

        // Print the test2 dictionary
        Console.WriteLine(string.Join(Environment.NewLine, test2.Select(kv => $"{kv.Key}: {string.Join(", ", kv.Value)}")));
    }
}
