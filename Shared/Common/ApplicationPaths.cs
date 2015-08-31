using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common
{
    public class ApplicationPaths
    {
        static string BaseDirectory = "";
        static string AppDataRoot = "";

        public static void Init(string appDataRoot)
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            AppDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + appDataRoot;
        }

        static string _util = null;
        public static string Util
        {
            get
            {
                if (_util == null)
                {
                    _util = AppDataRoot + "\\Util";

                    if (!Directory.Exists(_util))
                        Directory.CreateDirectory(_util);
                }

                return _util;
            }
        }

        static string _database = null;
        public static string Database
        {
            get
            {
                if (_database == null)
                {
                    _database = AppDataRoot + "\\Database";

                    if (!Directory.Exists(_database))
                        Directory.CreateDirectory(_database);
                }

                return _database;
            }
        }

        static string _export = null;
        public static string Export
        {
            get
            {
                if (_export == null)
                {
                    _export = AppDataRoot + "\\Export";

                    if (!Directory.Exists(_export))
                        Directory.CreateDirectory(_export);
                }

                return _export;
            }
        }

        static string _scripts = null;
        public static string Scripts
        {
            get
            {
                if (_scripts == null)
                {
                    _scripts = AppDataRoot + "\\Scripts";

                    if (!Directory.Exists(_scripts))
                        Directory.CreateDirectory(_scripts);
                }

                return _scripts;
            }
        }

        static string _log = null;
        public static string Log
        {
            get
            {
                if (_log == null)
                {
                    _log = AppDataRoot + "\\Log";

                    if (!Directory.Exists(_log))
                        Directory.CreateDirectory(_log);
                }

                return _log;
            }
        }

        // ***************************************************************************

        static string _deviceDefs = null;
        public static string DeviceDefinitions
        {
            get
            {
                if (_deviceDefs == null)
                {
                    _deviceDefs = Path.Combine(BaseDirectory, "DeviceDefs");                
                }

                return _deviceDefs;
            }
        }

    }
}
