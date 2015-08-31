using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.BusinessObjects.Poco;

namespace Shared.BusinessObjects.Devices
{
    [Serializable]
    public class Hub : IDevice
    {


        public DeviceType DeviceType { get { return DeviceType.Hub; } }

        public string SerialNumber { get; set; }

        public MACAddress MAC { get; set; }

        public string InstallCode { get; set; }
        public string PreConfiguredLinkKey { get; set; }

        public IEnumerable<SecurityKey> SecurityKeys { get; set; }


    }
}
