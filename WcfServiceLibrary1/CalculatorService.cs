using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Logic

{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculatorService" in both code and config file together.
    public class CalculatorService : ICalculatorService
    {
        public int Add(int val1, int val2)
        {
            return val1 + val2;
        }
        public int Multiply(int val1, int val2)
        {
            return val1 + val2;
        }
    }
}
