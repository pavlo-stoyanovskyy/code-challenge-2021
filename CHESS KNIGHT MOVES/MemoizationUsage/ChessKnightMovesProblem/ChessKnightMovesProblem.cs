using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ChessKnightMovesProblem
{
    // Time O(m * n * steps * max_vowels) but we can say O(m*n) since steps = 8 and max_vowels = 2
    // Space O(n * m * steps + steps + max_vowels) but we can say O(n * m) since steps = 8 and max_vowels = 2
    // Used techniques: Recursion, Memoization
    public class ChessKnightMovesProblem
    {
        private readonly char?[,] _matrix;
        private readonly bool _useMemoization;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private char[] _vowels = new char[] { 'A', 'E', 'I', 'O', 'U', 'Y' };

        private Dictionary<string, long> _memo;

        public ChessKnightMovesProblem(char?[,] matrix, bool useMemoization)
        {
            _matrix = matrix;
            _useMemoization = useMemoization;
        }

        public (long Result, long ElapsedTickets) SolveMatrix() 
        {
            if (_useMemoization) 
            {
                _memo = new Dictionary<string, long>();
            }

            _stopwatch.Start();

            long total = 0;

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (_matrix[i, j] != null)
                    {
                        total += GetNumberOfPathes(i, j, 7, IsVowel((i, j)) ? 1 : 0);
                    }
                }
            }

            _stopwatch.Stop();

            var elapsedTicks = _stopwatch.ElapsedTicks;

            _stopwatch.Reset();

            if (_useMemoization)
            {
                _memo = null;
            }

            return (Result: total, ElapsedTickets: elapsedTicks);
        }

        private long GetNumberOfPathes(int x, int y, int step, int numberOfVowels)
        {
            if (numberOfVowels == 3)
            {
                return 0; //return with 0 since it is invalid path
            }

            if (step == 0)
            {
                return 1; //return with 1 since path is checked successfully
            }

            var total = (long)0;

            var memoKey = $"{x}_{y}_{step}_{numberOfVowels}";

            if (_useMemoization && _memo.ContainsKey(memoKey)) 
            {
                return _memo[memoKey];
            }

            foreach (var position in GetPossibleMoves((x, y)))
            {
                total += GetNumberOfPathes(position.x, position.y, step - 1, IsVowel(position) ? numberOfVowels + 1 : numberOfVowels);
            }

            if (_useMemoization)
            {
                _memo[memoKey] = total;
            }

            return total;
        }

        //To calculate possible moves
        private IEnumerable<(int x, int y)> GetPossibleMoves((int x, int y) position)
        {
            // All possible moves of a knight 
            int[] x = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] y = { 1, 2, 2, 1, -1, -2, -2, -1 };

            var moves = new List<(int x, int y)>();

            // Check if each possible move is valid or not 
            for (int i = 0; i < 8; i++)
            {
                // Position of knight after move 
                int targetX = position.x + x[i];
                int targetY = position.y + y[i];

                // Check valid moves 
                if (targetX >= 0 && targetY >= 0 && targetX < _matrix.GetLength(1) && targetY < _matrix.GetLength(0) && _matrix[targetX, targetY] != null)
                {
                    moves.Add((x: targetX, y: targetY));
                }
            }

            // Return all possible moves 
            return moves;
        }

        private bool IsVowel((int row, int col) cell)
        {
            var data = _matrix[cell.row, cell.col];
            return data.HasValue
                ? _vowels.Contains(data.Value)
                : false;
        }
    }
}
