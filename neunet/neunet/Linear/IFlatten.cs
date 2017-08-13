using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Linear
{
    public interface IFlatten<out TResult>
    {
        TResult Flatten();
    }
}
