namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ScoreBoard
    {
        private const int MaxResult = 10000;
        private const int CheatPenalty = 2000;
        private int boardLength = 5;
        private List<Record> board;

        public ScoreBoard(int boardLength) 
        {
            this.BoardLength = boardLength;
            this.board = new List<Record>(boardLength);
        }

        public int BoardLength
        {
            get 
            { 
                return this.boardLength; 
            }

            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("ScoreBoard length must be positive!");
                }

                this.boardLength = value;
            }
        }

        public void AddScore(int guesses, int cheatsUsed)
        {
            bool isInTop = this.CheckIfInTop(guesses);

            // TO DO adding logic
            if (isInTop)
            {
                Console.WriteLine("You can add your nickname to top scores!");
                string playerNick = String.Empty;

                while (playerNick.Length <= 3)
                {
                    Console.Write("Enter your nickname: ");
                    playerNick = Console.ReadLine();

                    if (playerNick == null || playerNick.Length < 3)
                    {
                        Console.WriteLine("Please enter at least 3 letters!");
                        continue;
                    }

                    int score = MaxResult / guesses - (cheatsUsed * CheatPenalty);
                    Record newRecord = new Record(playerNick, score);
                    this.board.Add(newRecord);
                }
            }
            else
            {
                Console.WriteLine("Sorry! You didn't make it to top {0}.", this.boardLength);
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            int place = 1;
            result.AppendLine("----Scoreboard----");
            this.board.Sort();

            foreach (var record in this.board)
            {
                result.AppendLine(place + ". " + record.ToString());
            }

            result.AppendLine("------------------");
            return result.ToString();
        }

        private bool CheckIfInTop(int madeGuesses)
        {
            var isInTop = this.board.Count < this.boardLength || 
                this.board[this.boardLength - 1].Score > madeGuesses;
            
            return isInTop;
        }
    }
}
