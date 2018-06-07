using Hevadea.Framework.Sample.Scenes;
using OpenGLPlatform;
using System;

namespace Hevadea.Framework.Sample
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Rise.Initialize(new DesktopPlatform());
            Rise.Start(new UISample(), () => 
            {
                var font = Rise.Ressource.GetSpriteFont("font");
                Rise.Ui.DebugFont = font;
                Rise.Ui.DefaultFont = font;
            });
        }
    }
}
