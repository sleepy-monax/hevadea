using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Hevadea.Framework.Ressource
{
    public class RessourceManager
    {
        private readonly Dictionary<string, SpriteFont> _fontCache = new Dictionary<string, SpriteFont>();
        private readonly Dictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();

        public SoundEffect GetSoundEffect(string name)
        {
            if (Rise.NoGraphic) return null;

            Logger.Log<RessourceManager>($"Loading <SoundEffect>{name}");
            return Rise.MonoGame.Content.Load<SoundEffect>($"effects/{name}");
        }

        public Song GetSong(string name)
        {
            if (Rise.NoGraphic) return null;

            Logger.Log<RessourceManager>($"Loading <Song>{name}");
            return Rise.MonoGame.Content.Load<Song>($"songs/{name}");
        }

        public SpriteFont GetSpriteFont(string name)
        {
            if (Rise.NoGraphic) return null;

            if (!_fontCache.ContainsKey(name))
            {
                Logger.Log<RessourceManager>($"Loading <font>{name}");
                _fontCache.Add(name, Rise.MonoGame.Content.Load<SpriteFont>($"Fonts/{name}"));
            }

            return _fontCache[name];
        }

        public Texture2D GetTexture(string name)
        {
            if (Rise.NoGraphic) return null;

            if (!_textureCache.ContainsKey("tex:" + name))
            {
                Logger.Log<RessourceManager>($"Loading <image>{name}");
                _textureCache.Add("tex:" + name, Rise.MonoGame.Content.Load<Texture2D>($"textures/{name}"));
            }

            return _textureCache["tex:" + name];
        }

        public Texture2D GetImage(string name)
        {
            if (Rise.NoGraphic) return null;

            if (!_textureCache.ContainsKey("img:" + name))
            {
                Logger.Log<RessourceManager>($"Loading <image>{name}");
                _textureCache.Add("img:" + name, Rise.MonoGame.Content.Load<Texture2D>($"Images/{name}"));
            }

            return _textureCache["img:" + name];
        }
    }
}