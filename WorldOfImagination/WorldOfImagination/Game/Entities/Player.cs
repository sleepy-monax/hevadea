using Maker.Rise;
using Maker.Rise.GameComponent.Ressource;
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
        public int CurrentLevel = 0;

        public Player(RiseGame game)
        {
            Game = game;
            Width = 8;
            Height = 8;
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
            if (Game.Input.KeyPress(Keys.N)) { NoClip = !NoClip; Console.WriteLine($"noclip: {NoClip}"); }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int animationFrame = (int)(gameTime.TotalGameTime.TotalSeconds * 8 % 4);

            if (walking)
            {

                switch (animationFrame)
                {
                    case 0:
                        animationFrame = 0;
                        break;
                    case 1:
                        animationFrame = 2;
                        break;
                    case 2:
                        animationFrame = 1;
                        break;
                    case 3:
                        animationFrame = 2;
                        break;
                }

                PlayerSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X - 4, Position.Y - 8), new Point(animationFrame, (int)Facing), Color.White);
            }
            else
            {
                PlayerSprite.DrawSubSprite(spriteBatch, new Vector2(Position.X - 4, Position.Y - 8), new Point(2, (int)Facing), Color.White);
            }
        }
    }
}
