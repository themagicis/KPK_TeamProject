// <copyright file="ScoreBoardMemento.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class that saves ScoreBoards State. Implements a Memento design pattern
    /// </summary>
    public class ScoreBoardMemento
    {
        /// <summary>
        /// Holds all the records
        /// </summary>
        private Record[] records;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoardMemento"/> class.
        /// </summary>
        /// <param name="records">Array of records</param>
        public ScoreBoardMemento(Record[] records)
        {
            this.records = records;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoardMemento"/> class.
        /// </summary>
        public ScoreBoardMemento()
        {
            this.records = null;
        }

        /// <summary>
        /// Converts Record objects to a single string
        /// </summary>
        /// <returns>All stored records as string</returns>
        public string Serialize()
        {
            StringBuilder serialized = new StringBuilder();

            foreach (var record in this.records)
	        {
                serialized.AppendLine(string.Format("{0}:{1}", record.Name, record.Score));
	        }

            return serialized.ToString();
        }

        /// <summary>
        /// Desirialize a string of records to an array
        /// </summary>
        /// <param name="state">Serzialized records</param>
        public void Deserialize(string state)
        {
            string[] lines = state.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            this.records = new Record[lines.Length];
            int index = 0;
            foreach (var line in lines)
            {
                string[] elements = line.Split(':');
                Record newRecord = new Record(elements[0], int.Parse(elements[1]));
                this.records[index] = newRecord;
                index++;
            }
        }

        /// <summary>
        /// Gets the records
        /// </summary>
        public Record[] Records
        {
            get
            {
                return this.records.ToArray();
            }
        }
    }
}
