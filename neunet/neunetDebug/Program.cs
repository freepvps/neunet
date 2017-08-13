using System;
using Neunet;
using System.Linq.Expressions;
using Neunet.Linear;

namespace neunetDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = NetModel.Load("model.json");

            var net = NetModel.Load("model.json").NetChain.Compile<Vector, Vector>();
            Console.WriteLine(net.Process(new double[] { 1, 1 }));


            var netChain = model.NetChain;
            netChain.AddLayer(new SummationLayer<Vector>());

            var res = netChain.Compile<Vector, double>();

            var sw = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < 10000000; i++)
            {
                var b1 = i & 1;
                var b2 = (i >> 1) & 1;
                var vec = new Vector(new double[] { b1, b2 });
                var ans = res.Process(vec);
                //Console.WriteLine($"{b1} ^ {b2} = {Math.Round(ans[0], 2)}");
            }
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.Elapsed}");
        }
    }
}