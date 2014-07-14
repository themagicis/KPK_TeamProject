using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsCommandPattern
{
    public abstract class NumberGenerator
    {
        public abstract string GenerateNumber(int numberLength);
    }
}
