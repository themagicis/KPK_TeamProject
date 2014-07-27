// <copyright file="Engine.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCows
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class that executes the main game logic. It combines the UI, saving data and
    /// the data processing. It implements the singleton pattern and it represents a
    /// facade for the more complex game logic.
    /// </summary>
    public class Engine
    {
        private const int DefaultNumberLength = 4;
        private const int MaxResult = 10000;
        private const int CheatPenalty = 2500;
        private const int FieldPadding = 10;

        private static Engine engine = null;
        private ScoreBoard topScores;
        private ConsoleRenderer consoleRenderer;
        private NumberProccesser numberProcesser;
        private NumberGenerator numberGenerator;
        private int madeGuesses;
        private int usedCheats;
        private int numberLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="numberGenerator">Number generator for creating screen number</param>
        /// <param name="numberLength">The count of the digits in the secret number</param>
        private Engine(NumberGenerator numberGenerator, int numberLength = DefaultNumberLength)
        {
            this.topScores = new ScoreBoard(10);
            try
            {
                ScoreBoardMemento memento = new ScoreBoardMemento();
                memento.Deserialize(File.ReadAllText(@"scores.txt"));
                this.topScores.SetMemento(memento);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(@"scores.txt", string.Empty);
            }

            this.consoleRenderer = new ConsoleRenderer(80, 50, Engine.FieldPadding);
            this.numberLength = numberLength;
            this.numberGenerator = numberGenerator;
        }

        /// <summary>
        /// Gets the instance of the engine and sets the length of the secret number
        /// and the number generator.
        /// </summary>
        /// <param name="numberGenerator">Number generator for creating screen number</param>
        /// <param name="numberLength">The count of the digits in the secret number</param>
        /// <returns>Returns reference to the engine instance.</returns>
        public static Engine GetInstance(NumberGenerator numberGenerator, int numberLength = DefaultNumberLength)
        {
            if (engine == null) 
            {
                engine = new Engine(numberGenerator, numberLength);
            }

            return engine;
        }

        /// <summary>
        /// Gets the instance of the engine and sets the length of the secret number.
        /// </summary>
        /// <param name="numberLength">The count of the digits in the secret number</param>
        /// <returns>Returns reference to the engine instance.</returns>
        public static Engine GetInstance(int numberLength)
        {
            return GetInstance(new RandomNumberGenerator(), numberLength);
        }

        /// <summary>
        /// Gets the instance of the engine with default state.
        /// </summary>
        /// <returns>Returns reference to the engine instance.</returns>
        public static Engine GetInstance()
        {
            return GetInstance(new RandomNumberGenerator(), DefaultNumberLength);
        }

        /// <summary>
        /// Generates initial values for the engine
        /// and the start screen of the game
        /// </summary>
        public void Start()
        {
            this.Initialize();
            Console.Clear();
            this.consoleRenderer.PrintGameStartMessage();
        }

        /// <summary>
        /// Restarts the game. All progress is lost and the previous messages
        /// on the console are deleted.
        /// </summary>
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
        public void ProcessNumber(string number)
        {
            this.madeGuesses++;
            var isNumberGuessed = this.numberProcesser.CheckIsGuessed(number);

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

        /// <summary>
        /// Reveals a digit from the secret number(cheat). If all digits are shown
        /// prints again the secret number but it still counts it for used cheat.  
        /// </summary>
        public void RevealDigit()
        {
            this.usedCheats++;
            char[] revealedSecredNumber = this.numberProcesser.RevealDigit();
            this.consoleRenderer.PrintHelpingNumber(revealedSecredNumber);
        }

        /// <summary>
        /// Shows the top scores on the console.
        /// </summary>
        public void PrintScoreboard()
        {
            this.consoleRenderer.PrintTopScores(this.topScores);
        }

        /// <summary>
        /// Shows an error message on the console for the user.
        /// </summary>
        /// <param name="message">Error message</param>
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

        /// <summary>
        /// Executes the logic after game end (congratulation message and saving score methods).
        /// </summary>
        private void EndGame()
        {
            int score = (MaxResult / this.madeGuesses) - (this.usedCheats * CheatPenalty);
            this.consoleRenderer.PrintCongratulationMessage(this.usedCheats, this.madeGuesses, score);
            this.SaveScore(score);
            Console.ReadKey();
            this.Start(); 
        }

        /// <summary>
        /// Saves score of a player after game end.
        /// </summary>
        /// <param name="score">Score for saving</param>
        private void SaveScore(int score)
        {
            string name = string.Empty;

            if (this.topScores.CheckIfInTop(score))
            {
                this.consoleRenderer.SavingScoreMessage();
                while (name.Length < 3)
                {
                    this.consoleRenderer.AskPlayerName();
                    name = Console.ReadLine();

                    if (name == null || name.Length < 3)
                    {
                        continue;
                    }
                }

                this.topScores.AddScore(score, name);
                ScoreBoardMemento memento = this.topScores.CreateMemento();
                File.WriteAllText(@"scores.txt", memento.Serialize());
            }
            else
            {
                this.consoleRenderer.NotInTopMessage();
            }
        }

        /// <summary>
        /// Sets initial state for a new game.
        /// </summary>
        private void Initialize()
        {
            this.numberProcesser = new NumberProccesser(this.numberLength, this.numberGenerator);
            this.consoleRenderer = new ConsoleRenderer(80, 50, Engine.FieldPadding);
            this.madeGuesses = 0;
            this.usedCheats = 0;
        }
    }
}
