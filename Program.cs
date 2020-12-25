using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Capture(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Images\");

            // Console Exit Lines
            Console.Write("Please Enter Any Key To Exit...");
            Console.ReadKey();
        }
        /// <summary>
        /// Gets all files with image types and returns the count of them
        /// used for Capture
        /// </summary>
        /// <param name="path"></param>
        /// <returns>File count if any</returns>
        private static int GetItemsInPath(string path)
        {
            HashSet<string> acceptableFileTypes = new HashSet<string>() { "jpg", "png", "bmp" };


            if (!Directory.Exists(path))
            {
                DirectoryNotFoundException e = new DirectoryNotFoundException();
                Directory.CreateDirectory(path);
                Console.WriteLine($"Exception Found:\n{e}", Console.ForegroundColor = ConsoleColor.Red);
                Console.WriteLine($"\nException Caught:\n{path} Was Created", Console.ForegroundColor = ConsoleColor.Green);
            }
            DirectoryInfo di = new DirectoryInfo(path);

            int fCount = 0;
            try
            {
                FileInfo[] Files = new FileInfo[20];
                // File count
                // Check each for file extension with acceptable types
                foreach (var accept in acceptableFileTypes)
                {
                    // add all correctly typed files to Files array
                    Files = di.GetFiles("*." + accept);
                    // for each correct file
                    for (int i = 0; i < Files.Length; ++i)
                    {
                        Console.WriteLine(Files[i].Name);
                        fCount = i + 1;
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Unable To Find Directory:\n\n" + e, Console.ForegroundColor = ConsoleColor.Red);
            }
            if (di.Exists && fCount != 0)
                return fCount;
            return 0;
        }
        /// <summary>
        /// Creates a bitmap with graphics and saves it as a single image
        /// </summary>
        /// <param name="path"></param>
        /// <returns>success</returns>
        private static void Capture(string path)
        {
            // Sets int count to get all files in path and returns int
            var count = GetItemsInPath(path);

            // concatenate file name
            var local = $"Capture{count}.png";

            // create bitmap with screen bounds ex. 1920x1080
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            // stores graphics from bitmap as an image
            Graphics gfx = Graphics.FromImage(bmp as Image);
            // copies graphics from screen with bitmaps resolution
            gfx.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            // save the graphic bitmap with the external and local path using an image format
            try
            {

                bmp.Save(path + local);
                bmp.Dispose();
            }
            catch (System.Runtime.InteropServices.ExternalException e)
            {
                Console.WriteLine($"\nFailed To Save Screenshot:\n\n" + e, Console.ForegroundColor = ConsoleColor.Red);
            }
        }
    }
}
