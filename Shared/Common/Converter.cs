using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Shared.Common
{
    public static class Converter
    {

        public static string changeNumberFormat(string Number)
        {
            int @where = 0;

            // Get the registry entry for the decimal delimiter to '.'
            RegistryKey delimeter = Registry.CurrentUser.OpenSubKey("ACE4K Field Tool\\Decimal\\Delimiter", true);
            string DecimalDelimiter = Convert.ToString(delimeter.GetValue("Delimiter", "."));

            if (Number == "0") Number = "0.0";

            @where = Number.IndexOf(@".");  //Strings.InStr(1, Number, ".", CompareMethod.Text);
            if (@where > 0)
            {
                if (DecimalDelimiter == ",")
                {
                    Number = Number.Substring(0, @where) + "," + Number.Substring(@where + 1, Number.Length - @where - 1);
                }
                else
                {
                    return Number;
                }

            }
            return Number;
        }
        public static string reverseV850String(string V850_Frame)
        {
            int i = 0;
            string V850_String_Reversed = null;

            if (V850_Frame.Length > 2)
            {
                for (i = 0; i <= V850_Frame.Length - 1; i += 2)
                {
                    V850_String_Reversed = V850_Frame.Substring(i, 2) + V850_String_Reversed;
                }
            }
            return V850_String_Reversed;
        }
        public static string decodeTimeStamp(int NumSeconds)
        {
            DateTime convertedDateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(NumSeconds);
            return Convert.ToString(convertedDateTime);
        }
        public static ulong hexByteToDec(string HexString)
        {
            // Returns the decimal value for a hex string            
            ulong decValue = 0;
            try
            {
                decValue = (uint.Parse(HexString, System.Globalization.NumberStyles.HexNumber));
            }
            catch
            {
            }
            return decValue;
        }
        public static ulong hexToDec(string HexString)
        {
            // Returns the decimal value for a hex string            
            ulong decValue = 0;
            try
            {
                if (HexString.Length == 2) { decValue = (uint.Parse(HexString, System.Globalization.NumberStyles.HexNumber)); }
                else { decValue = (uint.Parse(HexString.Substring(2, 4), System.Globalization.NumberStyles.HexNumber)); }
            }
            catch
            {
            }
            return decValue;
        }
        public static string toBinary(string data)
        {
            string binary = "";

            foreach (string letter in data.Select(c => Convert.ToString(c, 2)))
            {
                binary = binary + letter;
            }

            return binary;
        }
        public static string toCharString(string data)
        {
            string charData = "";
            string decData = "";

            for (int i = 0; i < data.Length; i += 2)
            {
                int temp = Convert.ToInt32(hexByteToDec(data.Substring(i, 2)));
                decData = decData + temp.ToString();
                charData = charData + Convert.ToChar(temp);
            }

            return charData;

        }
        public static UInt32 hexToDec32Bit(string HexString)
        {
            // Returns the decimal value for a hex string            
            UInt32 decValue = 0;
            try
            {
                decValue = (uint.Parse(HexString, System.Globalization.NumberStyles.HexNumber));
            }
            catch
            {
            }
            return decValue;
        }
        public static string decToHex(string number, int LgthNeeded)
        {
            string functionReturnValue = null;
            UInt64 decValue = 0;
            // Input : Decimal Number, length as variant 
            // Output : Hexadecimal Value with a desired length 
            decValue = Convert.ToUInt64(number);
            functionReturnValue = decValue.ToString("X");

            functionReturnValue = "00000000" + functionReturnValue;
            functionReturnValue = functionReturnValue.Substring(functionReturnValue.Length - LgthNeeded, LgthNeeded);
            return functionReturnValue;
        }
        public static int calculateNumberSecondsPast1970()
        {
            // Returns the number of seconds past 01/01/1970 00:00:00 of the current time
            DateTime dt70 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long ticks1970 = dt70.Ticks;
            //get current time: 
            int gmt = (int)((DateTime.Now.Ticks - ticks1970) / 10000000L);
            return gmt;
        }
        public static int calculateNumberSecondsToPC_Time(string year, string month, string date, string hour, string minute, string second)
        {
            // Returns the number of seconds difference between now, and the time passed across in the argument
            DateTime timeToCompare = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date), int.Parse(hour), int.Parse(minute), int.Parse(second));
            long ticks_Time = timeToCompare.Ticks;
            //get current time: 
            int gmt = (int)((DateTime.Now.Ticks - ticks_Time) / 10000000L);
            return gmt;
        }
        public static int calculateNumberSecondsFromPC_Time(string year, string month, string date, string hour, string minute, string second)
        {
            // Returns the number of seconds difference between now, and the time passed across in the argument
            DateTime timeToCompare = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date), int.Parse(hour), int.Parse(minute), int.Parse(second));
            long ticks_Time = timeToCompare.Ticks;
            //get current time: 
            int gmt = (int)((ticks_Time - DateTime.Now.Ticks) / 10000000L);
            return gmt;
        }
        public static long convertDateTime_SecondsPast1970(DateTime newDateTime)
        {
            DateTime dt70 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long ticks1970 = dt70.Ticks;
            int secondsPast1970 = (int)((newDateTime.Ticks - ticks1970) / 10000000L);
            return secondsPast1970;
        }
        public static string convertSecondsPast1970_DateTime(double Seconds)
        {
            double date_TIME = Convert.ToDouble(Seconds);
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0); //Set default date 1/1/1970
            date = date.AddSeconds(date_TIME); //add seconds
            return date.ToString(); //Return result 
        }
        public static string convertSecondsPast2000_DateTime(double Seconds)
        {
            double date_TIME = Convert.ToDouble(Seconds);
            DateTime date = new DateTime(2000, 1, 1, 0, 0, 0, 0); //Set default date 1/1/2000
            date = date.AddSeconds(date_TIME); //add seconds
            return date.ToString(); //Return result 
        }
        public static int calculate_BCC(string CommsData)
        {

            int bcc = 0;
            int i = 0;
            bool stop_loop = false;
            string Current = null;
            bool start = false;
            string SOH = "";
            string STX = "";

            start = false;
            stop_loop = false;

            for (i = 0; i <= CommsData.Length - 1; i++)
            {
                if (start == false)
                {
                    Current = CommsData.Substring(i, 1);
                    if (Current == SOH)
                    {
                        bcc = 0;
                        start = true;
                    }
                    else if (Current == STX)
                    {
                        bcc = 0;
                        start = true;
                    }
                }
                else if (start == true)
                {
                    int charAsciiValue = Convert.ToInt32(CommsData[i]);
                    bcc = bcc ^ charAsciiValue;
                    if (Current == Convert.ToString(3))
                    {
                        stop_loop = true;
                    }
                }

                if (stop_loop == true) break; // TODO: might not be correct. Was : Exit For 

            }

            return bcc;

        }
        public static string padOut(string input_string, int LgthNeeded)
        {
            // and pad out to the desired length with preceding zeros. 
            while (input_string.Length < LgthNeeded)
            {
                input_string = "0" + input_string;
            }

            return input_string;
        }

        //selection of methods for manupulating ASCII 
        public static String ConvertToAsciiHex(string Character)//converts an ascii character to its equivalent hexadecimal value
        {
            byte[] dec;
            string hex = null;
            dec = Encoding.ASCII.GetBytes(Character);

            hex = dec[0].ToString("X2");
            return hex;
        }

        public static string HexStringToAscii(string hexString)//converts the hex byte value to ascii equivalent.
        {
            string converted = Convert.ToString(Convert.ToChar(Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber)));

            return converted;
        }
        public static string DecStringToAscii(string decString)//converts the decimal representation of a byte value to the ascii equivalent.
        {

            string converted = HexStringToAscii(Convert.ToInt16(decString).ToString("X").PadLeft(2, '0'));

            return converted;
        }
        public static string OctetStringToAscii(string[] decString)//converts the decimal representation of a byte value to the ascii equivalent.
        {
            string converted = "";
            foreach (string item in decString)
            {

                if (item != "")
                {
                    converted = converted + HexStringToAscii(Convert.ToInt16(item).ToString("X").PadLeft(2, '0'));
                }
            }

            return converted;
        }

        public static string hexToFormatedNumber(string hexString)
        {
            return Converter.changeNumberFormat(Convert.ToString(Convert.ToDouble(Converter.hexToDec(Converter.reverseV850String(hexString)))));
        }

        public static ushort hexToDec16Bit(string HexString)
        {
            // Returns the decimal value for a hex string            
            ushort decValue = 0;

            string upperHexString = HexString.ToUpper();
            try
            {
                decValue = (ushort.Parse(upperHexString, System.Globalization.NumberStyles.HexNumber));
            }
            catch
            {
            }
            return decValue;
        }
        public static string signedIntegerToHex(string decimalString, int bytesData)
        {
            //pass the string representation of the integer to the method along with the total padded length of  the required bytes;
            //integer8 will need 2, integer16 will need 4, integer32 will need 8 and so on
            //method ASSUMES that the right data will be passed to it  
            string sign = ""; string tempData = ""; string integerByteString; char[] bitString;
            if (decimalString.Contains("-")) { sign = "negative"; decimalString = decimalString.Replace("-", ""); }
            else { sign = "positive"; }
            string binaryText = Convert.ToString(Convert.ToInt32(decimalString), 2).PadLeft((bytesData * 4), '0');
            bitString = binaryText.ToCharArray();

            if (sign == "positive")
            {
                for (int j = 0; j < bitString.Length; j++)
                {

                    tempData = tempData + bitString[j].ToString();

                }

                tempData = (Convert.ToInt64(tempData, 2)).ToString("X").PadLeft(bytesData, '0');
            }

            else if (sign == "negative")
            {
                for (int j = 0; j < bitString.Length; j++)
                {
                    if (bitString[j] == '0') { bitString[j] = '1'; }
                    else if (bitString[j] == '1') { bitString[j] = '0'; }
                    tempData = tempData + bitString[j].ToString();
                }

                tempData = (Convert.ToInt64(tempData, 2) + 1).ToString("X").PadLeft(bytesData, '0');
            }

            integerByteString = tempData;
            return integerByteString;

        }
        public static string hexToSignedInteger(string data, int stringLength)
        {
            //pass the string representation of the byte to the method along with the total padded length of the required bytes;
            //integer8 will need 8, integer16 will need 16, integer32 will need 32 and so on
            //method ASSUMES that the right data will be passed to it
            char[] bitString;
            string sign = "";
            string tempData = "";
            string binaryText = Convert.ToString(Convert.ToInt32(data), 2).PadLeft(stringLength, '0');
            bitString = binaryText.ToCharArray();
            if (bitString[0] == '0')
            {
                sign = "";
                for (int j = 0; j < bitString.Length; j++)
                {

                    tempData = tempData + bitString[j].ToString();

                }

                tempData = (Convert.ToInt64(tempData, 2)).ToString();
            }

            else if (bitString[0] == '1')
            {
                sign = "-";
                for (int j = 0; j < bitString.Length; j++)
                {
                    if (bitString[j] == '0') { bitString[j] = '1'; }
                    else if (bitString[j] == '1') { bitString[j] = '0'; }
                    tempData = tempData + bitString[j].ToString();
                }

                tempData = (Convert.ToInt64(tempData, 2) + 1).ToString();
            }

            data = sign + tempData;

            return data;
        }
        public static string decOctetToBytes(string octet)
        {
            //pass the Octetstring in decimal form to the method space delimited, e.g. 255 255 255 255
            //Method will return the hexbyte representation of this. IT WILL ONLY WORK FOR OCTETSTRINGS UP TO 255!!
            string octetBytes;
            string temp = "";
            octet = octet.TrimEnd(' ');
            string[] tempDataArray = octet.Split(' ');
            int length = tempDataArray.Length;
            string lengthByte = lengthData(length);

            for (int i = 0; i < tempDataArray.Length; i++)
            {
                temp = temp + int.Parse(tempDataArray[i]).ToString("X").PadLeft(2, '0');
            }
            octetBytes = lengthByte + temp;
            return octetBytes;
        }
        public static string visibleStringToHexBytes(string visibleString)
        {
            //pass the visible string to the method eg. CALENDAR_1. the method will return it in the byte format preceded by the hex length.
            string stringBytes;
            string temp = "";
            string[] tempDataArray = visibleString.Split(' ');
            int length = tempDataArray.Length;
            string lengthByte = lengthData(length);


            byte[] dec = new byte[visibleString.Length];
            string[] hex = new string[visibleString.Length];
            dec = Encoding.ASCII.GetBytes(visibleString);
            for (int i = 0; i < dec.Length; i++)
            {
                hex[i] = dec[i].ToString("X").PadLeft(2, '0');
            }
            temp = (string.Join("", hex, 0, visibleString.Length));
            stringBytes = lengthByte + temp;
            return stringBytes;
        }
        public static string bitStringToHexBytes(string bitString)
        {
            string bitBytes;
            //split the bitstring into an array using the spaces
            string[] bitStringArray = bitString.TrimEnd().TrimStart().Split(' ');
            //calculate how may times the last loop will have to go round to extract the bytes. 
            //necessary to create the array to do this so it is divisible by 8
            int bitStringLength = bitStringArray.Length;

            int numberOfBytes = bitStringLength / 8;
            int remainder = bitStringLength - (numberOfBytes * 8);
            int extra;
            if (remainder > 0) { extra = 8; }
            else { extra = 0; }
            string bit;

            //now create the array for the final loop adding zeroes if the data is null
            string[] bitBytesArray = new string[(numberOfBytes * 8) + extra];
            for (int i = 0; i < bitBytesArray.Length; i++)
            {
                try { bit = bitStringArray[i]; }
                catch { bit = "0"; }
                bitBytesArray[i] = bit;

            }

            //calculate the number of bytes required for the length of the bitstring:
            int numberOfLoops = bitBytesArray.Length / 8;
            int count = 0;
            string hex = "";
            while (count < numberOfLoops)
            {
                string temp = "";
                for (int i = 0; i < 8; i++)
                {
                    temp = temp + bitBytesArray[i + (count * 8)];
                }
                hex = hex + Convert.ToInt64(temp, 2).ToString("X").PadLeft(2, '0');
                count++;


            }
            string lengthOfString = bitStringLength.ToString("X").PadLeft(2, '0'); //(hex.Length / 2 * 8).ToString("X").PadLeft(2, '0');
            string lengthByte = lengthData(hex.Length / 2 * 8);
            bitBytes = lengthByte + hex;
            return bitBytes;
        }
        public static string[] HexToByte(string data)
        {

            //remove any spaces from the string  

            data = data.Replace(" ", "");

            //create a byte array the length of the  

            //string divided by 2  

            string[] Array = new string[data.Length / 2];

            //loop through the length of the provided string  

            for (int i = 0; i < data.Length; i += 2)
            {
                //convert each set of 2 characters to a byte  

                //and add to the array  

                Array[i / 2] = data.Substring(i, 2);

                //return the array  
            }
            return Array;
        }
        public static int BinToInt(string binaryNumber)
        {
            int multiplier = 1;
            int converted = 0;

            for (int i = (binaryNumber.Length - 1); i >= 0; i--)
            {
                int t = System.Convert.ToInt16(binaryNumber[i].ToString());
                converted = converted + (t * multiplier);
                multiplier = multiplier * 2;
            }
            return converted;
        }
        /// <summary>
        ///         Convert a decimal to the binary format.
        ///         like: 
        ///         stra = Convert.ToString(4, 2).PadLeft(8, '0');
        ///         //stra=00000100
        /// </summary>
        /// <param name="DecimalNum"></param>
        /// <returns></returns>
        public static string int64ToLittleEndianBinary(Int64 decimalNum)// Little endian.byte to bit not reversed
        {
            //Int64 binaryHolder;
            char[] binaryArray;
            string binaryResult = (Convert.ToString(decimalNum, 2)).PadLeft(8, '0');

            binaryArray = binaryResult.ToCharArray();
            Array.Reverse(binaryArray);

            binaryResult = new string(binaryArray);



            return binaryResult;
        }
        public static string int64ToBinary(Int64 decimalNum)
        {
            Int64 binaryHolder;
            char[] binaryArray;
            string binaryResult = "";

            // fix issue#5943: StrHelper.ToBinary(0)  result "", it should be 0
            if (decimalNum == 0)
            {
                return "0";
            }
            // end fix issue#5943

            while (decimalNum > 0)
            {
                binaryHolder = decimalNum % 2;
                binaryResult += binaryHolder;
                decimalNum = decimalNum / 2;
            }

            // rever the binaryResult, e.g. we get 1101111, then revert to 1111011, this is the final result
            binaryArray = binaryResult.ToCharArray();
            Array.Reverse(binaryArray);
            binaryResult = new string(binaryArray);
            return binaryResult;
        }



        /*
        the following set of methods are specialized for use with extracting and encoding cosem data.
        */

        static string lengthData(int length)//this method is used to determine the length of long sequences of octetstring, bitstring or visible string.
        {

            string lengthByte;
            //this bit added 24/01/11 
            if (length > 127 && length <= 255)
            {
                lengthByte = "81" + decToHex(length.ToString(), 2);
                return lengthByte;
            }

            else if (length > 255 && length <= 65535)
            {
                lengthByte = "82" + decToHex(length.ToString(), 4);
                return lengthByte;
            }
            else if (length > 65535 && length <= 16777215)
            {
                lengthByte = "83" + decToHex(length.ToString(), 6);
                return lengthByte;
            }
            else { return length.ToString("X").PadLeft(2, '0'); ; }



        }
        //the following 2 methods will return the data string. another method must be used for returning the frame data.
        public static string[] returnObjectDataStringFromFile(string idData)
        {
            string[] wordArray;
            string[] newDataArray;
            wordArray = idData.Split('\n');
            newDataArray = returnObjectDataStringFromArray(wordArray);
            return newDataArray;
        }

        public static string[] returnObjectDataStringFromArray(string[] wordArray)
        {
            string logicalName; string classID; string attributeID;
            string[] newDataArray; string rawDataString;
            ArrayList DataList = new ArrayList();
            int j = 0;
            #region the extraction loop

            foreach (string item in wordArray)
            {
                if (wordArray[j].Contains("Logical Name"))
                {
                    logicalName = wordArray[j].Replace("//Logical Name =", "").TrimEnd().TrimStart();
                }
                if (wordArray[j].Contains("Class_ID"))
                {
                    classID = wordArray[j].Replace("//Class_Id =", "").TrimEnd().TrimStart();
                }
                if (wordArray[j].Contains("Attribut_ID"))
                {
                    attributeID = wordArray[j].Replace("//Attribut_Id =", "").TrimEnd().TrimStart();
                }

                if (wordArray[j].Contains("LN ="))
                {
                    logicalName = wordArray[j].Replace("// LN =", "").TrimEnd().TrimStart();
                }
                if (wordArray[j].Contains("Class_Id"))
                {
                    classID = wordArray[j].Replace("// Class_Id =", "").TrimEnd().TrimStart();
                }
                if (wordArray[j].Contains("Attribute Index"))
                {
                    attributeID = wordArray[j].Replace("// Attribute Index =", "").TrimEnd().TrimStart();
                }
                if (wordArray[j].Contains("elements in the array"))
                {
                    DataList.Add(wordArray[j].Replace("elements in the array", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("elements in the structure"))
                {
                    DataList.Add(wordArray[j].Replace("elements in the structure", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("UNSIGNED16,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("UNSIGNED16,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("INTEGER16,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("INTEGER16,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("UNSIGNED8,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("UNSIGNED8,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("UNSIGNED32,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("UNSIGNED32,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("ENUMERATED,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("ENUMERATED,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("INTEGER8,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("INTEGER8,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("INTEGER32,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("INTEGER32,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("OCTETSTRING,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("OCTETSTRING,", "").Replace("value:", "").Replace(";", " ").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("VISIBLESTRING,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("VISIBLESTRING,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("BITSTRING,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("BITSTRING,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("BOOLEAN,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("BOOLEAN,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("VISIBLESTRING,"))
                {
                    DataList.Add(wordArray[j].Replace("element", "").Replace("VISIBLESTRING,", "").Replace("value:", "").TrimEnd().TrimStart());

                }
                if (wordArray[j].Contains("NULL,"))
                {
                    DataList.Add("");

                }
                j++;
            }
            #endregion
            newDataArray = DataList.ToArray(typeof(string)) as string[];
            rawDataString = string.Join(";", newDataArray, 0, newDataArray.Length);



            return newDataArray;
        }
        //this method extracts the hex data from an array taken from a cosem object file
        public static string extractData(string[] dataArray)
        {
            string[] newDataArray;
            string rawdataString;
            string logicalName;
            string classID;
            string attributeID;
            string[] obisHexArray;
            string hexIDa2 = "";
            string hexIDc2 = "";
            string obis = "";
            string operationId = "";
            string errorMessage = "";

            try
            {
                ArrayList DataList = new ArrayList();
                int j = 0;


                foreach (string item in dataArray)
                {
                    string tempData = ""; //will create temporary variable in decimal, to be converted to hex and added to the string.

                    if (dataArray[j].Contains("Logical Name"))
                    {
                        logicalName = dataArray[j].Replace("//Logical Name =", "").TrimEnd().TrimStart();
                        string[] obisArray = logicalName.Split(' ');
                        ArrayList tempArray = new ArrayList();
                        for (int i = 0; i < 6; i++)
                        {
                            int temp = int.Parse(obisArray[i]);
                            string hexObis = temp.ToString("X");
                            string hexObis2 = hexObis.PadLeft(2, '0');
                            tempArray.Add(hexObis2);
                        }
                        obisHexArray = tempArray.ToArray(typeof(string)) as string[];
                        obis = string.Join("", obisHexArray, 0, obisHexArray.Length);
                    }
                    if (dataArray[j].Contains("Class_ID"))
                    {
                        classID = dataArray[j].Replace("//Class_ID =", "").TrimEnd().TrimStart();
                        int IDc = int.Parse(classID);
                        string hexIDc = IDc.ToString("X");
                        hexIDc2 = hexIDc.PadLeft(2, '0');

                    }
                    if (dataArray[j].Contains("Class_Id"))
                    {
                        classID = dataArray[j].Replace("//Class_Id =", "").TrimEnd().TrimStart();
                        int IDc = int.Parse(classID);
                        string hexIDc = IDc.ToString("X");
                        hexIDc2 = hexIDc.PadLeft(2, '0');

                    }
                    if (dataArray[j].Contains("Attribut_Id"))
                    {
                        attributeID = dataArray[j].Replace("//Attribut_Id =", "").TrimEnd().TrimStart();
                        int IDa = int.Parse(attributeID);
                        string hexIDa = IDa.ToString("X");
                        hexIDa2 = hexIDa.PadLeft(2, '0');
                    }

                    if (dataArray[j].Contains("LN ="))
                    {
                        logicalName = dataArray[j].Replace("//LN =", "").TrimEnd().TrimStart();

                    }
                    if (dataArray[j].Contains("Operation_Id ="))
                    {
                        operationId = dataArray[j].Replace("//Operation_Id =", "").TrimEnd().TrimStart();//looks to see if it is an action to perform
                        int IDa = int.Parse(operationId);
                        string hexIDa = IDa.ToString("X");
                        hexIDa2 = hexIDa.PadLeft(2, '0') + "01";
                    }
                    if (dataArray[j].Contains("Attribute Index"))
                    {
                        attributeID = dataArray[j].Replace("// Attribute Index =", "").TrimEnd().TrimStart();
                        int IDa = int.Parse(attributeID);
                        string hexIDa = IDa.ToString("X");
                        hexIDa2 = hexIDa.PadLeft(2, '0');
                    }
                    #region Structure elements are one byte each and directly encoded
                    //looks for the data structures and convert  to its relevant hex block
                    //First extract the data in decimal format. 
                    //If array we need 01  plus 1 bytes
                    //If structure we need 02  plus 2 bytes
                    if (dataArray[j].Contains("elements in the array"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("elements in the array", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(2, '0');
                        DataList.Add("01" + tempData);
                    }
                    if (dataArray[j].Contains("elements in the structure"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("elements in the structure", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(2, '0');
                        DataList.Add("02" + tempData);
                    }
                    if (dataArray[j].Contains("NULL"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("elements in the structure", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(2, '0');
                        DataList.Add("00" + "");
                    }

                    #endregion


                    #region Unsigned values boolean and enumerated are directly converted to the hex equivalent; preceded by its identifier byte.
                    //Now looks for the data and converts it to its relevant hex block
                    //First extract the data in decimal format. 
                    //If unsigned 8 we need 11  plus 1 bytes
                    //If unsigned 16 we need 12  plus 2 bytes
                    //If unsigned 32 we need 06  plus 4 bytes
                    //If Enumerated we need 16  plus 1 bytes
                    //If Boolean we need 03  plus 1 bytes
                    if (dataArray[j].Contains("UNSIGNED8,"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("UNSIGNED8,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(2, '0');
                        DataList.Add("11" + tempData);
                    }
                    if (dataArray[j].Contains("UNSIGNED16,"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("UNSIGNED16,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(4, '0');
                        DataList.Add("12" + tempData);
                    }
                    if (dataArray[j].Contains("UNSIGNED32,"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("UNSIGNED32,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(8, '0');
                        DataList.Add("06" + tempData);
                    }
                    if (dataArray[j].Contains("ENUMERATED,"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("ENUMERATED,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(2, '0');
                        DataList.Add("16" + tempData);

                    }
                    if (dataArray[j].Contains("BOOLEAN,"))
                    {
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("BOOLEAN,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString("X")).PadLeft(2, '0');
                        DataList.Add("03" + tempData);
                    }
                    #endregion


                    #region signed integers need to be passed through a conversion process before encoding
                    if (dataArray[j].Contains("INTEGER8,"))
                    {
                        //Extract the text and leave the raw data as decimal for processing                        
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("INTEGER8,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString());
                        //needs passing through a method to derive the hex value
                        tempData = signedIntegerToHex(tempData, 2);
                        DataList.Add("0F" + tempData);
                    }
                    if (dataArray[j].Contains("INTEGER16,"))
                    {
                        //Extract the text and leave the raw data as decimal for processing 
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("INTEGER16,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString());
                        //needs passing through a method to derive the hex value
                        tempData = signedIntegerToHex(tempData, 4);
                        DataList.Add("10" + tempData);
                    }
                    if (dataArray[j].Contains("INTEGER32,"))
                    {
                        //Extract the text and leave the raw data as decimal for processing 
                        tempData = (int.Parse(dataArray[j].Replace("element", "").Replace("INTEGER32,", "").Replace("value:", "").TrimEnd().TrimStart()).ToString());
                        //needs passing through a method to derive the hex value
                        tempData = signedIntegerToHex(tempData, 8);
                        DataList.Add("05" + tempData);
                    }
                    #endregion


                    #region all of these elements, octet, visible and bitstring are flexible so require length to accompany the data.

                    if (dataArray[j].Contains("OCTETSTRING,"))
                    {
                        tempData = dataArray[j].Replace("element", "").Replace("OCTETSTRING,", "").Replace("value:", "").Replace(";", " ").TrimEnd().TrimStart();
                        //pass through a method to derive the hex values and the length of the octet (09). will return in format eg. 255;255;255;255; = 04FFFFFFFF
                        tempData = decOctetToBytes(tempData);
                        //if the data is longer than the normal byte lenght, i.e. 255, another byte is required to give it length.

                        DataList.Add("09" + tempData);

                    }
                    if (dataArray[j].Contains("VISIBLESTRING,"))
                    {
                        tempData = dataArray[j].Replace("element", "").Replace("VISIBLESTRING,", "").Replace("value:", "").TrimEnd().TrimStart();
                        //pass through a method to derive the hex value and the length
                        tempData = visibleStringToHexBytes(tempData);


                        DataList.Add("0A" + tempData);

                    }
                    if (dataArray[j].Contains("BITSTRING,"))
                    {
                        //method does not work for long bitstrings.
                        tempData = dataArray[j].Replace("element", "").Replace("BITSTRING,", "").Replace("value:", "").Replace(";", " ").TrimEnd().TrimStart();
                        //pass through a method to derive the hex value and the length, send in format 1 0 1 0 0 1 0 0 1, will return as 02A480
                        tempData = bitStringToHexBytes(tempData);


                        DataList.Add("04" + tempData);

                    }

                    #endregion
                    j++;
                }

                newDataArray = DataList.ToArray(typeof(string)) as string[];
                rawdataString = string.Join("", newDataArray, 0, newDataArray.Length);

                return rawdataString;
            }

            catch { errorMessage = "Error building the data packet"; return errorMessage; }

        }
        public static string countUpCosemFrame(string hexNumber)
        {

            //takes the existing hex value and converts the hex to a number
            int number = (int.Parse(hexNumber, System.Globalization.NumberStyles.HexNumber));
            //counts up 34 each time in decimal
            if (number > 0)
            {
                number = number + 34;
            }
            //if the resulting number is greater than 255, the number count is restarted at 16
            if (number > 255)
            {
                number = 16;

            }
            //convert the number back to a padded hex string again to return the next frame number
            hexNumber = number.ToString("X").PadLeft(2, '0');
            string ping = _getPingCount(hexNumber);
            //Properties.Settings.Default.pingCount = ping;
            //Properties.Settings.Default.Save();

            return hexNumber;
        }
        private static string _getPingCount(string frameCount)
        {

            switch (frameCount)
            {
                case "32":
                    frameCount = "31";
                    break;
                case "54":
                    frameCount = "51";
                    break;
                case "76":
                    frameCount = "71";
                    break;
                case "98":
                    frameCount = "91";
                    break;
                case "BA":
                    frameCount = "B1";
                    break;
                case "DC":
                    frameCount = "D1";
                    break;
                case "FE":
                    frameCount = "F1";
                    break;
                case "10":
                    frameCount = "11";
                    break;
            }
            return frameCount;
        }


        public static string getErrorMessage(string errorCode)
        {
            string returnedError = "";
            switch (errorCode)
            {
                case "00":
                    returnedError = "Success";
                    break;
                case "01":
                    returnedError = "Error: Hardware Failure";
                    break;
                case "02":
                    returnedError = "Success";
                    break;
                case "03":
                    returnedError = "Error: Read/Write Denied";
                    break;
                case "04":
                    returnedError = "Error: Object Undefined";
                    break;
                case "09":
                    returnedError = "Error: Object Class Inconsistent";
                    break;
                case "0B":
                    returnedError = "Error: Object Unavailable";
                    break;
                case "0C":
                    returnedError = "Error: Type Unmatched";
                    break;
                case "0D":
                    returnedError = "Error: scope of access violation";
                    break;
                case "0E":
                    returnedError = "Error: Data Block Unavailable";
                    break;
                case "0F":
                    returnedError = "Error: Long-Get Aborted";
                    break;
                case "10":
                    returnedError = "Error: No Long-Set in Progress";
                    break;
                case "11":
                    returnedError = "Error: Long-Set Aborted";
                    break;
                case "12":
                    returnedError = "Error: No Long-Get in Progress";
                    break;
                case "13":
                    returnedError = "Error: Dat Block number Invalid";
                    break;
                case "FA":
                    returnedError = "Error: Undefined Error in Request";
                    break;
                case "1F":
                    returnedError = "Error: Meter Signed Off";
                    break;

            }

            return returnedError;
        }

        public static String ConvertToAsciiDec(string Character)//converts an ascii character to its equivalent decimal value
        {
            byte[] dec;
            string _dec = null;
            dec = Encoding.ASCII.GetBytes(Character);

            _dec = dec[0].ToString();
            return _dec;
        }




    }
}
