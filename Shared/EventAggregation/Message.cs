using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.EventAggregation
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public MessageType MessageType { get; set; }
        public bool Result { get; set; }
        public Exception ExceptionInfo { get; set; }

        public object Data { get; set; }

        public double ExpirationMilliSecondsFromNow
        {
            get
            {
                return Math.Max(0, Expiration.Subtract(DateTime.Now).TotalMilliseconds);
            }

            set
            {
                Expiration = DateTime.Now.AddMilliseconds(value);
            }
        }

        public DateTime _Expiration = DateTime.MinValue;
        public DateTime Expiration
        {
            get
            {
                return _Expiration;
            }

            set
            {
                _Expiration = value;
            }
        }

        public static Message Empty
        {
            get
            {
                return new Message
                {
                    Id = new Guid(),
                    MessageType = MessageType.Info,
                    Text = "",
                    Result = true
                };
            }
        }
    }
}
