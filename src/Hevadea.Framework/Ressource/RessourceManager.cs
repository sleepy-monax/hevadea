using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Framework.Ressource
{
    public class RessourceManager
    {
        private Dictionary<string, SpriteFont> FontCache = new Dictionary<string, SpriteFont>();
        private Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();

        public SoundEffect GetSoundEffect(string name)
        {
            if (Rise.NoGraphic) return null;

            return Rise.MonoGame.Content.Load<SoundEffect>($"Sounds/{name}");
        }

        public SpriteFont GetSpriteFont(string name)
        {
            if (Rise.NoGraphic) return null;

            if (!FontCache.ContainsKey(name))
            {
                Logger.Log<RessourceManager>($"Loading <font>{name}");
                FontCache.Add(name, Rise.MonoGame.Content.Load<SpriteFont>($"Fonts/{name}"));
            }

            return FontCache[name];
        }

        public Texture2D GetTexture(string name)
        {
            if (Rise.NoGraphic) return null;

            if (!TextureCache.ContainsKey("tex:" + name))
            {
                Logger.Log<RessourceManager>($"Loading <image>{name}");
                TextureCache.Add("tex:" + name, Rise.MonoGame.Content.Load<Texture2D>($"textures/{name}"));
            }

            return TextureCache["tex:" + name];
        }

        public Texture2D GetImage(string name)
        {
            if (Rise.NoGraphic) return null;

            if (!TextureCache.ContainsKey("img:" + name))
            {
                Logger.Log<RessourceManager>($"Loading <image>{name}");
                TextureCache.Add("img:" + name, Rise.MonoGame.Content.Load<Texture2D>($"Images/{name}"));
            }

            return TextureCache["img:" + name];
        }
    }
}