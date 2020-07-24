using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.ShokuDex.Business.OperationResults
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }
    }
}
