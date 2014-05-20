namespace BullsAndCows
{
    using System;
    using System.Linq;

    public class Record : IComparable<Record>
    {
        private string name;
        private int score;         
 
        public Record(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
        }

        public int CompareTo(Record other)
        {
            return this.score.CompareTo(other.score);
        }
    }
}
