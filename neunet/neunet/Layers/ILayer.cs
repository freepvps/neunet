using System;
using System.Collections.Generic;
using System.Text;

namespace Neunet
{
    public interface ILayer<in TInput, out TOutput>
    {
        TOutput Process(TInput input);
    }
}
