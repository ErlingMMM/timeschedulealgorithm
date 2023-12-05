using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var test = new Dictionary<string, List<string>>
        {
            { "fhi", new List<string> { "trym", "erling", "tom", "name1", "name2", "name3", "name4", "name5", "name6", "name7", "name8", "name9", "name10", "name11", "name12", "name13", "name14", "name15", "name16", "name17", "name18", "name19", "name20" } },
            { "dnb", new List<string> { "trym", "erling", "tom", "name1", "name2", "name3", "name4", "name5", "name6", "name7", "name8", "name9", "name10", "name11", "name12", "name13", "name14", "name15", "name16", "name17", "name18", "name19", "name20" } }
        };

        var test2 = new Dictionary<string, List<string>>();

        foreach (var entry in test)
        {
            var newList = new List<string>();
            var seenNames = new HashSet<string>();

            foreach (var name in entry.Value)
            {
                if (test2.Any(e => e.Value.Contains(name)))
                {
                    var shiftedName = name;
                    while (seenNames.Contains(shiftedName))
                    {
                        shiftedName += "_shifted";
                    }

                    newList.Add(shiftedName);
                    seenNames.Add(shiftedName);
                }
                else
                {
                    newList.Add(name);
                    seenNames.Add(name);
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
