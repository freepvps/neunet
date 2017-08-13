using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Linear
{
    public interface ILinearMultiplicative<in TInput, out TRes>
    {
        TRes Product(TInput value);
    }
}
