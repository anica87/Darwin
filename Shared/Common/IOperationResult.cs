using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common
{
    public interface IOperationResult
    {
        bool Result { get; set; }
        string MessageKey { get; set; }
        string Message { get; set; }
        object Data { get; set; }
        Exception Exception { get; set; }

        T Get<T>();
    }
}
