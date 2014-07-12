using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bullsAndCows
{
    
    public delegate void TopScoresDelegate(Game game, ScoreBoard board);
    class Program
    {
        private const int maximumNumberOfRecords = 5;
        private static void DoTopScores(Game game, ScoreBoard board)
        {
            if (game.score != -1 && game.score < board.board[maximumNumberOfRecords - 1].Score)
            {
                Console.Write("TOP SCORE! Please enter your name:");
                string name = Console.ReadLine();

                List<Record> list = new List<Record>(board.board);
                list.Add(new Record(name, game.score));
                list.Sort();
                for (int i = 0; i < maximumNumberOfRecords; i++)
                    board.board[i] = list[i];
            }
        }

        static void Main()
        {
            ScoreBoard board = new ScoreBoard();
            
            while (new Game(board, DoTopScores).Run())
            {
            }
        }
    }
}
