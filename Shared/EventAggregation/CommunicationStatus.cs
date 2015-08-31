using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;

namespace Shared.EventAggregation
{
    public class CommunicationStatus
    {
        public bool Connected { get; set; }
        public bool IsHLS { get; set; }
        public string ConnectionType { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Message { get; set; }
        public int Timeout { get; set; }
        public DateTime Modified { get; set; }
        public ConnectionEventMessages ConnectionEventMessages { get; set; }
        public Action ForceDisconnectAction { get; set; }
    }
}
