using Eto.Forms;
using System;

namespace WorldOfImagination.Editor
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // run application with our main form
            var app = new Application();
            
            app.Run(new MainForm());
        }
    }
}
