using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Linear
{
    public interface ILinearAdditive<in TInput, out TRes>
    {
        TRes Add(TInput value);
    }
}
