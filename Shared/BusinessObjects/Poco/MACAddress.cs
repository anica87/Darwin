using Shared.Common;

namespace Shared.BusinessObjects.Poco
{
    public class MACAddress
    {
        private string value = "";
        public string Value 
        {
            get { return value; }
            set { this.value = value.ToUpper().Replace(":", ""); }
        }

        public string FormatedValue
        {
            get { return Util.FormatMAC(value); }
        }
        
        public bool IsEmpty
        {
            get
            {
                return value.Replace('0', ' ').Trim().Equals("");
            }
        }

        public static string ExtractValue(string mac)
        {
            return mac.ToUpper().Replace(":", ""); 
        }
    }
}
