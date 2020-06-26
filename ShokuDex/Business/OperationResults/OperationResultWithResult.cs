using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.ShokuDex.Business.OperationResults
{
    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }
}
