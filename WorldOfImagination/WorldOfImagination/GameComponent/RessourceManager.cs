using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace WorldOfImagination.GameComponent
{
    public class RessourceManager : Microsoft.Xna.Framework.GameComponent
    {
        private Dictionary<string, SpriteFont> FontCache = new Dictionary<string, SpriteFont>();

        public RessourceManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine($"{nameof(RessourceManager)} initialized !");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public SpriteFont GetSpriteFont(string name)
        {
            if (!FontCache.ContainsKey(name))
            {
                FontCache.Add(name, Game.Content.Load<SpriteFont>($"Fonts/{name}"));
            }

            return FontCache[name];
        }
    }
}