namespace BullsAndCows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BullsAndCowsPlay
    {
        public static void Main()
        {
            ScoreBoard board = new ScoreBoard();
            while (new Game(board, DoTopScores).Run()) 
            {
            }
        }

        private static void DoTopScores(Game game, ScoreBoard board)
        {
            if (game.Score != -1 && game.Score < board.Board[4].Score)
            {
                Console.Write("TOP SCORE! Please enter your name:");
                string name = Console.ReadLine();

                List<Record> listOfRecords = new List<Record>(board.Board);
                listOfRecords.Add(new Record(name, game.Score));
                listOfRecords.Sort();

                for (int i = 0; i < 5; i++)
                {
                    board.Board[i] = listOfRecords[i];
                }
            }
        }
    }
}