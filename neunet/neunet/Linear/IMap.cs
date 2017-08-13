using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Linear
{
    public interface IMap<TBase, T> : IMap<TBase, T, T>
    {

    }
    public interface IMap<TBase, TInput, TOutput>
    {
        TBase Map(Func<TInput, TOutput> func);
    }
}
