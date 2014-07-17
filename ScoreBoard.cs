namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    public class ScoreBoard
    {
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

        public void AddScore(int score, string name)
        {
            this.board = this.board.OrderByDescending(x => x.Score).ToList();
            bool isInTop = this.CheckIfInTop(score);

            if (isInTop)
            {
                Record newRecord = new Record(name, score);
                this.board.Add(newRecord);
            }
            //else
            //{
            //    Console.WriteLine("Sorry! You didn't make it to top {0}.", this.boardLength);
            //}
        }

        public ScoreBoardMemento CreateMemento()
        {
            return new ScoreBoardMemento(this.board.ToArray());
        }

        public void SetMemento(ScoreBoardMemento memento)
        {
            List<Record> records = new List<Record>(memento.Records);
            this.board = records.OrderByDescending(x => x.Score).ToList();
        }

        public bool CheckIfInTop(int score)
        {
            var isInTop = this.board.Count < this.boardLength ||
                this.board[this.boardLength - 1].Score < score;
            
            return isInTop;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine("---------Scoreboard---------");

            int scoresCount = this.board.Count;
            if (scoresCount > this.boardLength)
            {
                scoresCount = this.boardLength;
            }

            for (int i = 0; i < scoresCount; i++)
            {
                string line = i + 1 + " " + this.board[i].ToString();
                result.AppendLine(line);
            }

            result.AppendLine("----------------------------");
            return result.ToString();
        }
    }
}
