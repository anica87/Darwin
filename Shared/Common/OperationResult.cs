using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common
{
    public class OperationResult : IOperationResult
    {
        public bool Result { get; set; }
        public string MessageKey { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Exception Exception { get; set; }

        public static IOperationResult Empty
        {
            get
            {
                return new OperationResult
                {
                    Result = false,
                    Data = null,
                    Message = "",
                    MessageKey = "",
                    Exception = null
                };
            }
        }

        public static IOperationResult EmptyOK
        {
            get
            {
                return new OperationResult
                {
                    Result = true,
                    Data = null,
                    Message = "",
                    MessageKey = "",
                    Exception = null
                };
            }
        }

        public static IOperationResult Join(params IOperationResult[] results)
        {
            IOperationResult result = new OperationResult
            {
                Result = true,
                Data = null,
                Message = "",
                MessageKey = "",
                Exception = null
            };

            List<OperationResult> data = new List<OperationResult>();

            foreach (OperationResult r in results)
            {
                // logicaly add all results 
                result.Result = result.Result && r.Result;
                data.Add(r);
            }

            // all operation results as list in data property of new operation result 
            result.Data = data;

            return result;
        }

        public T Get<T>()
        {
            if (Data == null)
                return default(T);

            return (T)Data;
        }

        /// <summary>
        /// String representation of result
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("Result={0}; MessageKey={1}; Message={2};", Result, MessageKey, Message));
            if(Exception != null)
                sb.AppendLine(string.Format("Exception={0}; ", Exception.Message));

            if (Data != null)
            {
                if (Data is List<OperationResult>)
                {
                    int count = 0;
                    foreach (OperationResult result in (Data as List<OperationResult>))
                        sb.AppendLine(string.Format("[result {0}] {1}", ++count, result.ToString()));
                }
                else
                {
                    sb.AppendLine(string.Format("Data={0}", Data));
                }
            }

            return sb.ToString();
        }

    }
}
