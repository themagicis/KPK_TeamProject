namespace BullsAndCowsCommandPattern
{
    using System;

    public class DigitCommand : Command
    {
        private string numberCommand;
        private int numberLength;
        public DigitCommand(Engine engine,int numberLength) : base(engine)
        {
            this.NumberLength = numberLength;
        }

        public override string CommandValue
        {
            get 
            {
                return this.numberCommand; 
            }

            set
            {
                if (value.Length != this.numberLength)
                {
                    string message = String.Format("Number length must be {0} digits", this.numberLength);
                    throw new ArgumentOutOfRangeException(message);
                }

                this.numberCommand = value;
            }
        }

        public int NumberLength
        {
            get 
            {
                return this.numberLength;
            }

            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("Length of the number must be 1-10 digits long");
                }
                else
                {
                    this.numberLength = value;
                }
            }
        }

        public override void Execute()
        {
           engine.ProcessNumber(this.numberCommand);
        }
    }
}
