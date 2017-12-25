using Maker.Rise;
using Maker.Rise.GameComponent.Ressource;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WorldOfImagination.Game.Entities
{
    public class Player : Mob
    {

        RiseGame Game;
        Sprite PlayerSprite;
        public Player(RiseGame game)
        {
            Game = game;
            Width = 16;
            Height = 16;
            PlayerSprite = new Sprite(Ressources.tile_entities, 0, new Point(16,16));
            
        }

        bool walking = false;

        public override void Update(GameTime gameTime)
        {
            walking = false;
            if (Game.Input.KeyDown(Keys.Z)) { Move(0, -1); Facing = Facing.Up; walking = true; }
            if (Game.Input.KeyDown(Keys.S)) { Move(0, 1);  Facing = Facing.Down; walking = true; }
            if (Game.Input.KeyDown(Keys.Q)) { Move(-1, 0); Facing = Facing.Left; walking = true; }
            if (Game.Input.KeyDown(Keys.D)) { Move(1, 0);  Facing = Facing.Right; walking = true; }
            if (Game.Input.KeyPress(Keys.N)) { noclip = !noclip; Console.WriteLine($"noclip: {noclip}"); }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (walking)
            {
                PlayerSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X, Position.Y), new Point((int)(gameTime.TotalGameTime.TotalSeconds * 4 % 2), (int)Facing), Color.White);
            }
            else
            {
                PlayerSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X, Position.Y), new Point(2, (int)Facing), Color.White);
            }
        }
    }
}
