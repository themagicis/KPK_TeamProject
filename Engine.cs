namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Engine
    {
        private static readonly Engine engine = new Engine();

        private ScoreBoard topScores;
        private bool wasGameStarted;
        private ConsoleRenderer consoleRenderer;
        public NumberProccesser numberProcesser;
        private int madeGuesses;
        private int usedCheats;

        private Engine()
        {
            this.topScores = new ScoreBoard(5);
            this.wasGameStarted = false;
            this.consoleRenderer = new ConsoleRenderer();
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
                consoleRenderer.PrintGameStartMessage();
            }

            this.Initialize();
        }

        public void Restart()
        {
            this.Start();
            Console.WriteLine("Game Restarted!");
            Console.WriteLine();
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
            this.topScores.AddScore(this.madeGuesses, this.usedCheats);

            // TO DO: Logic after game end(to continue or exit). Can be done in the engine
        }

        private void Initialize()
        {
            this.numberProcesser = new NumberProccesser(4);
            this.madeGuesses = 0;
            this.usedCheats = 0;
        }
    }
}
