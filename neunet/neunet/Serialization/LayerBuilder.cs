using System;
using System.Collections.Generic;
using System.Text;
using Neunet.Linear;

namespace Neunet.Serialization
{
    public class LayerBuilder
    {
        public static ILayer<Vector, Vector> Build(Layer layer)
        {
            switch (layer.Type)
            {
                case LayerType.Linear: return new LinearLayer(layer.Weights, layer.Biases);
                case LayerType.Elementwise: return new ElementwizeLayer(Functions.GetFunction(layer.Function));
            }
            throw new ArgumentException("Unknwon layer");
        }
    }
}
