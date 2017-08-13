using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Neunet.Serialization;

namespace Neunet
{
    public class NetModel
    {
        private NetModel() { }

        public NetChain NetChain { get; } = new NetChain();

        public static NetModel Load(string path)
        {
            var content = File.ReadAllText(path, Encoding.UTF8);
            return Parse(content);
        }
        public static NetModel Parse(string jsonModel)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ChainModel>(jsonModel);
            return Parse(model);
        }
        public static NetModel Parse(ChainModel model)
        {
            var result = new NetModel();
            foreach (var layer in model.Layers)
            {
                var completeLayer = LayerBuilder.Build(layer);
                result.NetChain.AddLayer(completeLayer);
            }
            return result;
        }
    }
}
