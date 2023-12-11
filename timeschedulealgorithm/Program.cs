namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.Text;

    internal class Program
    {
        static void Main()
        {
            var DUMMY_DATA = new Dictionary<string, List<int>>
        {
            { "fhi", Enumerable.Range(1, 20).ToList() },
            { "dnb", Enumerable.Range(1, 20).ToList() },
            { "storebrand", Enumerable.Range(1, 22).ToList() },
            { "storebrand2", Enumerable.Range(1, 23).ToList() },
            { "storebrand3", Enumerable.Range(1, 24).ToList() },
            { "storebrand4", Enumerable.Range(1, 20).ToList() },
          //  { "java1", Enumerable.Range(21, 20).ToList() },
            { "storebrand5", Enumerable.Range(1, 20).ToList() },
         //            { "java3", Enumerable.Range(21, 20).ToList() },
            { "dnb2", new List<int> { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 25,30 } },

            { "storebrand6", Enumerable.Range(1, 20).ToList() },
            { "storebrand7", Enumerable.Range(1, 20).ToList() },
            { "storebrand8", Enumerable.Range(3, 21).ToList() },
          //  { "java2", Enumerable.Range(21, 20).ToList() },
            { "storebrand9", Enumerable.Range(1, 20).ToList() },
            { "storebrand10", Enumerable.Range(1, 20).ToList() },
          //  { "java5", Enumerable.Range(21, 20).ToList() },
            { "storebrand11", Enumerable.Range(1, 20).ToList() },
            { "storebrand12", Enumerable.Range(1, 20).ToList() },
            { "storebrand13", Enumerable.Range(1, 20).ToList() },              //to many keys
            { "storebrand14", Enumerable.Range(1, 20).ToList() },             //to many keys
          //  { "java6", Enumerable.Range(21, 20).ToList() },
          //  { "java7", Enumerable.Range(21, 20).ToList() },
            //to many keys
            { "storebrand15", Enumerable.Range(1, 20).ToList() },
            { "obos2", Enumerable.Range(1, 20).ToList() },
           { "obos", Enumerable.Range(1, 20).ToList() },
            //  { "java8", Enumerable.Range(21, 20).ToList() },
              //          { "java9", Enumerable.Range(21, 20).ToList() },
                //        { "java10", Enumerable.Range(21, 20).ToList() },
               //         { "java11", Enumerable.Range(21, 20).ToList() },
                //        { "java12", Enumerable.Range(21, 20).ToList() }
        };


            // Calculate the maximum possible combination of keys and values
            //int maxCombination = DUMMY_DATA.Values.Max(list => list.Count) * DUMMY_DATA.Count;

            // Determine the shift value based on the maximum combination
            //int shift = maxCombination >= 3 ? 3 : (maxCombination == 2 ? 2 : 1);
            int shift = 1;

            var sortedList = new Dictionary<string, List<int>>();

            // Populate the first list as is
            var firstList = DUMMY_DATA.Values.First();
            sortedList.Add(DUMMY_DATA.Keys.First(), firstList);

            int[][] meetings = new int[DUMMY_DATA.Count][];
            //Looping through all clients
            for (int curr_client = 0; curr_client < DUMMY_DATA.Count; curr_client++)
            {
                meetings[curr_client] = DUMMY_DATA.Values.ElementAt(curr_client).ToArray();

                //Variables below is used to try and solve timeline conflicts
                int prev = -1;
                int firstConflict = -1;
                int numberOfConflicsOnPos = 0;
                int numberOfConflicsOnCli = 0;
                int conflictPos = -1;
                //looping through all candidates current client shall meet
                for (int meetingPos = 0; meetingPos < meetings[curr_client].Count(); meetingPos++)
                {

                    if (prev != meetingPos)
                    {
                        firstConflict = -1;

                    }
                    prev = meetingPos;
                    //Check if current candiate have any other meetings at this time
                    //looping through all other clients with a set timeschedule
                    for (int comparedClient = 0; comparedClient < curr_client; comparedClient++)
                    {
                        //Console.WriteLine($"j: {j}, i: {i}, k: {k}, firstConflict: {firstConflict}");
                        if (meetingPos < meetings[comparedClient].Count())
                        {
                            //Console.WriteLine(String.Join(',', meetings[i]));
                            if (meetings[comparedClient][meetingPos] == meetings[curr_client][meetingPos])
                            {
                                if (firstConflict == -1)
                                {
                                    // Console.WriteLine("Test2");
                                    firstConflict = meetings[curr_client][meetingPos];
                                    meetings[curr_client] = ShiftCandidates(meetings[curr_client], meetingPos);

                                    //set k to one less, so that it does a new check for the same position on new candidate and break loop of j (other clients schedules)
                                    meetingPos--;
                                    break;

                                }
                                else if (firstConflict == meetings[curr_client][meetingPos])
                                {

                                    if (numberOfConflicsOnCli > 1000)
                                    {
                                        Console.WriteLine($"More than 1000 attempts on client nr {curr_client} when creating schedule, determined to be unresolved, need manual solution by admin");
                                        meetingPos = meetings[curr_client].Count();
                                        break;
                                    }
                                    if (conflictPos != meetingPos)
                                    {
                                        numberOfConflicsOnPos = 0;
                                        conflictPos = meetingPos;
                                    }
                                    numberOfConflicsOnCli++;

                                    meetings[curr_client] = ShiftCandidates(meetings[curr_client], 0);

                                    numberOfConflicsOnPos++;
                                    meetingPos = -1;

                                    break;
                                }
                                else
                                {
                                    meetings[curr_client] = ShiftCandidates(meetings[curr_client], meetingPos);

                                    //set k to one less, so that it does a new check for the same position on new candidate and break loop of j (other clients schedules)
                                    meetingPos--;
                                    break;

                                }
                            }
                        }
                    }
                }
            }
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < meetings.Length; i++)
            {
                //sb.AppendLine(meetings[i].Count().ToString());
                sb.AppendLine(String.Join(',', meetings[i]));
            }
            Console.WriteLine(sb.ToString());
        }

        static int[] ShiftCandidates(int[] candidates, int position)
        {
            int tempCand = candidates[position];
            for (int i = position; i < candidates.Count() - 1; i++)
            {
                candidates[i] = candidates[i + 1];
            }
            candidates[candidates.Count() - 1] = tempCand;
            return candidates;
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
}