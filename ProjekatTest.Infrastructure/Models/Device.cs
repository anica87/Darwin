using Shared.BusinessObjects.Devices;
using Shared.BusinessObjects.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTest.Infrastructure.Models
{
   public class Device 
    {
       public virtual int id { get; set; }
       public virtual DeviceType DeviceType { get; set; }
       public virtual string SerialNumber { get; set; }
       public virtual MACAddress MAC { get; set; }
       public virtual string InstallCode { get; set; }
       public virtual string PreConfiguredLinkKey { get; set; }
       public virtual IEnumerable<SecurityKey> SecurityKeys { get; set; }
    }
}
