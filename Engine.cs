namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class Engine
    {
        public const int DEFAULT_NUMBER_LENGTH = 4;
        private const int MaxResult = 10000;
        private const int CheatPenalty = 2000;

        private static Engine engine = null; //new Engine();

        private ScoreBoard topScores;
        private ConsoleRenderer consoleRenderer;
        public NumberProccesser numberProcesser;
        private int madeGuesses;
        private int usedCheats;
        private int numberLength;
        private NumberGenerator numberGenerator;

        private Engine(NumberGenerator numberGenerator, int numberLength = DEFAULT_NUMBER_LENGTH)
        {
            this.topScores = new ScoreBoard(5);
            try
            {
                ScoreBoardMemento memento = new ScoreBoardMemento();
                memento.Deserialize(File.ReadAllText(@"scores.txt"));
                topScores.SetMemento(memento);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(@"scores.txt", string.Empty);
            }

            this.consoleRenderer = new ConsoleRenderer(80, 50);
            this.numberLength = numberLength;
            this.numberGenerator = numberGenerator;
        }

        public static Engine GetInstance(NumberGenerator numberGenerator, int numberLength = DEFAULT_NUMBER_LENGTH)
        {
            if (engine == null) 
            {
                engine = new Engine(numberGenerator, numberLength);
            }

            return engine;
        }

        public static Engine GetInstance(int numberLength)
        {
            return GetInstance(new RandomNumberGenerator(), numberLength);
        }

        public static Engine GetInstance()
        {
            return GetInstance(new RandomNumberGenerator(), DEFAULT_NUMBER_LENGTH);
        }

        /// <summary>
        /// Generates initial values for the engine
        /// and the start sceen of the game
        /// </summary>
        public void Start()
        {
            this.Initialize();
            Console.Clear();
            consoleRenderer.PrintGameStartMessage();
        }

        public void Restart()
        {
            Console.Clear();
            this.Start();
        }

        /// <summary>
        /// Process a digit command.
        /// Checks if the secret digit is guessed
        /// </summary>
        /// <param name="number">Guessing number</param>
        /// 
        public void ProcessNumber(string number)
        {
            this.madeGuesses++;
            var isNumberGuessed  =  numberProcesser.CheckIsGuessed(number);

            if (isNumberGuessed)
            {
                this.EndGame();
            }
            else
            {
                int bullsCount = this.numberProcesser.CountBulls(number);
                int cowsCount = this.numberProcesser.CountCows(number);
                this.consoleRenderer.PrintBullsAndCows(bullsCount, cowsCount);
            }
        }

        public void RevealDigit()
        {
            this.usedCheats++;
            char[] revealedSecredNumber = this.numberProcesser.RevealDigit();
            this.consoleRenderer.PrintHelpingNumber(revealedSecredNumber);
        }

        public void PrintScoreboard()
        {
            this.consoleRenderer.PrintTopScores(this.topScores);
        }

        public void ProcessError(string message)
        {
            this.consoleRenderer.PrintErrorMessage(message);
        }

        /// <summary>
        /// Exit from the program
        /// </summary>
        public void Exit()
        {
            Console.WriteLine("Good bye!");
            Environment.Exit(1);
        }

        private void EndGame()
        {
            consoleRenderer.PrintCongratulationMessage(usedCheats, madeGuesses);
            this.SaveScore();
            this.Start(); 

            // TO DO: Logic after game end(to continue or exit). Can be done in the engine
        }

        private void SaveScore()
        {
            int score = MaxResult / this.madeGuesses - (this.usedCheats * CheatPenalty);
            string name = String.Empty;


            // Printing to be moved to renderer.
            this.consoleRenderer.PrintLines("You can add your nickname to top scores!");
            Console.WriteLine();

            while (name.Length <= 3)
            {
                this.consoleRenderer.PrintLines("Enter your nickname: ");
                name = Console.ReadLine();

                if (name == null || name.Length < 3)
                {
                    this.consoleRenderer.PrintErrorMessage("Please enter at least 3 letters!");
                    continue;
                }
            }

            this.topScores.AddScore(score, name);

            ScoreBoardMemento memento = topScores.CreateMemento();
            File.WriteAllText(@"scores.txt", memento.Serialize());
        }

        private void Initialize()
        {
            this.numberProcesser = new NumberProccesser(numberLength, numberGenerator);
            this.consoleRenderer = new ConsoleRenderer(80, 50);
            this.madeGuesses = 0;
            this.usedCheats = 0;
        }
    }
}
