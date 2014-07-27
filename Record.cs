// <copyright file="Record.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCows
{
    using System;
    using System.Text;

    /// <summary>
    /// Class that holds info for a player's name and score
    /// </summary>
    public class Record : IComparable<Record>
    {
        /// <summary>
        /// Holds players name
        /// </summary>
        private string name;

        /// <summary>
        /// Holds players score
        /// </summary>
        private int score; 
        
        /// /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="score">Score of the player</param>
        public Record(string name, int score)
        {
            if (name == null || name == string.Empty)
            {
                this.name = "unknown";
            }
            else
            {
                this.name = name;
            }
            
            this.score = score;
        }

        /// <summary>
        /// Gets player name
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets player score
        /// </summary>
        public int Score
        {
            get { return this.score; }
        }

        /// <summary>
        /// Compares one record with another
        /// </summary>
        /// <param name="other">Record to compare to</param>
        /// <returns></returns>
        public int CompareTo(Record other)
        {
            return this.score.CompareTo(other.score);
        }

         /// <summary>
         /// Converts record info to string
         /// </summary>
         /// <returns>Record as string</returns>
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendFormat("{0} : {1} pts", this.name, this.score);

            return result.ToString();
        }
    }
}
