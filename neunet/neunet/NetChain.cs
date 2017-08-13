using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Neunet
{
    public class NetChain
    {
        private List<object> Layers { get; } = new List<object>();
        private List<MethodInfo> Methods { get; } = new List<MethodInfo>();

        public Type InputType { get => Methods.FirstOrDefault()?.GetParameters()?.First()?.ParameterType; }
        public Type OutputType { get => Methods.LastOrDefault()?.ReturnType; }

        public void AddLayer<TInput, TOutput>(ILayer<TInput, TOutput> layer)
        {
            var methodInfo = typeof(ILayer<TInput, TOutput>).GetMethod(
                    nameof(layer.Process), 
                    new Type[] { typeof(TInput) }
                );

            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            Methods.Add(methodInfo);
            Layers.Add(layer);
        }

        private Expression GenerateExpression(Expression inputExpr)
        {
            Expression curExpr = inputExpr;
            for (var i = 0; i < Layers.Count; i++)
            {
                var method = Methods[i];
                var layer = Layers[i];

                var layerExpr = Expression.Constant(layer);

                var nextExpr = Expression.Call(layerExpr, method, curExpr);
                curExpr = nextExpr;
            }
            return curExpr;
        }
        public LambdaLayer<TInput, TOutput> Compile<TInput, TOutput>()
        {
            var inputExpr = Expression.Parameter(typeof(TInput));
            var curExpr = GenerateExpression(inputExpr);

            var lambda = Expression.Lambda<Func<TInput, TOutput>>(curExpr, inputExpr);
            var compiled = lambda.Compile();
            return new LambdaLayer<TInput, TOutput>(compiled);
        }
        public Delegate CompileDelegate()
        {
            var inputType = InputType;

            var inputExpr = Expression.Parameter(inputType);
            var curExpr = GenerateExpression(inputExpr);

            var lambda = Expression.Lambda(curExpr, inputExpr);
            var compiled = lambda.Compile();

            return compiled;
        }
    }
}
