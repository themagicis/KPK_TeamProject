// <copyright file="DigitCommand.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    using System;

    /// <summary>
    /// Class that accepts and executes a digit command
    /// </summary>
    public class DigitCommand : Command
    {
        /// <summary>
        /// Contains number command as string
        /// </summary>
        private string numberCommand;

        /// <summary>
        /// Number of digits in the number
        /// </summary>
        private int numberLength;

        /// <summary>
        /// Initializes a new instance of the<see cref="DigitCommand"/> class.
        /// </summary>
        /// <param name="engine">Engine that would be executed</param>
        /// <param name="numberLength">Length of the number</param>
        public DigitCommand(Engine engine, int numberLength) : base(engine)
        {
            this.NumberLength = numberLength;
        }

        /// <summary>
        /// Sets or gets the command value. Number as string.
        /// </summary>
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
                    string message = string.Format("Number length must be {0} digits", this.numberLength);
                    throw new ArgumentOutOfRangeException(message);
                }

                this.numberCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of the number command.
        /// </summary>
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

        /// <summary>
        /// Executes the ProcessNumber method of the engine and pass the contained command(number)
        /// </summary>
        public override void Execute()
        {
           engine.ProcessNumber(this.numberCommand);
        }
    }
}
