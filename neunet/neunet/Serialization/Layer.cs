using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Serialization
{
    public class Layer
    {
        public LayerType Type { get; set; }
        public double[] Biases { get; set; }
        public double[,] Weights { get; set; }
        public string Function { get; set; }
    }
}
