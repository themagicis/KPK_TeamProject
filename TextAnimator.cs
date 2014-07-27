// <copyright file="TextAnimator.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCows
{
    using System;
    using System.Timers;

    /// <summary>
    /// Class that animates printing line on the console. Can do it without breaking
    /// the application execution flow or to stop it
    /// </summary>
    public class TextAnimator
    {
        private int startY;
        private int startX;
        private int messageIndex;
        private string message;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAnimator"/> class.
        /// </summary>
        /// <param name="x">Starting x</param>
        /// <param name="y">Starting y</param>
        /// <param name="message">Message for printing</param>
        public TextAnimator(int x, int y, string message)
        {
            this.messageIndex = 0;
            this.message = message;
            this.startX = x;
            this.startY = y;
        }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Types the message on the console. Does not stops the execution flow
        /// </summary>
        /// <param name="interval">Interval between each printing of a letter</param>
        public void Type(int interval)
        {
            this.timer = new Timer(interval);
            this.timer.Elapsed += this.printChar;
            this.timer.Start();
        }

        /// <summary>
        /// Types the message on the console
        /// </summary>
        /// <param name="interval">Interval between each printing of a letter</param>
        /// <param name="stopExecution">To stop or not the execution flow</param>
        public void Type(int interval, bool stopExecution)
        {
            if (!stopExecution)
            {
                this.Type(interval);
            }

            Console.SetCursorPosition(this.startX, this.startY);
            for (int i = 0; i < this.message.Length; i++)
            {
                Console.Write(this.message[i]);
                System.Threading.Thread.Sleep(interval);
            }
        }

        /// <summary>
        /// Prints char on the console. Callback from Timer
        /// </summary>
        /// <param name="sender">Sender from the Timer</param>
        /// <param name="e">Event args from the Timer</param>
        private void printChar(object sender, ElapsedEventArgs e)
        {
            if (messageIndex >= this.message.Length)
            {
                this.timer.Close();
                this.timer.Dispose();
            }
            else
            {
                Console.SetCursorPosition(this.startX, this.startY);
                Console.Write(this.message[this.messageIndex]);
                this.messageIndex++;
                this.startX++;
            }
        }
    }
}
