namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Text;
    using System.Linq;

    public class ConsoleRenderer
    {
        private const ConsoleColor LogoMessageColor = ConsoleColor.DarkYellow;
        private const ConsoleColor CongratMessageColor = ConsoleColor.Green;
        private const ConsoleColor TopScoresColor = ConsoleColor.DarkMagenta;
        private const ConsoleColor ComputerMessageColor = ConsoleColor.DarkCyan;
        private const ConsoleColor ErrorMessageColor = ConsoleColor.Red;

        public ConsoleRenderer(int fieldWidth, int fieldHeight)
        {
            Console.SetWindowSize(fieldWidth, fieldHeight);
            Console.Title = "Bulls and Cows";
        }

        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleRenderer.ErrorMessageColor;
            int startY = Console.CursorTop;
            int startX = Console.WindowWidth - message.Length - 10;

            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public void PrintGameStartMessage()
        {
            this.PrintLogo();
            string[] startMessageParts = new string[]
            {
                "Commands:   XXXX (4 digit number) ",
                "  'restart' - to reset the game;  ",
                "'help' - to reveal a digit(cheat);",
                "     'top' - view top scores;     ",
                "    'exit' - to exit the game;    "
            };

            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;

            int startY = Console.CursorTop + 3;
            int startX = Console.WindowWidth / 2 - startMessageParts[0].Length / 2;
            PrintStringArrayToPosition(startMessageParts, startX, startY);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage("Please try to guess my secret 4-digit number.");
            Console.WriteLine();
            Console.ResetColor();
        }

        public void PrintTopScores(ScoreBoard board)
        {
            Console.ForegroundColor = ConsoleRenderer.TopScoresColor;
            Console.WriteLine();
            Console.WriteLine(board.ToString());
            Console.WriteLine();
            Console.ResetColor();
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
            Console.ResetColor();
        }

        public void PrintHelpingNumber(char[] helpingNumber)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            string message = "The number looks like " + new string(helpingNumber) + '.';
            this.PrintComputerMessage(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        public void PrintBullsAndCows(int bullsCount, int cowsCount)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage(String.Format("Wrong number! Bulls: {0}, Cows: {1}!", 
                bullsCount, cowsCount));
            Console.WriteLine();
            Console.ResetColor();
        }

        private void PrintLogo()
        {
            string[] logoParts = new string[]
            {
                "####  #   # #     #      ####        ####  ###  #     #  ####",
                "#   # #   # #     #     #           #     #   # #     # #    ",
                "####  #   # #     #      ###        #     #   # #  #  #  ### ",
                "#   # #   # #     #         #       #     #   # # # # #     #",
                "####   ###  ##### ##### ####   AND   ####  ###   #   #  ####"
            };
            int startX = Console.WindowWidth / 2 - logoParts[0].Length/2;
            int startY = 2;

            Console.ForegroundColor = ConsoleRenderer.LogoMessageColor;
            PrintStringArrayToPosition(logoParts, startX, startY);
        }

        private void PrintStringArrayToPosition(string[] lines, int x, int y)
        {
            int currY = y;

            for (int i = 0; i < lines.Length; i++, currY++)
            {
                Console.SetCursorPosition(x, currY);
                Console.WriteLine(lines[i]);
            }
        }

        private void PrintComputerMessage(string message)
        {
            int startY = Console.CursorTop;
            int startX = Console.WindowWidth - message.Length - 10;
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(message);
        }
    }
}
