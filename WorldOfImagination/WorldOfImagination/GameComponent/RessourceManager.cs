using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace WorldOfImagination.GameComponent
{
    public class RessourceManager : GameComponent
    {
        private Dictionary<string, SpriteFont> FontCache = new Dictionary<string, SpriteFont>();
        private Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();

        public RessourceManager(WorldOfImaginationGame game) : base(game)
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
    }
}