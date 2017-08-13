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
                case LayerType.Elementwise:
                    {
                        var doubleFunction = Functions.GetDoubleFunction(layer.Function);
                        if (doubleFunction != null)
                        {
                            return new ElementwizeLayer<Vector, double>(doubleFunction);
                        }

                        var vectorFunction = Functions.GetVectorFunction(layer.Function);
                        if (vectorFunction != null)
                        {
                            return new ElementwizeLayer<Vector>(vectorFunction);
                        }
                        throw new ArgumentException($"{layer.Function} not found");
                    };
            }
            throw new ArgumentException("Unknwon layer");
        }
    }
}
