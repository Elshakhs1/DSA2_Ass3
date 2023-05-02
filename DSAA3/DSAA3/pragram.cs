using System;
using System.Collections.Generic;

namespace DSAA3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<decimal> denominations = new List<decimal>
            {
                0.01M, 0.02M, 0.05M, 0.1M, 0.2M, 0.5M, 1.0M, 2.0M,
            };
            
            Console.Write("Please write how much you want. The value should be between 0.01 and 5.00: ");
            string input = Console.ReadLine();

            decimal amount;
            while (!decimal.TryParse(input, out amount) || amount <= 0 || amount > 5)
            {
                Console.Write("Incorrect input. The value must be a number between 0.01 and 5.00: ");
                input = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine($"Possible combinations of {amount} are:");
            var combinations = GetCombinations(amount, denominations);
            DisplayCombinations(combinations);

            Console.WriteLine();
            Console.WriteLine("Lowest number of coins:");
            var lowest = GetLowestCoins(amount, denominations);
            DisplayCoins(lowest);

            Console.ReadLine();
        }

        static List<List<decimal>> GetCombinations(decimal amount, List<decimal> denominations)
        {
            var result = new List<List<decimal>>();
            GetCombinationsHelper(amount, denominations, new List<decimal>(), result);
            return result;
        }

        static void GetCombinationsHelper(decimal amount, List<decimal> denominations, List<decimal> current, List<List<decimal>> result)
        {
            if (amount == 0)
            {
                result.Add(new List<decimal>(current));
                return;
            }

            foreach (var d in denominations)
            {
                if (d <= amount)
                {
                    current.Add(d);
                    GetCombinationsHelper(amount - d, denominations, current, result);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }

        static List<decimal> GetLowestCoins(decimal amount, List<decimal> denominations)
        {
            var result = new List<decimal>();
            foreach (var d in denominations)
            {
                while (amount >= d)
                {
                    result.Add(d);
                    amount -= d;
                }
            }
            return result;
        }

        static void DisplayCombinations(List<List<decimal>> combinations)
        {
            foreach (var c in combinations)
            {
                Console.WriteLine(string.Join(", ", c));
            }
        }

        static void DisplayCoins(List<decimal> coins)
        {
            Console.WriteLine(string.Join(", ", coins));
        }
    }
}
