namespace BullsAndCowsCommandPattern
{
    using System;

    public class ConsoleRenderer
    {
        private const ConsoleColor WelcomeMessageColor = ConsoleColor.Yellow;
        private const ConsoleColor UserTypeColor = ConsoleColor.White;
        private const ConsoleColor CongratMessageColor = ConsoleColor.Green;

        public ConsoleRenderer(int fieldWidth, int fieldHeight)
        {
            Console.BufferHeight = fieldHeight;
            Console.BufferWidth = fieldWidth;
        }
        public void PrintGameStartMessage()
        {
            Console.ForegroundColor = ConsoleRenderer.WelcomeMessageColor;
            Console.WriteLine("Welcome to “BULLS AND COWS”!");
            Console.WriteLine("Please try to guess my secret 4-digit number.");
            Console.WriteLine("Allowed Commands:");
            Console.WriteLine("'restart' - to reset the game;");
            Console.WriteLine("'help' - to reveal a digit(cheat);");
            Console.WriteLine("'top' - view top scores;");
            Console.WriteLine("'exit' - to exit the game;");
            Console.WriteLine("a 4 digit number.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
        }

        public void PrintTopScores(ScoreBoard board)
        {
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
            Console.WriteLine();
            Console.WriteLine(board.ToString());
            Console.WriteLine();
        }

        public void PrintCongratulationMessage(int usedCheats, int madeGuesses)
        {
            Console.ForegroundColor = ConsoleRenderer.CongratMessageColor;
            Console.WriteLine();
            Console.Write("CONGRATULATIONS! You guessed" + " the secret number in {0} attempts.", madeGuesses);
            if (usedCheats > 0)
            {
                Console.Write(" But you used {0} cheats!", usedCheats);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
        }

        public void PrintHelpingNumber(char[] helpingNumber)
        {
            Console.WriteLine();
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
