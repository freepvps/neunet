using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Neunet.Serialization;

namespace Neunet
{
    public class NetModel<T> : NetModel
    {
        public T Extra { get; set; }
    }
    public class NetModel
    {
        public NetChain NetChain { get; } = new NetChain();
        
        public static NetModel Load(string path)
        {
            var content = File.ReadAllText(path, Encoding.UTF8);
            return Parse(content);
        }
        public static NetModel<T> Load<T>(string path)
        {
            var content = File.ReadAllText(path, Encoding.UTF8);
            return Parse<T>(content);
        }


        public static NetModel Parse(string jsonModel)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ChainModel>(jsonModel);
            return Parse(model);
        }
        public static NetModel<T> Parse<T>(string jsonModel)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ChainModel<T>>(jsonModel);
            return Parse<T>(model);
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
        public static NetModel<T> Parse<T>(ChainModel<T> model)
        {
            var result = new NetModel<T>();
            result.Extra = model.Extra;
            foreach (var layer in model.Layers)
            {
                var completeLayer = LayerBuilder.Build(layer);
                result.NetChain.AddLayer(completeLayer);
            }
            return result;
        }
    }
}
