using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Serialization
{
    public class ChainModel
    {
        public Layer[] Layers { get; set; }
    }
    public class ChainModel<T> : ChainModel
    {
        public T Extra { get; set; }
    }
}
