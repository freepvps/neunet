using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Linear
{
    public interface ILinearObject<T> : ILinearAdditive<T, T>, ILinearMultiplicative<T, T>, IEnumerable<double>, IMap<T, double>
    {
    }
}
