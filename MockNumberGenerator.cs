// <copyright file="MockNumberGenerator.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    /// <summary>
    /// Mocking class for testing purposes. Mocks Number generator
    /// so it can set a defined secret number 
    /// </summary>
    public class MockNumberGenerator : NumberGenerator
    {
        /// <summary>
        /// Keeps the value of the secret number
        /// </summary>
        private string staticNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockNumberGenerator"/> class.
        /// </summary>
        /// <param name="number">Secret number</param>
        public MockNumberGenerator(string number)
        {
            this.staticNumber = number;
        }

        /// <summary>
        /// Mocks generating of a secret number
        /// </summary>
        /// <param name="numberLength">Count of the digits in the secret number</param>
        /// <returns>Secret number as string</returns>
        public override string GenerateNumber(int numberLength)
        {
            return this.staticNumber;
        }
    }
}
