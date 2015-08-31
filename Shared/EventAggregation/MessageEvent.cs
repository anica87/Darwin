using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Shared.EventAggregation
{
    public class MessageEvent : PubSubEvent<Message>
    {
        /// <summary>
        /// Publish Info message
        /// </summary>
        /// <param name="text"></param>
        public void PublishInfo(string text)
        {
            Message message = new Message
            {
                Text = text,
                MessageType = MessageType.Info,
                Result = true
            };

            Publish(message);
        }

        /// <summary>
        /// Publish Error message
        /// </summary>
        /// <param name="text"></param>
        public void PublishError(string text)
        {
            Message message = new Message
            {
                Text = text,
                MessageType = MessageType.Error,
                Result = false
            };

            Publish(message);
        }

    }
}
