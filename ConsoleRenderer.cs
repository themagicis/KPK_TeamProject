namespace BullsAndCowsCommandPattern
{
    using System;

    public class ConsoleRenderer
    {
        private const ConsoleColor LogoMessageColor = ConsoleColor.Cyan;
        private const ConsoleColor UserTypeColor = ConsoleColor.White;
        private const ConsoleColor CongratMessageColor = ConsoleColor.Green;
        private const ConsoleColor TopScoresColor = ConsoleColor.DarkMagenta;
        private const ConsoleColor ComputerMessageColor = ConsoleColor.DarkCyan;
        private const ConsoleColor ErrorMessageColor = ConsoleColor.Red;

        public ConsoleRenderer(int fieldWidth, int fieldHeight)
        {
            Console.WindowHeight = fieldHeight;
            Console.WindowWidth = fieldWidth;
        }

        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleRenderer.ErrorMessageColor;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
        }

        public void PrintGameStartMessage()
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
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
            Console.ForegroundColor = ConsoleRenderer.TopScoresColor;
            Console.WriteLine();
            Console.WriteLine(board.ToString());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
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
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            Console.Write("The number looks like ");
            foreach (char ch in helpingNumber)
            {
                Console.Write(ch);
            }

            Console.Write(".\n");
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
        }

        public void PrintBullsAndCows(int bullsCount, int cowsCount)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}!", bullsCount, cowsCount);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleRenderer.UserTypeColor;
        }
    }
}
