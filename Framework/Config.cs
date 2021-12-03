using Hevadea.Framework.Data;
using Hevadea.Framework.Platform;
using System.IO;

namespace Hevadea.Framework
{
    public class Config
    {
        public float MasterVolume { get; set; } = 1f;
        public float MusicVolume { get; set; } = 1f;
        public float EffectVolume { get; set; } = 1f;

        public int ScreenWidth { get; set; } = 1366;
        public int ScreenHeight { get; set; } = 768;
        public bool Fullscreen { get; set; } = false;

        public float UIScaling { get; set; } = 1f;

        public void Save(string file)
        {
            File.WriteAllText(file, this.ToJson());
        }

        public void Load(string file)
        {
            if (!File.Exists(file)) Save(file);

            Rise.Config = File.ReadAllText(file).FromJson<Config>();
        }

        public void Apply()
        {
            if (Rise.Platform.Family == PlatformFamily.Desktop)
            {
                if (Fullscreen)
                    Rise.Graphic.SetFullscreen(ScreenWidth, ScreenHeight);
                else
                    Rise.Graphic.SetSize(ScreenWidth, ScreenHeight);
            }
            else
            {
                Rise.Graphic.ResetRenderTargets();
            }
        }
    }
}