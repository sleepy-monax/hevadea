using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Maker.Rise.Components
{
    public class RessourceManager : GameComponent
    {
        private Dictionary<string, SpriteFont> FontCache = new Dictionary<string, SpriteFont>();
        private Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();

        public RessourceManager(RiseGame game) : base(game)
        {
            
        }

        public override void Initialize()
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
 
        }

        public SoundEffect GetSoundEffect(string name)
        {
            return Game.Content.Load<SoundEffect>($"Sounds/{name}");
        }

        public SpriteFont GetSpriteFont(string name)
        {
            if (!FontCache.ContainsKey(name))
            {
                FontCache.Add(name, Game.Content.Load<SpriteFont>($"Fonts/{name}"));
            }

            return FontCache[name];
        }
        
        public Texture2D GetIcon(string name)
        {
            if (!TextureCache.ContainsKey("icon:" + name))
            {
                TextureCache.Add("icon:" + name, Game.Content.Load<Texture2D>($"Icons/{name}"));
            }

            return TextureCache["icon:" + name];
        }

        public Texture2D GetImage(string name)
        {
            if (!TextureCache.ContainsKey("img:" + name))
            {
                TextureCache.Add("img:" + name, Game.Content.Load<Texture2D>($"Images/{name}"));
            }

            return TextureCache["img:" + name];
        }
    }
}