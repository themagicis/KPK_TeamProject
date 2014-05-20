namespace BullsAndCows
{
    using System;
    using System.Linq;

    public class ScoreBoard
    {
        //ToDo: Fix Modifier
        internal Record[] Board = new Record[5];

        public ScoreBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                this.Board[i] = new Record("Unknown", int.MaxValue);
            }
        }

        public void Output()
        {
            Console.WriteLine("----Scoreboard----");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("{0}.({1}){2}", i + 1, this.Board[i].Score, this.Board[i].Name);
            }

            //ToDo: stingbuilder?
            Console.WriteLine("------------------");
        }
    }
}