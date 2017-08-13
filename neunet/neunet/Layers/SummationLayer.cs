using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Neunet
{
    public class SummationLayer<TInput> : ILayer<TInput, double>
        where TInput : IEnumerable<double>
    {
        public double Process(TInput input)
        {
            return input.Sum();
        }
    }
}
