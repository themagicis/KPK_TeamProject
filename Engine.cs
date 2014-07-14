namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Engine
    {
        private static readonly Engine engine = new Engine();

        private ScoreBoard topScores; 
        private int usedCheats;
        private int madeGuesses;
        private bool wasGameStarted;

        private string secretNumber;
        private char[] helpingNumber;
        private bool[] bulls;
        private Random randomGenerator;

        private Engine()
        {
            this.topScores = new ScoreBoard(5);
            this.bulls = new bool[4];
            this.wasGameStarted = false;
        }

        public static Engine Instance
        {
            get { return engine; }
        }

        /// <summary>
        /// Generates initial values for the engine
        /// and the start sceen of the game
        /// </summary>
        public void Start()
        {
            if (!this.wasGameStarted)
            {
                this.wasGameStarted = true;
                ConsoleRenderer.PrintGameStartMessage();
            }
            
            this.Initialize();
        }

        /// <summary>
        /// Process a digit command.
        /// Checks if the secret digit is guessed
        /// </summary>
        /// <param name="number">Guessing number</param>
        public void ProcessNumber(string number)
        {
            this.madeGuesses++;
            bool isNumberGuessed = number == this.secretNumber;

            if (isNumberGuessed)
            {
                this.EndGame();
            }
            else
            {
                int bullsCount = this.CountBulls(number);
                int cowsCount = this.CountCows(number);
                ConsoleRenderer.PrintBullsAndCows(bullsCount, cowsCount);
            }
        }

        public void Restart()
        {
            this.Start();
            Console.WriteLine("Game Restarted!");
            Console.WriteLine();
        }

        /// <summary>
        /// Reveals digit from the secret number.
        /// </summary>
        public void RevealDigit()
        {
            bool flag = false;
            int c = 0;
            while (!flag && c != 2 * this.secretNumber.Length)
            {
                int digitForReveal = this.randomGenerator.Next(0, 4);
                if (this.helpingNumber[digitForReveal] == 'X')
                {
                    this.helpingNumber[digitForReveal] =
                    this.secretNumber[digitForReveal];

                    flag = true;
                }

                c++;
            }

            this.usedCheats++;
            ConsoleRenderer.PrintHelpingNumber(this.helpingNumber);
        }

        public void EndGame()
        {
            ConsoleRenderer.PrintCongratulationMessage(this.usedCheats, this.madeGuesses);
            this.topScores.AddScore(this.madeGuesses, this.usedCheats);

             // TO DO: Logic after game end(to continue or exit). Can be done in the engine
        }

        public void PrintScoreboard()
        {
            ConsoleRenderer.PrintTopScores(this.topScores);
        }

        /// <summary>
        /// Exit from the program
        /// </summary>
        public void Exit()
        {
            Console.WriteLine("Good bye!");
            Environment.Exit(1);
        }

        /// <summary>
        /// Loads default values for the engine
        /// </summary>
        private void Initialize()
        {
            this.randomGenerator = new Random();
            this.madeGuesses = 0;
            this.usedCheats = 0;
            this.helpingNumber = new char[] { 'X', 'X', 'X', 'X' };
            this.GenerateSecretNumber(4);
        }

        /// <summary>
        /// Compares the secret number with guessing number and
        /// counts how many cows(matching only digit without position) are found.
        /// </summary>
        /// <param name="number">Guessing number</param>
        /// <returns>How many cows are found</returns>
        private int CountCows(string number)
        {
            int count = 0;

            for (int i = 0; i < number.Length; i++)
            {
                char currentDigit = number[i];

                for (int j = 0; j < this.secretNumber.Length; j++)
                {
                    if (i == j || this.bulls[j])
                    {
                        continue;
                    }
                    else if (currentDigit == this.secretNumber[j])
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Compares the secret number with guessing number and
        /// counts how many bull(matching digit and position) are found.
        /// </summary>
        /// <param name="number">Number from the player</param>
        /// <returns>How many bulls are found</returns>
        private int CountBulls(string number)
        {
            int count = 0;

            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == this.secretNumber[i])
                {
                    count++;
                    this.bulls[i] = true;
                    continue;
                }

                this.bulls[i] = false;
            }

            return count;
        }

        /// <summary>
        /// Generates the secret number as string with different digits 
        /// </summary>
        /// <param name="numberLength">The number of the digits in the number</param>
        /// <exception cref="ArgumentException">Thrown if the legnth of the number
        /// is less than 1 and more than 10(max different digits)</exception>
        private void GenerateSecretNumber(int numberLength)
        {
            if (numberLength < 1 || numberLength > 10)
            {
                throw new ArgumentException("Length of the number cannot be smaller than 1 or bigger than 10!");
            }

            List<char> secretNumberDigits = new List<char>(numberLength);
            int insertedNumbers = 0;

            while (insertedNumbers < numberLength)
            {
                int randomNumber = this.randomGenerator.Next(0, 9);

                if (!this.CheckIfDigitIsUsed(secretNumberDigits, randomNumber))
                {
                    secretNumberDigits.Add(randomNumber.ToString()[0]);
                    insertedNumbers++;
                }
            }

            this.secretNumber = new string(secretNumberDigits.ToArray());
        }

        /// <summary>
        /// Checks if a digit is met in given list of chars
        /// </summary>
        /// <param name="numberDigits">Digits of a number as a list of chars</param>
        /// <param name="number">Seeken number</param>
        /// <returns></returns>
        private bool CheckIfDigitIsUsed(List<char> numberDigits, int number)
        {
            var isDigitUsed = false;

            for (int i = 0; i < numberDigits.Count; i++)
            {
                if (numberDigits[i] == number.ToString()[0])
                {
                    isDigitUsed = true;
                    break;
                }
            }

            return isDigitUsed;
        }
    }
}
