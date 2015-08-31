using BusinessObjects.Devices;
using Shared.BusinessObjects.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BusinessObjects.Devices
{
    /// <summary>
    /// all of the elements in this structure are required 
    /// in order to fulfil the requirements of the access service
    /// Payload = the message payload whether Cosem or ZigBee
    /// OriginatorCounter = Uint value generally persisted from the previous message
    /// MessageCode = Decimal value of the UseCase message code
    /// OriginatorSystemTitle = the title of the Head End System: also applies to responses
    /// RecipientSystemTitle = the title of the device: also applies to responses
    /// MessageSensitivity = enum: Type of message encryption, Critical, Non-Critical or Sensitive
    /// EcSupplierDigitalSignaturePrivateKey must be in the corect format as org.BouncyCastle ECPrivateKeyParameters
    /// EcSupplierKdfPrivateKey must be in the corect format as org.BouncyCastle ECPrivateKeyParameters
    /// EcBrokerKdfPrivateKey must be in the corect format as org.BouncyCastle ECPrivateKeyParameters
    /// EcMeterDigitalSignaturePublicKey must be in the corect format as org.BouncyCastle ECPublicKeyParameters
    /// EcSupplierDigitalSignaturePublicKey must be in the corect format as org.BouncyCastle ECPublicKeyParameters
    /// EcMeterKdfPublicKey must be in the corect format as org.BouncyCastle ECPublicKeyParameters
    /// </summary>
    [Serializable]
    public class EMeter : IDevice
    {

        public DeviceType DeviceType { get { return DeviceType.EMeter; } }
        
        public string SerialNumber { get; set; }

        public MACAddress MAC { get; set; }

        public string InstallCode { get; set; }
        public string PreConfiguredLinkKey { get; set; }

        public IEnumerable<SecurityKey> SecurityKeys { get; set; }


    }
}
