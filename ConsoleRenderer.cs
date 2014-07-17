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

        private TextAnimator animator;
        private int currentRow;

        public ConsoleRenderer(int fieldWidth, int fieldHeight)
        {
            Console.SetWindowSize(fieldWidth, fieldHeight);
            Console.Title = "Bulls and Cows";
            this.currentRow = 0;
        }

        public void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleRenderer.ErrorMessageColor;
            this.PrintComputerMessage(message);
            this.DefaultMessage();
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
            this.currentRow += 2;
            PrintLines(startMessageParts);

            this.currentRow += 2;
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage("Please try to guess my secret 4-digit number.");
            this.DefaultMessage();
        }

        public void PrintTopScores(ScoreBoard board)
        {
            Console.ForegroundColor = ConsoleRenderer.TopScoresColor;
            string scores = board.ToString();
            var scoreString = scores.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            this.PrintLines(scoreString);
            this.DefaultMessage();
        }

        public void PrintCongratulationMessage(int usedCheats, int madeGuesses)
        {
            Console.ForegroundColor = ConsoleRenderer.CongratMessageColor;
            string[] message = new string[] 
            { 
                "CONGRATULATIONS!",
                String.Format("You guessed" + " the secret number in {0} attempts.", madeGuesses)
            };
            this.PrintLines(message);

            if (usedCheats > 0)
            {
                this.PrintLines(String.Format(" But you used {0} cheats!", usedCheats));
            }
        }

        public void PrintHelpingNumber(char[] helpingNumber)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            string message = "The number looks like " + new string(helpingNumber) + '.';
            this.PrintComputerMessage(message);
            this.DefaultMessage();
        }

        public void PrintBullsAndCows(int bullsCount, int cowsCount)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage(String.Format("Wrong number! Bulls: {0}, Cows: {1}!", 
                bullsCount, cowsCount));
            this.DefaultMessage();
        }

        public void PrintSavingScore()
        {
            // TO DO
        }

        public void PrintLines(params string[] lines)
        {
            for (int i = 0; i < lines.Length; i++, this.currentRow++)
            {
                int x = Console.WindowWidth / 2 - lines[i].Length / 2;
                Console.SetCursorPosition(x, this.currentRow);
                Console.WriteLine(lines[i]);
            }
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

            Console.ForegroundColor = ConsoleRenderer.LogoMessageColor;
            this.PrintLines(logoParts);
        }

        private void PrintComputerMessage(string message)
        {
            int startX = Console.WindowWidth - message.Length - 10;
            this.animator = new TextAnimator(startX, this.currentRow, message);
            this.animator.Type(50, true);
            this.currentRow += 2;
        }

        private void DefaultMessage()
        {
            Console.SetCursorPosition(10, this.currentRow);
            Console.ResetColor();
            Console.Write("Enter your guess or command: ");
            this.currentRow += 2;
        }
    }
}
