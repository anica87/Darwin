using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Darwin.Cosem.AccessService;

namespace Shared.BusinessObjects.Devices
{
    
    
    /// ecBrokerKdfPrivateKey = index 0,
    /// ecSupplierDigitalSignaturePrivateKey = index 1, 
    /// ecSupplierKdfPrivateKey = index 2,
    /// ecMeterDigitalSignaturePublicKey = index 3, 
    /// ecMeterKdfPublicKey = index 4,
    /// ecSupplierDigitalSignaturePublicKey = index 5
    public class SecurityKeyFactory
    {
        public static IEnumerable<SecurityKey> GetSecurityKeys
            (
            byte[] ecBrokerKdfPrivateKey,
            byte[] ecSupplierDigitalSignaturePrivateKey, 
            byte[] ecSupplierKdfPrivateKey,
            byte[] ecMeterDigitalSignaturePublicKey, 
            byte[] ecMeterKdfPublicKey,
            byte[] ecSupplierDigitalSignaturePublicKey
            )
        {
            var keys = new List<SecurityKey>();
            var keyA = new SecurityKey(SecurityKey.Type.PrivateKey, SecurityKey.Name.EcBrokerKdfPrivateKey,
                ecBrokerKdfPrivateKey,0);

            var keyB = new SecurityKey(SecurityKey.Type.PrivateKey,
                SecurityKey.Name.EcSupplierDigitalSignaturePrivateKey,
                ecSupplierDigitalSignaturePrivateKey,1);
            var keyC = new SecurityKey(SecurityKey.Type.PrivateKey, SecurityKey.Name.EcSupplierKdfPrivateKey,
                ecSupplierKdfPrivateKey,2);
            var keyD = new SecurityKey(SecurityKey.Type.PublicKey,
                SecurityKey.Name.EcMeterDigitalSignaturePublicKey,
                ecMeterDigitalSignaturePublicKey,3);
            var keyE = new SecurityKey(SecurityKey.Type.PublicKey, SecurityKey.Name.EcMeterKdfPublicKey,
                ecMeterKdfPublicKey,4);
            var keyF = new SecurityKey(SecurityKey.Type.PublicKey,
                SecurityKey.Name.EcSupplierDigitalSignaturePublicKey,
                ecSupplierDigitalSignaturePublicKey,5);
            keys.Add(keyA);
            keys.Add(keyB);
            keys.Add(keyC);
            keys.Add(keyD);
            keys.Add(keyE);
            keys.Add(keyF);
            return keys;
        }




    }
}
