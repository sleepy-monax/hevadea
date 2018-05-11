using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hevadea.Framework.Ressource
{
    public class RessourceManager
    {
        private Dictionary<string, SpriteFont> FontCache = new Dictionary<string, SpriteFont>();
        private Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();


        public Texture2D GetTexture(string name)
        {
            var fullName = $"assets/{name}.png";

            if (!TextureCache.ContainsKey(name))
            {
                if (File.Exists(fullName))
                {
                    FileStream fileStream = new FileStream(fullName, FileMode.Open);
                    TextureCache.Add(name, Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fileStream));
                    fileStream.Dispose();
                }
                else
                {
                    Logger.Log<RessourceManager>(LoggerLevel.Warning, $"Ressource: '{name}' not found !");
                    TextureCache.Add(name, Rise.Graphic.GetFallbackTexture() );
                }
            }

            return TextureCache[name];
        }

        public SoundEffect GetSoundEffect(string name)
        {
            return Rise.MonoGame.Content.Load<SoundEffect>($"Sounds/{name}");
        }

        public SpriteFont GetSpriteFont(string name)
        {
            if (!FontCache.ContainsKey(name))
            {
                Logger.Log<RessourceManager>($"Loading <font>{name}");
                FontCache.Add(name, Rise.MonoGame.Content.Load<SpriteFont>($"Fonts/{name}"));
            }

            return FontCache[name];
        }

        public Texture2D GetIcon(string name)
        {
            if (!TextureCache.ContainsKey("icon:" + name))
            {
                TextureCache.Add("icon:" + name, Rise.MonoGame.Content.Load<Texture2D>($"Icons/{name}"));
            }

            return TextureCache["icon:" + name];
        }

        [Obsolete] public Texture2D GetImage(string name)
        {
            if (!TextureCache.ContainsKey("img:" + name))
            {
                Logger.Log<RessourceManager>($"Loading <image>{name}");
                TextureCache.Add("img:" + name, Rise.MonoGame.Content.Load<Texture2D>($"Images/{name}"));
            }

            return TextureCache["img:" + name];
        }
    }
}