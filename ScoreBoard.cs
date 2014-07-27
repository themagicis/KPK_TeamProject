// <copyright file="ScoreBoard.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ScoreBoard
    {
        /// <summary>
        /// Hold the defaullt scoreboard length
        /// </summary>
        private int maxBoardLength = 5;

        /// <summary>
        /// Containes scoreboard data
        /// </summary>
        private List<Record> board;

        /// <summary>
        /// Longest plater name. Help for formating in ToString()
        /// </summary>
        private int longestName;

        /// <summary>
        /// Longest score as number of digits. Help for formating in ToString()
        /// </summary>
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

        /// <summary>
        /// Gets the maximun board length
        /// </summary>
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

        /// <summary>
        /// Gets the top scores.
        /// </summary>
        /// <returns>The top scores as array of Records</returns>
        public Record[] GetTopScores()
        {
            Record[] result = new Record[this.board.Count];
            Array.Copy(this.board.ToArray(), result, board.Count);

            return result;
        }

        /// <summary>
        /// Adds a score to the top score list if it is high enough .
        /// </summary>
        /// <param name="score">Score of the player</param>
        /// <param name="name">Player name</param>
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

        /// <summary>
        /// Creates a ScoreBoardMemento from the current top scores
        /// </summary>
        /// <returns>New ScoreBoardMemento</returns>
        public ScoreBoardMemento CreateMemento()
        {
            return new ScoreBoardMemento(this.board.ToArray());
        }

        /// <summary>
        /// Sets a previous memento state of the scoreboard
        /// </summary>
        /// <param name="memento">ScoreBoardMemento with saved top scores as array of records</param>
        public void SetMemento(ScoreBoardMemento memento)
        {
            List<Record> records = new List<Record>(memento.Records);
            this.board = records.OrderByDescending(x => x.Score).ToList();

            foreach (var record in this.board)
            {
                this.CheckForNameAndScoreLength(record.Name, record.Score.ToString());
            }
        }

        /// <summary>
        /// Checks if a score is large enouph
        /// </summary>
        /// <param name="score">Score to be checked</param>
        /// <returns>If the score manages to get in the top scores or not</returns>
        public bool CheckIfInTop(int score)
        {
            var isInTop = this.board.Count < this.maxBoardLength ||
                this.board[this.maxBoardLength - 1].Score < score;

            return isInTop;
        }

        /// <summary>
        /// Creates a multiline string with the top score list
        /// </summary>
        /// <returns>Top scores as string</returns>
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

        /// <summary>
        /// Generates a formatted line of top score for the ToString()
        /// </summary>
        /// <param name="place">Plase of the player</param>
        /// <param name="name">Name of the player</param>
        /// <param name="score">Score of the plater</param>
        /// <returns>String with the top score iinfornation</returns>
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

        /// <summary>
        /// Checks if current name and score is longer than the longest in the scoreboard
        /// and sets them
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <param name="score">Score to check</param>
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
