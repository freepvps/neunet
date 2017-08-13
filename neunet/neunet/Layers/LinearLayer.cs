using System;
using System.Collections.Generic;
using System.Text;
using Neunet.Linear;

namespace Neunet
{
    public class LinearLayer<TInput, TTransform, TOutput> : ILayer<TInput, TOutput> 
        where TTransform : ILinearMultiplicative<TInput, TOutput> 
        where TOutput : ILinearAdditive<TOutput, TOutput>
    {
        public TTransform Transform { get; }
        public TOutput Biases { get; }
        public LinearLayer(TTransform transform, TOutput biases)
        {
            Transform = transform;
            Biases = biases;
        }

        public TOutput Process(TInput input)
        {
            return Transform.Product(input).Add(Biases);
        }
    }

    public class LinearLayer : LinearLayer<Vector, Matrix, Vector>
    {
        public LinearLayer(Matrix transform, Vector biases) : base(transform, biases)
        {
        }
    }
}
