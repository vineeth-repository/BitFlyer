using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quiz
{
    public class Transaction
    {
        public long Id { get; set; }
        public long Size { get; set; }
        public decimal Fee { get; set; }

        /// <summary>
        /// Helper function to load transactions from a tab seperated text file.
        /// </summary>
        /// <param name="path">Path to the input file.</param>
        /// <returns>Enumerable of transaction.</returns>
        public static IEnumerable<Transaction> Load(string path)
        {
            return File.ReadLines(path)
                .Skip(1)
                .Select
                (
                    row =>
                    {
                        var cells = row.Split('\t');
                        return new Transaction
                        {
                            Id = long.Parse(cells[0]),
                            Size = long.Parse(cells[1]),
                            Fee = decimal.Parse(cells[2]),
                        };
                    }
                );
        }

        /// <summary>
        /// This method calculates the maximum reward
        /// </summary>
        /// <param name="transactions">Collection of transaction.</param>
        /// <param name="length">Maximum size of a block in bytes.</param>
        /// <returns>Maximumm reward in a block of transactions.</returns>
        public static decimal GetMaximumReward(IEnumerable<Transaction> transactions, long length)
        {
            var fee = 0M;
            var size = 0L;
            var sorted = transactions.OrderByDescending(transaction => transaction.Fee);

            foreach (var transaction in sorted)
            {
                if (size + transaction.Size <= length)
                {
                    size += transaction.Size;
                    fee += transaction.Fee;
                }
            }

            var reward = 12.5M + fee;
            return reward;
        }
    }
}
