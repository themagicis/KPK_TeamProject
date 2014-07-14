using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsCommandPattern
{
    // Mocking class for testing purposes
    public class MockNumberGenerator : NumberGenerator
    {
        private string staticNumber;

        public MockNumberGenerator(string number)
        {
            this.staticNumber = number;
        }

        public override string GenerateNumber(int numberLength)
        {
            return this.staticNumber;
        }
    }
}
