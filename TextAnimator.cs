namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Timers;
    public class TextAnimator
    {
        private int startY;
        private int startX;
        private int messageIndex;
        private string message;
        private Timer timer;
        private bool isAnimating;

        public TextAnimator(int x, int y, string message)
        {
            this.messageIndex = 0;
            this.message = message;
            this.startX = x;
            this.startY = y;
            this.isAnimating = false;
        }

        public string Message
        {
            get;
            set;
        }

        public bool IsAnimating
        {
            get { return this.isAnimating; }
        }

        public void Type(int interval)
        {
            this.timer = new Timer(interval);
            this.timer.Elapsed += this.printChar;
            this.timer.Start();
        }

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
