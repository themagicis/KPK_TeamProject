namespace BullsAndCowsCommandPattern
{
    using System;
    using BullsAndCowsCommandPattern.Exceptions;

    /// <summary>
    /// Class that defines and executes the text commands in the game.
    /// </summary>
    public class TextCommand : Command
    {
        private string command;

        public TextCommand(Engine engine) : base(engine)
        {
        }

        /// <summary>
        /// Gets and sets a command value 
        /// </summary>
        /// <exception cref="InvalidCommandException">
        /// Throw exception if the text command is not implemented</exception>
        public override string CommandValue
        {
            get
            { 
                return this.command;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "help":
                    case "exit":
                    case "restart":
                    case "top": this.command = value.ToLower(); 
                        break;
                    default: throw new InvalidCommandException("Invalid text command!");
                }
            }
        }

        /// <summary>
        /// Executes a set command
        /// </summary>
        public override void Execute()
        {
            switch (this.command)
            {
                case "help": this.engine.RevealDigit();
                    break;
                case "exit": this.engine.Exit(); 
                    break;
                case "restart": this.engine.Restart(); 
                    break;
                case "top": this.engine.PrintScoreboard(); 
                    break;
                default: Console.WriteLine("Command not set!");
                    break;
            }
        }
    }
}
