// <copyright file="ScoreBoard.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ScoreBoard
    {
        private int maxBoardLength = 5;
        private List<Record> board;

        private int longestName;
        private int longestScore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// </summary>
        /// <param name="boardLength"></param>
        public ScoreBoard(int boardLength) 
        {
            this.BoardLength = boardLength;
            this.board = new List<Record>(boardLength);
        }

        public int BoardLength
        {
            get 
            {
                return this.maxBoardLength; 
            }

            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("ScoreBoard length must be positive!");
                }

                this.maxBoardLength = value;
            }
        }

        public Record[] GetTopScores()
        {
            Record[] result = new Record[this.board.Count];
            Array.Copy(this.board.ToArray(), result, board.Count);

            return result;
        }

        public void AddScore(int score, string name)
        {
            if (name.Length > 25)
            {
                throw new ArgumentException("Name too long! Max 25 symbols!");
            }

            bool isInTop = this.CheckIfInTop(score);

            if (isInTop)
            {
                Record newRecord = new Record(name, score);
                this.board.Add(newRecord);
                this.board = this.board.OrderByDescending(x => x.Score).ToList();

                if (this.board.Count == this.maxBoardLength + 1)
                {
                    this.board.RemoveAt(this.maxBoardLength);
                }
                
                this.CheckForNameAndScoreLength(name, score.ToString());
            }
        }

        public ScoreBoardMemento CreateMemento()
        {
            return new ScoreBoardMemento(this.board.ToArray());
        }

        public void SetMemento(ScoreBoardMemento memento)
        {
            List<Record> records = new List<Record>(memento.Records);
            this.board = records.OrderByDescending(x => x.Score).ToList();

            foreach (var record in this.board)
            {
                this.CheckForNameAndScoreLength(record.Name, record.Score.ToString());
            }
        }

        public bool CheckIfInTop(int score)
        {
            var isInTop = this.board.Count < this.maxBoardLength ||
                this.board[this.maxBoardLength - 1].Score < score;
            
            return isInTop;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            string scoreBoardTitle = "Scoreboard";
            this.board = this.board.OrderByDescending(x => x.Score).ToList();

            // This is the space required to divide the words
            int aditionalSpace = 7;
            int longestLineLength = this.longestName + this.longestScore + aditionalSpace;
            int sideSymbolsLength = (longestLineLength - scoreBoardTitle.Length) / 2;

            if (this.board.Count == 0)
            {
                sideSymbolsLength = aditionalSpace;
                longestLineLength = (aditionalSpace * 2) + scoreBoardTitle.Length;
            }

            result.Append(new string('-', sideSymbolsLength));
            result.Append(scoreBoardTitle);
            result.Append(new string('-', sideSymbolsLength));
            result.AppendLine();

            for (int i = 0; i < this.board.Count; i++)
            {
                Record currRec = this.board[i];
                result.AppendLine(this.GenerateRecordLine(i + 1, currRec.Name, currRec.Score));
            }

            result.AppendLine(new string('-', longestLineLength));
            return result.ToString();
        }

        private string GenerateRecordLine(int place, string name, int score)
        {
            var sb = new StringBuilder();
            sb.Append(place.ToString()).Append(". ").Append(name);

            string currName = name;
            string currScore = score.ToString();

            if (currName.Length < this.longestName)
            {
                sb.Append(new string(' ', this.longestName - currName.Length));
            }

            sb.Append(" : ");

            if (currScore.Length < this.longestScore)
            {
                sb.Append(new string(' ', this.longestScore - currScore.Length));
            }

            sb.Append(currScore);
            return sb.ToString();
        }

        private void CheckForNameAndScoreLength(string name, string score)
        {
            if (name.Length > this.longestName)
            {
                this.longestName = name.Length;
            }

            if (score.ToString().Length > this.longestScore)
            {
                this.longestScore = score.ToString().Length;
            }
        }
    }
}
