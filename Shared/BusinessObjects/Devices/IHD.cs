using Shared.BusinessObjects.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BusinessObjects.Devices
{
    [Serializable]
    public class IHD : IDevice
    {


        public DeviceType DeviceType 
        { 
            get 
            { 
                return DeviceSubType == DeviceType.Undefined  ? DeviceType.IHD : DeviceSubType; 
            } 
        }

        private DeviceType _deviceSubType = DeviceType.IHD;
        public DeviceType DeviceSubType 
        {
            get { return _deviceSubType; }
            set { _deviceSubType = value; }
        }

        public string SerialNumber { get; set; }

        public MACAddress MAC { get; set; }

        public string InstallCode { get; set; }
        public string PreConfiguredLinkKey { get; set; }

        public IEnumerable<SecurityKey> SecurityKeys { get; set; }

        // TODO: add properties for holding certificates or any other smets 2 specific thing
        public string Certificate { get; set; }
    }
}
