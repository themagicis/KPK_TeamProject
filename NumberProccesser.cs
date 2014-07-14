namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NumberProccesser
    {
        public string secretNumber;
        private Random randomGenerator;
        private bool[] bulls;
        private char[] helpingNumber;

        public NumberProccesser(int numberLength)
        {
            this.bulls = new bool[numberLength];
            this.helpingNumber = new char[numberLength];

            for (int i = 0; i < numberLength; i++)
            {
                this.helpingNumber[i] = 'X';
            }

            this.randomGenerator = new Random();
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
                int digitForReveal = this.randomGenerator.Next(0, this.secretNumber.Length);
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
        /// Checks if a digit is met in given list of chars
        /// </summary>
        /// <param name="numberDigits">Digits of a number as a list of chars</param>
        /// <param name="number">Seeken number</param>
        /// <returns></returns>
        private bool CheckIfDigitIsUsed(List<char> numberDigits, int number)
        {
            var isDigitUsed = false;

            for (int i = 0; i < numberDigits.Count; i++)
            {
                if (numberDigits[i] == number.ToString()[0])
                {
                    isDigitUsed = true;
                    break;
                }
            }

            return isDigitUsed;
        }

        /// <summary>
        /// Generates the secret number as string with different digits 
        /// </summary>
        /// <param name="numberLength">The number of the digits in the number</param>
        /// <exception cref="ArgumentException">Thrown if the legnth of the number
        /// is less than 1 and more than 10(max different digits)</exception>
        private void GenerateSecretNumber(int numberLength)
        {
            if (numberLength < 1 || numberLength > 10)
            {
                throw new ArgumentException("Length of the number cannot be smaller than 1 or bigger than 10!");
            }

            List<char> secretNumberDigits = new List<char>(numberLength);
            int insertedNumbers = 0;

            while (insertedNumbers < numberLength)
            {
                int randomNumber = this.randomGenerator.Next(0, 9);

                if (!this.CheckIfDigitIsUsed(secretNumberDigits, randomNumber))
                {
                    secretNumberDigits.Add(randomNumber.ToString()[0]);
                    insertedNumbers++;
                }
            }

            this.secretNumber = new string(secretNumberDigits.ToArray());
        }
    }
}
