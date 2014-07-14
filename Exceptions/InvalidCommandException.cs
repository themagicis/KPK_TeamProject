namespace BullsAndCowsCommandPattern.Exceptions
{
    using System;

    /// <summary>
    /// Exception for passing an invalid command
    /// </summary>
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException() : base()
        {
        }

        public InvalidCommandException(string message) : base(message)
        {
        }
    }
}
