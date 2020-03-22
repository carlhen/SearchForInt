using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SearchForIntNet5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine(".NET 5:");
            Console.WriteLine();
            SearchForInt();
            Console.WriteLine();
            Console.WriteLine(".NET Standard:");
            Console.WriteLine();
            SearchForIntNetStandard.SearchForInt.Run();
            Console.ReadLine();
        }

        private static void SearchForInt()
        {
            const int maxCount = 100000000;
            const int searchFor = 60000000;
            const int consoleSpacing = 20;
            Func<long, string, string> printTS = (x, y) => $"{y}{new string(' ', consoleSpacing - y.Length)}{x}ms";

            int[] nums = new int[maxCount];
            Random ran = new Random();
            for (int i = 0; i < maxCount; i++)
            {
                nums[i] = i;//;ran.Next(0, 1000);
            }

            Console.WriteLine($"Stopwatch info: Frequency: { Stopwatch.Frequency / Math.Pow(10, 6) }MHz, IsHighPercicion: { Stopwatch.IsHighResolution }");
            Console.WriteLine();

            var stopWatch = Stopwatch.StartNew();

            foreach (int i in nums)
                if (i == searchFor)
                {
                    stopWatch.Stop();
                    break;
                }
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "linear:"));

            stopWatch.Restart();
            int test = nums.First(i => i == searchFor);
            stopWatch.Stop();
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "Linq First:"));

            stopWatch.Restart();
            test = nums.Single(i => i == searchFor);
            stopWatch.Stop();
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "Linq Single:"));

            stopWatch.Restart();
            test = nums.Where(i => i == searchFor).First();
            stopWatch.Stop();
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "Linq Where:"));


            List<int> li = nums.ToList();//make a list of the array
            stopWatch.Restart();
            test = li.Find(item => item == searchFor);
            stopWatch.Stop();
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "List Find:"));

            stopWatch.Restart();
            Predicate<int> found = x => x == searchFor;
            stopWatch.Stop();
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "List Predicate:"));


            stopWatch.Restart();
            test = li.BinarySearch(searchFor);
            stopWatch.Stop();
            Console.WriteLine(printTS(stopWatch.ElapsedMilliseconds, "List BinarySearch: "));
        }

    }
}
