using System;
using System.Collections.Generic;
using System.Text;
using Neunet.Linear;

namespace Neunet
{
    public class FlattenLayer<TInput, TOutput> : ILayer<TInput, TOutput>
        where TInput : IFlatten<TOutput>
    {
        public TOutput Process(TInput input)
        {
            return input.Flatten();
        }
    }
}
