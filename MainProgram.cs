namespace BullsAndCowsCommandPattern
{
    using System;
    using BullsAndCowsCommandPattern.Exceptions;

    /// <summary>
    /// Starting point of the program
    /// </summary>
    public class MainProgram
    {
        /// <summary>
        /// The game mode. Defines how many digits to be
        /// the length of the secret digit
        /// </summary>
        private const int GameMode = 4; 

        /// <summary>
        /// Main method of the program
        /// </summary>
        public static void Main()
        {
            var engine = Engine.GetInstance(GameMode);
            Play(engine);
        }

        /// <summary>
        /// Method that controls engine execution. Contains
        /// main game cycle, that gets input and passes the commands
        /// </summary>
        /// <param name="engine">Engine of the game</param>
        private static void Play(Engine engine)
        {
            long digit;
            string command;
            Command textCommand = new TextCommand(engine);
            Command digitCommand = new DigitCommand(engine, GameMode);

            engine.Start();
            
            while (true)
            {
                try
                {
                    command = Console.ReadLine();
                    Console.WriteLine();

                    if (long.TryParse(command, out digit))
                    {
                        digitCommand.CommandValue = command;
                        digitCommand.Execute();
                    }
                    else
                    { 
                        textCommand.CommandValue = command;
                        textCommand.Execute();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    engine.ProcessError(ex.ParamName);
                }
                catch (InvalidCommandException ex)
                {
                    engine.ProcessError(ex.Message);
                }
            }
        }
    }
}