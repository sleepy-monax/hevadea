using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TexPacker
{
    public static class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            var path = fbd.SelectedPath;

            var spriteAtlas = new SpriteAtlas(512, 512);
            spriteAtlas.InsertSprites(path);

            var frm = new Form
            {
                ClientSize = new Size(512, 512),
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                BackgroundImage = spriteAtlas.Bitmap,
                BackColor = Color.DarkGreen,
            };
            
            Application.EnableVisualStyles();
            Application.Run(frm);

            Console.ReadLine();
        }
    }
}
