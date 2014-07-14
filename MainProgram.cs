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
            var engine = Engine.Instance;
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
                    Console.Write("Enter your guess or command: ");
                    command = Console.ReadLine();

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
                    Console.WriteLine(ex.ParamName);
                    Console.WriteLine();
                }
                catch (InvalidCommandException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }
    }
}