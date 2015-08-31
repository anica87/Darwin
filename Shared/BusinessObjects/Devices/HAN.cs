using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Common;
using Shared.BusinessObjects.Devices;
using Shared.BusinessObjects.Poco;

namespace BusinessObjects.Devices
{
    [Serializable]
    public class HAN  
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string DeviceId
        {
            get
            {
                return string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", ID, GetMAC(Hub), GetMAC(EMeter), GetMAC(GMeter), GetMAC(IHD), GetMAC(AUX1), GetMAC(AUX2));
            }
        }

        public Hub Hub { get; set; }

        public EMeter EMeter { get; set; }

        public GMeter GMeter { get; set; }

        public IHD IHD { get; set; }

        public IHD AUX1 { get; set; }

        public IHD AUX2 { get; set; }

        private MACAddress GetMAC(IDevice device)
        {
            if (device == null || device.MAC == null)
                return new MACAddress { Value = "" };

            return device.MAC;
        }
    }
}
