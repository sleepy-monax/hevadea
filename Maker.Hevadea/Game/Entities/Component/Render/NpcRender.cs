using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Render
{
    public class NpcRender : EntityComponent, IDrawableComponent, IUpdatableComponent
    {
        public Sprite Sprite { get; set; }

        private bool isWalking = false;
        private int walkingFrame = 0;

        public NpcRender(Sprite sprite)
        {
            Sprite = sprite;
            Priority = 16;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isWalking)
            {
                walkingFrame = new int[] { 0, 2, 1, 2 }[(int)(gameTime.TotalGameTime.TotalSeconds * 8 % 4)];
                Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18), new Point(walkingFrame, (int)Owner.Facing),
                    Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18), new Point(2, (int)Owner.Facing), Color.White);
            }

        }

        public void Update(GameTime gameTime)
        {
            var move = Owner.Components.Get<Move>();
            if (move != null)
            {
                isWalking = move.IsMoving;
            }
        }
    }


}
