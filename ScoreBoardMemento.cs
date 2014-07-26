// <copyright file="ScoreBoardMemento.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Linq;
    using System.Text;

    public class ScoreBoardMemento
    {
        private Record[] records;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoardMemento"/> class.
        /// </summary>
        /// <param name="records"></param>
        public ScoreBoardMemento(Record[] records)
        {
            this.records = records;
        }

        public ScoreBoardMemento()
        {
            this.records = null;
        }

        public string Serialize()
        {
            StringBuilder serialized = new StringBuilder();

            foreach (var record in records)
	        {
                serialized.AppendLine(string.Format("{0}:{1}", record.Name, record.Score));
	        }

            return serialized.ToString();
        }

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

        public Record[] Records
        {
            get
            {
                return this.records.ToArray();
            }
        }
    }
}
