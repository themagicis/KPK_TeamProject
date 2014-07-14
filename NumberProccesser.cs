namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NumberProccesser
    {
        public string secretNumber;
        private NumberGenerator numberGenerator;
        private bool[] bulls;
        private char[] helpingNumber;

        public NumberProccesser(int numberLength, NumberGenerator numberGenerator)
        {
            this.bulls = new bool[numberLength];
            this.helpingNumber = new char[numberLength];

            for (int i = 0; i < numberLength; i++)
            {
                this.helpingNumber[i] = 'X';
            }

            //this.randomGenerator = new Random();
            this.numberGenerator = numberGenerator;
            this.GenerateSecretNumber(numberLength);
        }

        public bool CheckIsGuessed(string number)
        {
            bool isGuessed = number == this.secretNumber;
            return isGuessed;
        }

        /// <summary>
        /// Compares the secret number with guessing number and
        /// counts how many cows(matching only digit without position) are found.
        /// </summary>
        /// <param name="number">Guessing number</param>
        /// <returns>How many cows are found</returns>
        public int CountCows(string number)
        {
            int count = 0;

            for (int i = 0; i < number.Length; i++)
            {
                char currentDigit = number[i];

                for (int j = 0; j < this.secretNumber.Length; j++)
                {
                    if (i == j || this.bulls[j])
                    {
                        continue;
                    }
                    else if (currentDigit == this.secretNumber[j])
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Compares the secret number with guessing number and
        /// counts how many bull(matching digit and position) are found.
        /// </summary>
        /// <param name="number">Number from the player</param>
        /// <returns>How many bulls are found</returns>
        public int CountBulls(string number)
        {
            int count = 0;

            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == this.secretNumber[i])
                {
                    count++;
                    this.bulls[i] = true;
                    continue;
                }

                this.bulls[i] = false;
            }

            return count;
        }

        /// <summary>
        /// Reveals digit from the secret number.
        /// </summary>
        public char[] RevealDigit()
        {
            bool flag = false;
            int c = 0;
            while (!flag && c != 2 * this.secretNumber.Length)
            {
                Random random = new Random();
                int digitForReveal = random.Next(0, this.secretNumber.Length);
                if (this.helpingNumber[digitForReveal] == 'X')
                {
                    this.helpingNumber[digitForReveal] = this.secretNumber[digitForReveal];

                    flag = true;
                }

                c++;
            }

            return helpingNumber;
        }

        /// <summary>
        /// Generates the secret number as string with different digits 
        /// </summary>
        /// <param name="numberLength">The number of the digits in the number</param>
        private void GenerateSecretNumber(int numberLength)
        {
            this.secretNumber = numberGenerator.GenerateNumber(numberLength); 
        }
    }
}
