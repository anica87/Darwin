using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.OpenSsl;

namespace Shared.Common
{
    public class Util
    {
        public static string SerializeObject(object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, obj);
                    return stream.ToString();
                }
            }

            /*using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, obj);
                return stream.ToString();
            }*/
        }


        public static T DeserializeObject<T>(string xml)
        {
            T myObject = default(T);

            using (var read = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    myObject = (T)serializer.Deserialize(reader);

                    return myObject;
                }
            }
        }

        public static T DeserializeObject<T>(string xml, XmlReaderSettings settings)
        {
            T myObject = default(T);

            using (var read = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                using (XmlReader reader = XmlReader.Create(read, settings))
                {
                    myObject = (T)serializer.Deserialize(reader);

                    return myObject;
                }
            }
        }

        public static object DeserializeObject(string xml, Type listGenericType)
        {
            object myObject = null;
            var genericListType = typeof(List<>);
            var deserializeTo = genericListType.MakeGenericType(listGenericType);

            using (var read = new StringReader(xml))
            {
                var serializer = new XmlSerializer(deserializeTo);
                using (XmlReader reader = new XmlTextReader(read))
                {
                    myObject = serializer.Deserialize(reader);

                    return myObject;
                }
            }
        }

        public static decimal GetIHD_SignalStrength(decimal _signalStrength)
        {
            //string decString = utilities.hexToDec16Bit(signalStrength).ToString();
            //decimal _signalStrength = Convert.ToDecimal(utilities.hexToSignedInteger(decString, 8));
            int rssi = 0;
            if (_signalStrength >= -30) { rssi = 6; }
            else if (_signalStrength >= -60 & _signalStrength < -30) { rssi = 5; }
            else if (_signalStrength >= -70 & _signalStrength < -60) { rssi = 4; }
            else if (_signalStrength >= -80 & _signalStrength < -70) { rssi = 3; }
            else if (_signalStrength >= -90 & _signalStrength < -80) { rssi = 2; }
            else if (_signalStrength >= -100 & _signalStrength < -90) { rssi = 1; }
            else if (_signalStrength < -100) { rssi = 0; }

            return rssi;
        }

        public static string FormatMAC(string hexOrDec)
        {
            string mac = FormatFromMAC(hexOrDec);

            string m = "";
            for (int i = 0; i < mac.Length; i += 2)
                m += mac.Substring(i, 2) + ":";

            return m.TrimEnd(':');
        }

        public static string FormatFromMAC(string hexOrDec)
        {
            if (hexOrDec == null)
                return "";

            string mac = hexOrDec.ToUpper();

            Regex rgx = new Regex("[^a-fA-F0-9]");
            //mac = rgx.Replace(mac, "");

            Char delimiter = '.';

            if (mac.IndexOf(":") != -1)
                delimiter = ':';

            if (mac.IndexOf("-") != -1)
                delimiter = '-';

            string[] parts = mac.Split(delimiter);

            if (delimiter != '.' && parts.Length > 0)
            {
                int i = 0;

                StringBuilder sb = new StringBuilder();

                foreach (String part in parts)
                {
                    sb.Append(part.PadLeft(2, '0'));
                    i++;
                }

                for (; i < 8; i++)
                {
                    sb.Append("00");
                }

                mac = sb.ToString();
            }
            else
            {
                mac = rgx.Replace(mac, "");
                mac = mac.PadRight(16, '0');
                mac = mac.Substring(0, 16);
            }

            return mac;
        }

        public static bool isMACValid(String oldMacAddress, String newChar, int caretPosition, int selectionStart, int selectionLength)
        {
            String macAddress = oldMacAddress;

            if (caretPosition > selectionStart)
                caretPosition = selectionStart;

            if (selectionLength != 0)
                macAddress = macAddress.Remove(selectionStart, selectionLength);

            macAddress = macAddress.Insert(caretPosition, newChar);

            Char delimiter = '.';
            Char oldDelimiter = '.';

            if (oldMacAddress.IndexOf(":") != -1)
                oldDelimiter = ':';

            if (oldMacAddress.IndexOf("-") != -1)
                oldDelimiter = '-';

            if (macAddress.IndexOf(":") != -1)
                delimiter = ':';

            if (oldDelimiter != '.' && delimiter != '.' && oldDelimiter != delimiter)
                return false;

            if (macAddress.IndexOf("-") != -1)
                delimiter = '-';

            if (oldDelimiter != '.' && delimiter != '.' && oldDelimiter != delimiter)
                return false;

            if (macAddress.StartsWith(":") || macAddress.StartsWith("-"))
                return false;

            if (delimiter == '.')
            {
                delimiter = ':';

                String m = macAddress;

                macAddress = String.Empty;

                for (int i = 0; i < m.Length; i += 2)
                {
                    String sub = String.Empty;

                    if (i + 2 <= m.Length)
                        sub = m.Substring(i, 2);
                    else
                        sub = m.Substring(i, 1);

                    sub = sub.PadLeft(2, '0');

                    if (macAddress.Length > 0)
                        macAddress = macAddress + delimiter + sub;
                    else
                        macAddress = sub;
                }
            }

            string[] parts = macAddress.Split(delimiter);

            if (parts.Length > 8)
                return false;

            Regex regex = new System.Text.RegularExpressions.Regex("[^0-9a-f]+", RegexOptions.IgnoreCase); 
            
            foreach (String part in parts)
            {
                if (part.Length > 2)
                    return false;

                if (regex.IsMatch(part))
                    return false;
            }

            return true;
        }

        public static bool blockCharForMAC(String macAddress, String newChar, int selectionStart, int selectionLength)
        {
            bool blockCharacter = false;

            Char delimiter = '.';

            bool delimiterEntered = false;
            bool delimiterAllowed = true;

            if (macAddress.IndexOf('-') >= 0)
            {
                delimiter = '-';
                delimiterEntered = true;
            }

            if (macAddress.IndexOf(':') >= 0)
            {
                delimiter = ':';
                delimiterEntered = true;
            }

            if (delimiterEntered == true)
            {
                int numDelimiter = macAddress.Split(delimiter).Length - 1;

                if (numDelimiter == 7 && (newChar == ":" || newChar == "-"))
                {
                    blockCharacter = true;

                    return blockCharacter;
                }
            }
            else
            {
                if (macAddress.Length > 2)
                    delimiterAllowed = false;
            }

            Regex regex = new System.Text.RegularExpressions.Regex("[^0-9a-f]+", RegexOptions.IgnoreCase);

            if (delimiterEntered == true)
            {
                int index = macAddress.LastIndexOf(delimiter);

                int numChar = macAddress.Length - index - 1;

                if (numChar == 0)
                {
                    blockCharacter = (newChar == ":") || (newChar == "-") || (regex.IsMatch(newChar) == true);

                    return blockCharacter;
                }

                if (numChar == 1)
                {
                    blockCharacter = regex.IsMatch(newChar);

                    return blockCharacter;
                }

                if (numChar == 2)
                {
                    blockCharacter = ((newChar != delimiter.ToString())) || (regex.IsMatch(newChar) == false);

                    return blockCharacter;
                }
            }
            else
            {
                if (delimiterAllowed)
                {
                    if ((macAddress.Length < 2) && ((newChar == ":") || (newChar == "-")))
                    {
                        blockCharacter = true;

                        return blockCharacter;
                    }
                }
            }


            macAddress = macAddress.Replace(":", String.Empty);
            macAddress = macAddress.Replace("-", String.Empty);

            if (selectionLength == 0 && macAddress.Length == 16)
            {
                blockCharacter = true;

                return blockCharacter;
            }


            bool isMatch = regex.IsMatch(newChar);

            if (isMatch == true)
            {
                if (delimiterEntered && newChar[0] == delimiter)
                {
                    isMatch = false;
                }
                else if ((delimiterEntered == false) && (delimiterAllowed == true))
                {
                    isMatch = (newChar != ":") && (newChar != "-");
                }
            }

            blockCharacter = isMatch;

            return blockCharacter;
        }

        public static string[] GetIPAddressPartsFromHex(object ipAddressHex)
        {
            if (ipAddressHex.GetType().Name == "UInt32")
                return GetIPAddressPartsFromHex((UInt32)ipAddressHex);

            if (ipAddressHex.GetType().Name.ToLower() == "string")
            {
                string ip = ipAddressHex as string;
                if (ip.Contains("."))
                {
                    string[] parts = ip.Split(new char[] { '.' });
                    if (parts.Length == 4)
                        return parts;
                }
                else
                    return GetIPAddressPartsFromHex(ipAddressHex as string);
            }

            throw new InvalidDataException(string.Format("Can not convert data: {0} of type: {1} to IP Address ", ipAddressHex, ipAddressHex.GetType()));
        }

        public static string[] GetIPAddressPartsFromHex(UInt32 ipAddressHex)
        {
            return GetIPAddressPartsFromHex(Convert.ToString(ipAddressHex, 16));
        }

        public static string[] GetIPAddressPartsFromHex(string ipAddressHex)
        {
            string[] parts = new string[4];

            ipAddressHex = ipAddressHex.PadRight(8, '0');

            parts[0] = Convert.ToUInt16(ipAddressHex.Substring(0, 2), 16).ToString();
            parts[1] = Convert.ToUInt16(ipAddressHex.Substring(2, 2), 16).ToString();
            parts[2] = Convert.ToUInt16(ipAddressHex.Substring(4, 2), 16).ToString();
            parts[3] = Convert.ToUInt16(ipAddressHex.Substring(6, 2), 16).ToString();

            return parts;
        }

        public static byte[] GetBytesFromHexString(string octetValue)
        {
            if (octetValue.Length == 0 || octetValue.Length % 2 == 1)
                return new byte[] { };

            byte[] value = new byte[octetValue.Length / 2];

            for (int i = 0; i < octetValue.Length / 2; i++)
                value[i] = Convert.ToByte(octetValue.Substring(i * 2, 2), 16);

            return value;
        }

        public static string GetHexString(byte[] hexValue)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte h in hexValue)
                sb.Append(Convert.ToString(h, 16).PadLeft(2, '0'));

            return sb.ToString();
        }

        public static string GetHexString(char charValue)
        {
            return Convert.ToString((int)charValue, 16).PadLeft(2, '0');
        }

        public static string GetHexString(string hexOrDec)
        {
            // check if it is given as decimal "number;number;...number;" or "number number number..."
            if (hexOrDec.Contains(";") || hexOrDec.Contains(" "))
            {
                StringBuilder sb = new StringBuilder();
                string[] parts = hexOrDec.Split(new char[] { ';', ' ' });

                foreach (string p in parts)
                    if (!string.IsNullOrEmpty(p))
                        sb.Append(Convert.ToString(Convert.ToByte(p.Trim()), 16).PadLeft(2, '0'));

                return sb.ToString();
            }

            return hexOrDec;
        }

        public static IList<Type> GetTypesFromNamespace(string ns)
        {
            IList<Type> types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && (t.Namespace ?? "").StartsWith(ns))
                .ToList();

            return types;
        }

        public static bool IsNumber(Type t)
        {
            return IsInteger(t) || IsReal(t);
        }

        public static bool IsReal(Type t)
        {
            return "|float|double|decimal|single|"
                .IndexOf("|" + t.Name.ToLower() + "|") >= 0;
        }

        public static bool IsInteger(Type t)
        {
            return IsSignedInteger(t) || IsUnsignedInteger(t);
        }

        public static bool IsUnsignedInteger(Type t)
        {
            return "|byte|ushort|uint|ulong|uint32|uint64|uint16|"
                .IndexOf("|" + t.Name.ToLower() + "|") >= 0;
        }

        public static bool IsSignedInteger(Type t)
        {
            return "|sbyte|int|long|int32|int64|int16|"
                .IndexOf("|" + t.Name.ToLower() + "|") >= 0;
        }


        public static bool IsNumeric(object expression)
        {
            if (expression == null)
                return false;

            double number;

            return Double.TryParse(
                Convert.ToString(expression, CultureInfo.InvariantCulture),
                System.Globalization.NumberStyles.Any,
                NumberFormatInfo.InvariantInfo,
                out number);
        }

        public static bool IsNumber(object obj)
        {
            if (Equals(obj, null))
            {
                return false;
            }

            Type objType = obj.GetType();
            objType = Nullable.GetUnderlyingType(objType) ?? objType;

            if (objType.IsPrimitive)
            {
                return objType != typeof(bool) &&
                    objType != typeof(char) &&
                    objType != typeof(IntPtr) &&
                    objType != typeof(UIntPtr);
            }

            return objType == typeof(decimal);
        }

        public static string OctetStringToAsciiString(string octet)
        {
            StringBuilder sb = new StringBuilder();

            if (octet == null)
                return sb.ToString();

            int octetLength = octet.Length;

            for (int i = 0; i < octetLength; i += 2)
            {
                int len = 1;

                if (i + 2 <= octetLength)
                    len++;

                string c = octet.Substring(i, len);

                sb.Append(Convert.ToChar(Convert.ToUInt16(c, 16)));
            }

            return sb.ToString();
        }

        public static string AsciiStringToDecimalSeparatedList(string ascii)
        {
            StringBuilder sb = new StringBuilder();

            if (ascii == null)
                return sb.ToString();

            for (int i = 0; i < ascii.Length; i++)
            {
                sb.AppendFormat("{0};", ((byte)(ascii[i])).ToString());
            }

            return sb.ToString();
        }

        public static string FormatAsFileName(string text)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if ((c >= 'a' && c <= 'Z') || c == ' ' || (c >= '0' && c <= '9'))
                    sb.Append(c);
            }

            return sb.ToString();
        }



        public static string GetAttribute(XmlNode node, string attrName)
        {
            return GetAttribute(node, attrName, "");
        }

        /// <summary>
        /// Get attribute value or default value
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetAttribute(XmlNode node, string attrName, string defaultValue)
        {
            if (node == null)
                return "";

            XmlAttribute attr = node.Attributes[attrName];

            return attr == null ? defaultValue : attr.Value;
        }


        public static string[] GetCosemScriptParameters(string script)
        {
            if (string.IsNullOrEmpty(script))
                return null;

            string[] data = new string[3] { "", "", "" };

            try
            {
                string[] lines = script.Trim().Split(new char[] { '\r', '\n' });
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Trim().StartsWith("//Logical Name ="))
                        {
                            data[0] = line.Replace("//Logical Name =", "").Trim().Replace(" ", ";");
                        }
                        else if (line.Trim().StartsWith("//Class_Id ="))
                        {
                            data[1] = line.Replace("//Class_Id =", "").Trim();
                        }
                        else if (line.Trim().StartsWith("//Attribut_Id ="))
                        {
                            data[2] = line.Replace("//Attribut_Id =", "").Trim();
                        }
                        else if (line.Trim().StartsWith("//Operation_Id ="))
                        {
                            line = line.Replace("//Operation_Id =", "").Trim();
                            data[2] = (Int32.Parse(line) + 128).ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            // is valid script with all entries
            if (string.IsNullOrEmpty(data[0]) || string.IsNullOrEmpty(data[1]) || string.IsNullOrEmpty(data[2]))
                return null;

            return data;
        }


        public static List<string> GetAllScripts(string script)
        {
            // parse multiple scripts so each can be executed separately
            List<string> scripts = new List<string>();

            string[] lines = script.Split(new char[] { '\r', '\n' });
            StringBuilder current = new StringBuilder();

            foreach (string line in lines)
            {
                // ignore empty lines
                if (!string.IsNullOrEmpty(line.Trim()))
                {
                    if (line.Trim().StartsWith("//"))
                    {
                        // is this a beginning of new script or just a comment
                        if (line.Trim().StartsWith("//Logical Name"))
                        {
                            if (current.Length > 0)
                            {
                                scripts.Add(current.ToString());
                                current.Clear();
                            }

                            current.AppendLine(line);
                        }
                        else if (line.Trim().StartsWith("//Class_Id") || line.Trim().StartsWith("//Attribut_Id") || line.Trim().StartsWith("//Operation_Id"))
                        {
                            current.AppendLine(line);
                        }
                    }
                    else
                    {
                        // data
                        current.AppendLine(line);
                    }
                }
            }

            // last
            if (current.Length > 0)
            {
                scripts.Add(current.ToString());
            }

            return scripts;
        }


        public static ECPublicKeyParameters GetPublicKeyParameters(string path)
        {
            var reader = new PemReader(new StreamReader(path));
            var item = reader.ReadObject();
            return (ECPublicKeyParameters)item;
        }

        public static ECPrivateKeyParameters GetPrivateKeyParameters(string path)
        {
            var reader = new PemReader(new StreamReader(path));
            var item = reader.ReadObject();
            return (ECPrivateKeyParameters)item;
        }
        public static ECPublicKeyParameters PublicKeyFromBytes(byte[] key)
        {
            /*
            * Convert public key into a public key object
            * The public key comprises two co-ordinates X followed by Y, both 32 bytes
            */

            byte[] public_key_x = new byte[32];
            byte[] public_key_y = new byte[32];

            Array.Copy(key, 0, public_key_x, 0, public_key_x.Length);
            Array.Copy(key, 32, public_key_y, 0, public_key_y.Length);

            BigInteger bi_x = new BigInteger(1, public_key_x);
            BigInteger bi_y = new BigInteger(1, public_key_y);

            // the key needs to relate to a specific curve
            X9ECParameters ecP = X962NamedCurves.GetByName("prime256v1");
            var ecSpec = new ECDomainParameters(ecP.Curve, ecP.G, ecP.N, ecP.H, ecP.GetSeed());

            FpCurve c = (FpCurve)ecP.Curve;

            var fe_x = new FpFieldElement(c.Q, bi_x);
            var fe_y = new FpFieldElement(c.Q, bi_y);

            // point q represents the x,y co-ordinate of the public key
            ECPoint q = new FpPoint(c, fe_x, fe_y);

            return new ECPublicKeyParameters("ECDSA", q, ecSpec);
        }

        public static ECPrivateKeyParameters PrivateKeyFromBytes(byte[] key)
        {
            /*
            * Convert public_key into a public key object
            * The public key comprises two co-ordinates X followed by Y, both 32 bytes
            */

            var bi_key = new BigInteger(1, key);

            // the key needs to relate to a specific curve
            X9ECParameters ecP = X962NamedCurves.GetByName("prime256v1");
            var ecSpec = new ECDomainParameters(ecP.Curve, ecP.G, ecP.N, ecP.H, ecP.GetSeed());

            return new ECPrivateKeyParameters("ECDSA", bi_key, ecSpec);
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static void AddBytes(ref byte[] activeArray, byte[] bytesToAdd)
        {
            var activeArrayLength = 0;
            if (activeArray != null) activeArrayLength = activeArray.Length;

            var newBytes = new byte[activeArrayLength + bytesToAdd.Length];

            if (activeArray != null && activeArray.Length > 0)
            {
                for (int i = 0; i < activeArrayLength; i++)
                {
                    newBytes[i] = activeArray[i];
                }
            }

            for (int i = 0; i < bytesToAdd.Length; i++)
            {
                newBytes[i + activeArrayLength] = bytesToAdd[i];
            }
            activeArray = newBytes;

        }

        public static byte[] AddBytes(byte[] activeArray, byte[] bytesToAdd)
        {
            var activeArrayLength = 0;
            if (activeArray != null) activeArrayLength = activeArray.Length;

            var newBytes = new byte[activeArrayLength + bytesToAdd.Length];

            if (activeArray != null && activeArray.Length > 0)
            {
                for (int i = 0; i < activeArrayLength; i++)
                {
                    newBytes[i] = activeArray[i];
                }
            }

            for (int i = 0; i < bytesToAdd.Length; i++)
            {
                newBytes[i + activeArrayLength] = bytesToAdd[i];
            }

            return newBytes;
        }

        public static byte[] GetMessageLengthBytes(Int32 messageLength)
        {
            byte[] returnBytes = null;
            var byteval = BitConverter.GetBytes(messageLength);
            Array.Reverse(byteval);
            var byteList = new List<byte>();
            for (int i = 0; i < byteval.Length; i++)
            {
                if (byteval[i] > 0)
                {
                    while (i < byteval.Length)
                    {
                        byteList.Add(byteval[i]);
                        i++;
                    }
                }
            }
            byteval = byteList.ToArray();
            if (messageLength < 256 && messageLength > 127)
            {
                returnBytes = new byte[] { 0x81 };
                AddBytes(ref returnBytes, byteval);
            }
            else if (messageLength >= 256 && messageLength < 65535)
            {
                returnBytes = new byte[] { 0x82 };
                AddBytes(ref returnBytes, byteval);
            }
            else
            {
                returnBytes = null;
                AddBytes(ref returnBytes, byteval);
            }
            return returnBytes;
        }
        public static byte[] GenerateSharedSecret(byte[] privateKey, byte[] publicKey)
        {
            var prKey = PrivateKeyFromBytes(privateKey);
            var puKey = PublicKeyFromBytes(publicKey);
            return GenerateSharedSecret(puKey, prKey);
        }
        public static byte[] GenerateSharedSecret(ECPublicKeyParameters publicKey, ECPrivateKeyParameters privateKey)
        {
            var agreement = new ECDHBasicAgreement();
            agreement.Init(privateKey);
            var z = agreement.CalculateAgreement(publicKey);
            var sharedSecret = new byte[32];
            var zArr = z.ToByteArrayUnsigned();

            // zero the output array
            for (var i = 0; i < sharedSecret.Length; i++)
            {
                sharedSecret[i] = 0;
            }

            Array.Copy(zArr, 0, sharedSecret, 32 - zArr.Length, zArr.Length);

            return sharedSecret;

        }
    


    }
}
