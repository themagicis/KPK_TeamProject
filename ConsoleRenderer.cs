// <copyright file="ConsoleRenderer.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object that visualize data from the game engine.
    /// </summary>
    public class ConsoleRenderer
    {
        /// <summary>
        /// The color of the letters for the game logo
        /// </summary>
        private const ConsoleColor LogoMessageColor = ConsoleColor.DarkYellow;

        /// <summary>
        /// The color for the congratulation message
        /// </summary>
        private const ConsoleColor CongratMessageColor = ConsoleColor.Green;

        /// <summary>
        /// The color for the top scores list
        /// </summary>
        private const ConsoleColor TopScoresColor = ConsoleColor.DarkMagenta;

        /// <summary>
        /// The color for the basic computer message
        /// </summary>
        private const ConsoleColor ComputerMessageColor = ConsoleColor.DarkCyan;

        /// <summary>
        /// The color for the error message
        /// </summary>
        private const ConsoleColor ErrorMessageColor = ConsoleColor.Red;

        /// <summary>
        /// Animates the printing on the console.
        /// </summary>
        private TextAnimator animator;

        /// <summary>
        /// Saves the current row from which to begin next printing on the console
        /// </summary>
        private int currentRow;

        /// <summary>
        /// Determine the gap on each side of the console in symbols
        /// </summary>
        private int padding;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRenderer"/> class.
        /// Sets the Console size and title.
        /// </summary>
        /// <param name="fieldWidth">Console width in symbols</param>
        /// <param name="fieldHeight">Console height in symbols</param>
        /// <param name="padding">How many empty symbols on each side</param>
        public ConsoleRenderer(int fieldWidth, int fieldHeight, int padding)
        {
            int width = fieldWidth;
            int height = fieldHeight;
            int maxHeight = Console.LargestWindowHeight;
            int maxWidth = Console.LargestWindowWidth;

            if (maxWidth < fieldWidth)
            {
                width = maxWidth;
            }

            if (maxHeight < fieldHeight)
            {
                height = maxHeight;
            }
            
            Console.SetWindowSize(width, height);
            Console.Title = "Bulls and Cows";
            this.currentRow = 0;
            this.padding = padding;
        }

        /// <summary>
        /// Prints an error message in red color as a computer message(animated). 
        /// </summary>
        /// <param name="message">The error message to be printed.</param>
        public void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleRenderer.ErrorMessageColor;
            this.PrintComputerMessage(message);
            this.DefaultMessage();
        }

        /// <summary>
        /// Prints the starting message of the game. How to play and
        /// welcome message from the computer.
        /// </summary>
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
            this.PrintLines(startMessageParts);

            this.currentRow += 2;
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage("Please try to guess my secret 4-digit number.");
            this.DefaultMessage();
        }

        /// <summary>
        /// Prints a top score table to the console.
        /// </summary>
        /// <param name="board">ScoreBoard to be printed</param>
        public void PrintTopScores(ScoreBoard board)
        {
            Console.ForegroundColor = ConsoleRenderer.TopScoresColor;
            string scores = board.ToString();
            var scoreString = scores.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            this.PrintLines(scoreString);
            this.DefaultMessage();
        }

        /// <summary>
        /// Prints a message to the console when player guesses the secret number
        /// </summary>
        /// <param name="usedCheats">How many cheats were used.</param>
        /// <param name="madeGuesses">How many guesses were made.</param>
        /// <param name="score">The achieved score.</param>
        public void PrintCongratulationMessage(int usedCheats, int madeGuesses, int score)
        {
            Console.ForegroundColor = ConsoleRenderer.CongratMessageColor;
            string[] message = new string[] 
            { 
                "CONGRATULATIONS!",
                string.Format("You guessed the secret number in {0} attempts.", madeGuesses),
                string.Format("Your score is {0} points", score)
            };

            this.PrintLines(message);

            if (usedCheats > 0)
            {
                this.PrintLines(string.Format(" But you used {0} cheats!", usedCheats));
            }

            this.currentRow += 2;
        }

        /// <summary>
        /// Prints the helping number on the console as computer
        /// message (animated).
        /// </summary>
        /// <param name="helpingNumber">Digits of the helping number as array of chars.</param>
        public void PrintHelpingNumber(char[] helpingNumber)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            string message = "The number looks like " + new string(helpingNumber) + '.';
            this.PrintComputerMessage(message);
            this.DefaultMessage();
        }

        /// <summary>
        /// Prints the bulls and cows count to the console
        /// as computer message (animated).
        /// </summary>
        /// <param name="bullsCount">How many bulls</param>
        /// <param name="cowsCount">How many cows</param>
        public void PrintBullsAndCows(int bullsCount, int cowsCount)
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage(
                string.Format("Wrong number! Bulls: {0}, Cows: {1}!", bullsCount, cowsCount));
            this.DefaultMessage();
        }

        /// <summary>
        /// Prints an instructions for saving players score and name
        /// </summary>
        public void SavingScoreMessage()
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage("You can add your nickname to top scores!(3-20 characters)");
        }

        /// <summary>
        /// Prints on the console message to the user and asks him for his name.
        /// </summary>
        public void AskPlayerName()
        {
            Console.SetCursorPosition(this.padding, this.currentRow);
            Console.ResetColor();
            Console.Write("Enter your name: ");
            this.currentRow += 2;
        }

        /// <summary>
        /// Prints on the console message to the user and tells him that
        /// he is not in the top scores.
        /// </summary>
        public void NotInTopMessage()
        {
            Console.ForegroundColor = ConsoleRenderer.ComputerMessageColor;
            this.PrintComputerMessage("Sorry, you did not reached the top scores.");
            this.PrintComputerMessage("Press any key to restart.");
        }

        /// <summary>
        /// Prints on the console a set of lines. Every line is centered.
        /// </summary>
        /// <param name="lines">Set of strings to be printed. Every string is a single line.</param>
        private void PrintLines(params string[] lines)
        {
            for (int i = 0; i < lines.Length; i++, this.currentRow++)
            {
                int x = (Console.WindowWidth / 2) - (lines[i].Length / 2);
                Console.SetCursorPosition(x, this.currentRow);
                Console.WriteLine(lines[i]);
            }
        }

        /// <summary>
        /// Prints a line that is aligned on the ride side of the console.
        /// Printing is animated (simulates live typing).
        /// </summary>
        /// <param name="message">Message as string to be printed.</param>
        private void PrintComputerMessage(string message)
        {
            int startX = Console.WindowWidth - message.Length - this.padding;
            this.animator = new TextAnimator(startX, this.currentRow, message);
            this.animator.Type(50, true);
            this.currentRow += 2;
        }

        /// <summary>
        /// Prints the game logo.
        /// </summary>
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

        /// <summary>
        /// Prints on the right a default string that asks the player
        /// to enter his command.
        /// </summary>
        private void DefaultMessage()
        {
            Console.SetCursorPosition(this.padding, this.currentRow);
            Console.ResetColor();
            Console.Write("Enter your guess or command: ");
            this.currentRow += 2;
        }
    }
}
