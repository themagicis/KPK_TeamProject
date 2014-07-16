using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsCommandPattern
{
    public class ScoreBoardMemento
    {
        private Record[] records;

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
