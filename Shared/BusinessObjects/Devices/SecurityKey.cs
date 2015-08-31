using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;

namespace Shared.BusinessObjects.Devices
{
    [Serializable]
    public class SecurityKey 
    {
        public enum Type
        {
            PublicKey,
            PrivateKey,
        }

        public enum Name
        {
            EcSupplierDigitalSignaturePrivateKey,
            EcSupplierKdfPrivateKey,
            EcBrokerKdfPrivateKey,
            EcMeterDigitalSignaturePublicKey,
            EcSupplierDigitalSignaturePublicKey,
            EcMeterKdfPublicKey
        }

        public Type KeyType { get; set; }
        public Name KeyName { get; set; }
        public int ID { get; set; }//not needed
        public byte[] KeyBytes { get; set; }
        public string KeyString { get; set; }

        public string NameString { get; set; }
        /// <summary>
        /// empty constructor for serialization
        /// </summary>
        public SecurityKey()
        {
            
        }
        /// <summary>
        /// constructor accepting a byte array for the key
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="keyBytes"></param>
        public SecurityKey(Type type, Name name, byte[] keyBytes, int index)
        {
            ID = index;
            KeyBytes = keyBytes;
            KeyString = Util.ByteArrayToString(keyBytes);
            KeyName = name;
            KeyType = type;
            NameString = KeyName.ToString();
        }

        /// <summary>
        /// constructor accepting a string for the key
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="keyString"></param>
        public SecurityKey(Type type, Name name, string keyString)
        {
            KeyBytes = Util.StringToByteArray(keyString);
            KeyString = keyString;
            KeyName = name;
            KeyType = type;
        }
        
    }
}
