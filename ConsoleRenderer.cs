namespace BullsAndCowsCommandPattern
{
    using System;

    public class ConsoleRenderer
    {
        /*TO DO: class that prints on console
          To extract print methods from engine here*/
       

        public void PrintGameStartMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to “BULLS AND COWS”!");
            Console.WriteLine("Please try to guess my secret 4-digit number.");
            Console.WriteLine("Allowed Commands:");
            Console.WriteLine("'restart' - to reset the game;");
            Console.WriteLine("'help' - to reveal a digit(cheat);");
            Console.WriteLine("'top' - view top scores;");
            Console.WriteLine("'exit' - to exit the game;");
            Console.WriteLine("a 4 digit number.");
            Console.WriteLine();
        }

        public void PrintTopScores(ScoreBoard board)
        {
            Console.WriteLine();
            Console.WriteLine(board.ToString());
            Console.WriteLine();
        }

        public void PrintCongratulationMessage(int usedCheats, int madeGuesses)
        {
            Console.WriteLine();
            Console.Write("CONGRATULATIONS! You guessed" + " the secret number in {0} attempts.", madeGuesses);
            if (usedCheats > 0)
            {
                Console.Write(" But you used {0} cheats!", usedCheats);
            }

            Console.WriteLine();
        }

        public void PrintHelpingNumber(char[] helpingNumber)
        {
            Console.Write("The number looks like ");
            foreach (char ch in helpingNumber)
            {
                Console.Write(ch);
            }

            Console.Write(".");
            Console.WriteLine();
        }

        public void PrintBullsAndCows(int bullsCount, int cowsCount)
        {
            Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}!", bullsCount, cowsCount);
            Console.WriteLine();
        }
    }
}
