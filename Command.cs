﻿namespace BullsAndCowsCommandPattern
{
    using System;

    /// <summary>
    /// Abstract class that represents
    /// a command defined by Command pattern
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Engine that is executed by the command
        /// </summary>
        protected Engine engine;

        public Command(Engine engine)
        {
            this.engine = engine;
        }

        /// <summary>
        /// Gets or sets the current command value
        /// </summary>
        public abstract string CommandValue
        {
            get;
            set;
        }

        /// <summary>
        /// Executes a method from the engine
        /// depending on the current command state
        /// </summary>
        public abstract void Execute();
    }
}
