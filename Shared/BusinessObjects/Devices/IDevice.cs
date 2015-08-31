using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;
using Shared.BusinessObjects.Poco;

namespace Shared.BusinessObjects.Devices
{
    public interface IDevice 
    {
        DeviceType DeviceType { get; }
        string SerialNumber { get; set; }
        MACAddress MAC { get; set; }
        string InstallCode { get; set; }
        string PreConfiguredLinkKey { get; set; }
        IEnumerable<SecurityKey> SecurityKeys { get; set; }
       

    }
}
