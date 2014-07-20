namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Text;

    public class Record : IComparable<Record>
    {
        private string name;
        private int score; 
        
        public Record(string name, int score)
        {
            if (name == null || name == String.Empty)
            {
                this.name = "unknown";
            }
            else
            {
                this.name = name;
            }
            
            this.score = score;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Score
        {
            get { return this.score; }
        }

        public int CompareTo(Record other)
        {
            return this.score.CompareTo(other.score);
        }

         // TO DO
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendFormat("{0} : {1} pts", this.name, this.score);

            return result.ToString();
        }
    }
}
