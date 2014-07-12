using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bullsAndCows
{
    public class ScoreBoard
    {
        private const int boardLength = 5;
        internal Record[] board = new Record[boardLength];

        public ScoreBoard() {
            for (int i = 0; i < boardLength; i++)
                board[i] = new Record("Unknown", int.MaxValue);
        }
        public void Output()
        {
            Console.WriteLine("----Scoreboard----");
            for (int i = 0; i < boardLength; i++)
            {
                 Console.WriteLine(i+1 +".(" + board[i].Score + ")" + board[i].Name);
            }
            Console.WriteLine("------------------");
        }
    }
}
