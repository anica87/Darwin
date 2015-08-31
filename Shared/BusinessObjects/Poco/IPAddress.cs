using System;

namespace Shared.BusinessObjects.Poco
{
    public class IPAddress
    {
        private string[] parts = new string[] { "", "", "", "" };
        public string[] Parts 
        { 
            get
            {
                return parts;
            }

            set
            {
                parts = value;
            }
        }

        public byte[] ByteParts
        {
            get
            {
                byte[] bytes = new byte[]
                {
                    Convert.ToByte(parts[0]),
                    Convert.ToByte(parts[1]),
                    Convert.ToByte(parts[2]),
                    Convert.ToByte(parts[3])
                };

                return bytes;
            }

            set
            {
                parts = new string[] 
                { 
                    value[0].ToString(), 
                    value[1].ToString(), 
                    value[2].ToString(), 
                    value[3].ToString()
                };
            }
        }


        public string Value 
        {
            get 
            { 
                return string.Join(".", parts); 
            }

            set 
            {
                string[] p = value.Split(new char[] { '.', ' ' });

                parts[0] = p.Length > 0 ? p[0] : "0";
                parts[1] = p.Length > 1 ? p[1] : "0";
                parts[2] = p.Length > 2 ? p[2] : "0";
                parts[3] = p.Length > 3 ? p[3] : "0";
            }
        }
    }
}
