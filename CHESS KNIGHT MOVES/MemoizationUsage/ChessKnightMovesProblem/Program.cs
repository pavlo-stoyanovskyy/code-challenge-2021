using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessKnightMovesProblem
{
    class Program
    {
        const int _numberOfRuns = 100;

        static void Main(string[] args)
        {
            char?[,] _matrix = new char?[5, 5]
            {
                { 'A', 'B', 'C', null, 'E'},
                { null, 'G', 'H', 'I', 'J'},
                { 'K', 'L', 'M', 'N', 'O'},
                { 'P', 'Q', 'R', 'S', 'T'},
                { 'U', 'V', null, null, 'Y'}
            };

            var problem = new ChessKnightMovesProblem(_matrix, false);
            var problemWithMemo = new ChessKnightMovesProblem(_matrix, true);

            var resultList = new List<(long Result, long ElapsedTickets)>();
            var resultWithMemoList = new List<(long Result, long ElapsedTickets)>();

            for (var i = 1; i <= _numberOfRuns; i++) 
            {
                var result = problem.SolveMatrix();
                var resultWithMemo = problemWithMemo.SolveMatrix();

                if (result.Result != resultWithMemo.Result)
                {
                    throw new Exception("Please check your code. The memoization usage is working incorrectly.");
                }

                resultList.Add(result);
                resultWithMemoList.Add(resultWithMemo);
            }

            var elapsedTicketsAvg = resultList.Average(_ => _.ElapsedTickets);
            var elapsedTicketsWithMemoAvg = resultWithMemoList.Average(_ => _.ElapsedTickets);

            Console.WriteLine($"Unique paths of 8 cells: {resultList.First().Result}. " +
                $"The memoization usage cuts the time by { ((elapsedTicketsAvg - elapsedTicketsWithMemoAvg) / elapsedTicketsAvg).ToString("P") }.");
            Console.ReadKey();
        }
    }
}
