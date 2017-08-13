using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet
{
    public class LambdaLayer<TInput, TOutput> : ILayer<TInput, TOutput>
    {
        public Func<TInput, TOutput> LambdaFunction { get; }

        public LambdaLayer(Func<TInput, TOutput> lambdaFunction)
        {
            LambdaFunction = lambdaFunction;
        }

        public TOutput Process(TInput input)
        {
            return LambdaFunction(input);
        }
    }
}
