using System;
using System.IO;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = new FileInfo("input.txt").FullName;
            var transactions = Transaction.Load(path);
            var reward = Transaction.GetMaximumReward(transactions, 1000000);

            Console.WriteLine($"reward: {reward}");
        }
    }
}
