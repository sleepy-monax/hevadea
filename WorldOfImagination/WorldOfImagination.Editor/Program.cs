using Eto.Forms;
using System;
using WorldOfImagination.Data;
using WorldOfImagination.Editor.Forms;

namespace WorldOfImagination.Editor
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // run application with our main form
            var app = new Application
            {
                MainForm = new MainForm()
            };
            app.Run(app.MainForm);
            //app.Run(new ItemEditorForm(new Item() {Name = "Batton", Description = "Un joli baton", Notes = "Todo Animations"}));
        }
    }
}
