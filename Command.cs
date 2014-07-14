namespace BullsAndCowsCommandPattern
{
    using System;

    public abstract class Command
    {
        protected Engine engine;

        public Command(Engine engine)
        {
            this.engine = engine;
        }

        public abstract string CommandValue
        {
            get;
            set;
        }

        public abstract void Execute();
    }
}
