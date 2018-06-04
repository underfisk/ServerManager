using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ServerManager
{
    class IniFile
    {
        /// <summary>
        /// Ini file path
        /// </summary>
        string Path;

        /// <summary>
        /// Gets the exe name
        /// </summary>
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        /// <summary>
        /// Initializes the path if it's not yet
        /// </summary>
        /// <param name="IniPath"></param>
        public IniFile(string IniPath = null) => Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();

        /// <summary>
        /// Reads a key within a section or not and gets it's value
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Section"></param>
        /// <returns>key value</returns>
        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        /// <summary>
        /// Writes in a key a value within a section or not (if something exists, this overrides)
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="Section"></param>
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        /// <summary>
        /// Deletes a key within a section or not
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Section"></param>
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        /// <summary>
        /// Deletes a section and the keys binded within it
        /// </summary>
        /// <param name="Section"></param>
        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        /// <summary>
        /// Checks if a given key exists within a section or not
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Section"></param>
        /// <returns>boolean</returns>
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}


