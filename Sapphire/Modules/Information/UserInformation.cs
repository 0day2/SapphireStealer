using System;
using System.Net;
using System.Windows.Forms;
using System.Management;

namespace Sapphire.Modules.Information
{
    class UserInformation
    {
        public static string username = Environment.UserName;
        public static string pcname = Environment.MachineName;
        private static IPAddress iip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        public static string ip {
            get { return iip.ToString(); }
        }

        public static string screen
        {
            get
            {
                int width = int.Parse(Screen.PrimaryScreen.Bounds.Width.ToString());
                int height = int.Parse(Screen.PrimaryScreen.Bounds.Height.ToString());
                return width + "x" + height; 
            }
        }

        public static string OSVersion
        {
            get
            {
                string str1 = Environment.OSVersion.ToString();
                string str2 = Environment.Is64BitOperatingSystem ? "x64" : "x32";
                return str1 + " " + str2;
            }
        }

        public static string GetHWID()
        {
            try
            {
                var mng = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
                ManagementObjectCollection mbsList = mng.Get();
                foreach (ManagementObject mo in mbsList)
                    return mo["ProcessorId"].ToString();
            }
            catch { }
            return "Unknown";
        }

        public static string GetGPUName()
        {
            try
            {
                ManagementObjectSearcher mng = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject mObject in mng.Get())
                    return mObject["Name"].ToString();
            }
            catch { }
            return "Unknown";
        }
    }
}


