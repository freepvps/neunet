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
            var network = NetModel
                .Load("model.json")
                .NetChain
                .Compile<Vector, Vector>();

            Console.WriteLine(network.Process(new double[] { 1, 1 }));

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var vec = new Vector(2);
            for (var i = 0; i < 10000000; i++)
            {
                var b1 = i & 1;
                var b2 = (i >> 1) & 1;
                vec[0] = b1;
                vec[1] = b2;

                var ans = network.Process(vec);
                //Console.WriteLine($"{b1} ^ {b2} = {Math.Round(ans[0], 2)}");
            }
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.Elapsed}");
        }
    }
}