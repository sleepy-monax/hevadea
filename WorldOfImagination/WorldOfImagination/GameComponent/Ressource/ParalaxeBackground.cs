using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent.Ressource
{
    public class ParalaxeLayer
    {
        public readonly Texture2D Texture;
        public readonly float Factor;
        
        
        public ParalaxeLayer(Texture2D texture, float factor)
        {
            Texture = texture;
            Factor = factor;
        }
    }
    
    public class ParalaxeBackground
    {
        public ParalaxeLayer[] Layers;
        private WorldOfImaginationGame Game;
        public float Position = 0;
        public ParalaxeBackground(WorldOfImaginationGame game, params ParalaxeLayer[] layers)
        {
            Layers = layers;
            Game = game;
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Position = Position + (gameTime.ElapsedGameTime.Milliseconds / 1000f) * 32;
            foreach (var l in Layers)
            {
                
                var onScreenPos = (Position * l.Factor) % Game.Graphics.GetWidth();
                
                
                var dest = new Rectangle(
                    (int)onScreenPos, 0,
                    (int)(Game.Graphics.GetWidth()),
                    (int)(Game.Graphics.GetHeight())
                    );
                
                var dest2 = new Rectangle(
                    (int)onScreenPos - Game.Graphics.GetWidth(), 0,
                    (int)(Game.Graphics.GetWidth()),
                    (int)(Game.Graphics.GetHeight())
                );
                
                spriteBatch.Draw(l.Texture, dest, Color.White);
                spriteBatch.Draw(l.Texture, dest2, Color.White);
            }
        }
    }
}