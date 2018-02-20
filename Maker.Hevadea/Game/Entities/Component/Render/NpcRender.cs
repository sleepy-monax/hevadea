using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Render
{
    public class NpcRender : EntityComponent, IDrawableComponent, IUpdatableComponent
    {
        private bool isWalking;
        private int walkingFrame;

        public NpcRender(Sprite sprite)
        {
            Sprite = sprite;
            Priority = 16;
        }

        public Sprite Sprite { get; set; }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
             
            if (isWalking)
            {
                walkingFrame = new[] {0, 2, 1, 2}[(int) (gameTime.TotalGameTime.TotalSeconds * 8 % 4)];
                Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18),
                    new Point(walkingFrame, (int) Owner.Facing),
                    Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18),
                    new Point(2, (int) Owner.Facing), Color.White);
            }

            bool isSwiming = Owner.Components.Has<Swim>() && Owner.Components.Get<Swim>().IsSwiming;
            if (isSwiming)
            {
                if (isWalking)
                {
                    walkingFrame = new[] {0, 2, 1, 2}[(int) (gameTime.TotalGameTime.TotalSeconds * 8 % 4)];
                    Ressources.SprUnderWater.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18),
                        new Point(walkingFrame, (int) Owner.Facing),
                        Color.White);
                }
                else
                {
                    Ressources.SprUnderWater.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18),
                        new Point(2, (int) Owner.Facing), Color.White);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            var move = Owner.Components.Get<Move>();
            if (move != null) isWalking = move.IsMoving;
        }
    }
}